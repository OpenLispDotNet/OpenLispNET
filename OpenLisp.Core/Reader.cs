using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using OpenLisp.Core.AbstractClasses;
using OpenLisp.Core.DataTypes.Errors;
using OpenLisp.Core.DataTypes;
using OpenLisp.Core.StaticClasses;

namespace OpenLisp.Core
{
    public class Reader
    {
        public class ParseError : OpenLispThrowable
        {
            public ParseError(string msg) : base(msg) { }
        }

        public class TokensReader
        {
            readonly List<string> _tokens;
            int _position;
            public TokensReader(List<string> t)
            {
                _tokens = t;
                _position = 0;
            }

            public string Peek()
            {
                return _position >= _tokens.Count ? null : _tokens[_position];
            }

            public string Next() => _tokens[_position++];
        }

        public static List<string> Tokenize(string str)
        {
            string pattern = @"[\s ,]*(~@|[\[\]{}()'`~@]|""(?:[\\].|[^\\""])*""|;.*|[^\s \[\]{}()'""`~@,;]*)";
            Regex regex = new Regex(pattern);
            return regex.Matches(str)
                .Cast<Match>()
                .Select(match => match.Groups[1].Value)
                .Where(token => !string.IsNullOrEmpty(token) && token[0] != ';')
                .ToList();
        }

        public static OpenLispVal ReadAtom(TokensReader rdr)
        {
            string token = rdr.Next();

            string pattern = @"(^-?[0-9]+$)|(^-?[0-9][0-9.]*$)|(^nil$)|(^true$)|(^false$)|^("".*"")$|:(.*)|(^[^""]*$)";

            Regex regex = new Regex(pattern);

            Match match = regex.Match(token);
            //Console.WriteLine("token: ^" + token + "$");

            if (!match.Success) throw new ParseError("unrecognized token '" + token + "'");

            if (match.Groups[1].Value == String.Empty)
            {
                if (match.Groups[3].Value == String.Empty)
                {
                    if (match.Groups[4].Value == String.Empty)
                    {
                        if (match.Groups[5].Value == String.Empty)
                        {
                            if (match.Groups[6].Value == String.Empty)
                            {
                                if (match.Groups[7].Value == String.Empty)
                                {
                                    if (match.Groups[8].Value == String.Empty)
                                    {
                                        throw new ParseError("unrecognized '" + match.Groups[0] + "'");
                                    }
                                    return new OpenLispSymbol(match.Groups[8].Value);
                                }
                                return new OpenLispString("\u029e" + match.Groups[7].Value);
                            }
                            string str = match.Groups[6].Value;
                            str = str.Substring(1, str.Length - 2)
                                .Replace("\\\"", "\"")
                                .Replace("\\n", "\n");
                            return new OpenLispString(str);
                        }
                        return StaticOpenLispTypes.False;
                    }
                    return StaticOpenLispTypes.True;
                }
                return StaticOpenLispTypes.Nil;
            }
            return new OpenLispInt(int.Parse(match.Groups[1].Value));
        }

        public static OpenLispVal ReadList(TokensReader rdr, OpenLispList lst, char start, char end)
        {
            string token = rdr.Next();
            if (token[0] == start)
            {
                while ((token = rdr.Peek()) != null && token[0] != end)
                {
                    lst.Conj(ReadForm(rdr));
                }

                if (token == null)
                {
                    throw new ParseError("expected '" + end + "', got EOF");
                }
                rdr.Next();

                return lst;
            }

            throw new ParseError("expected '" + start + "'");
        }

        public static OpenLispVal ReadHashMap(TokensReader rdr)
        {
            OpenLispList lst = (OpenLispList)ReadList(rdr, new OpenLispList(), '{', '}');
            return new OpenLispHashMap(lst);
        }

        /// <summary>
        /// This recursived static method recursive processes
        /// OpenLisp.NET S-Expressions and tokenizes them.
        /// </summary>
        /// <param name="rdr"></param>
        /// <returns></returns>
        public static OpenLispVal ReadForm(TokensReader rdr)
        {
            string token = rdr.Peek();
            if (token == null) { throw new OpenLispContinue(); }
            OpenLispVal form = null;

            switch (token)
            {
                case "'":
                    rdr.Next();
                    return new OpenLispList(new OpenLispSymbol("quote"),
                                       ReadForm(rdr));
                case "`":
                    rdr.Next();
                    return new OpenLispList(new OpenLispSymbol("quasiquote"),
                                       ReadForm(rdr));
                case "~":
                    rdr.Next();
                    return new OpenLispList(new OpenLispSymbol("unquote"),
                                       ReadForm(rdr));
                case "~@":
                    rdr.Next();
                    return new OpenLispList(new OpenLispSymbol("splice-unquote"),
                                       ReadForm(rdr));
                case "^":
                    rdr.Next();
                    OpenLispVal meta = ReadForm(rdr);
                    return new OpenLispList(new OpenLispSymbol("with-meta"),
                                       ReadForm(rdr),
                                       meta);
                case "@":
                    rdr.Next();
                    return new OpenLispList(new OpenLispSymbol("deref"),
                                       ReadForm(rdr));

                case "(": form = ReadList(rdr, new OpenLispList(), '(', ')'); break;
                case ")": throw new ParseError("unexpected ')'");
                case "[": form = ReadList(rdr, new OpenLispVector(), '[', ']'); break;
                case "]": throw new ParseError("unexpected ']'");
                case "{": form = ReadHashMap(rdr); break;
                case "}": throw new ParseError("unexpected '}'");
                default: form = ReadAtom(rdr); break;
            }
            return form;
        }

        public static OpenLispVal ReadStr(string str)
        {
            return ReadForm(new TokensReader(Tokenize(str)));
        }
    }
}