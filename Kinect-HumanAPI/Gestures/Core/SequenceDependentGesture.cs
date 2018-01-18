using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKotstein.Kinect.API.Gestures
{
    /// <summary>
    /// A FrameDependentGesture is a gesture consisting of a sequence of gesture parts representing the order the incoming body frames must have, but gives no details how many frames must have the state defined in each gesture part.
    /// Nevertheless the maximum amount of analyzed frames is bounded by the MAX_WINDOW_SIZE. If this value is exceeded, the detection will be reset.
    /// </summary>
    public class SequenceDependentGesture : Gesture
    {

        //amount of analyzed frames
        private int _frameCounter = 0;

        //maximum amount of frames
        private readonly int MAX_WINDOW_SIZE;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="parts"></param>
        /// <param name="max_window_size">specifies the maximum amount of analyzed frames before a reset is performed</param>
        public SequenceDependentGesture(IGesturePart[] parts, int identifier, int max_window_size) : base(parts, identifier)
        {
            this.MAX_WINDOW_SIZE = max_window_size;
        }

        public override void Update(Microsoft.Kinect.Body body)
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

                //check whether maximal amount of frames has been arrived
                if (++_frameCounter == MAX_WINDOW_SIZE)
                {
                    //reset counter
                    this.Reset();

                    //check this frame again, maybe it might be the start of a new gesture:
                    //DO NOT USE RECURSITION since this might lead into a stack overflow!
                    result = this.CheckGesturePart(body);
                    if (result == GesturePartResult.Succeded)
                    {
                        CheckSuccededFlow(body);

                        // it is not necessary to check the boundaries of the array since we know, that this array has more than 2 fields
                    }
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

        //reset the frame counter as well
        public override void Reset()
        {
            base.Reset();
            this._frameCounter = 0;

        }

    }
}
