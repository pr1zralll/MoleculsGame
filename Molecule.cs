using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
// для работы с библиотекой OpenGL 
using Tao.OpenGl;
// для работы с библиотекой FreeGLUT 
using Tao.FreeGlut;
// для работы с элементом управления SimpleOpenGLControl 
using Tao.Platform.Windows;
using System.Threading;

namespace Moleculs
{
    public class Molecule
    {
        public bool alive;
        public float r;
        public float cx;
        public float cy;
        public float Vx;
        public float Vy;
        public int color=1;
        public Molecule()
        {
            alive = false;
        }
        public void generate()
        {
            Random rand = new Random();
            Vx = (float)rand.NextDouble()-0.5f;
            Vy = (float)rand.NextDouble()-0.5f;
            r = rand.Next(30);
            Thread.Sleep(8);
            alive = true;
            Color(1);
        }
        public static float k = 12.0f * 3.14f / 360.0f;
        public void move()
        {
            cx += Vx;
            cy += Vy;
        }
        public void Color(int a)
        {
            switch(a)
            {
                case 1: Gl.glColor3f(0.0f, 0.0f, 1.0f); break;
                case 2: Gl.glColor3f(1.0f, 0.0f, 0.0f); break;
                default: Gl.glColor3f(0.0f, 1.0f, 0.0f); break;
            }
        }
        public virtual void onDraw()
        {
            if(Form1.player.r>r)
                
                Gl.glColor4f(1.0f, 1.0f, 0.5f,0.5f);
            else
                Gl.glColor3f(0.0f, 0.0f, 1.0f);
	        Gl.glBegin(Gl.GL_POLYGON); 
	        for(int i = 0; i < 360; i++) 
	        { 
		        float theta = k * i;//get the current angle 

		        float x = r * (float)Math.Cos(theta);//calculate the x component 
		        float y = r * (float)Math.Sin(theta);//calculate the y component 

		        Gl.glVertex2f(x + cx, y + cy);//output vertex 
	        } 
	        Gl.glEnd();
          
        }
    }
}
