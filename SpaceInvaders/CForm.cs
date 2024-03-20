using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpaceInvaders
{
    public enum Pages
    {
        Menu,
        Game,
        GameOver,
    }
    public static class CForm
    {
        public static Form1 frmMain = null;
        public static List<Form> listPages = null;
        public static List<Control> listControls = null;
        public static Form frmCurrentPage = null;

        public static Form_Menu frmMenu = new Form_Menu();
        public static Form_Game frmGame = new Form_Game();
        public static Form_GameOver frmGameOver = new Form_GameOver();

        public static void Init_Form(Form1 mother)
        {
            frmMain = mother;
            listPages = new List<Form>();
            listPages.Clear();
            listControls = new List<Control>();
            listControls.Clear();

            Add_Page(frmMenu);
            Add_Page(frmGame);
            Add_Page(frmGameOver);

            // Remove overlap data
            listControls = Check_OverlapControl(listControls);

            Display_Page(Pages.Menu);
        }

        public static void Display_Page(Pages page, int option = 0) // Page Change
        {
            Form changeForm = null;
            switch (page)
            {
                case Pages.Menu:
                    changeForm = frmMenu;
                    break;
                case Pages.Game:
                    changeForm = frmGame;
                    break;
                case Pages.GameOver:
                    changeForm = frmGameOver;
                    frmGameOver.score = option;
                    break;
                default:
                    return;
            }
            frmCurrentPage = changeForm;
            string strNameForm = changeForm.Name;
            for (int i = 0; i < listPages.Count; i++)
            {
                if (listPages[i].Name == strNameForm)
                {
                    listPages[i].Show();
                }
                else
                {
                    listPages[i].Hide();
                }
            }
        }

        private static void Add_Page(System.Windows.Forms.Form form) // Add Page (Main Panel <- Form)
        {
            form.TopMost = true;
            form.TopLevel = false;
            form.Parent = frmMain;
            frmMain.pnl_main.Controls.Add(form);
            listPages.Add(form);
            Add_Controls(form.Controls);
            form.Hide();
        }

        private static void Add_Controls(System.Windows.Forms.Control.ControlCollection ctrl) // Add Control in List Value (Recurcive Function)
        {
            foreach (System.Windows.Forms.Control con in ctrl)
            {
                if (con.Controls.Count > 0)
                {
                    Add_Controls(con.Controls);
                }
                listControls.Add(con);
            }
        }

        private static List<System.Windows.Forms.Control> Check_OverlapControl(List<System.Windows.Forms.Control> list) // Remove overlap data
        {
            List<System.Windows.Forms.Control> newList = new List<System.Windows.Forms.Control>();
            newList.Clear();
            for (int i = 0; i < list.Count; i++)
            {
                if (!newList.Contains(list[i]))
                {
                    newList.Add(list[i]);
                }
            }
            return newList;
        }

        public static void Destroy()
        {
            // Kill Process
            Application.Exit();
        }
    }
}
