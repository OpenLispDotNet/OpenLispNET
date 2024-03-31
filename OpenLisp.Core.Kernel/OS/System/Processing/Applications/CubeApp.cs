﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenLisp.Core.Kernel.OS.System.Graphics.UI.GUI;

namespace OpenLisp.Core.Kernel.OS.System.Processing.Applications
{
    public class CubeApp : Application
    {
        public static string ApplicationName = "Cube";

        private Vertex[] _vertices;
        private int[][] _faces;
        private int _angle;

        public CubeApp(int width, int height, int x = 0, int y = 0) : base(ApplicationName, width, height, x, y)
        {
            ForceDirty = true;

            _vertices = new Vertex[]
            {
                new Vertex(-1, 1, -1),
                new Vertex(1, 1, -1),
                new Vertex(1, -1, -1),
                new Vertex  (-1, -1, -1),
                new Vertex(-1, 1, 1),
                new Vertex(1, 1, 1),
                new Vertex(1, -1, 1),
                new Vertex(-1, -1, 1)
            };

            _faces = new int[][]
            {
                new int[] {0, 1, 2, 3},
                new int[] {1, 5, 6, 2},
                new int[] {5, 4, 7, 6},
                new int[] {4, 0, 3, 7},
                new int[] {0, 4, 5, 1},
                new int[] {3, 2, 6, 7}
            };
            figure = new Figure(_vertices, _faces);
        }

        int viewWidth = 100;
        int viewHeight = 100;
        Color pen = Color.DeepSkyBlue;
        Figure figure;

        public override void Draw()
        {
            base.Draw();

            // Draw x-axis
            DrawLine(Color.White, 0 + 30 + 0, 0 + 30 + viewHeight / 2, 0 + 30 + viewWidth, 0 + 30 + viewHeight / 2);

            // Draw y-axis
            DrawLine(Color.White, 0 + 30 + viewWidth / 2, 0 + 30 + 0, 0 + 30 + viewWidth / 2, 0 + 30 + viewHeight);

            var projected = new Vertex[figure.Vertices.Length];
            for (var i = 0; i < figure.Vertices.Length; i++)
            {
                var vertex = figure.Vertices[i];

                var transformed = vertex.RotateX(_angle).RotateY(_angle).RotateZ(_angle);
                projected[i] = transformed.Project(viewWidth, viewHeight, 256, 6);
            }

            for (var j = 0; j < 6; j++) //This loop draws each of the six faces of the cube
            {
                DrawLine(pen,
                    0 + 30 + (int)projected[figure.Faces[j][0]].X,
                    0 + 30 + (int)projected[figure.Faces[j][0]].Y,
                    0 + 30 + (int)projected[figure.Faces[j][1]].X,
                    0 + 30 + (int)projected[figure.Faces[j][1]].Y);

                DrawLine(pen,
                    0 + 30 + (int)projected[figure.Faces[j][1]].X,
                    0 + 30 + (int)projected[figure.Faces[j][1]].Y,
                    0 + 30 + (int)projected[figure.Faces[j][2]].X,
                    0 + 30 + (int)projected[figure.Faces[j][2]].Y);

                DrawLine(pen,
                    0 + 30 + (int)projected[figure.Faces[j][2]].X,
                    0 + 30 + (int)projected[figure.Faces[j][2]].Y,
                    0 + 30 + (int)projected[figure.Faces[j][3]].X,
                    0 + 30 + (int)projected[figure.Faces[j][3]].Y);

                DrawLine(pen,
                    0 + 30 + (int)projected[figure.Faces[j][3]].X,
                    0 + 30 + (int)projected[figure.Faces[j][3]].Y,
                    0 + 30 + (int)projected[figure.Faces[j][0]].X,
                    0 + 30 + (int)projected[figure.Faces[j][0]].Y);
            }
            _angle++;
        }
    }

    public class Figure
    {
        public Figure(Vertex[] vertices, int[][] faces)
        {
            Vertices = vertices;
            Faces = faces;
        }

        public Vertex[] Vertices { get; set; }

        public int[][] Faces { get; set; }
    }

    public class Vertex
    {
        public Vertex(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public float X { get; set; }

        public float Y { get; set; }

        public float Z { get; set; }

        public Vertex RotateX(int angle)
        {
            float rad = (float)(angle * Math.PI / 180);
            float cos = (float)Math.Cos(rad);
            float sin = (float)Math.Sin(rad);
            float yn = Y * cos - Z * sin;
            float zn = Y * sin + Z * cos;
            return new Vertex(X, yn, zn);
        }

        public Vertex RotateY(int angle)
        {
            float rad = (float)(angle * Math.PI / 180);
            float cos = (float)Math.Cos(rad);
            float sin = (float)Math.Sin(rad);
            float Zn = Z * cos - X * sin;
            float Xn = Z * sin + X * cos;
            return new Vertex(Xn, Y, Zn);
        }

        public Vertex RotateZ(int angle)
        {
            float rad = (float)(angle * Math.PI / 180);
            float cos = (float)Math.Cos(rad);
            float sin = (float)Math.Sin(rad);
            float Xn = X * cos - Y * sin;
            float Yn = X * sin + Y * cos;
            return new Vertex(Xn, Yn, Z);
        }

        public Vertex Project(int viewWidth, int viewHeight, int fov, int viewDistance)
        {
            float factor = fov / (viewDistance + Z);
            float Xn = X * factor + viewWidth / 2;
            float Yn = Y * factor + viewHeight / 2;
            return new Vertex(Xn, Yn, 0);
        }
    }
}
