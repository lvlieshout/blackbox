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

namespace System
{
    using System.Diagnostics;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using BlackBox;
    using BlackBox.Writers;

    /// <summary>
    /// Static holder for a BlackBox manager instance. Use SetManager to assign an manager.
    /// </summary>
    public static class Log
    {
        private static BlackBoxManager _manager;

        static Log()
        {
            _manager = new BlackBoxManager();
            _manager.RegisterWriter(EventLevel.Critical, EventNullWriter.Write);
        }

        /// <summary>
        /// Set an BlackBoxManager instace.
        /// </summary>
        /// <param name="manager">BlackBoxManager</param>
        public static void SetManager(BlackBoxManager manager)
        {
            _manager = manager ?? throw new ArgumentNullException(nameof(manager), "Manager parameter cannot be NULL.");
        }

        //private static string GetCallingMethod(int index = 2)
        //{
        //    MethodBase method = new StackTrace().GetFrame(index).GetMethod();
        //    return String.Concat(method.ReflectedType.FullName, ".", method.Name);
        //}

        /// <summary>
        /// Write a critical event to the logger.
        /// </summary>
        /// <param name="content">Content</param>
        /// <param name="memberName">Calling method name</param>
        /// <param name="sourceFilePath">Calling source file path</param>
        /// <param name="sourceLineNumber">Calling source file line number</param>
        [DebuggerStepThrough]
        public static void Critical(string content, [CallerMemberName]string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
        {
            _manager.Write(new EventMessage(EventLevel.Critical, content, memberName, sourceFilePath, sourceLineNumber));
        }

        /// <summary>
        /// Write a event marked as critical.
        /// </summary>
        /// <param name="exception">Exception data used for the event message.</param>
        [DebuggerStepThrough]
        public static void Critical(Exception exception)
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
        public static void Debug(string content, [CallerMemberName]string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
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
        public static void Error(string content, [CallerMemberName]string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
        {
            _manager.Write(new EventMessage(EventLevel.Error, content, memberName, sourceFilePath, sourceLineNumber));
        }

        /// <summary>
        /// Write a event marked as error.
        /// </summary>
        /// <param name="exception">Exception data used for the event message.</param>
        [DebuggerStepThrough]
        public static void Error(Exception exception)
        {
            _manager.Write(exception.ToMessage());
        }

        /// <summary>
        /// Write a event marked as trace.
        /// </summary>
        /// <param name="content">Content</param>
        /// <param name="memberName">Calling method name</param>
        /// <param name="sourceFilePath">Calling source file path</param>
        /// <param name="sourceLineNumber">Calling source file line number</param>
        [DebuggerStepThrough]
        public static void Trace(string content, [CallerMemberName]string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
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
        public static void Warning(string content, [CallerMemberName]string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
        {
            _manager.Write(new EventMessage(EventLevel.Warning, content, memberName, sourceFilePath, sourceLineNumber));
        }

        /// <summary>
        /// Return the log instance in this holder.
        /// </summary>
        /// <returns>BlackBox manager instance</returns>
        public static BlackBoxManager GetManager()
        {
            return _manager;
        }
    }
}