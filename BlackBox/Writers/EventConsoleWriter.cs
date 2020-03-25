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

namespace BlackBox.Writers
{
    using System;

    /// <summary>
    /// Event writer which outputs to the console.
    /// </summary>
    public class EventConsoleWriter
    {
        /// <summary>
        /// Write an event message to the console.
        /// </summary>
        /// <param name="message"></param>
        public virtual void Write(EventMessage message)
        {
            ConsoleColor tempForegroundColor = Console.ForegroundColor;
            ConsoleColor tempBackgroudnColor = Console.BackgroundColor;

            Console.BackgroundColor = ConsoleColor.Black;
            switch (message.Level)
            {
                case EventLevel.Critical:   Console.ForegroundColor = ConsoleColor.Magenta;     break;
                case EventLevel.Debug:      Console.ForegroundColor = ConsoleColor.DarkGreen;   break;
                case EventLevel.Error:      Console.ForegroundColor = ConsoleColor.Red;         break;
                case EventLevel.Trace:      Console.ForegroundColor = ConsoleColor.Gray;        break;
                case EventLevel.Warning:    Console.ForegroundColor = ConsoleColor.DarkMagenta; break;
                case EventLevel.Info:       Console.ForegroundColor = ConsoleColor.Gray;        break;
                default:                    Console.ForegroundColor = ConsoleColor.Gray;        break;
            }
            Console.WriteLine(FormatMessage(message));
            Console.ForegroundColor = tempForegroundColor;
            Console.BackgroundColor = tempBackgroudnColor;
        }

        /// <summary>
        /// Format EventMessage to a string.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        protected virtual string FormatMessage(EventMessage message)
        {
            string output = "[";
            output += message.TimeStamp.ToString("yyyy-mm-dd hh:MM:ss");
            output += "][";
            output += message.Level.ToString();
            output += "][";
            output += message.Source;
            output += "]: ";
            output += message.Message;
            return output;
        }
    }
}
