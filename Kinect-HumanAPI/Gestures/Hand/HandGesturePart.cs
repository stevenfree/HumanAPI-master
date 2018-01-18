using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKotstein.Kinect.API.Gestures
{/// <summary>
 /// HandGesturePart represents a single segment of a complex hand gesture which should be recognized. 
 /// </summary>
    public class HandGesturePart : IGesturePart
    {
        //true for left hand, false for right hand
        private readonly bool _leftHand;

        private readonly HandState _successState;

        /// <summary>
        /// Creates a HandGesturePart object with the specified characteristics:
        /// By setting the leftHand flag, the hand being analyzed can be chosen. 
        /// The state which sould lead into a success result must be assigned over the successState parameter
        /// </summary>
        /// <param name="leftHand">Flag determines which hand is analyzed: true for the left hand, false for the right hand</param>
        /// <param name="successState">HandState which must be detected for achiving a success result </param>
        public HandGesturePart(bool leftHand, HandState successState)
        {
            this._leftHand = leftHand;
            this._successState = successState;

        }

        /// <summary>
        /// Checks whether the hand state of the passed body matches with the defined hand state.
        /// The result is <see cref="GesturePartResult.Succeded"/> if the defined successState (see <see cref="HandGesturePart(bool,HandState)"/>) matches with the actual hand state.
        /// The result is <see cref="GesturePartResult.Undetermined"/> for <see cref="HandState.NotTracked"/> and <see cref="HandState.Unknown"/> or if the passed body object is null.
        /// <see cref="GesturePart.Failed"/> is returned for any other <see cref="HandState"/>s.
        /// </summary>
        /// <param name="body">analyzed body</param>
        /// <returns>the result of the state analyzation</returns>
        public GesturePartResult CheckGesturePart(Body body)
        {
            //if the body is null, return undeteminded if the passed body object is null
            if (body == null)
            {
                return GesturePartResult.Undetermined;
            }
            else
            {
                //load hand state 
                HandState state = _leftHand ? body.HandLeftState : body.HandRightState;

                //success case
                if (state == _successState)
                {
                    return GesturePartResult.Succeded;

                }
                //undetermined if handstate is not tracked or unknown
                else if (state == HandState.NotTracked || state == HandState.Unknown)
                {
                    return GesturePartResult.Undetermined;
                }
                //otherwise the analyzation has a failed result
                else
                {
                    return GesturePartResult.Failed;
                }
            }

        }
    }
}
