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
    /// <summary>
    /// Event write delegate.
    /// </summary>
    /// <param name="message"></param>
    public delegate void EventWriter(EventMessage message);

    /// <summary>
    /// Holder class for event writers and level when to trigger the writer.
    /// </summary>
    public class EventWriterHolder
    {
        /// <summary>
        /// Constructor of EventWriteHolder.
        /// </summary>
        /// <param name="level">Level when to trigger the event writer.</param>
        /// <param name="write">Event writer method.</param>
        public EventWriterHolder(EventLevel level, EventWriter write)
        {
            Level = level;
            Write = write;
        }

        /// <summary>
        /// Gets level when to trigger the event writer.
        /// </summary>
        public EventLevel Level { get; private set; }

        /// <summary>
        /// Gets event writer method.
        /// </summary>
        public EventWriter Write { get; private set; }
    }
}
