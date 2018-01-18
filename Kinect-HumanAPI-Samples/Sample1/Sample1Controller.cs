using Microsoft.Kinect;
using SKotstein.Kinect.API.Core;
using SKotstein.Kinect.API.Core.Root;
using SKotstein.Kinect.API.Gestures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKotstein.Kinect.API.Samples.Sample1
{
    public class Sample1Controller
    {
        private HumanApi api;

        public void Start()
        {
            api = HumanApi.GetInstance(KinectSensor.GetDefault());
            api.Start();
            api.BodyDetected += Body_Detected;
            api.BodyLost += Body_Lost;
            Console.ReadKey();
            api.Stop();
        }

        private void Body_Detected(object sender, BodyEventArgs e)
        {
            Console.WriteLine("Body detected: " + e.TrackingId);

            IGestureContainer handContainer = new AutomatedGestureContainer(HandGestureFactory.GetInstance());
            IGestureContainer motionContainer = new AutomatedGestureContainer(MotionGestureFactory.GetInstance());

            IBodyController bc = e.BodyController;

            bc.LoadGestureContainer(handContainer);
            bc.LoadGestureContainer(motionContainer);




            bc.AddGestureEventHandler(Gesture_Handler, GestureIdentifier.LEFT_HAND_CLOSED_GESTURE);
            bc.AddGestureEventHandler(Gesture_Handler, GestureIdentifier.RIGHT_HAND_CLOSED_GESTURE);
            bc.AddGestureEventHandler(Gesture_Handler, GestureIdentifier.RIGHT_HAND_OPEN_GESTURE);
            bc.AddGestureEventHandler(Gesture_Handler, GestureIdentifier.RIGHT_HAND_QUICKLY_OPEN_GESTURE);
            bc.AddGestureEventHandler(Gesture_Handler, GestureIdentifier.RIGHT_HAND_QUICKLY_CLOSED_GESTURE);
            bc.AddGestureEventHandler(Gesture_Handler, GestureIdentifier.CIRCLE_CLOCKWISE_GESTURE);
            bc.AddGestureEventHandler(Gesture_Handler, GestureIdentifier.CIRCLE_COUNTER_CLOCKWISE_GESTURE);
            bc.AddGestureEventHandler(Gesture_Handler, GestureIdentifier.SWIPE_TO_LEFT_GESTURE);

        }

        private void Gesture_Handler(object sender, GestureEventArgs e)
        {
            switch (e.GestureIdentifier)
            {
                case GestureIdentifier.LEFT_HAND_CLOSED_GESTURE:
                    Console.WriteLine(e.TrackingId + " " + api.IsClosestBody(e.TrackingId) + ": Left Hand Closed");
                    break;
                case GestureIdentifier.LEFT_HAND_OPEN_GESTURE:
                    Console.WriteLine(e.TrackingId + " " + api.IsClosestBody(e.TrackingId) + ": Left Hand Opened");
                    break;
                case GestureIdentifier.LEFT_HAND_QUICKLY_CLOSED_GESTURE:
                    Console.WriteLine(e.TrackingId + " " + api.IsClosestBody(e.TrackingId) + ": Left Hand Quickly Closed");
                    break;
                case GestureIdentifier.LEFT_HAND_QUICKLY_OPEN_GESTURE:
                    Console.WriteLine(e.TrackingId + " " + api.IsClosestBody(e.TrackingId) + ": Left Hand Quickly Opened");
                    break;
                case GestureIdentifier.RIGHT_HAND_CLOSED_GESTURE:
                    Console.WriteLine(e.TrackingId + " " + api.IsClosestBody(e.TrackingId) + ": Right Hand Closed");
                    break;
                case GestureIdentifier.RIGHT_HAND_OPEN_GESTURE:
                    Console.WriteLine(e.TrackingId + " " + api.IsClosestBody(e.TrackingId) + ": Right Hand Opened");
                    break;
                case GestureIdentifier.RIGHT_HAND_QUICKLY_CLOSED_GESTURE:
                    Console.WriteLine(e.TrackingId + " " + api.IsClosestBody(e.TrackingId) + ": Right Hand Quickly Closed");
                    break;
                case GestureIdentifier.RIGHT_HAND_QUICKLY_OPEN_GESTURE:
                    Console.WriteLine(e.TrackingId + " " + api.IsClosestBody(e.TrackingId) + ": Right Hand Quickly Openend");
                    break;
                case GestureIdentifier.CIRCLE_CLOCKWISE_GESTURE:
                    Console.WriteLine(e.TrackingId + " " + api.IsClosestBody(e.TrackingId) + ": Circle Clockwise");
                    break;
                case GestureIdentifier.CIRCLE_COUNTER_CLOCKWISE_GESTURE:
                    Console.WriteLine(e.TrackingId + " " + api.IsClosestBody(e.TrackingId) + ": Anti Circle Clockwise");
                    break;
                case GestureIdentifier.SWIPE_TO_LEFT_GESTURE:
                    Console.WriteLine(e.TrackingId + " " + api.IsClosestBody(e.TrackingId) + ": Swipe To Left");
                    break;

            }
        }

        private void Body_Lost(object sender, BodyEventArgs e)
        {
            Console.WriteLine("Body lost: " + e.TrackingId);
        }
    }
}
