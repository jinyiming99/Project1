using System;
using System.Collections.Generic;
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
        private Dictionary<int, IUIControllerInterface> m_dic = new Dictionary<int, IUIControllerInterface>();
        private Func<int,IUIControllerInterface> m_func;

        private Queue<IUIControllerInterface> m_histroy = new Queue<IUIControllerInterface>();
        private Queue<WaitingShowNode> m_waiting = new Queue<WaitingShowNode>();

        private StackPool<WaitingShowNode> _pool = new StackPool<WaitingShowNode>();
        

        public void SetFunc(Func<int, IUIControllerInterface> func)
        {
            m_func = func;
        }
        public void OpenUI(int @enum ,params object[] args)
        {
            if (!m_dic.TryGetValue(@enum, out var panel))
            {
                if (m_func == null)
                {
                    throw new Exception();
                    return;
                }

                panel = m_func.Invoke(@enum);
                if (panel == null)
                {
                    throw new NullReferenceException();
                    return;
                }
                m_dic.Add(@enum,panel);
            }

            switch (panel.GetPopType())
            {
                case UIPopTypeEnum.Main:
                {
                    foreach (var v in m_dic)
                    {
                        if (v.Key != @enum && v.Value.GetPopType() == UIPopTypeEnum.Main && v.Value.GetActive())
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
                        if (v.Key != @enum && v.Value.GetPopType() == UIPopTypeEnum.Pop && v.Value.GetActive())
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
            panel.ShowPanel(args);
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