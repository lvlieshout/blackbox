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

// Info: https://stackoverflow.com/questions/2031163/when-to-use-the-different-log-levels

namespace BlackBox
{
    using System;

    /// <summary>
    /// Event level types.
    /// </summary>
    [Flags]
    public enum EventLevel
    {
        /// <summary>
        /// No log entry.
        /// </summary>
        None        = 0,
        
        /// <summary>
        /// Critical/Fatal, stops application to work.
        /// </summary>
        Critical    = 1 << 0,

        /// <summary>
        /// Error, there is something wrong but the application can continue to run.
        /// </summary>
        Error       = 1 << 1,

        /// <summary>
        /// Warning, there is something wrong but the application can recover itself.
        /// </summary>
        Warning     = 1 << 2,

        /// <summary>
        /// Info, Informational for normal functionality.
        /// </summary>
        Info        = 1 << 3,

        /// <summary>
        /// Trace, detailed info about the path and context of the application's going.
        /// </summary>
        Trace       = 1 << 4,

        /// <summary>
        /// Debug, very detailed info only activated in debug modus.
        /// </summary>
        Debug       = 1 << 5,

        Production  = Critical | Error | Warning,
        Acceptance  = Production | Info,
        Development = Acceptance | Trace | Debug,
        All         = Development,
    }
}
