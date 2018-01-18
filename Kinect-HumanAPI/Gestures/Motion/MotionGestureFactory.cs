using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKotstein.Kinect.API.Gestures
{
    public class MotionGestureFactory : IGestureFactory
    {

        private static IGestureFactory instance;

        private MotionGestureFactory()
        {

        }

        public static IGestureFactory GetInstance()
        {
            if (instance == null)
            {
                instance = new MotionGestureFactory();
            }
            return instance;
        }

        public Gesture CreateGesture(int identifier)
        {

            IGesturePart seg1 = new CircleGesturePart_1();
            IGesturePart seg2 = new CircleGesturePart_2();
            IGesturePart seg3 = new CircleGesturePart_3();
            IGesturePart seg4 = new CircleGesturePart_4();

            IGesturePart seg11 = new SwipeGesturePart_1();
            IGesturePart seg12 = new SwipeGesturePart_2();
            IGesturePart seg13 = new SwipeGesturePart_3();

            IGesturePart[] parts;

            if (identifier == GestureIdentifier.CIRCLE_CLOCKWISE_GESTURE)
            {
                parts = new IGesturePart[]
                        {
                           seg1,seg2,seg3,seg4
                        };
                return new SequenceDependentGesture(parts, identifier, 50);
            }
            if (identifier == GestureIdentifier.CIRCLE_COUNTER_CLOCKWISE_GESTURE)
            {
                parts = new IGesturePart[]
                       {
                           seg4,seg3,seg2,seg1
                       };
                return new SequenceDependentGesture(parts, identifier, 50);
            }

            if (identifier == GestureIdentifier.SWIPE_TO_LEFT_GESTURE)
            {
                parts = new IGesturePart[]
                       {
                           seg11,seg12,seg13
                       };
                return new SequenceDependentGesture(parts, identifier, 50);
            }
            return null;
        }

        public List<int> GesturePortfolio
        {
            get
            {
                List<int> identifiers = new List<int>();
                identifiers.Add(GestureIdentifier.CIRCLE_CLOCKWISE_GESTURE);
                identifiers.Add(GestureIdentifier.CIRCLE_COUNTER_CLOCKWISE_GESTURE);
                identifiers.Add(GestureIdentifier.SWIPE_TO_LEFT_GESTURE);
                return identifiers;
            }
        }
    }
}
