using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Reflection;
using GameFrameWork.Pool;
using UnityEngine;

namespace GameFrameWork.UI
{
    public class UIManager
    {
        protected class WaitingShowNode
        {
            internal IUIControllerInterface panel;
            internal object[] args;
        }
        protected Dictionary<int, UIControllerBase> m_dic = new Dictionary<int, UIControllerBase>();

        protected Queue<IUIControllerInterface> m_histroy = new Queue<IUIControllerInterface>();
        protected Queue<WaitingShowNode> m_waiting = new Queue<WaitingShowNode>();

        protected StackPool<WaitingShowNode> _pool = new StackPool<WaitingShowNode>();

        private Dictionary<int, string> m_resourceDic = new Dictionary<int, string>();

        public void SetDic(Dictionary<int, string> dic)
        {
            m_resourceDic = dic;
        }
        public void OpenUI<T>(int @enum ,params object[] args) where T : UIControllerBase
        {
            if (!m_dic.TryGetValue(@enum, out var panel))
            {
                if (m_resourceDic.TryGetValue(@enum, out var name))
                {
                    UIPrefabLoader loader = UIPrefabLoader.CreateLoader<T>(name, (t) =>
                    {
                        if (t != null)
                        {
                            m_dic.Add(@enum, t);
                            t.transform.SetParent(UIRoot.root.transform);
                            var rect = t.GetComponent<RectTransform>();
                            rect.offsetMax = Vector2.zero;
                            rect.offsetMin = Vector2.zero;
                            rect.anchoredPosition = Vector2.zero;
                            t.transform.localScale = Vector3.one;

                            OpenUI(t, @enum, args);
                        }
                    });
                }
                return;
            }

            OpenUI(panel, @enum, args);
        }

        private void OpenUI(UIControllerBase panel,int @enum ,params object[] args)
        {
            switch (panel.GetPopType())
            {
                case UIPopTypeEnum.Main:
                {
                    foreach (var v in m_dic)
                    {
                        if (v.Key != @enum && v.Value.GetPopType() == UIPopTypeEnum.Main && v.Value.gameObject.activeInHierarchy)
                        {
                            v.Value.HidePanel();
                        }
                    }
                    break;
                }
                case UIPopTypeEnum.Sub:
                {
                    break;
                }
                case UIPopTypeEnum.Pop:
                {
                    foreach (var v in m_dic)
                    {
                        if (v.Key != @enum && v.Value.GetPopType() == UIPopTypeEnum.Pop && v.Value.gameObject.activeInHierarchy)
                        {
                            var node = _pool.Pop();
                            if (node == null)
                                node = new WaitingShowNode();
                            node.panel = panel;
                            node.args = args;
                            m_waiting.Enqueue(node);
                        }
                    }
                    break;
                }
            }
            panel.Show();
            panel.ShowPanel(args);
        }

        public bool IsShow(int index)
        {
            if (m_dic.TryGetValue(index, out var panel))
            {
                return panel.gameObject.activeInHierarchy;
            }

            return false;
        }

        public void CloseUI(IUIControllerInterface panel)
        {
            foreach (var v in m_dic)
            {
                if (v.Value == panel)
                {
                    CloseUI(v.Key);
                    return;
                }
            }
        }

        public void DestroyUI(int @enum)
        {
            if (m_dic.TryGetValue(@enum, out var panel))
            {
                GameObject.Destroy(panel.gameObject);
                m_dic.Remove(@enum);
            }
        }

        public void DestroyAllUIExclude(int @enum)
        {
            if (m_dic.TryGetValue(@enum, out var panel))
            {
                foreach (var value in m_dic)
                {
                    GameObject.Destroy(value.Value.gameObject);
                }
                m_dic.Clear();
                m_dic.Add(@enum,panel);
            }
        }

        public void CloseUI(int @enum)
        {
            if (m_dic.TryGetValue(@enum, out var panel))
            {
                panel.HidePanel();
                switch (panel.GetPopType())
                {
                    case UIPopTypeEnum.Main:
                    case UIPopTypeEnum.Sub:
                    {
                        m_histroy.Enqueue(panel);
                        break;
                    }
                    case UIPopTypeEnum.Pop:
                    {
                        if (m_waiting.Count > 0)
                        {
                            var nextPanel = m_waiting.Dequeue();
                            nextPanel.panel.ShowPanel(nextPanel.args);
                            nextPanel.args = null;
                            nextPanel.panel = null;
                            _pool.Push(nextPanel);
                        }
                        break;
                    }
                }
            }
        }

        public void CloseAllPanel()
        {
            foreach (var v in m_dic)
            {
                v.Value.HidePanel();
            }
        }

        public void ClearHistroy()
        {
            m_histroy.Clear();
        }
        
        

        public void PopHistroy()
        {
            if (m_histroy.Count > 0)
            {
                var panel = m_histroy.Dequeue();
                panel.ShowPanel();
            }
        }
    }
}