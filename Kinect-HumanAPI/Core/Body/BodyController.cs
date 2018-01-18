using Microsoft.Kinect;
using SKotstein.Kinect.API.Gestures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKotstein.Kinect.API.Core
{
    public class BodyController : IBodyController
    {

        private List<IGestureContainer> _gestureContainers = new List<IGestureContainer>();

        private Body _body;
        private ulong _trackingId;

        public BodyController(ulong trackingId)
        {
            this._trackingId = trackingId;
        }

        public void AddGestureEventHandler(EventHandler<GestureEventArgs> e, int gestureIdentifier)
        {
            for (int i = 0; i < _gestureContainers.Count; i++)
            {
                if (_gestureContainers[i].IsContainerResponsibleForGesture(gestureIdentifier))
                {
                    _gestureContainers[i].AddEventHandler(gestureIdentifier, e);
                }
            }
        }

        public Body Body{
            get
            {
                return _body;
            }
        }

        public ulong GetTrackingId()
        {
            return _trackingId;
        }

        public void LoadGestureContainer(IGestureContainer gestureContainer)
        {
            this._gestureContainers.Add(gestureContainer);
        }

        public void RemoveGestureContainer(IGestureContainer gestureContainer)
        {
            this._gestureContainers.Remove(gestureContainer);
        }

        public void RemoveGestureEventHandler(EventHandler<GestureEventArgs> e, int gestureIdentifier)
        {
            for (int i = 0; i < _gestureContainers.Count; i++)
            {
                if (_gestureContainers[i].IsContainerResponsibleForGesture(gestureIdentifier))
                {
                    _gestureContainers[i].RemoveEventHandler(gestureIdentifier, e);
                }
            }
        }

        public void Update(Body body)
        {
            this._body = body;
            for (int i = 0; i < _gestureContainers.Count; i++)
            {
                _gestureContainers[i].Update(body);
            }
        }
    }
}
