using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKotstein.Kinect.API.Gestures
{
    /// <summary>
    /// AutomatedGestureContainer is a special implementation of the IGestureContainer interface. 
    /// Instead of preloading gesture objects manually, gesture objects are created once and loaded only if an event handler should be added.Thus <see cref="LoadGesture(Gesture,int)"/> and <see cref="RemoveGesture(int)"/> are NOT IMPLEMENTED.
    /// Those methods have and must not be called. The responsible factory for creating those gesture objects is passed while creating this container (see constructor <see cref="AutomatedGestureContainer(IGestureFactory)"/>).
    /// </summary>
    public class AutomatedGestureContainer : IGestureContainer
    {

        //Dictionary of loaded gestures with Key=identifier, Value=gesture
        protected IDictionary<int, Gesture> _loadedGestures = new Dictionary<int, Gesture>();

        //List of active gestures
        //first list contains the gesture elements (value)
        protected List<Gesture> _activeGestures = new List<Gesture>();
        //second list contains the associtated identifiers (key)
        protected List<int> _identifiersOfActiveGestures = new List<int>();

        //factory for automated creation of gestures
        protected IGestureFactory _factory;

        public AutomatedGestureContainer(IGestureFactory factory)
        {
            this._factory = factory;
        }

        public void LoadGesture(Gesture g, int identifier)
        {
            throw new NotImplementedException();
        }

        public void RemoveGesture(int identifier)
        {
            throw new NotImplementedException();
        }

        public void AddEventHandler(int identifier, EventHandler<GestureEventArgs> handler)
        {
            //if the gesture is active and in use
            if (_identifiersOfActiveGestures.Contains(identifier))
            {
                _activeGestures[_identifiersOfActiveGestures.IndexOf(identifier)].GestureRecognized += handler;
            }
            else
            {
                //if not....
                //check whether Gesture has already been created:
                if (!_loadedGestures.ContainsKey(identifier))
                {
                    _loadedGestures.Add(identifier, _factory.CreateGesture(identifier));
                }

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

        //if the gesture can be created by the underlying factory, it is automatically scoped by this container
        public bool IsContainerResponsibleForGesture(int identifier)
        {
            return _factory.GesturePortfolio.Contains(identifier);
        }
    }
}
