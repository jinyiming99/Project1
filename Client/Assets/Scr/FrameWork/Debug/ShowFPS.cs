using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GameFrameWork.DebugTools
{
    internal class ShowFPS : MonoBehaviour
    { 
        public float updateInterval = 0.5f;
        private float lastInterval;
        private int frames = 0;
        private float fps;
        GUIStyle m_style;

        void Start()
        {
            //设置帧率
            Application.targetFrameRate = 60;
            QualitySettings.vSyncCount = 0;
            lastInterval = Time.realtimeSinceStartup;
            frames = 0;
            m_style = new GUIStyle();
            m_style.normal.background = null;    //这是设置背景填充的
            m_style.normal.textColor = Color.red; //设置字体颜色的
            m_style.fontSize = 40;       //当然，这是字体颜色
        }

        // Update is called once per frame  
        void Update()
        {
            ++frames;
            float timeNow = Time.realtimeSinceStartup;
            if (timeNow >= lastInterval + updateInterval)
            {
                fps = frames / (timeNow - lastInterval);
                frames = 0;
                lastInterval = timeNow;
            }

        }

        void OnGUI()
        {
            var color = GUI.color;
            GUI.color = Color.red;

            GUI.Label(new Rect(200, 40, 100, 100), fps.ToString("f2"), m_style);
            GUI.color = color;
        }
    }
}