///*
// * Copyright (c) 2012 Lambert van Lieshout, YUMMO Software Development
// * 
// * Permission is hereby granted, free of charge, to any person obtaining a copy
// * of this software and associated documentation files (the "Software"), to
// * deal in the Software without restriction, including without limitation the
// * rights to use, copy, modify, merge, publish, distribute, sublicense, and/or
// * sell copies of the Software, and to permit persons to whom the Software is
// * furnished to do so, subject to the following conditions:
// * 
// * The above copyright notice and this permission notice shall be included in
// * all copies or substantial portions of the Software.
// * 
// * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// * FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS
// * IN THE SOFTWARE.
//*/

//namespace BlackBox.Writers
//{
//    using System;
//    using System.Diagnostics;
//    using System.Configuration;
//    using System.Security;

//    /// <summary>
//    /// Event writer which outputs to the Windows Event Log.
//    /// </summary>
//    [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Assert, Name = "FullTrust")]
//    public class EventWinLogWriter
//    {
//        private string _application;
//        private string _source;

//        /// <summary>
//        /// Constructor of EventWinLogWriter. 
//        /// </summary>
//        /// <param name="application">Application group (eg. System, Application, Security in the Event Viewer. This can be a custom application name).</param>
//        /// <param name="source">Source group (column Source in the event entry).</param>
//        public EventWinLogWriter(string application, string source)
//        {
//            if (String.IsNullOrEmpty(application)) throw new ArgumentNullException(nameof(application));
//            if (String.IsNullOrEmpty(source)) throw new ArgumentNullException(nameof(source));
//            _source = source;
//            _application = application;

//            // If an app does not have administrator rights then it is not possible to create a event source. On Windows
//            // Vista or higher it is not possible to call SourceExists without administrator rights. -Lambert 2013-05-25
//            try
//            {
//                if (!EventLog.SourceExists(_source)) EventLog.CreateEventSource(_source, _application);
//            }
//            catch { }

//            try
//            {
//                if (!EventLog.SourceExists(_source))
//                    throw new Exception("WinEventLogWriter.Open : " + _source + " in Windows event log does not exist. Application need administrator rights to create.");
//            }
//            catch (SecurityException ex)
//            {
//                throw new Exception("Administrator rights are needed to create a Windows Event Log source.", ex);
//            }
//        }

//        /// <summary>
//        /// Write an event message to the Windows Event Log.
//        /// </summary>
//        /// <param name="message">EventMessage</param>
//        public void Write(EventMessage message)
//        {
//            EventLog.WriteEntry(_source, FormatMessage(message), TypeConversion(message.EventType));
//        }

//        private static string FormatMessage(EventMessage message)
//        {
//            string result = "";
//            result += "Type: " + message.Severity.ToString();
//            result += "\r\nTimeStamp: " + message.TimeStamp.ToString();
//            result += "\r\nService: " + message.Service;
//            result += "\r\nMessage:" + "\r\n" + message.Message;
//            return result;
//        }

//        private static EventLogEntryType TypeConversion(SeverityLevel type)
//        {
//            switch (type)
//            {
//                case SeverityLevel.Critical:    return EventLogEntryType.FailureAudit;
//                case SeverityLevel.Error:       return EventLogEntryType.Error;
//                case SeverityLevel.Trace:       return EventLogEntryType.Informat;
//                case SeverityLevel.Info:        return EventLogEntryType.Warning;
//                case SeverityLevel.Debug:       return EventLogEntryType.Information;
//                case SeverityLevel.Warning:     return EventLogEntryType.Warning;
//                default:                        return EventLogEntryType.SuccessAudit;
//            }
//        }

//        /// <summary>
//        /// Create event writer from config settings using the following app settings entities: WinEventLogSource, WinEventLogApplication and WinEventLogLevel.
//        /// </summary>
//        /// <param name="manager">BlackBox manager to register the writer.</param>
//        /// <returns>Created and registered EventWinLogWriter.</returns>
//        public static EventWinLogWriter RegisterFromConfig(BlackBoxManager manager)
//        {
//            if (ConfigurationManager.AppSettings["WinEventLogSource"] == null) throw new ArgumentNullException("EvenLog source not set in configuration");
//            if (ConfigurationManager.AppSettings["WinEventLogApplication"] == null) throw new ArgumentNullException("EvenLog application not set in configuration");
//            if (ConfigurationManager.AppSettings["WinEventLogLevel"] == null) throw new ArgumentNullException("EvenLog level not set in configuration");
//            EventTypes eventLevel = BlackBoxUtilities.GetWriteLevelFromString(ConfigurationManager.AppSettings["WinEventLogLevel"]);
//            EventWinLogWriter writer = new EventWinLogWriter(ConfigurationManager.AppSettings["WinEventLogSource"], ConfigurationManager.AppSettings["WinEventLogApplication"]);
//            manager.RegisterWriter(eventLevel, writer.Write);
//            return writer;
//        }
//    }
//}
