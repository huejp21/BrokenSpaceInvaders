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
    public partial class Form_Test : Form
    {
        Label[,] arr = new Label[30, 20];
        public Form_Test()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            for (int i = 0; i < CForm.frmGame.grid_map.GetLength(0); i++)
            {
                for (int j = 0; j < CForm.frmGame.grid_map.GetLength(1); j++)
                {
                    switch ((Location_Code)CForm.frmGame.grid_map[i,j])
                    {
                        case Location_Code.Empty:
                            arr[i, j].BackColor = Color.White;
                            break;
                        case Location_Code.Me:
                            arr[i, j].BackColor = Color.Blue;
                            break;
                        case Location_Code.Enemy:
                            arr[i, j].BackColor = Color.Red;
                            break;
                        case Location_Code.Bullet:
                            arr[i, j].BackColor = Color.Black;
                            break;
                        case Location_Code.Hit:
                            arr[i, j].BackColor = Color.Orange;
                            break;
                        default:
                            arr[i, j].BackColor = Color.White;
                            break;
                    }
                }
            }
            timer1.Enabled = true;
        }

        private void Form_Test_Load(object sender, EventArgs e)
        {

            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    Label dp = new Label();
                    arr[i, j] = dp;
                    this.Controls.Add(dp);
                    dp.AutoSize = false;
                    dp.Location = new Point(i * 10, j * 10);
                    dp.Size = new Size(10, 10);
                    dp.BackColor = Color.White;
                }
            }
        }
        
    }
}
