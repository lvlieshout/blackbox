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

namespace BlackBox.Writers
{
    using System;
    using System.IO;
    using System.Text;

    /// <summary>
    /// Event message to string formatter
    /// </summary>
    /// <param name="message">Message to convert to string.</param>
    /// <returns>String conversion of an event message.</returns>
    public delegate string EventMessageFormatter(IEventMessage message);

    /// <summary>
    /// Event writer which outputs to an file.
    /// </summary>
    public class EventFileWriter : IEventWriter
    {
        private string  _path;
        private string  _name;
        private int     _maxSizeInKB;
        
        /// <summary>
        /// Set custom message to string formatter.
        /// </summary>
        public EventMessageFormatter Formatter { private get; set; }

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
            Formatter = FormatMessage;
            if (!Directory.Exists(_path)) Directory.CreateDirectory(_path);
        }

        /// <summary>
        /// Write an event message to a log file.
        /// </summary>
        /// <param name="message">EventMessage</param>
        public void Write(IEventMessage message)
        {
            string rawFilename = Path.Combine(_path, _name + message.TimeStamp.ToString("yyyy-MM-dd")) + " ({0}).txt";
            int n = 0;
            string filename;
            FileInfo fileInfo;
            do
            {
                n++;
                filename = String.Format(rawFilename, n);
                fileInfo = new FileInfo(filename);
            }
            while (fileInfo.Exists && fileInfo.Length >= _maxSizeInKB * 1024);
            File.AppendAllText(filename, Formatter(message), Encoding.UTF8);            
        }

        /// <summary>
        /// Format event message for text writer.
        /// </summary>
        /// <param name="message">Event message to format</param>
        /// <returns>Formatted string of the event message</returns>
        protected virtual string FormatMessage(IEventMessage message)
        {
            return String.Concat(
                "-- ",
                message.TimeStamp.ToString("yyyy-MM-dd HH:mm:ss.ffff"),
                " ",
                message.Level.ToString().ToUpper(),
                " @ ",
                message.Source,
                " --",
                Environment.NewLine,
                message.Content,
                Environment.NewLine);
        }
    }
}
