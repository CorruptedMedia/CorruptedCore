using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if NAUGHTY_EXISTS
using NaughtyAttributes;
#endif

namespace Corrupted
{

    [CreateAssetMenu(fileName ="CorruptedEvent", menuName = "Corrupted/Events/Event", order = 0)]
    public class CorruptedEvent : CorruptedModel
    {
        System.Action actionListeners;
        private List<CorruptedEventListener> listeners = new List<CorruptedEventListener>();

        [TextArea]
        [SerializeField]
        string desciption;
        [SerializeField] bool fireOnce = false;
        bool hasFired = false;

#if NAUGHTY_EXISTS
        [Button]
#endif
        public void Raise()
        {
            if (fireOnce && hasFired)
                return;//Stop this event from firing more than designed
            for (int i = listeners.Count-1; i >= 0; i--)
            {
                listeners[i]?.OnEventRaised();
            }
            actionListeners?.Invoke();
        }

        /// <summary>
        /// If this is a fire once event, we can reset it to indicate it can be fired again
        /// </summary>
        public void ResetEvent()
        {
            hasFired = false;
        }

        private void OnEnable()
        {
            //Debug.Log($"CorruptedEvent: {name} has cleared its listeners!");
            listeners.Clear();
        }

        //IEnumerator RaiseEvent()
        //{
        //    yield return null;
            
        //}

        public void RegisterListener(CorruptedEventListener listener)
        {
            listeners.Add(listener);
        }

        public void UnRegisterListener(CorruptedEventListener listener)
        {
            listeners.Remove(listener);
        }

        public void RegisterListener(System.Action listener)
        {
            actionListeners += listener;
        }

        public void UnRegisterListener(System.Action listener)
        {
            actionListeners -= listener;
        }
    }
}
