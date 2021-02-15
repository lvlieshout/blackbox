/*
 * Copyright (c) 2021 Lambert van Lieshout, YUMMO Software Development
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
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// Logger instance.
    /// </summary>
    public class BlackBoxLogger : IBlackBoxLogger
    {
        /// <summary>
        /// BlackBox manager used for processing event messages.
        /// </summary>
        protected readonly IBlackBoxManager _manager;

        /// <summary>
        /// Logger instance
        /// </summary>
        /// <param name="manager"></param>
        public BlackBoxLogger(IBlackBoxManager manager)
        {
            _manager = manager ?? throw new ArgumentNullException(nameof(manager), "Log manager cannot be NULL.");
        }

        /// <summary>
        /// Write a critical event to the logger.
        /// </summary>
        /// <param name="content">Content</param>
        /// <param name="memberName">Calling method name</param>
        /// <param name="sourceFilePath">Calling source file path</param>
        /// <param name="sourceLineNumber">Calling source file line number</param>
        [DebuggerStepThrough]
        public void Critical(string content, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
        {
            _manager.Write(new EventMessage(EventLevel.Critical, content, memberName, sourceFilePath, sourceLineNumber));
        }

        /// <summary>
        /// Write a event marked as critical.
        /// </summary>
        /// <param name="exception">Exception data used for the event message.</param>
        [DebuggerStepThrough]
        public void Critical(Exception exception)
        {
            _manager.Write(exception.ToMessage(EventLevel.Critical));
        }

        /// <summary>
        /// Write a event marked as debug.
        /// </summary>
        /// <param name="content">Content</param>
        /// <param name="memberName">Calling method name</param>
        /// <param name="sourceFilePath">Calling source file path</param>
        /// <param name="sourceLineNumber">Calling source file line number</param>
        [DebuggerStepThrough]
        public void Debug(string content, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
        {
            _manager.Write(new EventMessage(EventLevel.Debug, content, memberName, sourceFilePath, sourceLineNumber));
        }

        /// <summary>
        /// Write a event marked as error.
        /// </summary>
        /// <param name="content">Content</param>
        /// <param name="memberName">Calling method name</param>
        /// <param name="sourceFilePath">Calling source file path</param>
        /// <param name="sourceLineNumber">Calling source file line number</param>
        [DebuggerStepThrough]
        public void Error(string content, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
        {
            _manager.Write(new EventMessage(EventLevel.Error, content, memberName, sourceFilePath, sourceLineNumber));
        }

        /// <summary>
        /// Write a event marked as error.
        /// </summary>
        /// <param name="exception">Exception data used for the event message.</param>
        [DebuggerStepThrough]
        public void Error(Exception exception)
        {
            _manager.Write(exception.ToMessage());
        }

        /// <summary>
        /// Write a event marked as informational.
        /// </summary>
        /// <param name="content">Content</param>
        /// <param name="memberName">Calling method name</param>
        /// <param name="sourceFilePath">Calling source file path</param>
        /// <param name="sourceLineNumber">Calling source file line number</param>
        [DebuggerStepThrough]
        public void Info(string content, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
        {
            _manager.Write(new EventMessage(EventLevel.Trace, content, memberName, sourceFilePath, sourceLineNumber));
        }

        /// <summary>
        /// Write a event marked as trace.
        /// </summary>
        /// <param name="content">Content</param>
        /// <param name="memberName">Calling method name</param>
        /// <param name="sourceFilePath">Calling source file path</param>
        /// <param name="sourceLineNumber">Calling source file line number</param>
        [DebuggerStepThrough]
        public void Trace(string content, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
        {
            _manager.Write(new EventMessage(EventLevel.Trace, content, memberName, sourceFilePath, sourceLineNumber));
        }

        /// <summary>
        /// Write a event marked as warning.
        /// </summary>
        /// <param name="content">Content</param>
        /// <param name="memberName">Calling method name</param>
        /// <param name="sourceFilePath">Calling source file path</param>
        /// <param name="sourceLineNumber">Calling source file line number</param>
        [DebuggerStepThrough]
        public void Warning(string content, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
        {
            _manager.Write(new EventMessage(EventLevel.Warning, content, memberName, sourceFilePath, sourceLineNumber));
        }
    }
}
