using System;
using UnityEngine;

namespace FrameWork
{
    public struct FloatTimeCounter
    {
        float m_startTime ;
        float m_endTime ;

        public static FloatTimeCounter Now
        {
            get => new FloatTimeCounter() { m_startTime = Time.time, m_endTime = 0f };
        }

        public static FloatTimeCounter Zero
        {
            get => new FloatTimeCounter() {m_startTime = 0f, m_endTime = 0f};
        }

        public FloatTimeCounter(float endtime)
        {
            m_startTime = Time.time;        
            m_endTime = endtime;
        }

        public float GetPassTime()
        {
            return Time.time - m_startTime;        
        }
        public float GetEndTime()
        {
            return m_endTime - GetPassTime();
        }
        public void AddStartTime(float time)
        {
            m_startTime += time;
        }
        public void ResetCounter()
        {
            m_startTime = Time.time;        
        }
    }

    public class LongTimeCounter
    {
        private long m_startTime;
        private long m_endTime;

        public long StartTime
        {
            get => m_startTime;
            set => m_startTime = value;
        }
        
        public long EndTime
        {
            get => m_endTime;
            set => m_endTime = value;
        }

        public static LongTimeCounter Now
        {
            get => new LongTimeCounter() {m_startTime = System.DateTime.Now.Ticks};
        }

        public static LongTimeCounter CreateCountDownSecond(int second)
        {
            var now = Now;
            now.m_endTime = now.m_startTime + second * TimeSpan.TicksPerSecond;
            return now;
        }
        
        

        public void Zero()
        {
            m_startTime = 0L;
            m_endTime = 0L;
        }

        public float GetPassTime()
        {
            float f = (System.DateTime.Now.Ticks - m_startTime) / TimeSpan.TicksPerSecond;
            return f;
        }

        public long GetPassLongTime()
        {
            long f = (System.DateTime.Now.Ticks - m_startTime);
            return f;
        }

        public float GetCountDownTime()
        {
            return (m_endTime - System.DateTime.Now.Ticks) / TimeSpan.TicksPerSecond;
        }

        public long GetCountDownLongTime()
        {
            return (m_endTime - System.DateTime.Now.Ticks) ;
        }
        public void ResetCounter()
        {

            m_startTime = System.DateTime.Now.Ticks;
        }
    }
}


