namespace OpenLisp.Core.Interfaces
{
    /// <summary>
    /// Interface to describe an OpenLisp.NET provider
    /// </summary>
    public interface ILispProvider
    {
        /// <summary>
        /// <see cref="string"/> Name
        /// </summary>
        string Name { get; set; } 

        /// <summary>
        /// <see cref="object"/> Tokenizer 
        /// </summary>
        object Tokenizer { get; set; }

        /// <summary>
        /// <see cref="object"/> Parser
        /// </summary>
        object Parser { get; set; }

        /// <summary>
        /// <see cref="object"/> Terminal / REPL
        /// </summary>
        object Terminal { get; set; }
    }
}