﻿using System;
using System.Collections.Concurrent;
using dts.server.BlockProcessing;
using dts.server.Commons;

namespace dts.server.OutputWriter
{
    public interface IOutputWriterFactory
    {
        IOutputWriter Create(TargetType targetType, BlockingCollection<Block> outputQueue, ITarget target);
    }

    public class OutputWriterFactory : IOutputWriterFactory
    {
        #region IOutputWriterFactory Members

        public IOutputWriter Create(TargetType targetType, BlockingCollection<Block> outputQueue, ITarget target)
        {
            switch (targetType)
            {
                case TargetType.Callback:
                    return new CallbackWriter(outputQueue, target);
                default:
                    throw new ArgumentOutOfRangeException("targetType");
            }
        }

        #endregion
    }
}
