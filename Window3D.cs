using System;
using System.Collections.Generic;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;


namespace Proiect
{
    class Window3D:GameWindow

    {
        KeyboardState previousKeybord;
        
        Randomizer random;
        private Triangle triunghi1;
        private Triangle2 triunghi2;
        private Camera cam;
        List<Color> colors;
        private const float Increase = 0.1f;

        //Cream constructorul si dam dimeniunea ferestrei cheman constructorul implicit

        public Window3D() : base(800, 600,new GraphicsMode(32,24,0,8))
        {
            VSync = VSyncMode.On;
            random = new Randomizer();
            cam = new Camera();
            triunghi1 = new Triangle(random);
            triunghi2 = new Triangle2(random);
            colors = new List<Color>();
            colors.Add(random.RandomColor());
            colors.Add(random.RandomColor()); 
            colors.Add(random.RandomColor());
       
            DisplayHelp();
        }

        //Aici ne vom ocupa de bafferul de adancime si de anti alazing
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            GL.Enable(EnableCap.DepthTest);
            GL.DepthFunc(DepthFunction.Less);//stabileste ordinea elementelor pe care le va afisa

            GL.Hint(HintTarget.PolygonSmoothHint,HintMode.Nicest);//activeaza anti alizing pentru forme

        }
        //specifica viwportul.Cum vad eu lumea 3D si cum vede camera lumea 3D
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            //set Background
            GL.ClearColor(Color.LightPink);

            //set viewport
            GL.Viewport(0, 0, this.Width, this.Height);

            //set perspectiv
            Matrix4 perspectiva = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4,(float)this.Width/(float)this.Height,1,250);//factorul de deschidere a maticei prima valoare,raportul de aspect
            GL.MatrixMode(MatrixMode.Projection);//incarca proiectia
            GL.LoadMatrix(ref perspectiva);// activeaza

            //set camera
            Matrix4 eye = Matrix4.LookAt(30,30,30,0,0,0,0,1,0);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref eye);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            //LOGIC Code
            KeyboardState currentkeyboard = Keyboard.GetState();
            MouseState currentmouse = Mouse.GetState();
            int x_click = currentmouse.X;
            int y_click = currentmouse.Y;

           

            if (currentkeyboard[Key.Escape]) 
            {
                Exit();
            }
            if ((x_click != X || y_click != Y) && currentmouse[MouseButton.Middle])
            {
                GL.Viewport(x_click, -y_click, Width, Height);
            }
            if ((x_click != X || y_click != Y) && currentmouse[MouseButton.Right])
            {
                cam.MoveRight();
            }
            if ((x_click != X || y_click != Y) && currentmouse[MouseButton.Left])
            {
                cam.MoveLeft();
            }
            if (currentkeyboard[Key.H] && !previousKeybord[Key.H])
            {
                DisplayHelp();
            }
            if (currentkeyboard[Key.R] && !previousKeybord[Key.R]) 
            {
                triunghi1.ChangeColor(0.0f,Increase,0.0f);


            }
            if (currentkeyboard[Key.B] && !previousKeybord[Key.B])
            {
                triunghi1.ChangeColor( Increase,0.0f, 0.0f);

            }
            if (currentkeyboard[Key.G] && !previousKeybord[Key.G])
            {
                triunghi1.ChangeColor(0.0f,  0.0f,Increase);

            }
         
            if (currentkeyboard[Key.V])
            {
                triunghi1.ToggleVisibility();
            }
            
            previousKeybord = currentkeyboard;
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.Clear(ClearBufferMask.DepthBufferBit);

            //renedr code
            triunghi1.Draw();
            triunghi2.DrawTriangleRGB(colors);
            //end render code
            SwapBuffers();
        }
       

        private void DisplayHelp()
        {
            Console.WriteLine("\n Meniu");
            Console.WriteLine(" H- meniul");
            Console.WriteLine(" ESC - parasire aplicatie");
            Console.WriteLine(" V - seteaza vizibilitatea triunghiului");
            Console.WriteLine("Clik dreapta,stanga,middle schimba pozitia camerei");
            Console.WriteLine(" (R,G,B) - schimba culorile triungiului pentru fiecare canal de culoare");
            Console.WriteLine("\n============================Valorile RGB===============================");
            Console.WriteLine(colors[0]);
            Console.WriteLine(colors[1]);
            Console.WriteLine(colors[2]);
            ;

        }
    }
}
