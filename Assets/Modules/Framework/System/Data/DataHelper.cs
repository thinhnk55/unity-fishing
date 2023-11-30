using Sirenix.Serialization;
using System;
using System.IO;
using UnityEngine;

namespace Framework
{
    public static class DataHelper
    {
        public static void Save<T>(T data, string filePath)
        {
            byte[] bytes = SerializationUtility.SerializeValue(data, DataFormat.Binary);

            File.WriteAllBytes(Path.Combine(Application.persistentDataPath, filePath), bytes);
        }

        public static T Load<T>(string filePath) where T : class
        {
            try
            {
                string path = Path.Combine(Application.persistentDataPath, filePath);
                if (!File.Exists(path))
                    return null;

                byte[] bytes = File.ReadAllBytes(path);

                return SerializationUtility.DeserializeValue<T>(bytes, DataFormat.Binary);
            }
            catch (Exception e)
            {
                PDebug.Log("Something wrong when load data: {0}", e);
                return null;
            }
        }

        public static void Delete(string filePath)
        {
            string path = Path.Combine(Application.persistentDataPath, filePath);
            if (!File.Exists(path))
                return;

            File.Delete(path);
        }
    }
}