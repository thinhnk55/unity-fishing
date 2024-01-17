using System;
using UnityEngine;

namespace Framework
{
    public abstract class Timee<U> : MonoBehaviour where U : Timee<U>
    {
        protected virtual void Awake()
        {
            Timer<U>.Instance.OnTrigger += OnTrigger;
            Timer<U>.Instance.OnElapse += OnElapse;
        }
        protected virtual void OnDestroy()
        {
            Timer<U>.Instance.OnTrigger -= OnTrigger;
            Timer<U>.Instance.OnElapse -= OnElapse;
        }
        protected virtual void Update()
        {
            Timer<U>.Instance.Elasping();
        }
        protected abstract void OnTrigger();
        protected abstract void OnElapse();
    }
    public class Timer<T> : Singleton<Timer<T>> where T : Timee<T>
    {
        private int triggerInterval_Sec; public int TriggerInterval_Sec { get { return triggerInterval_Sec; } set { triggerInterval_Sec = Mathf.Clamp(value, 1, int.MaxValue); } }
        private long beginPoint; public long BeginPoint
        {
            get { return beginPoint; }
            set
            {
                beginPoint = value;
                elapse = 0;
                MarkedPoint = value;
            }
        }
        private long markedPoint; public long MarkedPoint
        {
            get { return markedPoint; }
            set { markedPoint = value; }
        }

        private float elapse; public float ELaspe
        {
            get { return elapse; }
            set
            {
                elapse = value;
                if (elapse >= 1)
                {
                    elapse -= 1;
                    if (Remain_Sec - 1 == 0)
                    {
                        OnTrigger?.Invoke();
                    }
                }
            }
        }

        public long Elasped_Tick { get { return DateTime.UtcNow.Ticks - beginPoint; } }
        public long Residal_Sec { get { return Elasped_Tick.ToSecond() % triggerInterval_Sec; } }
        public long Remain_Sec { get { return Math.Clamp(TriggerInterval_Sec - Residal_Sec, 0, TriggerInterval_Sec); } }
        public int TotalTriggers { get { return Elasped_Tick.ToSecond() / triggerInterval_Sec; } }
        public int MarkedTriggers { get { return TotalTriggers - ((markedPoint - beginPoint).ToSecond() / triggerInterval_Sec); } }

        public Callback OnTrigger;
        public Callback OnElapse;

        public void Elasping()
        {
            ELaspe += Time.deltaTime;
            OnElapse?.Invoke();
        }
        public void Begin(long? tick = null)
        {
            if (tick.HasValue)
            {
                BeginPoint = tick.Value;
            }
            else
            {
                BeginPoint = DateTime.UtcNow.Ticks;
            }
        }
        public void Mark()
        {
            MarkedPoint = DateTime.UtcNow.Ticks;
        }
    }
}