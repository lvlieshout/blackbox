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
    /// Black Box Manager. Handles log writing on same thread.
    /// </summary>
    public interface IBlackBoxManager
    {
        /// <summary>
        /// Register a fallback event writer which will be triggered when other event writers throw an exception on the Write method.
        /// </summary>
        /// <param name="writer">Event writer delegate.</param>
        void RegisterFallbackWriter(EventWriter writer);

        /// <summary>
        /// Register a event writer which will be triggered at given level.
        /// </summary>
        /// <param name="minimumLevel">Event level to trigger the writer.</param>
        /// <param name="writer">Event writer delegate.</param>
        void RegisterWriter(EventLevel minimumLevel, EventWriter writer);

        /// <summary>
        /// Write event message. Use fallback event writer when primairy writer throws an exception. This write method blocks until the writer is finished.
        /// </summary>
        /// <param name="message">Event message to write</param>
        void Write(IEventMessage message);
    }
}