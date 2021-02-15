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

namespace BlackBox.Writers
{
    using System.Collections.Concurrent;

    /// <summary>
    /// Event queue writer, all items are enqueued into a concurrent queue. And are dequeueable.
    /// </summary>
    public class EventQueueWriter : IEventWriter
    {
        /// <summary>
        /// Queue of written event messages.
        /// </summary>
        public ConcurrentQueue<IEventMessage> Messages { get; protected set; }

        /// <summary>
        /// Constructor of the EventQueueWriter.
        /// </summary>
        public EventQueueWriter()
        {
            Messages = new ConcurrentQueue<IEventMessage>();
        }

        /// <summary>
        /// Write an event message to the EventMessages queue.
        /// </summary>
        /// <param name="message">EventMessage</param>
        public virtual void Write(IEventMessage message)
        {
            Messages.Enqueue(message);
        }
    }
}
