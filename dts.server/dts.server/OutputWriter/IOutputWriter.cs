using System;
using dts.server.Commons;

namespace dts.server.OutputWriter
{
    public interface IOutputWriter : IDisposable
    {
        /// <summary>
        /// Execute Write in a seperate thread
        /// </summary>
        void Write();
    }
}
