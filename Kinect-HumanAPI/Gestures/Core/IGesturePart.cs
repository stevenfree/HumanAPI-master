using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKotstein.Kinect.API.Gestures
{
    /// <summary>
    /// A gesture is a sequence of specific states of the analyzed body. Each state must occur or even better said, must match in the right order for recognizing the whole gesture.
    /// Each gesture part is represented by a single class implementing this interface. IGesturePart defines a method <see cref="CheckGesturePart(Body)"/> which ckecks whether the body state matches.
    /// </summary>
    public interface IGesturePart
    {
        /// <summary>
        /// Checks whether the body state matches with the defined gesture part state. If so, the method returns <see cref="GesturePartResult.Succeded"/>, if not <see cref="GesturePartResult.Failed"/>.
        /// If the passed body object is null or if the body state is undetermineable it returns <see cref="GesturePartResult.Undetermined"/>.
        /// </summary>
        /// <param name="body">analyzed body</param>
        /// <returns>the result of the state analyzation</returns>
        GesturePartResult CheckGesturePart(Body body);

    }

    /// <summary>
    /// Enum representing those three result states of the <see cref="IGesturePart.GesturePartResult(body)"/> method.
    /// </summary>
    public enum GesturePartResult
    {
        Failed,
        Succeded,
        Undetermined
    }
}
