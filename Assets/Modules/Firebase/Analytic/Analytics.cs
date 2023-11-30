#if FIREBASE
using Firebase.Analytics;
using System.Collections.Generic;
using UnityEngine;
namespace FirebaseIntegration
{
    public class Analytics
    {
        public static void Log(string @event, List<KeyValuePair<string, object>> @params)
        {
            Firebase.Analytics.Parameter[] parameters = new Firebase.Analytics.Parameter[@params.Count];
            int i = 0;
            foreach (var param in @params)
            {
                if (param.Value is double)
                {
                    parameters[i] = new Parameter(param.Key, (double)param.Value);
                }
                else if (param.Value is long)
                {
                    parameters[i] = new Parameter(param.Key, (long)param.Value);
                }
                else if (param.Value is float)
                {
                    parameters[i] = new Parameter(param.Key, (float)param.Value);
                }
                else if (param.Value is int)
                {
                    parameters[i] = new Parameter(param.Key, (int)param.Value);
                }
                else
                {
                    parameters[i] = new Parameter(param.Key, param.Value.ToString());
                }
                i++;
            }
            Firebase.Analytics.FirebaseAnalytics.LogEvent(@event, parameters);
            Debug.Log(@event);
        }
    }
}
#endif