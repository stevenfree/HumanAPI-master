using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKotstein.Kinect.API.Core.Root
{
    public class HumanApi
    {
        private static HumanApi instance;

        private bool _started = false;

        private KinectSensor _sensor;
        private BodyFrameReader _reader;

        private Body[] _bodies;

        private List<BodyController> _bodyControllers = new List<BodyController>();


        public event EventHandler<BodyEventArgs> BodyDetected;

        public event EventHandler<BodyEventArgs> BodyLost;

        public int AmountOfBodiesDetected
        {
            get { return _bodyControllers.Count; }
        }

        public static HumanApi GetInstance(KinectSensor sensor)
        {
            if (instance == null)
            {
                instance = new HumanApi(sensor);
            }

            return instance;
        }

        private HumanApi(KinectSensor sensor)
        {
            this._sensor = sensor;
        }

        public void Start()
        {
            if (_sensor != null)
            {
                _sensor.Open();
                _reader = _sensor.BodyFrameSource.OpenReader();
                _reader.FrameArrived += Reader_FrameArrived;
                _started = true;
            }

        }

        public void Stop()
        {
            if (_sensor.IsOpen)
            {
                _sensor.Close();
                if (_reader != null)
                {
                    _reader.Dispose();
                }
                _started = false;
            }
            _bodyControllers = new List<BodyController>();
        }

        public bool IsClosestBody(ulong trackingId)
        {
            //1. search body
            Body b = null;
            foreach (BodyController bc in _bodyControllers)
            {
                if (bc.Body.TrackingId == trackingId)
                {
                    b = bc.Body;
                }
            }
            //2. check invalid condition if trackingId is invalid
            if (b == null)
            {
                return false;
            }

            //3. check all other bodies
            foreach (BodyController bc in _bodyControllers)
            {
                if (bc.Body.TrackingId != trackingId)
                {
                    if (bc.Body.Joints[JointType.SpineBase].Position.Z < b.Joints[JointType.SpineBase].Position.Z)
                    {
                        return false;
                    }
                }
            }
            return true;
        }


        private void Reader_FrameArrived(object sender, BodyFrameArrivedEventArgs e)
        {



            //load body frame
            using (BodyFrame bodyFrame = e.FrameReference.AcquireFrame())
            {


                if (bodyFrame != null)
                {
                    //TODO: Check whether this step is required or might be lead to malfunction, maybe the GetAndRefreshBodyData is already performing this job
                    if (this._bodies == null)
                    {
                        //if bodies have been arrived, the array will be resetted and adapted to the size of captured bodies 
                        this._bodies = new Body[bodyFrame.BodyCount];
                    }

                    //load body information to array and refresh the contained inforamtion
                    bodyFrame.GetAndRefreshBodyData(this._bodies);

                    for (int i = 0; i < this._bodies.Length; i++)
                    {
                        if (_bodies[i] != null && _bodies[i].TrackingId != 0)
                        {
                            //1. check whether body is already listed (BodyController)
                            bool found = false;
                            foreach (BodyController bc in _bodyControllers)
                            {
                                if (bc.GetTrackingId() == _bodies[i].TrackingId)
                                {
                                    bc.Update(_bodies[i]);
                                    found = true;
                                }
                            }
                            //if the boy has not been listed yet, add a BodyController and fire an event
                            if (!found)
                            {
                                BodyController bodyController = new BodyController(_bodies[i].TrackingId);
                                _bodyControllers.Add(bodyController);
                                bodyController.Update(_bodies[i]);

                                //fire event
                                if (BodyDetected != null)
                                {

                                    BodyDetected(this, new BodyEventArgs(bodyController, _bodies[i].TrackingId));
                                }
                            }
                        }

                    }
                    //2. check which bodies has been removed:
                    bool found2 = false;
                    List<BodyController> toBeDeleted = new List<BodyController>();
                    foreach (BodyController bc in _bodyControllers)
                    {
                        for (int i = 0; i < this._bodies.Length; i++)
                        {
                            if (bc.GetTrackingId() == _bodies[i].TrackingId)
                            {
                                found2 = true;
                            }
                        }
                        if (!found2)
                        {
                            toBeDeleted.Add(bc);

                            //fire event
                            if (BodyLost != null)
                            {
                                BodyLost(this, new BodyEventArgs(bc, bc.GetTrackingId()));
                            }
                        }
                    }
                    //delete all unecessary BodyController
                    for (int i = 0; i < toBeDeleted.Count; i++)
                    {
                        this._bodyControllers.Remove(toBeDeleted[i]);
                    }
                }

            }
        }
    }
}

