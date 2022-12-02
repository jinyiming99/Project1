#define OPEN_DEBUG_MODE
#define Working_Debugger
#if UNITY_STANDALONE

#endif
using System;
using UnityEngine;
namespace GameFrameWork.DebugTools
{
    /// <summary>
    /// debug用的对外的
    /// </summary>
    public class DebugHelper
    {
        public static void Log(string str)
        {
            if (!DebugManager.DebugWork)
                return;
            if (string.IsNullOrEmpty(str))
                return;
            var logStr = $" time = {System.DateTime.Now.ToString()}, log = {str}";
            FrameWork.GetFrameWork()?.Components?.DebugManager?.Log(logStr);
            Debug.Log( logStr);
        }
        /// <summary>
        /// 为了防止在外面组装string，控制不了
        /// </summary>
        /// <param name="strFunc"></param>
        /// <param name="type"></param>
        public static void Log(Func<string> strFunc)
        {

            if (!DebugManager.DebugWork)
                return;
            string str = string.Empty;
            if (strFunc != null)
                str = strFunc();
            else
                return;
            Log(str);
        }

        public static void LogWarning(string str, DebugTypeEnum type = DebugTypeEnum.Base)
        {

            if (!DebugManager.DebugWork)
                return;

            if (string.IsNullOrEmpty(str))
                return;

            var logStr = $" time = {System.DateTime.Now.ToString()}, log = {str}";
            FrameWork.GetFrameWork()?.Components?.DebugManager?.LogWarning(logStr);
            Debug.LogWarning( logStr);
        }

        public static void LogWarning(Func<string> strFunc, DebugTypeEnum type = DebugTypeEnum.Base)
        {

            if (!DebugManager.DebugWork)
                return;
            string str = string.Empty;
            if (strFunc != null)
                str = strFunc();
            else
                return;

            LogWarning(str, type);
           
        }

        public static void LogColor(string str,string color )
        {
            if (!DebugManager.DebugWork)
                return;

            if (string.IsNullOrEmpty(str))
                return;
            var logStr = $" time = {System.DateTime.Now.ToString()}, log = {string.Format("<color={2}> time ={0}   log :   </color> {1}", System.DateTime.Now.ToString() ,str, color)}";
            FrameWork.GetFrameWork()?.Components?.DebugManager?.LogError(logStr);
            Debug.Log(logStr);
        }

        public static void LogColor(Func<string> strFunc, string color)
        {
            if (!DebugManager.DebugWork)
                return;

            string str = string.Empty;
            if (strFunc != null)
                str = strFunc();
            else
                return;

            LogColor(str, color);
        }

        public static void LogError(string str, DebugTypeEnum type = DebugTypeEnum.Base)
        {
            if (!DebugManager.DebugWork)
                return;

            if (string.IsNullOrEmpty(str))
                return;
            var logStr = $" time = {System.DateTime.Now.ToString()}, log = {str}";
            FrameWork.GetFrameWork()?.Components?.DebugManager?.LogError(logStr);
            Debug.LogError(logStr);
        }

        public static void LogError(Func<string> strFunc, DebugTypeEnum type = DebugTypeEnum.Base)
        {
            if (!DebugManager.DebugWork)
                return;

            string str = string.Empty;
            if (strFunc != null)
                str = strFunc();
            else
                return;
            LogError(str, type);
        }

    }
}


