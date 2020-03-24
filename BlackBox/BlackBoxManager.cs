/*
 * Copyright (c) 2012 Lambert van Lieshout, YUMMO Software Development
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
    public class BlackBoxManager
    {
        /// <summary>
        /// Collection of event writers. These will all be used when for event writing.
        /// </summary>
        protected List<EventWriterHolder>   _writers;
        
        /// <summary>
        /// Fallback event writer. This will be used when a primary event writer throws an exception.
        /// </summary>
        protected EventWriterHolder         _writerFallBack;

        /// <summary>
        /// Constructor of the event logger.
        /// </summary>
        public BlackBoxManager()
        {
            _writers = new List<EventWriterHolder>();
        }

        /// <summary>
        /// Register a event writer which will be triggered at given level.
        /// </summary>
        /// <param name="level">Event level to trigger the writer.</param>
        /// <param name="write">Event writer delegate.</param>
        public void RegisterWriter(EventLevel level, EventWriter write)
        {
            if (write == null) throw new ArgumentNullException("writer", "Event writer cannot be NULL");
            _writers.Add(new EventWriterHolder(level, write));
        }

        /// <summary>
        /// Register a fallback event writer which will be triggered when other event writers throw an exception on the Write method.
        /// </summary>
        /// <param name="write">Event writer delegate.</param>
        public void RegisterFallbackWriter(EventWriter write)
        {
            if (write == null) throw new ArgumentNullException("writer", "Event writer cannot be NULL");
            _writerFallBack = new EventWriterHolder(EventLevel.All, write);
        }

        /// <summary>
        /// Write event message. Use fallback event writer when primairy writer throws an exception. This write method blocks until the writer is finished.
        /// </summary>
        /// <param name="message">Event message to write</param>
        public virtual void Write(EventMessage message)
        {
            if (message == null) return;
            for (int i = 0; i < _writers.Count; i++)
            {
                if (!_writers[i].Level.HasFlag(message.Severity)) continue;
                if (message.Severity.HasFlag(EventLevel.Debug) && !Debugger.IsAttached) continue;
                try
                {
                    _writers[i].Write(message);
                }
                catch (Exception exception)
                {
                    if (_writerFallBack != null)
                    {
                        _writerFallBack.Write(message);
                        _writerFallBack.Write(exception.ToMessage());
                    }
                }
            }
        }

        /// <summary>
        /// Write custom event
        /// </summary>
        /// <param name="severity">Event level</param>
        /// <param name="message">Message</param>
        /// <param name="service">Service</param>
        //public void Write(EventLevel severity, string message, [CallerMemberName]string service = "")
        //{
        //    if (String.IsNullOrEmpty(service))
        //    {
        //        MethodBase m = new StackTrace().GetFrame(1).GetMethod();
        //        service = String.Concat(m.ReflectedType.FullName, ".", m.Name);
        //    }
        //    Write(new EventMessage(severity, service, message));
        //}
    }
}
