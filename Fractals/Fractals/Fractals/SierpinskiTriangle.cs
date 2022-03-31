using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Collections.Generic;

namespace Fractals
{
    /// <summary>
    /// "Sierpinski Triangle" class.
    /// </summary>
    class SierpinskiTriangle : Fractal
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
            Point p3 = new Point(startPoint.X + size / 2, startPoint.Y - size * Math.Sqrt(3) / 2.0);
            Polygon newPolygon = new Polygon();
            newPolygon.Points.Add(p1);
            newPolygon.Points.Add(p2);
            newPolygon.Points.Add(p3);
            newPolygon.Stroke = new SolidColorBrush(Color.FromRgb(100, 100, 100));
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
                    Point p1 = new Point(polygon.Points[0].X + Math.Cos(Math.PI / 3) * size / 2, polygon.Points[0].Y - Math.Sin(Math.PI / 3) * size / 2);
                    Point p2 = new Point(polygon.Points[1].X - Math.Cos(Math.PI / 3) * size / 2, polygon.Points[1].Y - Math.Sin(Math.PI / 3) * size / 2);
                    Point p3 = new Point(polygon.Points[0].X + size / 2, polygon.Points[0].Y);
                    Polygon polygon1 = new Polygon();
                    polygon1.Points.Add(polygon.Points[0]);
                    polygon1.Points.Add(p3);
                    polygon1.Points.Add(p1);
                    polygon1.Stroke = new SolidColorBrush(Color.FromRgb(100, 100, 100));
                    Polygon polygon2 = new Polygon();
                    polygon2.Points.Add(p3);
                    polygon2.Points.Add(polygon.Points[1]);
                    polygon2.Points.Add(p2);
                    polygon2.Stroke = new SolidColorBrush(Color.FromRgb(100, 100, 100));
                    Polygon polygon3 = new Polygon();
                    polygon3.Points.Add(p1);
                    polygon3.Points.Add(p2);
                    polygon3.Points.Add(polygon.Points[2]);
                    polygon3.Stroke = new SolidColorBrush(Color.FromRgb(100, 100, 100));
                    elements.Add(polygon1);
                    elements.Add(polygon2);
                    elements.Add(polygon3);
                    CreateFractal(polygon1.Points[0], iteration - 1, polygon1, size / 2);
                    CreateFractal(polygon2.Points[0], iteration - 1, polygon2, size / 2);
                    CreateFractal(polygon3.Points[0], iteration - 1, polygon3, size / 2);
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
