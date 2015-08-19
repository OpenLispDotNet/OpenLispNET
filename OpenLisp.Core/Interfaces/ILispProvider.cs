namespace OpenLisp.Core.Interfaces
{
    public interface ILispProvider
    {
        string Name { get; set; } 

        object Tokenizer { get; set; }

        object Parser { get; set; }

        object Terminal { get; set; }
    }
}