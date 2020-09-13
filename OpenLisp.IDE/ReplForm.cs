using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenLisp.IDE
{
    public partial class ReplForm : Form
    {
        public ReplForm()
        {
            InitializeComponent();
        }

        private void TextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            Console.WriteLine(sender.ToString());
            Console.WriteLine(e.ToString());
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            // TODO: add REPL to background worker
            // TODO: send TextBox1 text to REPL for eval
            // TODO: append output from REPL to RichextBox1
        }
    }
}
