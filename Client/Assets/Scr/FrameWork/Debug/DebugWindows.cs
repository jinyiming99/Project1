// using UnityEngine;
//
// namespace GameFrameWork.DebugTools
// {
//     /// <summary>
//     /// debug的log日志显示，可以在打包后的安卓和IOS上显示
//     /// </summary>
//     public class DebugWindows : GUIWindowBase
//     {
//         Vector2 m_pos;
//         DebugTypeEnum m_e;
//         public DebugWindows(Rect rect)
//             : base(rect)
//         {
//             m_pos = new Vector2();
//             m_e = DebugTypeEnum.Base;
//         }
//         protected override void DrawWindow()
//         {
//             int size = GUI.skin.button.fontSize;
//             GUI.skin.label.fontSize = 20;
//             DrawControl();
//             DebugData data = DebugManager.Instance.GetData(m_e);
//             if (data == null)
//                 return;
//             int count = data.m_data.Count;
//             m_pos = GUILayout.BeginScrollView(m_pos);
//             for (int i = 0; i < count; i++)
//             {
//                 GUILayout.Label(data.m_data[i].ToString());
//             }
//             GUILayout.EndScrollView();
//             GUI.skin.button.fontSize = size;
//         }
//         private void DrawControl()
//         {
//             GUILayout.BeginHorizontal();
//             if (GUILayout.Button("back"))
//             {
//                 int e = (int)m_e - 1;
//                 if (e > (int)DebugTypeEnum.Start)
//                     m_e = (DebugTypeEnum)e;
//             }
//
//             GUILayout.Label(m_e.ToString());
//
//             if (GUILayout.Button("next"))
//             {
//                 int e = (int)m_e + 1;
//                 if (e < (int)DebugTypeEnum.End)
//                     m_e = (DebugTypeEnum)e;
//             }
//
//             GUILayout.EndHorizontal();
//         }
//     }
// }
//
//
