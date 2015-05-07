using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelProcessingOfData.Interfaces
{
    public interface IMatrix
    {
        /// <summary>
        /// Gets a value indicating whether this instance is invertible.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is invertible; otherwise, <c>false</c>.
        /// </value>
        bool IsInvertible { get; }

        /// <summary>
        /// Rotates the specified degrees.
        /// </summary>
        /// <param name="degrees">The degrees.</param>
        void Rotate(float degrees);

        /// <summary>
        /// Inverts this instance.
        /// </summary>
        void Invert();
    }
}
