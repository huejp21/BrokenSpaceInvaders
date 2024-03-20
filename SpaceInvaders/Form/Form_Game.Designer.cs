namespace SpaceInvaders
{
    partial class Form_Game
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Game));
            this.iml_icons = new System.Windows.Forms.ImageList(this.components);
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbl_score = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // iml_icons
            // 
            this.iml_icons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("iml_icons.ImageStream")));
            this.iml_icons.TransparentColor = System.Drawing.Color.White;
            this.iml_icons.Images.SetKeyName(0, "Honeyview_an1_0.png");
            this.iml_icons.Images.SetKeyName(1, "Honeyview_an1_1.png");
            this.iml_icons.Images.SetKeyName(2, "Honeyview_Ship.png");
            this.iml_icons.Images.SetKeyName(3, "Bullet.png");
            // 
            // timer
            // 
            this.timer.Interval = 1;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // panel1
            // 
            this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel1.BackgroundImage = global::SpaceInvaders.Properties.Resources.BackGround;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.lbl_score);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(600, 400);
            this.panel1.TabIndex = 0;
            // 
            // lbl_score
            // 
            this.lbl_score.BackColor = System.Drawing.Color.Transparent;
            this.lbl_score.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_score.ForeColor = System.Drawing.Color.White;
            this.lbl_score.Location = new System.Drawing.Point(3, 0);
            this.lbl_score.Name = "lbl_score";
            this.lbl_score.Size = new System.Drawing.Size(84, 20);
            this.lbl_score.TabIndex = 0;
            this.lbl_score.Text = "00000000";
            this.lbl_score.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Form_Game
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(600, 400);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "Form_Game";
            this.Text = "Form_Game";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Form_Game_Load);
            this.VisibleChanged += new System.EventHandler(this.Form_Game_VisibleChanged);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form_Game_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form_Game_KeyUp);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.ImageList iml_icons;
        private System.Windows.Forms.Label lbl_score;
    }
}