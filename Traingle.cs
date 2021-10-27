﻿using OpenTK;
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
        
        private bool visibility;
        private Color meshColor;
        private float maxColor = 1.0f;
        private float minColor = 0.0f;
        private float R = 0, G = 0, B = 0;
        private List<Vector3> coordsList; 


        public Triangle(Randomizer r)
        {
            visibility = true;
            meshColor = r.RandomColor();
        }
        public void ToggleVisibility()
        {
            visibility = !visibility;
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
        public void ChangeColor(float r, float g, float b)
        {
            if (R < maxColor)
                R += r;
            if (B < maxColor)
                B += b;
            if (R < maxColor)
                G += g;

        }
        public void Draw()
        {
            coordsList = LoadFromObjFile(FILENAME);
            if (visibility)
            {
                GL.Color3(minColor + R, minColor + G, minColor + B);
                GL.Begin(PrimitiveType.Triangles);
                foreach (var vert in coordsList)
                {
                    GL.Vertex3(vert);
                }
                GL.End();
            }
        }
        public void DrawTriangleRGB(List<Color> Colors)
        {
            int i = 0;
            coordsList = LoadFromObjFile(FILENAME);
            if (visibility)
            {
                GL.Begin(PrimitiveType.Triangles);
                foreach (var vert in coordsList)
                {
                    GL.Color3(Colors[i]);
                    GL.Vertex3(vert);
                    i++;
                }
                GL.End();
            }
        }
    }
}
