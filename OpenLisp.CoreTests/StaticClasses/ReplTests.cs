using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenLisp.Core.StaticClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcanaErp.UnitTests.Repl
{
    [TestClass()]
    public class ReplTests
    {
        [TestMethod()]
        public void EvalTest()
        {
            try
            {
                // Arrange
                OpenLisp.Core.StaticClasses.Repl.Eval(null, null);

                // Act

                // Assert
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}\n{ex.StackTrace}");
            }
        }
    }
}