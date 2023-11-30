using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Framework
{
    public static class Messenger<T> where T : Enum
    {
        #region Internal variables

        public static Dictionary<T, Dictionary<string, Delegate>> eventTable = new() { };
        //Message handlers (index) that should never be removed, regardless of calling Cleanup
        private static List<T> permanentMessages = new List<T>();

        #endregion

        #region Helper methods

        //Marks a certain message as permanent.
        public static void MarkAsPermanent(T gameEvent)
        {
            permanentMessages.Add(gameEvent);
        }

        public static void Cleanup()
        {
            foreach (var _event in eventTable)
            {
                if (!permanentMessages.Contains(_event.Key))
                    eventTable[_event.Key] = null;
            }
        }

        public static void PrintEventTable()
        {
            PDebug.Log("\t\t\t=== MESSENGER PrintEventTable ===");

            foreach (var _event in eventTable)
            {
                PDebug.Log("Event:{0}|{1}", _event, eventTable[_event.Key]);
            }

            PDebug.Log("\n");
        }

        #endregion

        #region Message logging and exception throwing

        static void OnListenerAdding(T _event, string keyParam, Delegate listenerBeingAdded)
        {
            Delegate d = eventTable[_event][keyParam];
            if (d != null && d.GetType() != listenerBeingAdded.GetType())
            {
                PDebug.LogError("Attempting to add listener with inconsistent signature for event type {0}. Current listeners have type {1} and listener being added has type {2}", _event, d.GetType().Name, listenerBeingAdded.GetType().Name);
            }
        }

        static void OnListenerRemoving(T _event, string keyParam, Delegate listenerBeingRemoved)
        {
            Delegate d = eventTable[_event][keyParam];

            if (d == null)
            {
                PDebug.LogWarning("Attempting to remove listener with for event type \"{0}\" but current listener is null.", _event);
            }
            else if (d.GetType() != listenerBeingRemoved.GetType())
            {
                PDebug.LogError("Attempting to remove listener with inconsistent signature for event type {0}. Current listeners have type {1} and listener being removed has type {2}", _event, d.GetType().Name, listenerBeingRemoved.GetType().Name);
            }
        }

        static void OnBroadcasting(T _event, Delegate broadcastMessage)
        {
            PDebug.LogError("Broadcasting message \"{0}\" but listeners have a different signature than the broadcaster.", _event);
        }

        #endregion

        #region AddListener

        //No parameters
        static public void AddListener(T gameEvent, Callback handler, bool isPermanent = true)
        {

            OnListenerAdding(gameEvent, "", handler);
            eventTable[gameEvent][""] = (Callback)eventTable[gameEvent][""] + handler;
            if (isPermanent)
            {
                permanentMessages.Add(gameEvent);
            }
        }

        //Single parameter
        static public void AddListener<M>(T gameEvent, Callback<M> handler, bool isPermanent = true)
        {
            string keyParam = typeof(M).ToString();
            OnListenerAdding(gameEvent, keyParam, handler);
            eventTable[gameEvent][keyParam] = (Callback<M>)eventTable[gameEvent][keyParam] + handler;
            if (isPermanent)
            {
                permanentMessages.Add(gameEvent);
            }
        }

        //Two parameters
        static public void AddListener<M, U>(T gameEvent, Callback<M, U> handler, bool isPermanent = true)
        {
            string keyParam = typeof(M).ToString() + typeof(U).ToString();
            OnListenerAdding(gameEvent, keyParam, handler);
            eventTable[gameEvent][keyParam] = (Callback<M, U>)eventTable[gameEvent][keyParam] + handler + handler;
            if (isPermanent)
            {
                permanentMessages.Add(gameEvent);
            }
        }

        //Three parameters
        static public void AddListener<M, U, V>(T gameEvent, Callback<M, U, V> handler, bool isPermanent = true)
        {
            string keyParam = typeof(M).ToString() + typeof(U).ToString() + typeof(V).ToString();
            OnListenerAdding(gameEvent, keyParam, handler);
            eventTable[gameEvent][keyParam] = (Callback<M, U, V>)eventTable[gameEvent][keyParam] + handler;
            if (isPermanent)
            {
                permanentMessages.Add(gameEvent);
            }
        }

        #endregion

        #region RemoveListener

        //No parameters
        static public void RemoveListener(T gameEvent, Callback handler)
        {
            OnListenerRemoving(gameEvent, "", handler);
            eventTable[gameEvent][""] = (Callback)eventTable[gameEvent][""] - handler;
        }

        //Single parameter
        static public void RemoveListener<M>(T gameEvent, Callback<M> handler)
        {
            string keyParam = typeof(M).ToString();
            OnListenerRemoving(gameEvent, keyParam, handler);
            eventTable[gameEvent][keyParam] = (Callback<M>)eventTable[gameEvent][keyParam] - handler;
        }

        //Two parameters
        static public void RemoveListener<M, U>(T gameEvent, Callback<M, U> handler)
        {
            string keyParam = typeof(M).ToString() + typeof(U).ToString();
            OnListenerRemoving(gameEvent, keyParam, handler);
            eventTable[gameEvent][keyParam] = (Callback<M, U>)eventTable[gameEvent][keyParam] - handler;
        }

        //Three parameters
        static public void RemoveListener<M, U, V>(T gameEvent, Callback<M, U, V> handler)
        {
            string keyParam = typeof(M).ToString() + typeof(U).ToString() + typeof(V).ToString();
            OnListenerRemoving(gameEvent, keyParam, handler);
            eventTable[gameEvent][keyParam] = (Callback<M, U, V>)eventTable[gameEvent][keyParam] - handler;
        }

        #endregion

        #region Broadcast

        //No parameters
        static public void Broadcast(T gameEvent)
        {
            if (eventTable[gameEvent] != null && eventTable[gameEvent][""] != null)
                ((Callback)eventTable[gameEvent][""])?.Invoke();
        }

        //Single parameter
        static public void Broadcast<M>(T gameEvent, M arg1)
        {
            string keyParam = typeof(M).ToString();
            if (eventTable[gameEvent] != null && eventTable[gameEvent][keyParam] != null)
                ((Callback<M>)eventTable[gameEvent][keyParam])?.Invoke(arg1);
        }

        //Two parameters
        static public void Broadcast<M, U>(T gameEvent, M arg1, U arg2)
        {
            string keyParam = typeof(M).ToString() + typeof(U).ToString();
            if (eventTable[gameEvent] != null && eventTable[gameEvent][keyParam] != null)
                ((Callback<M, U>)eventTable[gameEvent][keyParam])?.Invoke(arg1, arg2);
        }

        //Three parameters
        static public void Broadcast<M, U, V>(T gameEvent, M arg1, U arg2, V arg3)
        {
            string keyParam = typeof(M).ToString() + typeof(U).ToString() + typeof(V).ToString();
            if (eventTable[gameEvent] != null && eventTable[gameEvent][keyParam] != null)
                ((Callback<M, U, V>)eventTable[gameEvent][keyParam])?.Invoke(arg1, arg2, arg3);
        }

        #endregion

        #region Messenger behaviour

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        static void RuntimeInit()
        {
            SceneManager.sceneLoaded += SceneLoadedCallback;
        }

        static void SceneLoadedCallback(Scene scene, LoadSceneMode mode)
        {
            // Clear event table every time scene changed
            Cleanup();
        }

        #endregion
    }
}