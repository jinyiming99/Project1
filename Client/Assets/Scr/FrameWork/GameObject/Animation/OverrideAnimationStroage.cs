// using System;
// using System.Collections.Generic;
// using System.Collections;
// using System.IO;
// using System.Threading.Tasks;
// using UnityEngine;
//
// class OverrideAnimationStroage : AnimationStroage
// {
//     public class AnimationOverrideClips : List<KeyValuePair<AnimationClip,AnimationClip>>
//     {
//         public AnimationOverrideClips(int capacity) : base(capacity) { }
//         public AnimationOverrideClips() : base() { }
//
//         public AnimationClip this[string name]
//         {
//             get { return this.Find(x => x.Key.name.Equals(name)).Value; }
//             set
//             {
//                 int index = this.FindIndex(x => {
//                     if (x.Key == null)
//                         return false;
//                     return x.Key.name.Equals(name);
//                     });
//                 if (index != -1)
//                     this[index] = new KeyValuePair<AnimationClip, AnimationClip>(this[index].Key, value);
//             }
//         }
//         public bool Have(string name)
//         {
//             int index = this.FindIndex(x =>
//             {
//                 if (x.Key == null)
//                     return false;
//                 return x.Key.name.Equals(name);
//             });
//             return index != -1;
//         }
//     }
//     //Queue<ClipPack> m_queueClipData = new Queue<ClipPack>(2);
//     public AnimatorOverrideController m_controller;
//     AnimatorOverrideClipGameData m_data;
//     Dictionary<string, int> m_count = new Dictionary<string, int>();
//     Dictionary<string, KeyValuePair<AnimationClip, AnimationClip>> m_packDic = new Dictionary<string, KeyValuePair<AnimationClip, AnimationClip>>();
//     OverrideAnimationStroageWorkMode m_mode;
//
//     AnimationOverrideClips m_clips = new AnimationOverrideClips();
//     public override bool Create(Animator animator)
//     {
//
//         if (animator.runtimeAnimatorController is AnimatorOverrideController)
//         {
//             m_controller = animator.runtimeAnimatorController as AnimatorOverrideController;
//             
//             Util.Resource.AnimatorOverrideClipManager.GetData(m_controller.name.ToLower() + Util.Resource.ResourceDefine.ClipDataSuffixSmall ,ref m_data);
//
//             m_controller.GetOverrides(m_clips);
//             foreach (var v in m_clips)
//             {
//                 if (v.Value != null && !m_dic.ContainsKey(v.Key.name))
//                 {
//                     m_dic.Add(v.Key.name, v.Value);
//                 }
//             }
//             if (m_data == null)
//             {
//                 Util.DebugTools.DebugHelper.Log($"AnimatorOverrideController name {m_controller.name} clip data is not find");
//             }
//             return m_data != null;
//         }
//         return false;
//     }
//
//     public override AnimationStroageType GetStroageType()
//     {
//         return AnimationStroageType.OverrideStroage;
//     }
//
//     public override bool HaveState(string name)
//     {
//         if (m_data == null) return false;
//         return m_data.GetState(name) != null;
//     }
//     public override string GetStateName(AnimatorStateInfo info)
//     {
//         if (m_data == null) return string.Empty;
//         var state = m_data.GetState(info);
//         if (state == null)
//             return string.Empty;
//         return state.m_stateName;
//     }
//
//     public override void OnStateEnter(AnimatorStateInfo info)
//     {
//         if (m_data == null) return;
//         Util.Profiler.Profiler.BeginSample("OnStateEnter");
//         var state = m_data.GetState(info);
//
//         if (state == null)
//         {
//             Util.Profiler.Profiler.EndSample();
//             return;
//         }
//         //Util.DebugTools.DebugHelper.Log(() => $"OnStateEnter {state.m_clipName} Enter---------------------------------");
//         var d = m_data.GetStateData(state);
//         if (d == null)
//         {
//             Util.Profiler.Profiler.EndSample();
//             return;
//         }
//
//         BindClip(state, d);
//         
//         Util.Profiler.Profiler.EndSample();
//         //Util.DebugTools.DebugHelper.Log(() => $"OnStateEnter {state.m_clipName} over------------------------------------");
//     }
//
//     public override void OnStateExit(AnimatorStateInfo info)
//     {
//         if (m_mode == OverrideAnimationStroageWorkMode.Add)
//             return;
//         var state = m_data.GetState(info);
//         
//         if (state == null)
//         {
//             Util.DebugTools.DebugHelper.Log(() => $"animator = {m_animator.runtimeAnimatorController.name} | shortNameHash = {info.shortNameHash} | state  is null");
//             return;
//         }
//         //Util.DebugTools.DebugHelper.Log(() => $"OnStateExit {state.m_clipName} enter -------------------------------------------");
//         if (m_packDic.TryGetValue(state.m_stateName, out var data))
//         {
//             
//             Util.Profiler.Profiler.BeginSample("OnStateExit");
//             var d = m_data.GetStateData(info);
//             if (--m_count[d.m_back] == 0)
//             {
//                 var dd = m_packDic[state.m_stateName];
//                 if (m_dic.ContainsKey(data.Key.name))
//                     m_dic.Remove(data.Key.name);
//
//                 m_packDic[state.m_stateName] = new KeyValuePair<AnimationClip, AnimationClip>(dd.Key, dd.Key);
//                 
//             }
//
//             Apply();
//             Util.CoroutineLoader.GetInstance().Do(() => ReleaseClip(d));
//
//             Util.Profiler.Profiler.EndSample();
//             //Util.DebugTools.DebugHelper.Log(() => $"OnStateExit {state.m_clipName} over ----------------------------------------");
//         }
//     }
//     /// <summary>
//     /// 销毁时释放全部动画资源
//     /// </summary>
//     public override void OnDestroy()
//     {
//         foreach (var v in m_count)
//         {
//                 Util.Resource.ResourceManager.Instance?.ReleaseResourceAtPath(v.Key, true,v.Value);
//         }
//
//         m_data.Release();
//     }
//
//     public override void SetWorkMode(OverrideAnimationStroageWorkMode mode)
//     {
//         m_mode = mode;   
//     }
//
//     private void Apply()
//     {
//         //m_controller.ApplyOverrides(new List<KeyValuePair<AnimationClip, AnimationClip>>(m_packDic.Values));
//         m_controller.ApplyOverrides(m_clips);
//     }
//     public override AnimationClip GetClip(string name)
//     {
//         if (!m_dic.ContainsKey(name))
//         {
//             var s = m_data.GetState(name);
//             var d = m_data.GetStateData(s);
//             BindClip(s, d);
//         }
//         return base.GetClip(name);
//     }
//     public override float GetClipTime(string name)
//     {
//         if (!m_dic.ContainsKey(name))
//         {
//             var s = m_data.GetState(name);
//             var d = m_data.GetStateData(s);
//             BindClip(s, d);
//         }
//         return base.GetClipTime(name);
//     }
//     private void SetCount(ClipPack d)
//     {
//         if (!m_count.ContainsKey(d.m_head))
//         {
//             m_count.Add(d.m_head, 1);
//         }
//         if (!string.IsNullOrEmpty(d.m_back))
//         {
//             if (m_mode == OverrideAnimationStroageWorkMode.Normal)
//             {
//                 if (m_count.ContainsKey(d.m_back))
//                 {
//                     m_count[d.m_back]++;
//                 }
//                 else
//                 {
//                     m_count.Add(d.m_back, 1);
//                 }
//             }
//             else
//             {
//                 if (!m_count.ContainsKey(d.m_back))
//                     m_count.Add(d.m_back, 1);
//             }
//         }
//         //Util.DebugTools.DebugHelper.Log(() => $"======================================================");
//         //Util.DebugTools.DebugHelper.Log(() => $"control name = {m_animator.gameObject.name}   m_count[d.m_back] {d.m_head} = {m_count[d.m_head]} | m_count[d.m_back] {d.m_back} = {m_count[d.m_back]}");
//     }
//     private void BindClip(StatePack state, ClipPack d)
//     {
//         if (state == null) return;
//         
//
//         var name = state.m_clipName;
//         var clipName = state.m_stateName;
//         //var clip = m_clips[clipName];
//         if (m_count.TryGetValue(d.m_back,out var num))
//         {
//
//         }
//         else
//         {
//             AnimationClip b = null;
//             if (!string.IsNullOrEmpty(d.m_back))
//                 b = LoadClip(d.m_back);
//             if (m_clips.Have(clipName))
//             {
//                 m_clips[clipName] = b;
//                 if (m_dic.ContainsKey(clipName))
//                 {
//                     m_dic[clipName] = b;
//                 }
//                 else
//                 {
//                     m_dic.Add(clipName,b);
//                 }
//             }
//             else
//             {
//                 AnimationClip h = null;
//                 if (d.m_head != d.m_back)
//                 {
//                     h = LoadClip(d.m_head);
//                 }
//                 else
//                 {
//                     h = b;
//                 }
//                 m_dic.Add(h.name, b);
//                 m_clips.Add(new KeyValuePair<AnimationClip, AnimationClip>(h, b));
//             }
//         }
//
//         ////var id = state.m_code;
//         //AnimationClip h = null;
//         //if (m_packDic.TryGetValue(clipName, out var oldData))
//         //{
//         //    if (m_mode == OverrideAnimationStroageWorkMode.Add)
//         //        return;
//         //    h = oldData.Key;
//         //    m_packDic.Remove(clipName);
//         //}
//         //if (h == null)
//         //    h = Util.Resource.ResourceManager.Instance.LoadResourceByPath<AnimationClip>(d.m_head);
//         //AnimationClip b = null;
//         //if (!string.IsNullOrEmpty(d.m_back))
//         //    b = Util.Resource.ResourceManager.Instance.LoadResourceByPath<AnimationClip>(d.m_back);
//         //if (h == null) Util.DebugTools.DebugHelper.Log(() => $"animator = {m_animator.runtimeAnimatorController.name} | name = {name} | d.m_head is {d.m_head} h is null");
//         //if (b == null) Util.DebugTools.DebugHelper.Log(() => $"animator = {m_animator.runtimeAnimatorController.name} | name = {name} | d.m_back is {d.m_back} b is null");
//         //m_packDic.Add(clipName, new KeyValuePair<AnimationClip, AnimationClip>(h, b));
//         //if (!m_dic.ContainsKey(h.name))
//         //    m_dic.Add(h.name, b);
//         //else
//         //    m_dic[h.name] = b;
//         Apply();
//     }
//
//     public AnimationClip LoadClip(string path)
//     {
//         if (!m_count.ContainsKey(path))
//             m_count.Add(path, 1);
//         else
//             return null;
//         return Util.Resource.ResourceManager.Instance.LoadResourceByPath<AnimationClip>(path);
//     }
//
//     private IEnumerator ReleaseClip(ClipPack d)
//     {
//         yield return new WaitForEndOfFrame();
//
//         Util.Resource.ResourceManager.Instance.ReleaseResourceAtPath(d.m_back, true);
//
//
//     }
// }
//
