using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dts.server.Commons;

namespace dts.server.Reader
{
    public interface IReader
    {
        /// <summary>
        /// Whole read operation should be done on a single thread.
        /// </summary>
        void Read();
    }
}
