using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKotstein.Kinect.API.Gestures
{
    public class GestureEventArgs : EventArgs
    {

        private ulong _trackingId;
        private int _gestureIdentifier;

        public GestureEventArgs(ulong trackingId, int gestureIdentifier)
        {
            _trackingId = trackingId;
            _gestureIdentifier = gestureIdentifier;
        }

        public ulong TrackingId
        {
            get
            {
                return _trackingId;
            }
        }
        
        public int GestureIdentifier
        {
            get
            {
                return _gestureIdentifier;
            }
        }
    }
}
