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
    public partial class Form_GameOver : Form
    {
        public int score = 0;
        public Form_GameOver()
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
