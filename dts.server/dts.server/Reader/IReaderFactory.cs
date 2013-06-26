using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using dts.server.BlockProcessing;
using dts.server.Commons;
using dts.server.PersonModel;

namespace dts.server.Reader
{
    public interface IReaderFactory
    {
        IReader Create(SourceType sourceType, string sourceName, BlockingCollection<Block> outputQueue);
    }

    [Export(typeof(IReaderFactory))]
    public class ReaderFactory : IReaderFactory
    {
        public IReader Create(SourceType sourceType, string sourceName, BlockingCollection<Block> outputQueue)
        {
            switch (sourceType)
            {
                case SourceType.File:
                    return new PersonReader(sourceName, outputQueue);
                case SourceType.Database:
                    break;
                default:
                    throw new ArgumentOutOfRangeException("sourceType");
            }

            return null;
        }
    }
}
