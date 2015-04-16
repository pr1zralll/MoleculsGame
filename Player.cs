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

namespace Moleculs
{
    public class Player : Molecule
    {
        public Player()
        {
            alive = true;
            r = 15;
        }
        public override void onDraw()
        {
            Gl.glColor3f(1, 0f, 0);
            Gl.glBegin(Gl.GL_POLYGON);
            for (int i = 0; i < 360; i++)
            {
                float theta = k * i;//get the current angle 

                float x = r * (float)Math.Cos(theta);//calculate the x component 
                float y = r * (float)Math.Sin(theta);//calculate the y component 

                Gl.glVertex2f(x + cx, y + cy);//output vertex 
            }
            Gl.glEnd();
            Gl.glColor3f(0, 0, 0);
            for (int u = 0; u < r-2; u++)
            {
                Gl.glBegin(Gl.GL_LINE_LOOP);
                for (int i = 0; i < 360; i++)
                {
                    float theta = k * i;//get the current angle 

                    float x = u * (float)Math.Cos(theta);//calculate the x component 
                    float y = u * (float)Math.Sin(theta);//calculate the y component 

                    Gl.glVertex2f(x + cx, y + cy);//output vertex 
                }
                Gl.glEnd();
            }
        }
    }
}
