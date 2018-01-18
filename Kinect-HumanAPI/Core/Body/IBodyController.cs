using Microsoft.Kinect;
using SKotstein.Kinect.API.Gestures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKotstein.Kinect.API.Core
{
    public interface IBodyController
    {

        void LoadGestureContainer(IGestureContainer gestureContainer);

        void RemoveGestureContainer(IGestureContainer gestureContainer);

        void AddGestureEventHandler(EventHandler<GestureEventArgs> e, int gestureIdentifier);

        void RemoveGestureEventHandler(EventHandler<GestureEventArgs> e, int gestureIdentifier);

        ulong GetTrackingId();

        Body Body { get; }



    }
}
