using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
// для работы с библиотекой OpenGL 
using Tao.OpenGl;
// для работы с библиотекой FreeGLUT 
using Tao.FreeGlut;
// для работы с элементом управления SimpleOpenGLControl 
using Tao.Platform.Windows;

namespace Moleculs
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
            AnT.InitializeContexts();
            AnT.Width = this.Width;
            AnT.Height = this.Height;
        }
        Molecule[] list = new Molecule[200];
        public static Player player;
        Engine engine;
        private void Form1_Load(object sender, EventArgs e)
        {
            AnT.Width = this.Width;
            AnT.Height = this.Height;
            // инициализация Glut 
            Glut.glutInit();
            Glut.glutInitDisplayMode(Glut.GLUT_RGB | Glut.GLUT_DOUBLE | Glut.GLUT_DEPTH);

            // очитка окна 
            Gl.glClearColor(0,0, 0, 1);

            // установка порта вывода в соотвествии с размерами элемента anT 
            Gl.glViewport(0, 0, AnT.Width, AnT.Height);

            // настройка проекции 
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glLoadIdentity();
            Glu.gluOrtho2D(0.0, AnT.Width, 0.0, AnT.Height);
            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glLoadIdentity();
            for(int i=0; i <list.Length;i++)
            {
                list[i] = new Molecule();
            }
            
            player = new Player();
            engine = new Engine(list, this.AnT, player, timer1);
            player.cx = AnT.Width / 2;
            player.cy = AnT.Height / 2;
            Random r = new Random();
            list[used++] = player;
            for (int i = 0; i < 90;i++)
            {
                list[used].cx = r.Next(50,AnT.Width);
                list[used].cy = r.Next(50, AnT.Height);
                list[used].generate();
                used++;
            }
            for (int i = 1; i < used; i++)
            {
                if ((player.cx - list[i].cx) * (player.cx - list[i].cx) + (player.cy - list[i].cy) * (player.cy - list[i].cy) <= (player.r + list[i].r) * (player.r + list[i].r) + 10)
                {
                    list[i].cx += player.r+list[i].r;
                    list[i].cy += player.r + list[i].r;
                }
            }
        }
        private void Render()
        {
                Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);
                for (int i = 0; i < used; i++)
                {
                    if (!list[i].alive) continue;
                    list[i].onDraw();
                    list[i].move();
                    engine.update();
                    list[i].onDraw();
                    if(player.r<1)
                    {
                        timer1.Enabled = false;
                        MessageBox.Show("Game Over");
                        Application.Restart();
                        return;
                    }
                }
                AnT.Invalidate();
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Render();
        }
        public static int used = 0;
        private void AnT_MouseClick(object sender, MouseEventArgs e)
        {
            float mx = e.X;
            float my = AnT.Height - e.Y;
            float mVx = player.cx-mx;
            float mVy = player.cy - my;
            float d = (float)Math.Sqrt(mVx*mVx+mVy*mVy);
            player.Vx += mVx / d;
            player.Vy += mVy / d;
            player.r-=0.5f;
        }
        private void moveTimer_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < used; i++)
            {
                list[i].move();
                engine.update();
            }
        }
    }
}
