/*
 * Copyright (c) 2020 Lambert van Lieshout, YUMMO Software Development
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to
 * deal in the Software without restriction, including without limitation the
 * rights to use, copy, modify, merge, publish, distribute, sublicense, and/or
 * sell copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
 * FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS
 * IN THE SOFTWARE.
*/

namespace BlackBox
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    /// <summary>
    /// Black Box Manager. Handles log writing on same thread.
    /// </summary>
    public class BlackBoxManager : IBlackBoxManager
    {
        /// <summary>
        /// Collection of event writers. These will all be used when for event writing.
        /// </summary>
        protected readonly List<IEventWriterHolder> _writers;

        /// <summary>
        /// Fallback event writer. This will be used when a primary event writer throws an exception.
        /// </summary>
        protected IEventWriterHolder _writerFallBack;

        /// <summary>
        /// Constructor of the event logger.
        /// </summary>
        public BlackBoxManager()
        {
            _writers = new List<IEventWriterHolder>();
        }

        /// <summary>
        /// Register a event writer which will be triggered at given level.
        /// </summary>
        /// <param name="minimumLevel">Event level to trigger the writer.</param>
        /// <param name="writer">Event writer delegate.</param>
        public virtual void RegisterWriter(EventLevel minimumLevel, EventWriter writer)
        {
            if (writer == null) throw new ArgumentNullException(nameof(writer), "Event writer cannot be NULL");
            _writers.Add(new EventWriterHolder(minimumLevel, writer));
        }

        /// <summary>
        /// Unregister a event writer.
        /// </summary>
        /// <param name="writer">Event writer to remove.</param>
        public virtual void UnregisterWriter(EventWriter writer)
        {
            if (writer == null) throw new ArgumentNullException(nameof(writer), "Event writer cannot be NULL");
            _writers.RemoveAll(t => t.Write == writer);
        }

        /// <summary>
        /// Clear all event writers.
        /// </summary>
        public virtual void ClearWriters()
        {
            _writers.Clear();
        }

        /// <summary>
        /// Register a fallback event writer which will be triggered when other event writers throw an exception on the Write method. Last registered writer will be active.
        /// </summary>
        /// <param name="writer">Event writer delegate.</param>
        public virtual void RegisterFallbackWriter(EventWriter writer)
        {
            if (writer == null) throw new ArgumentNullException(nameof(writer), "Event writer cannot be NULL");
            _writerFallBack = new EventWriterHolder(EventLevel.Debug, writer);
        }

        /// <summary>
        /// Write event message. Use fallback event writer when primairy writer throws an exception. This write method blocks until the writer is finished.
        /// </summary>
        /// <param name="message">Event message to write</param>
        public virtual void Write(IEventMessage message)
        {
            if (message == null) return;
            if (message.Level == EventLevel.Debug && !Debugger.IsAttached) return;
            for (int i = 0; i < _writers.Count; i++)
            {
                if (_writers[i].Level < message.Level) continue;
                try
                {
                    _writers[i].Write(message);
                }
                catch (Exception exception)
                {
                    if (_writerFallBack != null)
                    {
                        try
                        {
                            _writerFallBack.Write(message);
                            _writerFallBack.Write(exception.ToMessage());
                        }
                        catch { }
                    }
                }
            }
        }
    }
}
