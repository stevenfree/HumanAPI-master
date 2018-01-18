using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKotstein.Kinect.API.Gestures
{
    /// <summary>
    /// A gesture is composed of a sequence of gesture parts which must match before a gesture can be considered as recognized. 
    /// An implementation of this abstract class is responsbible for handling and checking all underlying gesture parts forming the gesture. 
    /// Whenever a new body state will be arrived via <see cref="Update(Body)"/> the next gesture part in the sequence must be checked. 
    /// If it matches the sequence counter will be increased, if not the sequence counter will be reset.
    /// Only if all parts have been matched, the gesture can be considered as recognized and the <see cref="GestureRecognized"/> will be fired.
    /// </summary>
    public abstract class Gesture
    {

        // Sequence of gesture parts composing this gesture
        private readonly IGesturePart[] _parts;

        //points on the currently analyzed part
        private int _partCounter = 0;

        /// <summary>
        /// Event handler firing if the gesture has been recognized
        /// </summary>
        public event EventHandler<GestureEventArgs> GestureRecognized;

        private int _gestureIdentifier;

        /// <summary>
        /// Creates a new gesture object and passes the sequence of gesture parts this gesture is compossed of.
        /// </summary>
        /// <param name="parts">gesture parts</param>
        /// <param name="identifier">identifier of this gesture</param>
        public Gesture(IGesturePart[] parts, int identifier)
        {
            this._parts = parts;
            this._gestureIdentifier = identifier;
        }

        /// <summary>
        /// Delivers a new body state to this gesture recognition engine. This method checks the next gesture part in the sequence and fires a <see cref="GestureRecognized"/> event if a gesture has been detected.
        /// </summary>
        /// <param name="body">body information object</param>
        public abstract void Update(Body body);

        /// <summary>
        /// Resets all counters
        /// </summary>
        public virtual void Reset()
        {
            _partCounter = 0;
        }

        /// <summary>
        /// Increases the part counter and returns true if the parts limit has been reached after increasing the counter (++partCounter == parts.length) or false if not.
        /// Use <see cref="Reset"/> if the result is true and fire the <see cref="GestureRecognized"/> event, if all gesture parts have been matched successfully.
        /// </summary>
        /// <returns>true if the counter is equals parts.Length, false if not</returns>
        protected bool IncreaseAndCheckPartCounter()
        {
            return ++this._partCounter == _parts.Length;
        }

        /// <summary>
        /// Checks whether the currently selected gesture of the sequence matches and returns the result.
        /// </summary>
        /// <param name="body">analyzed body</param>
        /// <returns>the result of the gesture part matching</returns>
        protected GesturePartResult CheckGesturePart(Body body)
        {
            int index = this._partCounter;

            if (index >= _parts.Length || index < 0)
            {
                throw new IndexOutOfRangeException();
            }
            else
            {
                return this._parts[index].CheckGesturePart(body);
            }
        }

        public bool IsEventHandlerEmpty()
        {
            return GestureRecognized == null;
        }

        /// <summary>
        /// Fires the <see cref="GestureRecognized"/> event
        /// </summary>
        protected void Fire(Body body)
        {
            GestureRecognized?.Invoke(this, new GestureEventArgs(body.TrackingId, this._gestureIdentifier));
        }
    }
}
