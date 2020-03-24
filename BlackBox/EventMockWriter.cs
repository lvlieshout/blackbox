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
    using System.Collections.Concurrent;

    /// <summary>
    /// Event writer for testing purposes. Add event message to EventMessages list.
    /// </summary>
    public class EventMockWriter
    {
        /// <summary>
        /// List of written event messages.
        /// </summary>
        public ConcurrentQueue<EventMessage> Messages { get; protected set; }

        /// <summary>
        /// Constructor of the EventMockWriter.
        /// </summary>
        public EventMockWriter()
        {
            Messages = new ConcurrentQueue<EventMessage>();
        }

        /// <summary>
        /// Write an event message to the EventMessages list.
        /// </summary>
        /// <param name="message">EventMessage</param>
        public void Write(EventMessage message)
        {
            Messages.Enqueue(message);
        }
    }
}
