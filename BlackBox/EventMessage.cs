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
    using System.Runtime.CompilerServices;

    /// <summary>
    /// Data transfer object holding an event message
    /// </summary>
    public class EventMessage
    {
        /// <summary>
        /// Event severity level.
        /// </summary>
        public EventLevel Level { get; private set; }

        /// <summary>
        /// Time stamp of the event happend.
        /// </summary>
        public DateTime TimeStamp { get; private set; }

        /// <summary>
        /// Source e.g. namespace and method.
        /// </summary>
        public string Source { get; private set; }

        /// <summary>
        /// Message
        /// </summary>
        public string Message { get; private set; }

        /// <summary>
        /// Constructs event messages. Sets time stamp.
        /// </summary>
        public EventMessage(EventLevel severity, string message, [CallerMemberName]string source = "")
        {
            TimeStamp   = DateTime.Now;
            Level       = severity;
            Source      = source;
            Message     = message;
        }

        /// <summary>
        /// Formats message to a string.
        /// </summary>
        /// <returns>Formatted event message.</returns>
        public override string ToString()
        {
            return String.Concat(TimeStamp.ToString("yyyy-mm-dd hh:MM:ss"), " ", Level.ToString(), ": ", Message, " @ ", Source);
        }
    }
}
