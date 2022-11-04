using System.Collections.Generic;
using UnityEngine;
namespace GameFrameWork.DebugTools
{
    internal class DebugManager : SingleInstance.MonoSingleInstanceDontDestroy<DebugManager>
    {
        public override bool IsNeedRegister => false;
        public Dictionary<DebugTypeEnum, DebugData> m_dic;
        GUIWindowBase m_windows = null;

        Dictionary<string, GUIWindowBase> m_windowsDic = new Dictionary<string, GUIWindowBase>();
        public DebugSaveFileManager m_logFile ;

        ShowFPS m_fpsTip = null;
        bool m_isShow = false;
        public bool debugBattle = false;
        protected override void OnAwake()
        {
            //if (!DebugHelper.DebugWork) return;
            m_dic = new Dictionary<DebugTypeEnum, DebugData>();
            for (var i = DebugTypeEnum.Start; i < DebugTypeEnum.End;i++)
                m_dic.Add(i, new DebugData());

            m_logFile = new DebugSaveFileManager();
            m_fpsTip = gameObject.AddComponent<ShowFPS>();
            ShowFPSTip(DebugHelper.ShowFPS);
        }

        protected override void OnRelease()
        {
            base.OnRelease();
            DebugManager.Instance.m_logFile.Save();
        }
        public DebugData GetData(DebugTypeEnum e)
        {
            if (!m_dic.ContainsKey(e))
                return null;
            return m_dic[e];
        }

        public void ShowFPSTip(bool b)
        {
            m_fpsTip.enabled = b;
        }

        public void CreateDebugWindow()
        {
#if !UNITY_EDITOR
           //m_windowsDic.Add("log", new DebugWindows(new UnityEngine.Rect(0,150,640,880))); 
#endif
        }

        private void OnGUI()
        {
            if (!DebugHelper.DebugWork) return;
            m_isShow = GUI.Toggle(new Rect(10, 1500, 100, 100), m_isShow, "");
            if (m_isShow)
            {

                GUILayout.BeginHorizontal();
                int i = 0;
                foreach (var value in m_windowsDic)
                {

                    if (GUI.Button(new Rect(i * 150, 0, (i + 1) * 150, 150), value.Key))
                    {
                        if (m_windows == value.Value)
                            m_windows = null;
                        else
                            m_windows = value.Value;
                    }

                    i++;
                }


                GUILayout.EndHorizontal();
                if (m_windows != null)
                {
                    m_windows.DrawOnGUI();
                }

                
            }
            else
            {
            }
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

