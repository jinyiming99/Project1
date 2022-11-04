using System;


namespace GameFrameWork.DebugTools
{
    /// <summary>
    /// 将日志文件保存为文件
    /// </summary>
    internal class DebugSaveFileManager
    {
        public static long LOG_MAX_SIZE = 1024 * 5 * 1024;//5M
        public static DateTime m_startTime = DateTime.Now;
        private DebugLogSaveFile m_file;
        private int m_index = 0;
        private string m_foldPath = "";
        public DebugSaveFileManager()
        {
#if UNITY_IOS || UNITY_IPHONE

#else
            m_foldPath =  string.Format("{0}_{1}_{2}-{3}-{4}", m_startTime.Month.ToString("00"), 
                m_startTime.Day.ToString("00"), 
                m_startTime.Hour.ToString("00"), 
                m_startTime.Minute.ToString("00"), 
                m_startTime.Second.ToString("00"));
            m_file = new DebugLogSaveFile(m_index++, m_foldPath);
#endif
        }
        public void AddLog(string log)
        {
            if (m_file == null)
                return;
            m_file.Write(log);
            if (m_file.GetSize() > LOG_MAX_SIZE)
            {
                m_file.Save();
                m_file = new DebugLogSaveFile(m_index++, m_foldPath);
            }
        }

        private void Stop()
        {
            if (m_file == null)
                return;
            m_file.Save();
            m_file = new DebugLogSaveFile(m_index++, m_foldPath);
        }
        internal void Save()
        {
            if (m_file == null)
                return;
            m_file.Save();
            m_file = null;
        }
    }
}


