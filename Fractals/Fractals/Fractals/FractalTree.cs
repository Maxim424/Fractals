using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Collections.Generic;


namespace Fractals
{
    /// <summary>
    /// "Fractal tree" class.
    /// </summary>
    class FractalTree : Fractal
    {
        private List<Line> elements = new List<Line>();
        private double coef;
        private double leftAngle;
        private double rightAngle;

        /// <summary>
        /// Method for creating an element of the first iteration of drawing a fractal.
        /// </summary>
        /// <param name="startPoint">The point from which the drawing of the fractal begins.</param>
        /// <param name="size">Fractal size.</param>
        /// <returns>A line - an element of the first iteration of drawing a fractal.</returns>
        private Line CreateFirstElement(Point startPoint, double size)
        {
            Point nextPoint = new Point(startPoint.X, (int)(startPoint.Y - size));
            Line line = new Line();
            line.X1 = startPoint.X;
            line.Y1 = startPoint.Y;
            line.X2 = nextPoint.X;
            line.Y2 = nextPoint.Y;
            line.Stroke = new SolidColorBrush(Color.FromRgb(100, 100, 100));
            elements.Add(line);
            return line;
        }

        /// <summary>
        /// Method for creating a fractal.
        /// </summary>
        /// <param name="startPoint">The point from which the drawing of the fractal begins.</param>
        /// <param name="iteration">Current iteration.</param>
        /// <param name="currentLeftAngle">The current angle of the left branch.</param>
        /// <param name="currentRightAngle">The current angle of the right branch.</param>
        /// <param name="size">Fractal size.</param>
        private void CreateFractal(Point startPoint, int iteration, double currentLeftAngle, double currentRightAngle, double size)
        {
            if (iteration > 0)
            {
                if (iteration == depth)
                {
                    Line line = CreateFirstElement(startPoint, size);
                    CreateFractal(new Point(line.X2, line.Y2), iteration - 1, leftAngle, rightAngle, size * coef);
                }
                else
                {
                    Line leftLine = new Line() { X1 = startPoint.X, Y1 = startPoint.Y, X2 = startPoint.X - Math.Sin(currentLeftAngle) * size, Y2 = startPoint.Y - Math.Cos(currentLeftAngle) * size };
                    Line rightLine = new Line() { X1 = startPoint.X, Y1 = startPoint.Y, X2 = startPoint.X + Math.Sin(currentRightAngle) * size, Y2 = startPoint.Y - Math.Cos(currentRightAngle) * size };
                    leftLine.Stroke = new SolidColorBrush(Color.FromRgb(100, 100, 100));
                    rightLine.Stroke = new SolidColorBrush(Color.FromRgb(100, 100, 100));
                    elements.Add(leftLine);
                    elements.Add(rightLine);
                    CreateFractal(new Point(leftLine.X2, leftLine.Y2), iteration - 1, currentLeftAngle + leftAngle, -currentLeftAngle + rightAngle, size * coef);
                    CreateFractal(new Point(rightLine.X2, rightLine.Y2), iteration - 1, -currentRightAngle + leftAngle, currentRightAngle + rightAngle, size * coef);
                }
            }
        }

        /// <summary>
        /// Fractal drawing method.
        /// </summary>
        /// <param name="startPoint">The point from which the drawing of the fractal begins.</param>
        /// <param name="depth">Recursion depth.</param>
        /// <param name="leftAngle">The angle of the left branch.</param>
        /// <param name="rightAngle">The angle of the right branch.</param>
        /// <param name="coef">Segment ratio coefficient.</param>
        /// <param name="size">Fractal size.</param>
        public override void Paint(Point startPoint, int depth, double leftAngle, double rightAngle, double coef, double size)
        {
            try
            {
                MainWindow.Canvas.Children.Clear();
                elements.Clear();
                this.leftAngle = leftAngle;
                this.rightAngle = rightAngle;
                this.coef = coef;
                this.depth = depth;
                if (this.depth <= maxDepth)
                {
                    CreateFractal(startPoint, depth, leftAngle, rightAngle, size);
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
