using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKotstein.Kinect.API.Gestures
{
    /// <summary>
    /// Factory class for creatring hand gestures
    /// </summary>
    public class HandGestureFactory : IGestureFactory
    {

        private static IGestureFactory instance;

        private HandGestureFactory()
        {

        }

        public static IGestureFactory GetInstance()
        {
            if (instance == null)
            {
                instance = new HandGestureFactory();
            }
            return instance;
        }



        public Gesture CreateGesture(int identifier)
        {

            //choose right hand for default
            bool leftHand = false;
            //but check whether left hand has been chosen
            if (identifier == GestureIdentifier.LEFT_HAND_CLOSED_GESTURE ||
                identifier == GestureIdentifier.LEFT_HAND_OPEN_GESTURE ||
                identifier == GestureIdentifier.LEFT_HAND_QUICKLY_CLOSED_GESTURE ||
                identifier == GestureIdentifier.LEFT_HAND_QUICKLY_OPEN_GESTURE)
            {
                leftHand = true;
            }

            IGesturePart closed = new HandGesturePart(leftHand, HandState.Closed);
            IGesturePart open = new HandGesturePart(leftHand, HandState.Open);
            IGesturePart[] parts;

            if (identifier == GestureIdentifier.LEFT_HAND_CLOSED_GESTURE || identifier == GestureIdentifier.RIGHT_HAND_CLOSED_GESTURE)
            {
                parts = new IGesturePart[]
                        {
                            //1x open
                            open,
                            //15x closed
                            closed, closed, closed, closed, closed,
                            closed, closed, closed, closed, closed,
                            closed, closed, closed, closed, closed,
                        };
                return new FrameDependentGesture(parts, identifier);
            }
            if (identifier == GestureIdentifier.LEFT_HAND_OPEN_GESTURE || identifier == GestureIdentifier.RIGHT_HAND_OPEN_GESTURE)
            {
                parts = new IGesturePart[]
                        {
                            //1x closed
                            closed,
                            //15x open
                            open, open, open, open, open,
                            open, open, open, open, open,
                            open, open, open, open, open,
                        };
                return new FrameDependentGesture(parts, identifier);
            }
            if (identifier == GestureIdentifier.LEFT_HAND_QUICKLY_CLOSED_GESTURE || identifier == GestureIdentifier.RIGHT_HAND_QUICKLY_CLOSED_GESTURE)
            {
                parts = new IGesturePart[]
                        {
                            open, closed, open
                        };
                return new SequenceDependentGesture(parts, identifier, 10);
            }
            if (identifier == GestureIdentifier.LEFT_HAND_QUICKLY_OPEN_GESTURE || identifier == GestureIdentifier.RIGHT_HAND_QUICKLY_OPEN_GESTURE)
            {
                parts = new IGesturePart[]
                        {
                            closed, open, closed
                        };
                return new SequenceDependentGesture(parts, identifier, 10);
            }
            //default case:
            return null;

        }

        public List<int> GesturePortfolio
        {
            get
            {
                List<int> identifiers = new List<int>();
                identifiers.Add(GestureIdentifier.LEFT_HAND_OPEN_GESTURE);
                identifiers.Add(GestureIdentifier.LEFT_HAND_CLOSED_GESTURE);
                identifiers.Add(GestureIdentifier.LEFT_HAND_QUICKLY_OPEN_GESTURE);
                identifiers.Add(GestureIdentifier.LEFT_HAND_QUICKLY_CLOSED_GESTURE);
                identifiers.Add(GestureIdentifier.RIGHT_HAND_OPEN_GESTURE);
                identifiers.Add(GestureIdentifier.RIGHT_HAND_CLOSED_GESTURE);
                identifiers.Add(GestureIdentifier.RIGHT_HAND_QUICKLY_OPEN_GESTURE);
                identifiers.Add(GestureIdentifier.RIGHT_HAND_QUICKLY_CLOSED_GESTURE);

                return identifiers;

            }
        }
    }
}
