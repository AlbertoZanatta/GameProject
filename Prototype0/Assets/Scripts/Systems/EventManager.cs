using System.Collections;
using System.Collections.Generic; //dictionary to get a hold of events
using UnityEngine.Events; //necessary to use events
using UnityEngine;

public class EventManager : MonoBehaviour {

    private Dictionary<string, UnityEvent> eventDictionary;

    //metods inside the that will accept new events and listeners to come in.
    //If there is no entry in the dictionary we create one, if there is already an entry we'll add to it.
    //We also want to give the opportunity to unregister/unlisten to events when we're done with them
    private static EventManager eventManager;

    public static EventManager instance
    {
        get
        {
            if (!eventManager)
            {
                //if we don't have a reference to the instance we get ahead and find it
                eventManager = FindObjectOfType(typeof(EventManager)) as EventManager;

                if (!eventManager)
                {
                    Debug.LogError("There needs to be one active EventManger script on a GameObject in your scene.");
                }
                else
                {
                    eventManager.Init();
                }
            }

            return eventManager;
        }
    }

    void Init()
    {   //If we didn't find the EventManager the first time, it means that we need to init its events list
        if (eventDictionary == null)
        {
            //this means we have an initialized dictionary.
            eventDictionary = new Dictionary<string, UnityEvent>();
        }
    }

    //Methods for start listening and stop listening to events
                                                         //Function pointer that serves as a listener
    public static void StartListening(string eventName, UnityAction listener)
    {
        UnityEvent thisEvent = null; //as faster and as efficient as ContainsKey, use to access keys that can be not found in the dictionary.
                                     //more efficient than catching the KeyNotFound exception
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            //the first time we are creating a certain kind of event
            thisEvent = new UnityEvent();
            thisEvent.AddListener(listener);
            instance.eventDictionary.Add(eventName, thisEvent);
        }
    }

    public static void StopListening(string eventName, UnityAction listener)
    {
        //if we are trying to stop listening and the dictionary has already gone, we do not want to get in a null ref exception
        if (eventManager == null) return;
        UnityEvent thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }

    //Finally, a public method to Trigger events in the game!
    public static void TriggerEvent(string eventName)
    {
        UnityEvent thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            //The event will call all the functions listening to it
            thisEvent.Invoke();
        }
    }

}
