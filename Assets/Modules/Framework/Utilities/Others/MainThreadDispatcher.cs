using System;
using System.Collections.Concurrent;
using UnityEngine;

public class MainThreadDispatcher : SingletonMono<MainThreadDispatcher>
{
    private readonly ConcurrentQueue<Action> actions = new ConcurrentQueue<Action>();
    private void Update()
    {
        while (actions.TryDequeue(out var action))
        {
            action.Invoke();
        }
    }

    public static void ExecuteOnMainThread(Action action)
    {
        Instance.actions.Enqueue(action);
    }
}