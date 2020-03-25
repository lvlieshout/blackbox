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
    using System.Collections;
    using System.Diagnostics;
    using System.Reflection;

    /// <summary>
    /// Extension methods and other helpers for BlackBox Event Logger.
    /// </summary>
    public static class EventMessageExtensions
    {
        /// <summary>
        /// Converts exception into a event message.
        /// </summary>
        /// <param name="exception">Exception to convert.</param>
        /// <param name="level">Event level.</param>
        /// <returns>Event message</returns>
        public static EventMessage ToMessage(this Exception exception, EventLevel level = EventLevel.Error)
        {
            string source;
            if (exception.Source == null)
            {
                if (exception.TargetSite == null)
                {
                    MethodBase m = new StackTrace().GetFrame(2).GetMethod();
                    source = String.Concat(m.ReflectedType.FullName, ".", m.Name);
                }
                else
                {
                    source = String.Concat(exception.TargetSite.ReflectedType.FullName, ".", exception.TargetSite.Name);
                }
            }
            else
            {
                source = exception.Source;
            }
            return new EventMessage(level, source, exception.ToStringEx());

            // message.Host = host ?? System.Net.Dns.GetHostName();
            // message.Thread = String.Concat(System.Threading.Thread.CurrentThread.Name, " (", System.Threading.Thread.CurrentThread.ManagedThreadId, ")");
            // message.Parameters = ParametersToString(parameters) + ParametersToString(exception.Data);
        }

        /// <summary>
        /// Converts exception to a formatted string.
        /// </summary>
        /// <param name="exception">Exception to convert.</param>
        /// <returns>String with exception.</returns>
        public static string ToStringEx(this Exception exception)
        {
            string result = "";
            result += "Exception: " + exception.Message;
            result += "\r\nHelplink: " + exception.HelpLink;
            result += "\r\nSource: " + exception.Source;
            result += "\r\nStackTrace:\r\n" + exception.StackTrace;
            if (exception.InnerException != null)
            {
                result = String.Concat(result, "\r\n\r\nInner exception:\r\n", exception.InnerException.ToStringEx());
            }
            return result;
        }

        /// <summary>
        /// Converts dictionary entries into tring format. Format used is: @key="value";@key2=222;
        /// </summary>
        /// <param name="parameters">Parameters.</param>
        /// <returns>Parameters in string format.</returns>
        public static string ToParameterString(this IDictionary parameters)
        {
            if (parameters == null || parameters.Count == 0) return "";
            string result = "";
            foreach (DictionaryEntry item in parameters)
            {
                if (item.Value == null)
                {
                    result += String.Concat("@", item.Key, "=NULL;");
                }
                else
                {
                    string value = item.Value.GetType().IsPrimitive ? item.Value.ToString() : String.Concat("\"", item.Value.ToString(), "\"");
                    result += String.Concat("@", item.Key, "=", value, ";");
                }
            }
            return result;
        }
    }
}
