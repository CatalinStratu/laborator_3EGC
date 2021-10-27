using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace Proiect
{
    class Triangle
    {


        private const String FILENAME = "assets/Triunghi.txt";
        private List<Vector3> coordsList;
        private bool visibility;
        private float maxColor = 1.0f;
        private float minColor = 0.0f;
        private Color meshColor;
        private float R = 0, G = 0, B = 0;
      

        public Triangle(Color col)
        {
            visibility = true;
            meshColor = col;
        }

        public void ToggleVisibility()
        {

            visibility = !visibility;

        }
        public void Show()
        {
            visibility = true;
        }

        public void Hide()
        {
            visibility = false;
        }
 
 

        public void ChangeColor(float r, float g, float b)
        {
            if (R < maxColor)
                R +=r;
            if (B <maxColor)
                B += b;
            if (R < maxColor)
                G+= g;

        }
        private List<Vector3> LoadFromObjFile(string fname)
        {
            List<Vector3> vlc3 = new List<Vector3>();


            var lines = File.ReadLines(fname);
            foreach (var line in lines)
            {
                string[] block = line.Trim().Split(' ');
                float xval = float.Parse(block[0].Trim());
                float yval = float.Parse(block[1].Trim());
                float zval = float.Parse(block[2].Trim());
                vlc3.Add(new Vector3((int)xval, (int)yval, (int)zval));
            }

            return vlc3;
        }
        public void DrawColor()
        {
            coordsList = LoadFromObjFile(FILENAME);
            if ( visibility)
            {
                GL.Color3(minColor+R,minColor+G,minColor+B);
                GL.Begin(PrimitiveType.Triangles);
                foreach (var vert in coordsList)
                {
                    GL.Vertex3(vert);
                }
                GL.End();
            }
        }
    }
}
