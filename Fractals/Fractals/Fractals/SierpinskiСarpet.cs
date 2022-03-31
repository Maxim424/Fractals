using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Collections.Generic;

namespace Fractals
{
    /// <summary>
    /// "Sierpinski carpet" class.
    /// </summary>
    class SierpinskiСarpet : Fractal
    {
        private List<Polygon> elements = new List<Polygon>();

        /// <summary>
        /// Method for creating an element of the first iteration of drawing a fractal.
        /// </summary>
        /// <param name="startPoint">The point from which the drawing of the fractal begins.</param>
        /// <param name="size">Fractal size.</param>
        /// <returns>A polygon - an element of the first iteration of drawing a fractal.</returns>
        private Polygon CreateFirstElement(Point startPoint, double size)
        {
            Point p1 = startPoint;
            Point p2 = new Point(startPoint.X + size, startPoint.Y);
            Point p3 = new Point(startPoint.X + size, startPoint.Y + size);
            Point p4 = new Point(startPoint.X, startPoint.Y + size);
            Polygon newPolygon = new Polygon();
            newPolygon.Points.Add(p1);
            newPolygon.Points.Add(p2);
            newPolygon.Points.Add(p3);
            newPolygon.Points.Add(p4);
            newPolygon.Fill = new SolidColorBrush(Color.FromRgb(100, 100, 100));
            elements.Add(newPolygon);
            return newPolygon;
        }

        /// <summary>
        /// Method for creating a fractal.
        /// </summary>
        /// <param name="startPoint">The point from which the drawing of the fractal begins.</param>
        /// <param name="iteration">Current iteration.</param>
        /// <param name="polygon">The polygon with which operations will be carried out.</param>
        /// <param name="size">Fractal size.</param>
        private void CreateFractal(Point startPoint, int iteration, Polygon polygon, double size)
        {
            if (iteration > 0)
            {
                if (iteration == depth)
                {
                    Polygon newPolygon = CreateFirstElement(startPoint, size);
                    CreateFractal(startPoint, iteration - 1, newPolygon, size);
                }
                else
                {
                    Point p1 = new Point(polygon.Points[0].X + size / 3, polygon.Points[0].Y + size / 3);
                    Point p2 = new Point(p1.X + size / 3, p1.Y);
                    Point p3 = new Point(p1.X + size / 3, p1.Y + size / 3);
                    Point p4 = new Point(p1.X, p1.Y + size / 3);
                    Polygon polygon0 = new Polygon() { Points = { p1, p2, p3, p4 } };
                    polygon0.Fill = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                    elements.Add(polygon0);
                    Polygon polygon1 = new Polygon() { Points = { polygon.Points[0], new Point(p1.X, polygon.Points[0].Y), p1, new Point(polygon.Points[0].X, p1.Y) } };
                    Polygon polygon2 = new Polygon() { Points = { new Point(p1.X, p1.Y - size / 3), new Point(p2.X, p2.Y - size / 3), p2, p1 } };
                    Polygon polygon3 = new Polygon() { Points = { new Point(p2.X, p2.Y - size / 3), new Point(polygon.Points[2].X, p2.Y - size / 3), new Point(polygon.Points[2].X, p2.Y), p2 } };
                    Polygon polygon4 = new Polygon() { Points = { p2, new Point(polygon.Points[2].X, p2.Y), new Point(polygon.Points[2].X, p3.Y), p3 } };
                    Polygon polygon5 = new Polygon() { Points = { p3, new Point(polygon.Points[2].X, p3.Y), polygon.Points[2], new Point(p3.X, polygon.Points[2].Y) } };
                    Polygon polygon6 = new Polygon() { Points = { p4, p3, new Point(p3.X, polygon.Points[2].Y), new Point(p4.X, polygon.Points[2].Y) } };
                    Polygon polygon7 = new Polygon() { Points = { new Point(polygon.Points[3].X, p4.Y), p4, new Point(p4.X, polygon.Points[3].Y), polygon.Points[3] } };
                    Polygon polygon8 = new Polygon() { Points = { new Point(polygon.Points[0].X, p1.Y), p1, p4, new Point(polygon.Points[0].X, p4.Y) } };
                    CreateFractal(polygon1.Points[0], iteration - 1, polygon1, size / 3);
                    CreateFractal(polygon2.Points[0], iteration - 1, polygon2, size / 3);
                    CreateFractal(polygon3.Points[0], iteration - 1, polygon3, size / 3);
                    CreateFractal(polygon4.Points[0], iteration - 1, polygon4, size / 3);
                    CreateFractal(polygon5.Points[0], iteration - 1, polygon5, size / 3);
                    CreateFractal(polygon6.Points[0], iteration - 1, polygon6, size / 3);
                    CreateFractal(polygon7.Points[0], iteration - 1, polygon7, size / 3);
                    CreateFractal(polygon8.Points[0], iteration - 1, polygon8, size / 3);
                }
            }
        }

        /// <summary>
        /// Fractal drawing method.
        /// </summary>
        /// <param name="startPoint">The point from which the drawing of the fractal begins.</param>
        /// <param name="depth">Recursion depth.</param>
        /// <param name="param1">The first parameter for constructing a fractal.</param>
        /// <param name="param2">The second parameter for constructing a fractal.</param>
        /// <param name="param3">The third parameter for constructing a fractal.</param>
        /// <param name="size">Fractal size.</param>
        public override void Paint(Point startPoint, int depth, double param1, double param2, double param3, double size)
        {
            try
            {
                MainWindow.Canvas.Children.Clear();
                elements.Clear();
                this.depth = depth;
                if (this.depth <= maxDepth)
                {
                    CreateFractal(startPoint, depth, new Polygon(), size);
                    foreach (Polygon item in elements)
                    {
                        MainWindow.Canvas.Children.Add(item);
                    }
                }
            }
            catch { }
        }
    }
}
