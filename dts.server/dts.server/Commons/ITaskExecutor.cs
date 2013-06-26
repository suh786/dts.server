using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dts.server.OutputWriter;
using dts.server.Reader;

namespace dts.server.Commons
{
    public interface ITaskExecutor
    {
        void Execute();
    }

    public class TaskExecutor : ITaskExecutor
    {
        private readonly IReader _reader;
        private readonly IOutputWriter _outputWriter;

        public TaskExecutor(IReader reader, IOutputWriter outputWriter)
        {
            _reader = reader;
            _outputWriter = outputWriter;
        }

        public void Execute()
        {
            Task.Factory.StartNew(_reader.Read);

            _outputWriter.Write();
        }
    }
}
