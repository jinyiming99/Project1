using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
public enum AnimationStroageType
{
    NormalStroage,
    OverrideStroage,
}

public enum OverrideAnimationStroageWorkMode
{
    Normal,
    Add,
}


public abstract class AnimationStroage 
{

    public static AnimationStroage CreateStroage(Animator animator)
    {
        AnimationStroage stroage = null;
        bool b = true;
#if UNITY_EDITOR
        // if (ConfigManager.InstanceData.m_resourceConfig.m_workMode != ResrouceLoadWorkMode.Pack)
        // {
        //     b = false;
        // }
#endif
        //if (b && animator.runtimeAnimatorController is AnimatorOverrideController)
        //{
        //    stroage = new OverrideAnimationStroage();
        //    stroage.m_animator = animator;
        //    stroage.m_rac = animator.runtimeAnimatorController;
        //    if (!stroage.Create(animator))
        //    {
        //        stroage = new NormalAnimationStroage();
        //        stroage.m_animator = animator;
        //        stroage.m_rac = animator.runtimeAnimatorController;
        //        stroage.Create(animator);
        //    }
        //}
        //else
        //{
            stroage = new NormalAnimationStroage();
            stroage.m_animator = animator;
            stroage.m_rac = animator.runtimeAnimatorController;
            stroage.Create(animator);
        //}
        //UnityEngine.Debug.Assert(, $"name {animator.runtimeAnimatorController.name} stroage is create failed");



        return stroage;
    }
    public Animator m_animator;
    public RuntimeAnimatorController m_rac;
    protected Dictionary<string, AnimationClip> m_dic = new Dictionary<string, AnimationClip>();

    public abstract bool Create(Animator animator);
    public abstract AnimationStroageType GetStroageType();
    public abstract void OnStateEnter(AnimatorStateInfo info);
    public abstract void OnStateExit(AnimatorStateInfo info);
    public abstract void OnDestroy();
    public abstract bool HaveState(string name);
    public abstract string GetStateName(AnimatorStateInfo info);

    public virtual void SetWorkMode(OverrideAnimationStroageWorkMode mode)
    {

    }

    public virtual AnimationClip GetClip(string name)
    {
        if (!m_dic.TryGetValue(name, out var clip))
        {
            GameFrameWork.DebugTools.DebugHelper.Log(() => { return $"{GetStroageType()} |  {m_animator.name} | AnimationStroage : not find AnimationClip name = {name}"; });
        }
        return clip;
    }

    public virtual float GetClipTime(string name)
    {
        var clip = GetClip(name);
        if (clip != null) return clip.length;
        return -1f;
    }



}

