using UnityEngine;
namespace GameFrameWork.DebugTools
{

    public class ShowMemory : GUIWindowBase
    {
        public ShowMemory():base(new Rect(Screen.width - 400, 0f, 400f, 200f))
        {

        }

        protected override void DrawWindow()
        {
            GUILayout.BeginArea(m_rect);
            GUILayout.Label("Total Reserved memory by Unity: " + ByteGetString(UnityEngine.Profiling.Profiler.GetTotalReservedMemoryLong()));
            GUILayout.Label("- Allocated memory by Unity: " + ByteGetString(UnityEngine.Profiling.Profiler.GetTotalAllocatedMemoryLong()));
            GUILayout.Label("- Reserved but not allocated: " + ByteGetString(UnityEngine.Profiling.Profiler.GetTotalUnusedReservedMemoryLong()));
            GUILayout.Label("- Mono Heap Size" + ByteGetString(UnityEngine.Profiling.Profiler.GetMonoHeapSizeLong()));
            GUILayout.Label("- Mono Used Size: " + ByteGetString(UnityEngine.Profiling.Profiler.GetMonoUsedSizeLong()));
            GUILayout.EndArea();
        }
        string[] c = new string[] {"b" ,"KB", "M", "G", "T" };
        string ByteGetString(long l)
        {
            long cur = l;
            int i = 0;
            float p = 0f;
            while (cur > 1024)
            {
                i++;
                p = (cur % 1024) * 1f / 1024;
                cur = cur / 1024;
            }
            return $"{cur + p} {c[i]}";
        }
    }
}
