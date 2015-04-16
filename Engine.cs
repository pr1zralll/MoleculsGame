using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
// для работы с библиотекой OpenGL 
using Tao.OpenGl;
// для работы с библиотекой FreeGLUT 
using Tao.FreeGlut;
// для работы с элементом управления SimpleOpenGLControl 
using Tao.Platform.Windows;

namespace Moleculs
{
    class Engine
    {
        private Molecule[] arr;
        private SimpleOpenGlControl AnT;
        private Player player;
        Timer t;
        public Engine(Molecule[] a,SimpleOpenGlControl ant,Player p,Timer te)
        {
            this.arr = a;
            this.AnT = ant;
            this.player = p;
            t = te;
        }
        private void draw_lines(int i, int j)
        {
            Gl.glLineWidth(0);
            Gl.glColor3f(1.0f, 0, 0);
            Gl.glBegin(Gl.GL_LINES);
            Gl.glVertex2f(arr[i].cx, arr[i].cy);
            Gl.glVertex2f(arr[j].cx, arr[j].cy);
            Gl.glEnd();
        }

        public void update()
        {
            if (player.r + player.r > AnT.Height)
            {
                t.Enabled = false;
                MessageBox.Show("YOU WIN!!!!!!!!!!");
                Application.Exit();
            }
            for (int i = 0; i < Form1.used ; i++)
            {
                if (!arr[i].alive) continue;

                if (arr[i].cx + arr[i].r > AnT.Width)
                {
                    if (arr[i].Vx > 0) arr[i].Vx = -arr[i].Vx;
                }
                else if(arr[i].cx-arr[i].r < 0)
                {
                    if (arr[i].Vx < 0) arr[i].Vx = -arr[i].Vx;
                }
                else if (arr[i].cy - arr[i].r < 0)
                {
                    if (arr[i].Vy < 0) arr[i].Vy = -arr[i].Vy;
                }
                else if (arr[i].cy + arr[i].r > AnT.Height)
                {
                    if (arr[i].Vy > 0) arr[i].Vy = -arr[i].Vy;
                }
                int c = 0;
                //test for colision
                for (int j = 0; j < Form1.used ; j++)
                {
                    
                    if (i == j || !arr[j].alive) continue;
                    c++;
                    //draw_lines(i, j);
                    if ((arr[i].cx - arr[j].cx) * (arr[i].cx - arr[j].cx) + (arr[i].cy - arr[j].cy) * (arr[i].cy - arr[j].cy) <= (arr[i].r + arr[j].r) * (arr[i].r + arr[j].r))
                    {
                        
                        if(arr[i].r > arr[j].r)
                        {
                            arr[i].r +=0.5f;
                            arr[j].r--;
                            if (arr[j].r < 1)
                            {
                                arr[j].alive = false;

                            }
                        }
                        else
                        {
                            arr[i].r--;
                            arr[j].r +=0.5f;
                            if (arr[i].r < 1)
                            {
                                arr[i].alive = false;

                            }
                        }
                        /*float newVelX1 = (arr[i].Vx * (arr[i].r - arr[j].r) + (2 * arr[j].r * arr[j].Vx)) / (arr[i].r + arr[j].r);
                        float newVelY1 = (arr[i].Vy * (arr[i].r - arr[j].r) + (2 * arr[j].r * arr[j].Vy)) / (arr[i].r + arr[j].r);
                        float newVelX2 = (arr[j].Vx * (arr[j].r - arr[i].r) + (2 * arr[i].r * arr[i].Vx)) / (arr[i].r + arr[j].r);
                        float newVelY2 = (arr[j].Vy * (arr[j].r - arr[i].r) + (2 * arr[i].r * arr[i].Vy)) / (arr[i].r + arr[j].r);
                        arr[i].Vx = newVelX1;
                        arr[i].Vy = newVelY1;
                        arr[j].Vx = newVelX2;
                        arr[j].Vy = newVelY2;
                        */
                        break;
                    }
                }
                if (c == 0)
                {
                    t.Enabled = false;
                    MessageBox.Show("YOU WIN!!!!!!!!!!");
                    Application.Exit();
                }
            }
        }
    }
}
