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
    using System.Collections;
    using System.Reflection;
    using BlackBox;

    /// <summary>
    /// Static holder for a BlackBox manager instance. Use SetManager to assign an manager.
    /// </summary>
    public static class Log
    {
        private static BlackBoxManager _manager;

        static Log()
        {
            _manager = new BlackBoxManager();
            _manager.RegisterWriter(EventLevel.None, new EventNullWriter().Write);
        }

        /// <summary>
        /// Set an BlackBoxManager instace.
        /// </summary>
        /// <param name="manager">BlackBoxManager</param>
        public static void SetManager(BlackBoxManager manager)
        {
            if (manager == null) throw new ArgumentNullException("manager", "Manager parameter cannot be NULL.");
            _manager = manager;
        }

        private static string GetCallingMethod()
        {
            MethodBase m = new StackTrace().GetFrame(2).GetMethod();
            return String.Concat(m.ReflectedType.FullName, ".", m.Name);
        }

        /// <summary>
        /// Write a event marked as critical.
        /// </summary>
        /// <param name="data">Data</param>
        /// <param name="service">Namespace, class and method (formated as namespace.class.Method). When NULL it will be auto generated.</param>
        [DebuggerStepThrough]
        public static void Critical(string data, string service = null)
        {
            if (service == null) service = GetCallingMethod();
            _manager.Write(new EventMessage(EventLevel.Critical, data, service));
        }

        /// <summary>
        /// Write a event marked as debug.
        /// </summary>
        /// <param name="data">Data</param>
        /// <param name="service">Namespace, class and method (formated as namespace.class.Method). When NULL it will be auto generated.</param>
        [DebuggerStepThrough]
        public static void Debug(string data, string service = null)
        {
            if (service == null) service = GetCallingMethod();
            _manager.Write(new EventMessage(EventLevel.Debug, data, service));
        }

        /// <summary>
        /// Write a event marked as error.
        /// </summary>
        /// <param name="data">Data</param>
        /// <param name="service">Namespace, class and method (formated as namespace.class.Method). When NULL it will be auto generated.</param>
        [DebuggerStepThrough]
        public static void Error(string data, string service = null)
        {
            if (service == null) service = GetCallingMethod();
            _manager.Write(new EventMessage(EventLevel.Error, data, service));
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
        /// <param name="data">Data</param>
        /// <param name="service">Namespace, class and method (formated as namespace.class.Method). When NULL it will be auto generated.</param>
        [DebuggerStepThrough]
        public static void Trace(string data, string service = null)
        {
            if (service == null) service = GetCallingMethod();
            _manager.Write(new EventMessage(EventLevel.Trace, data, service));
        }

        /// <summary>
        /// Write a event marked as warning.
        /// </summary>
        /// <param name="data">Data</param>
        /// <param name="service">Namespace, class and method (formated as namespace.class.Method). When NULL it will be auto generated.</param>
        [DebuggerStepThrough]
        public static void Warning(string data, string service = null)
        {
            if (service == null) service = GetCallingMethod();
            _manager.Write(new EventMessage(EventLevel.Warning, data, service));
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