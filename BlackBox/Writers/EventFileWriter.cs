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
    using System;
    using System.IO;
    using System.Text;
    using System.Configuration;

    /// <summary>
    /// Event writer which outputs to an file.
    /// </summary>
    public class EventFileWriter
    {
        private string  _path;
        private string  _name;
        private int     _maxSizeInKB;

        /// <summary>
        /// Constructor of EventFileWriter.
        /// </summary>
        /// <param name="path">Path where to write the log files. If it's empty then the application directory will be used.</param>
        /// <param name="name">Name prefix of the log file.</param>
        /// <param name="maxSizeInKB">Max file size in kilobytes.</param>
        public EventFileWriter(string path = "", string name = "log-", int maxSizeInKB = 1024)
        {
            if (String.IsNullOrEmpty(path)) _path = AppDomain.CurrentDomain.BaseDirectory;
            else _path = path;
            _name = name;
            _maxSizeInKB = maxSizeInKB;
            if (!Directory.Exists(_path)) Directory.CreateDirectory(_path);
        }

        /// <summary>
        /// Write an event message to a log file.
        /// </summary>
        /// <param name="message">EventMessage</param>
        public void Write(EventMessage message)
        {
            bool foundUsableFilename = false;
            int n = 1;
            string rawFilename = Path.Combine(_path, _name + message.TimeStamp.ToString("yyyy-MM-dd")) + " ({0}).txt";
            string filename = String.Format(rawFilename, n);

            while (!foundUsableFilename)
            {
                if (File.Exists(filename))
                {
                    FileInfo fileInfo = new FileInfo(filename);
                    if (fileInfo.Length >= _maxSizeInKB * 1024)
                    {
                        n++;
                        filename = String.Format(rawFilename, n);
                    }
                    else
                        foundUsableFilename = true;
                }
                else
                    foundUsableFilename = true;
            }
            File.AppendAllText(filename, FormatMessage(message), Encoding.UTF8);            
        }

        /// <summary>
        /// Format event message for text writer.
        /// </summary>
        /// <param name="message">Event message to format</param>
        /// <returns>Formatted string of the event message</returns>
        protected virtual string FormatMessage(EventMessage message)
        {
            string result = "--[BlackBox Event Message]-----------------------------------------------------";
            result += "\r\nType:\t\t"       + message.Level.ToString();
            result += "\r\nTimeStamp:\t"    + message.TimeStamp.ToString();
            result += "\r\nSource:\t\t"     + message.Source;
            result += "\r\nMessage:\r\n"    + message.Message;
            result += "\r\n";
            return result;
        }

        // /// <summary>
        // /// Create event writer from config settings using the following app settings entities: FileLogName, FileLogPath, FileLogLevel and FileLogMaxSize.
        // /// </summary>
        // /// <param name="manager">BlackBox manager to register the writer.</param>
        // /// <returns>Created and registered EventFileWriter.</returns>
        //public static EventFileWriter RegisterFromConfig(BlackBoxManager manager)
        //{
        //    if (ConfigurationManager.AppSettings["FileLogName"] == null) throw new ArgumentNullException("File logname is not set in configuration");
        //    if (ConfigurationManager.AppSettings["FileLogPath"] == null) throw new ArgumentNullException("Path of file log is not set in configuration");
        //    if (ConfigurationManager.AppSettings["FileLogMaxSize"] == null) throw new ArgumentNullException("Max file size log in kilobyte is not set in configuration");
        //    int size = 0;
        //    if (!int.TryParse(ConfigurationManager.AppSettings["FileLogMaxSize"], out size))
        //        throw new ArgumentException("Max file size log is not an integer", "FileLogMaxSize");
        //    if (ConfigurationManager.AppSettings["FileLogLevel"] == null) throw new ArgumentNullException("EventLog level not set in configuration");
        //    EventTypes eventLevel = BlackBoxUtilities.GetWriteLevelFromString(ConfigurationManager.AppSettings["FileLogLevel"]);
        //    EventFileWriter writer = new EventFileWriter(ConfigurationManager.AppSettings["FileLogPath"], ConfigurationManager.AppSettings["FileLogName"], size);
        //    manager.RegisterWriter(eventLevel, writer.Write);
        //    return writer;
        //}
    }
}
