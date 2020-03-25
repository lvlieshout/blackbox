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
        /// Content
        /// </summary>
        public string Content { get; private set; }

        /// <summary>
        /// Construct event message with current timestamp.
        /// </summary>
        /// <param name="level">Severity level</param>
        /// <param name="content">Content</param>
        /// <param name="source">Source</param>
        public EventMessage(EventLevel level, string content, string source)
        {
            TimeStamp   = DateTime.Now;
            Level       = level;
            Source      = source;
            Content     = content;
        }

        /// <summary>
        /// Construct event message with current timestamp and reference source file.
        /// </summary>
        /// <param name="level">Severity level</param>
        /// <param name="content">Content</param>
        /// <param name="memberName">Calling method name</param>
        /// <param name="sourceFilePath">Calling source file path</param>
        /// <param name="sourceLineNumber">Calling source file line number</param>
        public static EventMessage Createx(EventLevel level, string content, [CallerMemberName]string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
        {
            return new EventMessage(level, content, String.Concat(sourceFilePath, ":", sourceLineNumber, ":", memberName));
        }

        /// <summary>
        /// Construct event message with current timestamp and reference source file.
        /// </summary>
        /// <param name="level">Severity level</param>
        /// <param name="content">Content</param>
        /// <param name="memberName">Calling method name</param>
        /// <param name="sourceFilePath">Calling source file path</param>
        /// <param name="sourceLineNumber">Calling source file line number</param>
        public EventMessage(EventLevel level, string content, [CallerMemberName]string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
        {
            TimeStamp   = DateTime.Now;
            Level       = level;
            Source      = String.Concat(sourceFilePath, "#", sourceLineNumber, "#", memberName);
            Content     = content;
        }

        /// <summary>
        /// Formats message to a string.
        /// </summary>
        /// <returns>Formatted event message.</returns>
        public override string ToString()
        {
            return String.Concat(TimeStamp.ToString("yyyy-MM-dd hh:mm:ss.ffff"), " ", Level.ToString().ToUpper(), " @ ", Source, ": ", Content);
        }
    }
}
