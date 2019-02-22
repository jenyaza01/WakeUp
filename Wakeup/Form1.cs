using System;
using System.Windows.Forms;
using System.IO;
using System.Drawing;

namespace Wakeup
{
    public partial class Form1 : Form
    {
        DateTime farTime;
        DateTime lastTime;
        DateTime currTime;
        TimeSpan deltaTime;

        Point farPos;
        Point lastPos;
        Point currPos;
        double deltaPos;

        string path = Application.StartupPath + "\\Wakeup.bat";
        string param = "/c \"" + Application.StartupPath + "\\Wakeup.bat\"";


        public Form1()
        {
            InitializeComponent();
            this.Hide();
            timer1.Enabled = true; // init timer
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            currTime = DateTime.Now;
            lastTime = currTime;
            currPos = Cursor.Position;
            lastPos = currPos;
            timer1.Enabled = false;
            timer2.Enabled = true;
            Update();
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer2.Enabled = false;
            this.Close();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            Update();
        }


        private void Form1_Leave(object sender, EventArgs e)
        {
            notifyIcon1.Visible = false;
            notifyIcon1.Dispose();
        }


        new void Update()
        {
            farTime = lastTime;
            lastTime = currTime;
            currTime = DateTime.Now;
            deltaTime = lastTime - farTime;

            farPos = lastPos;
            lastPos = currPos;
            currPos = Cursor.Position;
            deltaPos = dist2(lastPos, farPos);

            if (deltaTime.TotalSeconds > 42)
            {
                if( deltaPos < 2)
                    System.Diagnostics.Process.Start("cmd.exe", param);
            }
            timeToolStripMenuItem.Text = String.Format("{0:f}s, {1:f1} px2", deltaTime.TotalSeconds, deltaPos);

        }

        private double dist2(Point p1, Point p2)
        {
            int dx = p2.X - p1.X;
            int dy = p2.Y - p1.Y;
            return (dx * dx) + (dy * dy);
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            if (!File.Exists(path))
            {
                FileStream f = File.Create(path);
                f.Close();
            }
        }

        private void timeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("cmd.exe", param);
        }
    }
}
