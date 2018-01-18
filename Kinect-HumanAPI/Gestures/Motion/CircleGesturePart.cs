using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKotstein.Kinect.API.Gestures
{
    public class CircleGesturePart_1 : IGesturePart
    {
        public GesturePartResult CheckGesturePart(Body body)
        {
            if (body.Joints[JointType.HandRight].Position.Y > body.Joints[JointType.Head].Position.Y && body.Joints[JointType.HandRight].Position.X > body.Joints[JointType.Head].Position.X)
            {
                return GesturePartResult.Succeded;
            }
            else
            {
                return GesturePartResult.Failed;
            }
        }
    }

    public class CircleGesturePart_2 : IGesturePart
    {
        public GesturePartResult CheckGesturePart(Body body)
        {
            if (body.Joints[JointType.HandRight].Position.Y > body.Joints[JointType.Head].Position.Y && body.Joints[JointType.HandRight].Position.X < body.Joints[JointType.Head].Position.X)
            {
                return GesturePartResult.Succeded;
            }
            else
            {
                return GesturePartResult.Failed;
            }
        }
    }

    public class CircleGesturePart_3 : IGesturePart
    {
        public GesturePartResult CheckGesturePart(Body body)
        {
            if (body.Joints[JointType.HandRight].Position.Y < body.Joints[JointType.Head].Position.Y && body.Joints[JointType.HandRight].Position.X < body.Joints[JointType.Head].Position.X)
            {
                return GesturePartResult.Succeded;
            }
            else
            {
                return GesturePartResult.Failed;
            }
        }
    }

    public class CircleGesturePart_4 : IGesturePart
    {
        public GesturePartResult CheckGesturePart(Body body)
        {
            if (body.Joints[JointType.HandRight].Position.Y < body.Joints[JointType.Head].Position.Y && body.Joints[JointType.HandRight].Position.X > body.Joints[JointType.Head].Position.X)
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
