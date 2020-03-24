using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace BlackBox
{
    public static  class EventMessageExtensions
    {
        /// <summary>
        /// Converts exception into a event message.
        /// </summary>
        /// <param name="exception">Exception to convert.</param>
        /// <returns>Event message</returns>
        public static EventMessage ToMessage(this Exception exception)
        {
            string service;
            if (exception.Source == null)
            {
                if (exception.TargetSite == null)
                {
                    MethodBase m = new StackTrace().GetFrame(2).GetMethod();
                    service = String.Concat(m.ReflectedType.FullName, ".", m.Name);
                }
                else
                {
                    service = String.Concat(exception.TargetSite.ReflectedType.FullName, ".", exception.TargetSite.Name);
                }
            }
            else
            {
                service = exception.Source;
            }
            return new EventMessage(EventLevel.Error, service, exception.ToStringEx());

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
            result += "Message: " + exception.Message;
            result += "\r\nHelplink: " + exception.HelpLink;
            result += "\r\nSource: " + exception.Source;
            result += "\r\nStackTrace: " + exception.StackTrace;
            if (exception.InnerException != null)
            {
                result = String.Concat(result, "\r\n\r\nInner exception:\r\n", exception.InnerException.ToStringEx());
            }
            return result;
        }

        /// <summary>
        /// Converts dictionary entries into tring format. Format used is: @key=value;@key2="222";
        /// </summary>
        /// <param name="parameters">Parameters.</param>
        /// <returns>Parameters in string format.</returns>
        public static string ToParameterString(this IDictionary parameters)
        {
            if (parameters == null || parameters.Count == 0) return "";
            string result = "";
            foreach (DictionaryEntry item in parameters)
            {
                if (item.Value == null) continue;
                string value = item.Value.GetType().IsPrimitive ? item.Value.ToString() : String.Concat("\"", item.Value.ToString(), "\"");
                result += String.Concat("@", item.Key, "=", value, ";");
            }
            return result;
        }
    }
}
