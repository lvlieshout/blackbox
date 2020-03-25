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
    /// <summary>
    /// Event levels, defines the severity of an event. Also used to inducate the minimum level to log.
    /// Excellent explanation of levels: https://stackoverflow.com/questions/2031163/when-to-use-the-different-log-levels
    /// </summary>
    public enum EventLevel
    {
        /// <summary>
        /// Critical/Fatal. Use when something happens which terminates the application. Direct attention is needed.
        /// </summary>
        Critical    = 1,

        /// <summary>
        /// Error. An exception has happend but the application can continue to run.
        /// </summary>
        Error       = 2,

        /// <summary>
        /// Warning. Something goes wrong but does not need direct attention. The application can recover itself.
        /// </summary>
        Warning     = 3,

        /// <summary>
        /// Info. Informational for normal activity.
        /// </summary>
        Info        = 4,

        /// <summary>
        /// Trace. Detailed info about the path and context of the application's going.
        /// </summary>
        Trace       = 5,

        /// <summary>
        /// Debug. Very detailed info only activated in debug modus/debugger attached.
        /// </summary>
        Debug       = 6,
    }
}
