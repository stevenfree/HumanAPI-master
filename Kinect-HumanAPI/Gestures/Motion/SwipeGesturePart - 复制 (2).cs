﻿using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKotstein.Kinect.API.Gestures
{
    public class SwipeGesturePart_11 : IGesturePart
    {
        public GesturePartResult CheckGesturePart(Body body)
        {
            if ((body.Joints[JointType.HandRight].Position.X < body.Joints[JointType.SpineShoulder].Position.X) && (body.Joints[JointType.HandRight].Position.Y < body.Joints[JointType.SpineShoulder].Position.Y))
            {
                return GesturePartResult.Succeded;
            }
            else
            {
                return GesturePartResult.Failed;
            }
        }
    }

    public class SwipeGesturePart_22 : IGesturePart
    {
        public GesturePartResult CheckGesturePart(Body body)
        {
            if ((body.Joints[JointType.HandRight].Position.X > body.Joints[JointType.SpineShoulder].Position.X) && (body.Joints[JointType.HandRight].Position.X < body.Joints[JointType.ShoulderRight].Position.X) && (body.Joints[JointType.HandRight].Position.Y < body.Joints[JointType.SpineShoulder].Position.Y))
            {
                return GesturePartResult.Succeded;
            }
            else
            {
                return GesturePartResult.Failed;
            }
        }
    }

    public class SwipeGesturePart_33 : IGesturePart
    {
        public GesturePartResult CheckGesturePart(Body body)
        {
            if ((body.Joints[JointType.HandRight].Position.X > body.Joints[JointType.ShoulderRight].Position.X) && (body.Joints[JointType.HandRight].Position.Y < body.Joints[JointType.SpineShoulder].Position.Y))
            {
                return GesturePartResult.Succeded;
            }
            else
            {
                return GesturePartResult.Failed;
            }
        }
    }

}
