using UnityEngine;
using System.Collections;
namespace GameFrameWork
{
    /// <summary>
    /// gui的界面基类
    /// </summary>
    public abstract class GUIWindowBase
    {
        protected Rect m_rect;
        protected int m_id;
        protected bool m_isShow = false;
        protected bool m_isStatic = false;
        protected string m_title = "";
        public GUIWindowBase(Rect rect)
        {
            m_rect = rect;
            m_isShow = false;
        }

        public void DrawOnGUI()
        {
            //m_isShow = GUI.Toggle(new Rect(10, 610, 100, 100), m_isShow, m_title);
            //if (m_isShow)
            //{
                GUI.Window(0, m_rect, Draw, m_title);
            //}
        }
        private void Draw(int i)
        {
            DrawWindow();
        }
        protected virtual void DrawWindow() { }

    }
}

