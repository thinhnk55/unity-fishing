using System;
using System.Collections.Generic;
using UnityEngine;

namespace Framework
{
    public static class StatisticTrackingSystem
    {
        public class statisticUnit
        {
            public StatisticType Type { get; private set; }
            public int Progress { get; private set; }
            public statisticUnit(StatisticType type)
            {
                Type = type;
                statisticTrackers[Type].Item2.Add(this);
                type.AddListenerOnProgress(OnProgress);

            }
            public statisticUnit(StatisticType type, int initProgress)
            {
                Type = type;
                Progress = initProgress;
                statisticTrackers[Type].Item2.Add(this);
                type.AddListenerOnProgress(OnProgress);
            }
            public void Dispose()
            {
                statisticTrackers[Type].Item2.Remove(this);
                Type.RemoveListenerOnProgress(OnProgress);
            }

            protected virtual void OnProgress(int o, int n)
            {
                Progress += (n - o);
            }
        }

        static readonly public Dictionary<StatisticType, Tuple<ObservableDataFull<int>, List<statisticUnit>>> statisticTrackers = new();
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        static void Init()
        {
            foreach (StatisticType value in Enum.GetValues(typeof(StatisticType)))
            {
                value.TrackStatistic();
            }

        }
        static void TrackStatistic(this StatisticType requireTypes)
        {
            statisticTrackers.Add(requireTypes, new Tuple<ObservableDataFull<int>, List<statisticUnit>>
                (new ObservableDataFull<int>(0), new List<statisticUnit>()));
        }
        static void AddListenerOnProgress(this StatisticType requireTypes, Callback<int, int> onProgress)
        {
            statisticTrackers[requireTypes].Item1.OnDataChanged += onProgress;
        }
        static void RemoveListenerOnProgress(this StatisticType requireTypes, Callback<int, int> onProgress)
        {
            statisticTrackers[requireTypes].Item1.OnDataChanged -= onProgress;
        }
        public static void DisposeAllTrackees()
        {
            foreach (StatisticType statistic in Enum.GetValues(typeof(StatisticType)))
            {
                foreach (var trackee in statisticTrackers[statistic].Item2)
                {
                    trackee.Dispose();
                }
            }
        }
        public static void SetProgress(this StatisticType requireTypes, int progress)
        {
            statisticTrackers[requireTypes].Item1.Data = progress;
        }
        public static void Progress(this StatisticType requireTypes, int progress)
        {
            statisticTrackers[requireTypes].Item1.Data = statisticTrackers[requireTypes].Item1.Data + progress;
        }
    }
}
