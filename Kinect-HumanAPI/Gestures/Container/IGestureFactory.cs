using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKotstein.Kinect.API.Gestures
{
    /// <summary>
    /// Implementations of IGestureFactory are responsible for creating gesture objects. Note that each implementation is responsible for its own set of identifiers.  
    /// </summary>
    public interface IGestureFactory
    {
        /// <summary>
        /// Create a gesture object specified by the gesture identifier
        /// </summary>
        /// <param name="identifier">identifier of the gesture</param>
        /// <returns>the gesture object or null, if the identifier is unknown</returns>
        Gesture CreateGesture(int identifier);

        /// <summary>
        /// Returns a list of identifiers of gestures which can be created by this factory
        /// </summary>
        /// <returns>list with identifiers</returns>
        List<int> GesturePortfolio { get; }
    }
}
