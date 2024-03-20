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
    public partial class Form_Menu : Form
    {
        public Form_Menu()
        {
            InitializeComponent();
        }

        private void btn_Click(object sender, EventArgs e)
        {
            string name = ((Button)sender).Name;
            switch (name)
            {
                case "btn_start":
                    CForm.Display_Page(Pages.Game);
                    //Form_Test ts = new Form_Test();
                    //ts.Show();
                    break;
                case "btn_setting":

                    break;
                case "btn_scoreBoard":

                    break;
                case "btn_quit":
                    CForm.Destroy();
                    break;
                default:
                    break;
            }

        }
    }
}
