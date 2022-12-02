using System;
using System.Collections.Generic;
using UnityEngine;
namespace GameFrameWork.DebugTools
{
    internal class DebugManager : MonoBehaviour
    {
        
        private static bool isDebug = false;
        /// <summary>
        /// ÊÇ·ñ¹¤×÷
        /// </summary>
        public static bool DebugWork
        {
            get
            {
                return isDebug;
            }
            set
            {
                isDebug = value;
            }
        }
        internal enum DebugType
        {
            Log,
            Warning,
            Error,
        }
        /// <summary>
        /// ÅäÖÃ
        /// </summary>
        private DebugManagerConfig m_config = new DebugManagerConfig() { m_bShowFPS = true,m_bSaveLog = true, m_bDebugWork = true, m_bShowDebugWindow = true };

        public DebugManagerConfig Config => m_config;
        public Dictionary<DebugTypeEnum, DebugData> m_dic;
        GUIWindowBase m_windows = null;

        public DebugSaveFileManager m_logFile ;
        
        bool m_isShow = false;

        protected void Awake()
        {
            m_logFile = new DebugSaveFileManager();
        }

        protected void OnDestroy()
        {
            m_logFile.Save();
        }
        // private void OnGUI()
        // {
        //     if (!DebugWork) return;
        //     m_isShow = GUI.Toggle(new Rect(10, 1500, 100, 100), m_isShow, "");
        //     if (m_isShow)
        //     {
        //
        //         GUILayout.BeginHorizontal();
        //         int i = 0;
        //         foreach (var value in m_windowsDic)
        //         {
        //
        //             if (GUI.Button(new Rect(i * 150, 0, (i + 1) * 150, 150), value.Key))
        //             {
        //                 if (m_windows == value.Value)
        //                     m_windows = null;
        //                 else
        //                     m_windows = value.Value;
        //             }
        //
        //             i++;
        //         }
        //
        //
        //         GUILayout.EndHorizontal();
        //         if (m_windows != null)
        //         {
        //             m_windows.DrawOnGUI();
        //         }
        //     }
        // }

        public void Log(string str)
        {
            LogData(str, DebugType.Log);
        }
        
        public void LogWarning(string str)
        {
            LogData(str, DebugType.Warning);
        }
        
        public void LogError(string str)
        {
            LogData(str, DebugType.Error);
        }


        private void LogData(string str, DebugType type)
        {
            if (m_config.m_bSendLog)
            {

            }
            if (m_config.m_bSaveLog)
                m_logFile.AddLog(str);
        }

        public void AddData(DebugTypeEnum e, string data)
        {
            if (m_dic == null) return;
            if (!m_dic.ContainsKey(e))
            {
                m_dic.Add(e, new DebugData());
            }

            m_dic[e].Add(data);
        }
    }
}

