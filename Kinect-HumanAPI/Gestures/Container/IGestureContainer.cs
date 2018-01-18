using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKotstein.Kinect.API.Gestures
{
    /// <summary>
    /// Gestures observing similar body parts or which might interfer are clustered within implementations of IGestureContainer. 
    /// This interface provides a bundle of methods for handling and accessing those gesture objects. For improving the overall performance and keep the amount of created gesture objects as low as possible, 
    /// a IGestureContainer implementation should consider two stages where those gesture objects are handled annd stored:
    /// Gesture objects should be preloaded by using <see cref="LoadGesture(Gesture,int)"/> (and removed by <see cref="LoadGesture(int)"/>). Only a single object per gesture should be loaded into this stage on demand.
    /// If the first event handler is added to a specific gesture by using <see cref="AddEventHandler(int,EventHandler)"/>, the preloaded gesture object is copied (by-reference) into the stage of active gesture. 
    /// Additonal added event handlers are pooled on a single active gesture. Only if the last event handler of a specific gesture has been removed by calling <see cref="RemoveEventHandler(int,EventHandler)"/> 
    /// the gesture is removed from the stage of active gestures.
    /// Note that loading or removing a gesture has no influence on active gestures with the same idenfitier (and vice versa). But consider that an gesture object being part of the active stage is still 
    /// referenced in the loaded stage (until it is overloaded). Changing parameters of the gesture object passed with <see cref="LoadGesture(Gesture,int)"/> might influence the behaviour of the
    /// corresponding active gesture object (same reference). There existis an default implementation of this interface <see cref="DefaultGestureContainer"/> and an automated version <see cref="AutomatedGestureContainer"/> which is linked to an <see cref="IGestureFactory"/> implementation.
    /// Each implementation is resposible for its own identifiers.
    /// </summary>
    public interface IGestureContainer
    {

        /// <summary>
        /// Loads a gesture with the specified identifier into the stage of preloaded gesture objects.
        /// Note that active gestures with the same identifier are not influenced by this operation.
        /// </summary>
        /// <param name="g">gesture object</param>
        /// <param name="identifier">identifier</param>
        void LoadGesture(Gesture g, int identifier);

        /// <summary>
        /// Removes a gesture with the specified identifier from the stage of preloaded gesture objects.
        /// Note that active gestures with the same identifier are not influenced by this operation
        /// </summary>
        /// <param name="identifier">identifier</param>
        void RemoveGesture(int identifier);

        /// <summary>
        /// Adds an event handler to the specified gesture. Note that this gesture must be loaded before by calling <see cref="LoadGesture(Gesture,int)"/>
        /// </summary>
        /// <param name="identifier">identifier</param>
        /// <param name="handler">event handler</param>
        void AddEventHandler(int identifier, EventHandler<GestureEventArgs> handler);

        /// <summary>
        /// Removes an event handler from the specified gesture.
        /// </summary>
        /// <param name="identifier">identifier</param>
        /// <param name="handler">event handler</param>
        void RemoveEventHandler(int identifier, EventHandler<GestureEventArgs> handler);

        /// <summary>
        /// Updates all contained gesture with new body information
        /// </summary>
        /// <param name="body">body information</param>
        void Update(Body body);

        /// <summary>
        /// Checks whether this container is responsible for the gesture identified over the passed identifier
        /// </summary>
        /// <param name="identifier">gesture identifier</param>
        /// <returns>true or false</returns>
        bool IsContainerResponsibleForGesture(int identifier);
    }
}
