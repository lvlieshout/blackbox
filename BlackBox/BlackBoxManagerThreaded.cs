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
    using System.Threading;
    using System.Collections.Concurrent;

    /// <summary>
    /// Black Box Manager. Handles log writing in a seperate background thread.
    /// </summary>
    public class BlackBoxManagerThreaded : BlackBoxManager, IDisposable
    {
        private readonly AutoResetEvent                 _resetEvent;
        private readonly Thread                         _writeThread;
        private readonly ConcurrentQueue<IEventMessage> _queue;
        private readonly int                            _maxBufferSize;
        private readonly int                            _safeBufferSize;
        private bool                                    _running;

        /// <summary>
        /// Constructor of the event logger. Starts a new thread for writing events.
        /// </summary>
        public BlackBoxManagerThreaded(int maxBufferSize = 10000, int safeBufferSize = 8000) : base()
        {
            if (maxBufferSize < 1) throw new ArgumentOutOfRangeException(nameof(maxBufferSize));
            if (safeBufferSize < 1) throw new ArgumentOutOfRangeException(nameof(safeBufferSize));
            if (safeBufferSize > maxBufferSize) throw new ArgumentOutOfRangeException(nameof(safeBufferSize));

            AppDomain.CurrentDomain.ProcessExit += new EventHandler(ProcessExit);
            AppDomain.CurrentDomain.DomainUnload += new EventHandler(ProcessExit);
            _queue = new ConcurrentQueue<IEventMessage>();
            _resetEvent = new AutoResetEvent(false);
            _maxBufferSize = maxBufferSize;
            _safeBufferSize = safeBufferSize;
            _running = true;
            _writeThread = new Thread(new ThreadStart(Writer));
            _writeThread.IsBackground = true;
            _writeThread.Name = nameof(BlackBoxManager);
            _writeThread.Start();
        }

        /// <summary>
        /// Write event message. Use fallback event writer when primairy writer throws an exception. This write method queues uo the event. A seperate thread writes the event.
        /// </summary>
        /// <param name="message">Event message to write</param>
        public override void Write(IEventMessage message)
        {
            if (_queue.Count > _maxBufferSize) return;
            if (_queue.Count > _safeBufferSize && message.Level <= EventLevel.Error) return;
            _queue.Enqueue(message);
            _resetEvent.Set();
        }

        /// <summary>
        /// Event writer running in a seperate thread. Blocked through an AutoResetEvent.
        /// </summary>
        private void Writer()
        {
            while (_running)
            {
                _resetEvent.WaitOne();
                while (_queue.TryDequeue(out IEventMessage message))
                {
                    base.Write(message);
                }
            }
        }

        /// <summary>
        /// Triggers when application proccess is stopped and call Dispose.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">EventArgs</param>
        private void ProcessExit(object sender, EventArgs e)
        {
            Dispose();
        }

        /// <summary>
        /// Dispose BlackBoxManagerThreaded. Empty queue and ends writer thread.
        /// </summary>
        public void Dispose()
        {
            _running = false;
            _resetEvent.Set();
            _writeThread.Join();
        }
    }
}
