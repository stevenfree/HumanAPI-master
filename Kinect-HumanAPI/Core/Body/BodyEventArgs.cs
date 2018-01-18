using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKotstein.Kinect.API.Core
{
    public class BodyEventArgs : EventArgs
    {
        private IBodyController _bc;
        private ulong _trackingId;

        public BodyEventArgs(IBodyController bc, ulong trackingId)
        {
            this._bc = bc;
            this._trackingId = trackingId;
        }

        public IBodyController BodyController
        {
            get
            {
                return _bc;
            }
        }

        public ulong TrackingId
        {
            get
            {
                return _trackingId;
            }
        }

    }
}
