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
    /// <summary>
    /// Reads and tokenizes OpenLisp.NET S-Expressions and forms. 
    /// </summary>
    public class Reader
    {
        /// <summary>
        /// Custom throwable parse error.
        /// 
        /// TODO: move to OpenLisp.Core.DataTypes.Errors.Throwable?
        /// </summary>
        private class ParseError : OpenLispThrowable
        {
            /// <summary>
            /// ParseError constructor.
            /// </summary>
            /// <param name="message"></param>
            public ParseError(string message) : base(message) { }
        }

        /// <summary>
        /// Allows OpenLisp.NET to peek ahead to the next token and
        /// return the next token in the collection.
        /// </summary>
        public class TokensReader
        {
            readonly List<string> _tokens;
            int _position;

            /// <summary>
            /// TokensReader constructor.
            /// </summary>
            /// <param name="tokens"></param>
            public TokensReader(List<string> tokens)
            {
                _tokens = tokens;
                _position = 0;
            }

            /// <summary>
            /// Peeks a token.
            /// </summary>
            /// <returns></returns>
            public string Peek()
            {
                return _position >= _tokens.Count ? null : _tokens[_position];
            }

            /// <summary>
            /// Gets the next token.
            /// </summary>
            /// <returns></returns>
            public string Next() => _tokens[_position++];
        }

        /// <summary>
        /// A simple tokenizer to tokenize OpenLisp.NET S-Expressions.
        /// </summary>
        /// <param name="tokens"></param>
        /// <returns></returns>
        private static List<string> Tokenize(string tokens)
        {
            // WARNING: while a regular expression may not be ideal, Lisp is small enough of a language
            //          where a single small regular expression can match every possible S-Expression
            //          in OpenLisp.NET; however, this regexp suits our purpose.  Interpreting these
            //          arcane hieroglyphs of the ancient neckbeards is an exercise left to the reader.  =)
            string pattern = @"[\s ,]*(~@|[\[\]{}()'`~@]|""(?:[\\].|[^\\""])*""|;.*|[^\s \[\]{}()'""`~@,;]*)";

            Regex regex = new Regex(pattern);
            return regex.Matches(tokens)
                .Cast<Match>()
                .Select(match => match.Groups[1].Value)
                .Where(token => !string.IsNullOrEmpty(token) && token[0] != ';')
                .ToList();
        }

        /// <summary>
        /// Reads an OpenLisp.NET atom.
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        private static OpenLispVal ReadAtom(TokensReader reader)
        {
            string token = reader.Next();

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

        /// <summary>
        /// Reads an OpenLisp.NET list expression.
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="openLispList"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static OpenLispVal ReadList(TokensReader reader, OpenLispList openLispList, char start, char end)
        {
            string token = reader.Next();
            if (token[0] == start)
            {
                while ((token = reader.Peek()) != null && token[0] != end)
                {
                    openLispList.Conj(ReadForm(reader));
                }

                if (token == null)
                {
                    throw new ParseError("expected '" + end + "', got EOF");
                }
                reader.Next();

                return openLispList;
            }

            throw new ParseError("expected '" + start + "'");
        }

        /// <summary>
        /// Reads a hash map using an instance of <see cref="TokensReader"/>.
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static OpenLispVal ReadHashMap(TokensReader reader)
        {
            OpenLispList openLispList = (OpenLispList)ReadList(reader, new OpenLispList(), '{', '}');
            return new OpenLispHashMap(openLispList);
        }

        /// <summary>
        /// This static method recursively processes
        /// OpenLisp.NET forms and tokenizes them.
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static OpenLispVal ReadForm(TokensReader reader)
        {
            string token = reader.Peek();
            if (token == null)
            {
                throw new OpenLispContinue();
            }

            OpenLispVal form = null;

            switch (token)
            {
                case "'":
                    reader.Next();
                    return new OpenLispList(new OpenLispSymbol("quote"),
                                       ReadForm(reader));
                case "`":
                    reader.Next();
                    return new OpenLispList(new OpenLispSymbol("quasiquote"),
                                       ReadForm(reader));
                case "~":
                    reader.Next();
                    return new OpenLispList(new OpenLispSymbol("unquote"),
                                       ReadForm(reader));
                case "~@":
                    reader.Next();
                    return new OpenLispList(new OpenLispSymbol("splice-unquote"),
                                       ReadForm(reader));
                case "^":
                    reader.Next();
                    OpenLispVal meta = ReadForm(reader);
                    return new OpenLispList(new OpenLispSymbol("with-meta"),
                                       ReadForm(reader),
                                       meta);
                case "@":
                    reader.Next();
                    return new OpenLispList(new OpenLispSymbol("deref"),
                                       ReadForm(reader));

                case "(": form = ReadList(reader, new OpenLispList(), '(', ')'); break;
                case ")": throw new ParseError("unexpected ')'");
                case "[": form = ReadList(reader, new OpenLispVector(), '[', ']'); break;
                case "]": throw new ParseError("unexpected ']'");
                case "{": form = ReadHashMap(reader); break;
                case "}": throw new ParseError("unexpected '}'");
                default: form = ReadAtom(reader); break;
            }
            return form;
        }

        /// <summary>
        /// Reads an OpenLisp.NET str.
        /// </summary>
        /// <param name="tokens"></param>
        /// <returns></returns>
        public static OpenLispVal ReadStr(string tokens)
        {
            return ReadForm(new TokensReader(Tokenize(tokens)));
        }
    }
}
