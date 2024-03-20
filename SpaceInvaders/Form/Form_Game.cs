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
    public enum Location_Code
    {
        Empty = 0,
        Me = 1,
        Enemy = 2,
        Bullet = 3,
        Hit = 4,
    }

    public enum ImageCode
    {
        Bug_0,
        Bug_1,
        Me,
        Bullet,
    }

    public enum EnemyDirection
    {
        Right,
        Left,
        Down,
    }

    public enum MyAction
    {
        None,
        Left,
        Right,
    }

    public partial class Form_Game : Form
    {
        public const int grid_size_width = 30;
        public const int grid_size_height = 20;

        public int one_width = 0;
        public int one_height = 0;

        public int[,] grid_map = new int[grid_size_width, grid_size_height];

        public List<Label> enemies = new List<Label>();
        public Label me = new Label();
        public List<Label> bullets = new List<Label>();

        public int stage_limit = 5;
        public int stage = 0;

        public int state_limit = 9;
        public int state = 0;
        public int interval = 10;
        public int countTime = 0;

        public int shootInterval = 5;
        public int shootDelay = 0;

        public int score = 0;
        public int oneEnemyScore = 10;

        public EnemyDirection enDir = EnemyDirection.Right;
        public MyAction myAction = MyAction.None;

        public bool key_Left = false;
        public bool key_Right = false;
        public bool key_Shoot = false;

        public bool gameOverBit = false;

        public Form_Game()
        {
            InitializeComponent();
        }

        private void Form_Game_Load(object sender, EventArgs e)
        {
            one_width = panel1.Width / grid_size_width;
            one_height = panel1.Height / grid_size_height;

            iml_icons.ImageSize = new Size(one_width, one_height);
            iml_icons.TransparentColor = Color.Transparent;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            timer.Enabled = false;
            lbl_score.Text = score.ToString();
            countTime++;
            if (0 < shootDelay)
            {
                shootDelay--;
            }
            if (key_Shoot && shootDelay == 0)
            {
                shootDelay = shootInterval;
                Location_Code tile = (Location_Code)grid_map[GetMyPosition()[0], GetMyPosition()[1] - 1];
                if (tile == Location_Code.Empty)
                {
                    grid_map[GetMyPosition()[0], GetMyPosition()[1] - 1] = (int)Location_Code.Bullet;
                }
                else if(tile == Location_Code.Enemy)
                {
                    grid_map[GetMyPosition()[0], GetMyPosition()[1] - 1] = (int)Location_Code.Empty;
                    score += oneEnemyScore;
                }
            }
            if (countTime > interval)
            {
                state++;
                countTime = 0;
                EnemiesShift();
                BulletShift();
            }
            if (state > state_limit)
            {
                state = 0;
            }

            for (int i = 0; i < enemies.Count; i++)
            {
                if (state % 2 == 0)
                {
                    enemies[i].ImageIndex = (int)ImageCode.Bug_0;
                }
                else
                {
                    enemies[i].ImageIndex = (int)ImageCode.Bug_1;
                }
            }

            if (countTime % 4 == 0)
            {
                int[] myPos = GetMyPosition();
                switch (myAction)
                {
                    case MyAction.Left:
                        if (myPos[0] > 0)
                        {
                            grid_map[myPos[0] - 1, myPos[1]] = (int)Location_Code.Me;
                            grid_map[myPos[0], myPos[1]] = (int)Location_Code.Empty;
                            me.Location = new Point((myPos[0] - 1) * one_width, myPos[1] * one_height);
                        }
                        break;
                    case MyAction.Right:
                        if (myPos[0] < grid_size_width - 1)
                        {
                            grid_map[myPos[0] + 1, myPos[1]] = (int)Location_Code.Me;
                            grid_map[myPos[0], myPos[1]] = (int)Location_Code.Empty;
                            me.Location = new Point((myPos[0] + 1) * one_width, myPos[1] * one_height);
                        }
                        break;
                    default:
                        break;
                }
            }


            if (gameOverBit == false)
            {
                timer.Enabled = true;
            }
            
        }

        private void StageChange(int changeStage)
        {
            if (changeStage < 1 || changeStage > stage_limit)
            {
                GameOver(score);
                return;
            }
            stage = changeStage;
            for (int i = 0; i < enemies.Count; i++)
            {
                panel1.Controls.Remove(enemies[i]);
                enemies[i] = null;
            }
            Clean_Map();
            enemies.Clear();
            int enemyCount = 0;
            if (changeStage == 1)
            {
                score = 0;
            }
            switch (changeStage)
            {
                case 1: enemyCount = 3; break;
                case 2: enemyCount = 5; break;
                case 3: enemyCount = 9; break;
                case 4: enemyCount = 10; break;
                case 5: enemyCount = 12; break;
                default: break;
            }

            for (int i = 0; i < enemyCount; i++)
            {
                int x = i * 2;
                int y = 0;
                if ((x / grid_size_width) > 0)
                {
                    y = (x / grid_size_width);
                    x = (x % grid_size_width);
                }
                Label en = new Label();
                en.BackColor = Color.Transparent;
                en.ImageList = iml_icons;
                en.ImageIndex = (int)ImageCode.Bug_1;
                en.AutoSize = false;
                en.Size = new Size(one_width, one_height);
                en.Location = new Point(x * one_width, y * one_height);
                panel1.Controls.Add(en);
                enemies.Add(en);
                grid_map[PointToArr(en.Location)[0], PointToArr(en.Location)[1]] = (int)Location_Code.Enemy;
            }
            Revive();
            gameOverBit = false;
            timer.Enabled = true;
        }

        private void Form_Game_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                StageChange(1);
            }
        }

        private void Revive()
        {
            panel1.Controls.Remove(me);
            me = null;
            me = new Label();
            panel1.Controls.Add(me);
            me.Name = "Me";
            me.AutoSize = false;
            me.Location = new Point(((grid_size_width / 2) - 1) * one_width, (grid_size_height - 1) * one_height);
            me.Size = new Size(one_width, one_height);
            me.BackColor = Color.Transparent;
            me.ImageList = iml_icons;
            me.ImageIndex = (int)ImageCode.Me;
            grid_map[PointToArr(me.Location)[0], PointToArr(me.Location)[1]] = (int)Location_Code.Me;
        }

        private void Clean_Map()
        {
            for (int i = 0; i < grid_size_width; i++)
            {
                for (int j = 0; j < grid_size_height; j++)
                {
                    grid_map[i, j] = (int)Location_Code.Empty;
                }
            }
        }

        private int[] PointToArr(Point loc)
        {
            int[] xy = new int[2];
            xy[0] = loc.X / one_width;
            xy[1] = loc.Y / one_height;
            return xy;
        }

        private void Form_Game_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.A:
                case Keys.Left:
                    key_Left = true;
                    myAction = MyAction.Left;
                    break;
                case Keys.D:
                case Keys.Right: key_Right = true;
                    myAction = MyAction.Right;
                    break;
                case Keys.Space:
                    key_Shoot = true;
                    break;
                default:
                    break;
            }
        }

        private void Form_Game_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.A:
                case Keys.Left:
                    key_Left = false;
                    if (myAction == MyAction.Left)
                    {
                        myAction = MyAction.None;
                    }
                    break;
                case Keys.D:
                case Keys.Right: key_Right = false;
                    if (myAction == MyAction.Right)
                    {
                        myAction = MyAction.None;
                    }
                    break;
                case Keys.Space:
                    key_Shoot = false;
                    break;
                default:
                    break;
            }
        }

        private int[] GetMyPosition()
        {
            int[] xy = new int[2];
            xy[0] = -1;
            xy[1] = -1;
            bool found = false;
            for (int i = 0; i < grid_size_width; i++)
            {
                for (int j = 0; j < grid_size_height; j++)
                {
                    if (grid_map[i, j] == (int)Location_Code.Me)
                    {
                        xy[0] = i;
                        xy[1] = j;
                        return xy;
                    }
                }
            }
            return xy;
        }

        private void EnemiesShift()
        {
            int x = -1;
            int y = -1;

            int[,] temp = new int[grid_size_width, grid_size_height];
            switch (enDir)
            {
                case EnemyDirection.Right:
                    x = -1;
                    y = -1;
                    for (int i = 0; i < grid_size_width; i++)
                    {
                        for (int j = 0; j < grid_size_height; j++)
                        {
                            if (grid_map[i, j] == (int)Location_Code.Enemy)
                            {
                                if (i > x)
                                {
                                    if (i + 1 < grid_size_width)
                                    {
                                        if (grid_map[i + 1, j] == (int)Location_Code.Bullet)
                                        {
                                            continue;
                                        }
                                    }
                                    x = i;
                                    y = j;
                                }
                            }
                        }
                    }

                    if (x < (grid_size_width - 1))
                    {
                        Array.Copy(grid_map, temp, grid_map.Length);
                        for (int i = 0; i < grid_size_width; i++)
                        {
                            for (int j = 0; j < grid_size_height; j++)
                            {
                                if (grid_map[i, j] == (int)Location_Code.Enemy)
                                {
                                    if (i + 1 < grid_size_width)
                                    {
                                        if (grid_map[i + 1, j] == (int)Location_Code.Bullet)
                                        {
                                            temp[i, j] = (int)Location_Code.Empty;
                                            temp[i + 1, j] = (int)Location_Code.Empty;
                                            score += oneEnemyScore;
                                        }
                                        else
                                        {
                                            temp[i + 1, j] = grid_map[i, j];
                                            temp[i, j] = grid_map[i + 1, j];
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else if (x >= (grid_size_width - 1))
                    {
                        Array.Copy(grid_map, temp, grid_map.Length);
                        for (int i = 0; i < grid_size_width; i++)
                        {
                            for (int j = 0; j < grid_size_height; j++)
                            {
                                if (grid_map[i, j] == (int)Location_Code.Enemy)
                                {
                                    if (j + 1 < grid_size_height)
                                    {
                                        if (grid_map[i, j + 1] == (int)Location_Code.Bullet)
                                        {
                                            temp[i, j] = (int)Location_Code.Empty;
                                            temp[i, j + 1] = (int)Location_Code.Empty;
                                            score += oneEnemyScore;
                                        }
                                        else
                                        {
                                            temp[i, j + 1] = grid_map[i, j];
                                            temp[i, j] = grid_map[i, j + 1];
                                        }
                                    }
                                }
                            }
                        }
                        enDir = EnemyDirection.Left;
                    }
                    break;
                case EnemyDirection.Left:
                    x = 999;
                    y = -1;
                    for (int i = 0; i < grid_size_width; i++)
                    {
                        for (int j = 0; j < grid_size_height; j++)
                        {
                            if (grid_map[i, j] == (int)Location_Code.Enemy)
                            {
                                if (i < x)
                                {
                                    if (i - 1 > 0)
                                    {
                                        if (grid_map[i - 1, j] == (int)Location_Code.Bullet)
                                        {
                                            continue;
                                        }
                                    }
                                    x = i;
                                    y = j;
                                }
                            }
                        }
                    }

                    if (x > 0)
                    {
                        Array.Copy(grid_map, temp, grid_map.Length);
                        for (int i = 0; i < grid_size_width; i++)
                        {
                            for (int j = 0; j < grid_size_height; j++)
                            {
                                if (grid_map[i, j] == (int)Location_Code.Enemy)
                                {
                                    if (i - 1 >= 0)
                                    {
                                        if (grid_map[i - 1, j] == (int)Location_Code.Bullet)
                                        {
                                            temp[i, j] = (int)Location_Code.Empty;
                                            temp[i - 1, j] = (int)Location_Code.Empty;
                                            score += oneEnemyScore;
                                        }
                                        else
                                        {
                                            temp[i - 1, j] = grid_map[i, j];
                                            temp[i, j] = grid_map[i - 1, j];
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else if (x <= 0)
                    {
                        Array.Copy(grid_map, temp, grid_map.Length);
                        for (int i = 0; i < grid_size_width; i++)
                        {
                            for (int j = 0; j < grid_size_height; j++)
                            {
                                if (grid_map[i, j] == (int)Location_Code.Enemy)
                                {
                                    if (j + 1 < grid_size_height)
                                    {
                                        if (grid_map[i, j + 1] == (int)Location_Code.Bullet)
                                        {
                                            temp[i, j] = (int)Location_Code.Empty;
                                            temp[i, j + 1] = (int)Location_Code.Empty;
                                            score += oneEnemyScore;
                                        }
                                        else
                                        {
                                            temp[i, j + 1] = grid_map[i, j];
                                            temp[i, j] = grid_map[i, j + 1];
                                        }
                                    }
                                }
                            }
                        }
                        enDir = EnemyDirection.Right;
                    }
                    break;
                case EnemyDirection.Down:
                default:
                    break;
            }
            for (int i = 0; i < grid_size_width; i++)
            {
                for (int j = 0; j < grid_size_height; j++)
                {
                    if (temp[i, j] == (int)Location_Code.Empty)
                    {
                        grid_map[i, j] = (int)Location_Code.Empty;
                    }
                    else if (temp[i, j] == (int)Location_Code.Enemy)
                    {
                        grid_map[i, j] = (int)Location_Code.Enemy;
                    }
                }
            }

            bool check = false;
            for (int i = 0; i < grid_size_width; i++)
            {
                for (int j = 0; j < grid_size_height; j++)
                {
                    if (grid_map[i, j] == (int)Location_Code.Enemy)
                    {
                        check = true;
                    }
                }
            }
            if (check == false)
            {
                StageChange(stage + 1);
                return;
            }

            for (int i = 0; i < grid_size_width; i++)
            {
                for (int j = grid_size_height - 1; j < grid_size_height; j++)
                {
                    if (grid_map[i, j] == (int)Location_Code.Enemy)
                    {
                        GameOver(score);
                    }
                }
            }

            EnemiesRefresh();
        }

        private void EnemiesRefresh()
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                panel1.Controls.Remove(enemies[i]);
                enemies[i] = null;
            }
            enemies.Clear();
            for (int i = 0; i < grid_size_width; i++)
            {
                for (int j = 0; j < grid_size_height; j++)
                {
                    if (grid_map[i, j] == (int)Location_Code.Enemy)
                    {
                        Label en = new Label();
                        en.BackColor = Color.Transparent;
                        en.ImageList = iml_icons;
                        en.ImageIndex = (int)ImageCode.Bug_1;
                        en.Name = "E" + enemies.Count.ToString();
                        en.AutoSize = false;
                        en.Size = new Size(one_width, one_height);
                        en.Location = new Point(i * one_width, j * one_height);
                        panel1.Controls.Add(en);
                        enemies.Add(en);
                    }
                }
            }
        }

        private void BulletShift()
        {
            int[,] temp = new int[grid_size_width, grid_size_height];
            Array.Copy(grid_map, temp, grid_map.Length);

            for (int i = 0; i < grid_size_width; i++)
            {
                for (int j = 0; j < grid_size_height; j++)
                {
                    if (grid_map[i,j] == (int)Location_Code.Bullet)
                    {
                        if (j > 0)
                        {
                            if (grid_map[i, j - 1] == (int)Location_Code.Enemy)
                            {
                                temp[i, j] = (int)Location_Code.Empty;
                                temp[i, j - 1] = (int)Location_Code.Empty;
                                score += oneEnemyScore;
                            }
                            else if (grid_map[i, j - 1] == (int)Location_Code.Empty)
                            {
                                temp[i, j] = (int)Location_Code.Empty;
                                temp[i, j - 1] = (int)Location_Code.Bullet;
                            }
                        }
                        else
                        {
                            temp[i, j] = (int)Location_Code.Empty;
                        }
                    }
                }
            }

            for (int i = 0; i < grid_size_width; i++)
            {
                for (int j = 0; j < grid_size_height; j++)
                {
                    if (temp[i, j] == (int)Location_Code.Empty)
                    {
                        grid_map[i, j] = (int)Location_Code.Empty;
                    }
                    else if (temp[i, j] == (int)Location_Code.Bullet)
                    {
                        grid_map[i, j] = (int)Location_Code.Bullet;
                    }
                }
            }
            BulletRefresh();
        }

        private void BulletRefresh()
        {
            for (int i = 0; i < bullets.Count; i++)
            {
                panel1.Controls.Remove(bullets[i]);
                bullets[i] = null;
            }
            for (int i = 0; i < grid_size_width; i++)
            {
                for (int j = 0; j < grid_size_height; j++)
                {
                    if (grid_map[i, j] == (int)Location_Code.Bullet)
                    {
                        Label en = new Label();
                        en.BackColor = Color.Transparent;
                        en.ImageList = iml_icons;
                        en.ImageIndex = (int)ImageCode.Bullet;
                        en.Name = "Bullet" + bullets.Count.ToString();
                        en.AutoSize = false;
                        en.Size = new Size(one_width, one_height);
                        en.Location = new Point(i * one_width, j * one_height);
                        panel1.Controls.Add(en);
                        bullets.Add(en);
                    }
                }
            }
        }

        private void GameOver(int Score)
        {
            gameOverBit = true;
            timer.Enabled = false;
            CForm.Display_Page(Pages.GameOver, score);
        }
    }
}
