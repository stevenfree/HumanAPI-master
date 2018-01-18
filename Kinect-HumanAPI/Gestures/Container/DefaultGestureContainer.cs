using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKotstein.Kinect.API.Gestures.Container
{
    /// <summary>
    /// DefaultGestureContainer is a default implementation for the IGestureContainer interface. Attributes of this class are protected such that this class can be extended by inheritance.
    /// </summary>
    public class DefaultGestureContainer : IGestureContainer
    {

        //Dictionary of loaded gestures with Key=identifier, Value=gesture
        protected IDictionary<int, Gesture> _loadedGestures = new Dictionary<int, Gesture>();

        //List of active gestures
        //first list contains the gesture elements (value)
        protected List<Gesture> _activeGestures = new List<Gesture>();
        //second list contains the associtated identifiers (key)
        protected List<int> _identifiersOfActiveGestures = new List<int>();


        public void LoadGesture(Gesture g, int identifier)
        {
            _loadedGestures.Add(identifier, g);
        }

        public void RemoveGesture(int identifier)
        {
            _loadedGestures.Remove(identifier);
        }

        public void AddEventHandler(int identifier, EventHandler<GestureEventArgs> handler)
        {
            if (_identifiersOfActiveGestures.Contains(identifier))
            {
                _activeGestures[_identifiersOfActiveGestures.IndexOf(identifier)].GestureRecognized += handler;
            }
            else
            {
                //load gesture from dictionay
                Gesture g = _loadedGestures[identifier];
                //reset gesture object
                g.Reset();
                //add it to active list
                _activeGestures.Add(g);
                _identifiersOfActiveGestures.Add(identifier);
                //add handler
                g.GestureRecognized += handler;
            }
        }

        public void RemoveEventHandler(int identifier, EventHandler<GestureEventArgs> handler)
        {
            if (_identifiersOfActiveGestures.Contains(identifier))
            {
                _activeGestures[_identifiersOfActiveGestures.IndexOf(identifier)].GestureRecognized -= handler;

                //check whether event handler is empty now
                if (_activeGestures[_identifiersOfActiveGestures.IndexOf(identifier)].IsEventHandlerEmpty())
                {
                    int index = _identifiersOfActiveGestures.IndexOf(identifier);
                    _identifiersOfActiveGestures.Remove(identifier);
                    _activeGestures.RemoveAt(index);
                }

            }
        }

        public void Update(Microsoft.Kinect.Body body)
        {
            for (int i = 0; i < _activeGestures.Count; i++)
            {
                _activeGestures[i].Update(body);
            }
        }

        public bool IsContainerResponsibleForGesture(int identifier)
        {
            return _loadedGestures.ContainsKey(identifier);
        }
    }
}
