using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Collections.Generic;

namespace Fractals
{
    /// <summary>
    /// "Cantor set" class.
    /// </summary>
    class CantorSet : Fractal
    {
        private List<Line> elements = new List<Line>();
        private double size;
        private double gap;

        /// <summary>
        /// Method for creating an element of the first iteration of drawing a fractal.
        /// </summary>
        /// <param name="startPoint">The point from which the drawing of the fractal begins.</param>
        /// <param name="size">Fractal size.</param>
        /// <returns>A line - an element of the first iteration of drawing a fractal.</returns>
        private Line CreateFirstElement(Point startPoint, double size)
        {
            Point nextPoint = new Point(startPoint.X + size, startPoint.Y);
            Line newLine = new Line() { X1 = startPoint.X, Y1 = startPoint.Y, X2 = nextPoint.X, Y2 = nextPoint.Y };
            newLine.Stroke = new SolidColorBrush(Color.FromRgb(100, 100, 100));
            newLine.StrokeThickness = 5;
            elements.Add(newLine);
            return newLine;
        }

        /// <summary>
        /// Method for creating a fractal.
        /// </summary>
        /// <param name="startPoint">The point from which the drawing of the fractal begins.</param>
        /// <param name="iteration">Current iteration.</param>
        /// <param name="line">The line with which operations will be carried out.</param>
        private void CreateFractal(Point startPoint, int iteration, Line line)
        {
            if (iteration > 0)
            {
                if (iteration == depth)
                {
                    Line newLine = CreateFirstElement(startPoint, size);
                    CreateFractal(new Point(startPoint.X, startPoint.Y + gap), iteration - 1, newLine);
                }
                else
                {
                    Line leftLine = new Line() { X1 = startPoint.X, Y1 = startPoint.Y, X2 = startPoint.X + (line.X2 - line.X1) / 3, Y2 = startPoint.Y };
                    leftLine.StrokeThickness = 5;
                    leftLine.Stroke = new SolidColorBrush(Color.FromRgb(100, 100, 100));
                    Line rightLine = new Line() { X1 = startPoint.X + 2 * (line.X2 - line.X1) / 3, Y1 = startPoint.Y, X2 = line.X2, Y2 = startPoint.Y };
                    rightLine.StrokeThickness = 5;
                    rightLine.Stroke = new SolidColorBrush(Color.FromRgb(100, 100, 100));
                    elements.Add(leftLine);
                    elements.Add(rightLine);
                    CreateFractal(new Point(leftLine.X1, leftLine.Y2 + gap), iteration - 1, leftLine);
                    CreateFractal(new Point(rightLine.X1, rightLine.Y2 + gap), iteration - 1, rightLine);
                }
            }
        }

        /// <summary>
        /// Fractal drawing method.
        /// </summary>
        /// <param name="startPoint">The point from which the drawing of the fractal begins.</param>
        /// <param name="depth">Recursion depth.</param>
        /// <param name="gap">Distance between lines.</param>
        /// <param name="param1">The first parameter for constructing a fractal.</param>
        /// <param name="param2">The second parameter for constructing a fractal.</param>
        /// <param name="size">Fractal size.</param>
        public override void Paint(Point startPoint, int depth, double gap, double param1, double param2, double size)
        {
            try
            {
                MainWindow.Canvas.Children.Clear();
                elements.Clear();
                this.size = size;
                this.gap = gap;
                this.depth = depth;
                if (this.depth <= maxDepth)
                {
                    CreateFractal(startPoint, depth, new Line());
                    foreach (Line item in elements)
                    {
                        MainWindow.Canvas.Children.Add(item);
                    }
                }
            }
            catch { }
        }
    }
}
