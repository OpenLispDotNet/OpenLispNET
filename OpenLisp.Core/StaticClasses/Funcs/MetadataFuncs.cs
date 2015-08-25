using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenLisp.Core.AbstractClasses;
using OpenLisp.Core.DataTypes;
using OpenLisp.Core.DataTypes.Errors.Throwable;

namespace OpenLisp.Core.StaticClasses.Funcs
{
    public class MetadataFuncs
    {
        public static OpenLispFunc Meta = new OpenLispFunc(x => x[0].Meta);

        public static OpenLispFunc WithMeta = new OpenLispFunc(x => x[0].Copy().Meta = x[1]);
    }
}