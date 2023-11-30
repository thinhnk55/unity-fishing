using System;

namespace Framework
{
    public class PDataBlock<T> where T : PDataBlock<T>
    {
        static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = DataHelper.Load<T>(typeof(T).ToString());
                    if (_instance == null)
                        _instance = (T)Activator.CreateInstance(typeof(T));

                    _instance.Init();
                }

                return _instance;
            }
        } 

        protected virtual void Init()
        {
            PGameMaster.OnGamePaused += PGameMaster_OnGamePaused;
            PGameMaster.OnGameQuit += PGameMaster_OnGameQuit;
            PGameMaster.OnClearData += PGameMaster_OnClearData;
            PGameMaster.OnSceneChanged += PGameMaster_OnSceneChanged;
        }

        void PGameMaster_OnClearData()
        {
            Delete();
        }

        void PGameMaster_OnGameQuit()
        {
            Save();
        }

        void PGameMaster_OnGamePaused(bool paused)
        {
            if (paused)
                Save();
        }

        void PGameMaster_OnSceneChanged()
        {
            Save();
        }

        public void Save()
        {
            DataHelper.Save(Instance, typeof(T).ToString());
        }

        public static void Delete()
        {
            _instance = null;

            DataHelper.Delete(typeof(T).ToString());
        }
    }
}