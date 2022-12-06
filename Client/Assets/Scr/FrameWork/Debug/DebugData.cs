using System.Collections.Generic;
using UnityEngine;

namespace GameFrameWork.DebugTools
{
    internal class DebugData
    {
        const int MAX_LENGTH = 200;
        public List<DebugNodeData> m_data;

        public DebugData()
        {
            m_data = new List<DebugNodeData>();
        }
        public void Add(string data)
        {
            DebugNodeData d = new DebugNodeData(data);
            m_data.Add(d);
            if (m_data.Count > MAX_LENGTH)
            {
                m_data.RemoveAt(0);
            }
            //#if UNITY_ANDROID || U
            //            DebugSaveFileManager.instance.AddLog(d.ToString());
            //#endif
        }
    }
}


