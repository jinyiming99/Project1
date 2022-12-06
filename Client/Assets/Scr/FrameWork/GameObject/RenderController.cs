using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class RenderController 
    {
        /// <summary>
        /// 获得所有子物件的render
        /// </summary>
        List<Renderer> m_renderer = new List<Renderer>();
        public RenderController()
        {
            
        }

        public void Init(Transform transform)
        {
            var rs = transform.GetComponentsInChildren<Renderer>();
            m_renderer.AddRange(rs);
        }
        
        /// <summary>
        /// 设置shader中的keyword
        /// </summary>
        /// <param name="key"></param>
        /// <param name="enable"></param>
        public void SetEnableKey(string key, bool enable)
        {
            foreach (var r in m_renderer)
            {
                if (enable)
                {
                    foreach (var m in r.materials)
                    {
                        //if (m.HasProperty(key))
                        m.EnableKeyword(key);
                    }
                }
                else
                {
                    foreach (var m in r.materials)
                    {
                        //if (m.HasProperty(key))
                        m.DisableKeyword(key);
                    }
                }
            }
        }
        /// <summary>
        /// 设置shader中的pass
        /// </summary>
        /// <param name="lightMode"></param>
        /// <param name="enable"></param>
        public void SetEnablePass(string lightMode, bool enable)
        {
            foreach (var r in m_renderer)
            {
                foreach (var m in r.materials)
                {
                    m.SetShaderPassEnabled(lightMode, enable);
                }
            }
        }
    }
}