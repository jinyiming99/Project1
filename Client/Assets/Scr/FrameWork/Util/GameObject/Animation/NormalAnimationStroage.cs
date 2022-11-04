using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class NormalAnimationStroage : AnimationStroage
{
    public override bool Create(Animator animator)
    {
        var controller = animator.runtimeAnimatorController;
        foreach (var clip in controller.animationClips)
        {
            if (!m_dic.ContainsKey(clip.name))
                m_dic.Add(clip.name, clip);
        }
        return true;
    }

    public override AnimationStroageType GetStroageType()
    {
        return AnimationStroageType.NormalStroage;
    }

    public override bool HaveState(string name)
    {
        return m_dic.ContainsKey(name);
    }

    public override string GetStateName(AnimatorStateInfo info)
    {
        foreach (var v in m_dic)
        {
            if (info.IsName(v.Key))
            {
                return v.Key;
            }
        }
        return string.Empty;
    }

    public override void OnStateEnter(AnimatorStateInfo info)
    {

    }

    public override void OnStateExit(AnimatorStateInfo info)
    {

    }
    public override void OnDestroy()
    {

    }
}

