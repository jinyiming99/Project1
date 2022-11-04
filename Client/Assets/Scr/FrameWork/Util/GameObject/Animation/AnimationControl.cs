using System;
using System.Collections.Generic;
using UnityEngine;

public class AnimationControl : MonoBehaviour
{
    /// <summary>
    /// 
    /// </summary>
    Animator m_animator = null;

    private AnimationStroage m_clipStroage;
    /// <summary>
    /// 事件回调
    /// </summary>
    Action<string> m_action;
    /// <summary>
    /// 初始化
    /// </summary>
    private void Awake()
    {
        m_animator = GetComponent<Animator>();
        m_animator.cullingMode = AnimatorCullingMode.AlwaysAnimate;
        m_clipStroage = AnimationStroage.CreateStroage(m_animator);
    }

    public void SetCullMode(AnimatorCullingMode mode)
    {
        m_animator.cullingMode = mode;
    }


    /// <summary>
    /// 获得动画clip时间长度
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public float GetClipTime(string name)
    {
        if (m_clipStroage == null)
            return 0f;
        return m_clipStroage.GetClipTime(name);
    }
    /// <summary>
    /// 查找动画clip
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public AnimationClip GetClip(string name)
    {
        if (m_clipStroage == null)
            return null;
        return m_clipStroage.GetClip(name);
    }

    /// <summary>
    /// 设置动画速度
    /// </summary>
    /// <param name="mult"></param>
    public void ChangePlaySpeed(float mult)
    {
        if (m_animator != null)
            m_animator.speed = mult;
    }

    public void SetTime(float t)
    {
        m_animator.playbackTime = t;
    }
    public void SetStroageMode(OverrideAnimationStroageWorkMode mode)
    {
        m_clipStroage.SetWorkMode(mode);
    }

    /// <summary>
    /// 检查当前动画状态名字
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public bool IsState(string name)
    {
        if (m_animator != null)
        {
            var info = m_animator.GetCurrentAnimatorStateInfo(0);
            return info.IsName(name);
        }
        return false;
    }
    /// <summary>
    /// 设置事件回调
    /// </summary>
    /// <param name="action"></param>
    public void SetListener(Action<string> action)
    {
        m_action = action;
    }
    /// <summary>
    /// 销毁
    /// </summary>
    void OnDestroy()
    {
        m_action = null;

        if (m_clipStroage != null)
        {
            m_clipStroage.OnDestroy();
        }
        m_clipStroage = null;
    }
    /// <summary>
    /// 播放动画
    /// </summary>
    /// <param name="name"></param>
    /// <param name="speed"></param>
    public void Play(string name, float speed = 1)
    {
        /*if (m_clipStroage.HaveState(name))
        {
            m_animator.speed = speed;
            m_animator.Play(name, 0, 0);
        }*/
        m_animator.speed = speed;
        m_animator.Play(name, 0, 0);
    }
    /// <summary>
    /// 从某个时间开始播放动画
    /// </summary>
    /// <param name="name"></param>
    /// <param name="time"></param>
    /// <param name="speed"></param>
    public void PlayAnimationAtTime(string name, float time, float speed = 1)
    {
        //Debug.Log("name is = " + name + "   time is = " + time);
        /*if (m_clipStroage.HaveState(name))
        {
            m_animator.speed = speed;
            m_animator.Play(name, 0, time);
            //m_animator.playbackTime = time;
        }*/
        m_animator.speed = speed;
        m_animator.Play(name, 0, time);
    }
    /// <summary>
    /// 获得当前动画状态机的属性
    /// </summary>
    /// <returns></returns>
    public AnimationStateInfo GetCurClipState()
    {
        AnimationStateInfo outInfo = new AnimationStateInfo()
        {
            m_time = 0f,
            m_clipName = string.Empty
        };

        if (m_animator)
        {
            var info = m_animator.GetCurrentAnimatorStateInfo(0);
            outInfo.m_time = info.normalizedTime;
            outInfo.m_clipName = m_clipStroage.GetStateName(info);
        }

        return outInfo;
    }
    public string GetCurClipStateName()
    {
        return GetCurClipState().m_clipName;
    }
    public string GetInfoStateName(AnimatorStateInfo info)
    {
        return m_clipStroage.GetStateName(info);
    }
    /// <summary>
    /// 过度播放动画
    /// </summary>
    /// <param name="name"></param>
    /// <param name="time"></param>
    /// <param name="speed"></param>
    public void CrossPlay(string name, float time, float speed = 1)
    {
        if (m_clipStroage!=null && m_clipStroage.HaveState(name))
        {
            m_animator.speed = speed;
            m_animator.CrossFade(name, time, 0, 0);
        }
    }
    /// <summary>
    /// 动画事件响应监听
    /// </summary>
    /// <param name="name"></param>
    void OnAnimationEvent(string name)
    {
        if (m_action != null)
        {
            m_action(name);
        }
    }
    
    public void SetInt(string name, int r)
    {
        m_animator.SetInteger(name, r);
    }
    public void SetBool(string name, bool r)
    {
        m_animator.SetBool(name, r);
    }

    public void SetTrigger(string name)
    {
        m_animator.SetTrigger(name);
    }

    public void EnableAnimator(bool enabled)
    {
        m_animator.enabled = enabled;
    }

    /// <summary>
    /// 释放
    /// </summary>
    public void Release()
    {
        m_action = null;
    }

    public void ReBuildBone()
    {
        m_animator.Rebind();
    }

    public void StopPlayback()
    {
        m_animator.StopPlayback();
    }

    public void OnStateEnter(AnimatorStateInfo info)
    {
        m_clipStroage.OnStateEnter(info);
    }

    public void OnStateExit(AnimatorStateInfo info)
    {
        m_clipStroage.OnStateExit(info);
    }
}
