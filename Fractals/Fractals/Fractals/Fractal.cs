using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;

namespace Fractals
{
    /// <summary>
    /// Abstract class "Fractal".
    /// </summary>
    abstract class Fractal
    {
        protected const int maxDepth = 13;
        protected int depth;

        /// <summary>
        /// Fractal construction method.
        /// </summary>
        /// <param name="startPoint">The point from which the drawing of the fractal begins.</param>
        /// <param name="depth">Recursion depth.</param>
        /// <param name="param1">The first parameter for constructing a fractal.</param>
        /// <param name="param2">The second parameter for constructing a fractal.</param>
        /// <param name="param3">The third parameter for constructing a fractal.</param>
        /// <param name="size">Fractal size.</param>
        public abstract void Paint(Point startPoint, int depth, double param1, double param2, double param3, double size);
    }
}
