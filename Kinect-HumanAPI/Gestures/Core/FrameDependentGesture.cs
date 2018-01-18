using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKotstein.Kinect.API.Gestures
{
    /// <summary>
    /// A FrameDependentGesture is a gesture consisting of a sequence of gesture parts representing the exact amount and order the incoming body frames must have.
    /// </summary>
    public class FrameDependentGesture : Gesture
    {


        public FrameDependentGesture(IGesturePart[] parts, int identifier) : base(parts, identifier)
        {

        }


        public override void Update(Body body)
        {
            //check next gesture of the sequence
            GesturePartResult result = this.CheckGesturePart(body);

            //if result has been positive
            if (result == GesturePartResult.Succeded)
            {
                //increase counter and check whether the limit has been reached (--> fire event)
                this.CheckSuccededFlow(body);
            }
            //if result has been negative
            else
            {
                //reset counter
                this.Reset();

                //now we want to check this body state aggain, but with the first gesture part in the sequence since we have reset the counter
                //this step is necessary, otherwise the beginning of a possible matching gesture might be lost
                result = this.CheckGesturePart(body);
                if (result == GesturePartResult.Succeded)
                {
                    this.CheckSuccededFlow(body);
                }
            }

        }

        //increase the counter and checks whether the limit has been reached.
        //If so, the event is fired and the counter is reset.
        private void CheckSuccededFlow(Body body)
        {
            //increase counter and check whether gesture is complete
            if (this.IncreaseAndCheckPartCounter())
            {

                //reset counter
                this.Reset();
                //fire event
                this.Fire(body);

            }
        }
    }
}
