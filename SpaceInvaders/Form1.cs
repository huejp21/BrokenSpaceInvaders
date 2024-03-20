using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpaceInvaders
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string name = ((ToolStripMenuItem)sender).Name;
            switch (name)
            {
                case "aboutToolStripMenuItem":
                    Form_About form = new Form_About();
                    form.ShowDialog();
                    break;
                default:
                    break;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CForm.Init_Form(this);
        }
    }
}
