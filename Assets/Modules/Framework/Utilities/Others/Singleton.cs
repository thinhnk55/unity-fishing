using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Framework
{
    public class Singleton<T> where T : class, new()
    {
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new T();
                }
                return instance;
            }
        }
        private static T instance = null;
    }

}