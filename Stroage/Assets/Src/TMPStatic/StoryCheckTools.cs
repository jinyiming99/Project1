using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using Bao.Game;
using Code.Editor;
using DP.Avg;
using TMPro.EditorUtilities;
using UnityEditor;
using UnityEngine;
using UnityEngine.TextCore.LowLevel;

namespace AVG.Editor
{
    public static class StoryCheckTools
    {
        static string filePath = "Assets/AVG/BundleResources/AvgStory/story.json";
        [MenuItem("AVG/StoryCheckTools/检查最后一句对话是否有标点符号")]
        public static void CheckLastSentencePunctuation()
        {
            var storyStr = File.ReadAllText(filePath);
            if (string.IsNullOrEmpty(storyStr))
            {
                return;
            }

            var storys = JsonUtility.FromJson<StoryData>(storyStr);
            foreach (var node in storys.storyNodeDataList)
            {
                if (string.IsNullOrEmpty(node.dialogueData.text))
                    continue;
                var lastChar = node.dialogueData.text[node.dialogueData.text.Length - 1];
                if (lastChar != '。' && lastChar != ' ' &&
                    lastChar != '！' && lastChar != '~' &&
                    lastChar != '？' &&
                    lastChar != '—' &&
                    lastChar != '」' &&
                    lastChar != '”' &&
                    lastChar != '"' &&
                    lastChar != '’' &&
                    lastChar != '.')
                {
                    Debug.Log($"对话{node.dialogueData.text} 最后没有标点符号");
                }

                if (lastChar == '，')
                {
                    Debug.Log($"对话{node.dialogueData.text} 最后标点是逗符号");
                }
            }
        }
        
        [MenuItem("AVG/StoryCheckTools/检查对话中符号前是否有空格")]
        public static void CheckDialoguePunctuationSpace()
        {
            var storyStr = File.ReadAllText(filePath);
            if (string.IsNullOrEmpty(storyStr))
            {
                return;
            }

            var storys = JsonUtility.FromJson<StoryData>(storyStr);
            foreach (var node in storys.storyNodeDataList)
            {
                if (string.IsNullOrEmpty(node.dialogueData.text))
                    continue;
                for (int i = 0; i < node.dialogueData.text.Length; i++)
                {
                    if (CheckIsPunctuation(node.dialogueData.text[i]) )
                    {
                        if (i-1 >= 0 && node.dialogueData.text[i - 1] == ' ')
                        {
                            Debug.Log($"对话{node.dialogueData.text} 符号{node.dialogueData.text[i]}前有空格");
                            node.dialogueData.text = node.dialogueData.text.Remove(i - 1);
                        }
                    }
                }
            }
            storyStr = JsonUtility.ToJson(storys);
            File.WriteAllText(filePath,storyStr);
        }
        
        // [MenuItem("AVG/StoryCheckTools/检查excel对话中符号前是否有空格")]
        // public static void CheckExcelDialoguePunctuationSpace()
        // {
        //     FileInfo newFile = new FileInfo(AvgExcelHelper.ExcelPath);
        //     using (ExcelPackage package = new ExcelPackage(newFile))
        //     {
        //         var worksheet = package.Workbook.Worksheets["动态"];
        //         int row = worksheet.Cells.Rows;
        //         int column = worksheet.Cells.Columns;
        //         Debug.Log(row + "," + column);
        //         for (int iy = 6; iy < row; iy++)
        //         {
        //             for (int ix = 0; ix < column; ix++)
        //             {
        //                 var cell = worksheet.Cells[ix, iy];
        //                 if (cell == null)
        //                 {
        //                     continue;
        //                 }
        //             }
        //             
        //             // for (int i = 0; i < dialogue.Length; i++)
        //             // {
        //             //     if (CheckIsPunctuation(dialogue[i]) )
        //             //     {
        //             //         if (i-1 >= 0 && dialogue[i - 1] == ' ')
        //             //         {
        //             //             Debug.Log($"对话{dialogue} 符号{dialogue[i]}前有空格");
        //             //             dialogue = dialogue.Remove(i - 1);
        //             //         }
        //             //     }
        //             // }
        //             // worksheet.Cells[iy, 2].Value = dialogue;
        //         }
        //     };
        //     
        // }
        /// <summary>
        /// 是否是符号
        /// </summary>
        private static bool CheckIsPunctuation(char c)
        {
            if (c == '。' || c == ' ' ||
                c == '！' || c == '~' ||
                c == '？' || c == '—' || 
                c == '」' || c == '”' || 
                c == '"' || c == '’' || 
                c == '.')
            {
                return true;
            }

            return false;
        }

        [MenuItem("AVG/打包/Release")]
        public static void PackRelease()
        {
            PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone, "RELEASE");
            Pack(false);
        }

        [MenuItem("AVG/打包/Debug")]
        public static void PackDebug()
        {
            Pack(true);
        }

        public static void Pack(bool isDebug)
        {
            PackTyping();
            AssetDatabase.Refresh();
            PackAvg(isDebug);
        }
        
        public static void PackAvg(bool debug)
        {
            //AvgEditorHelper.ReadAllExcel();
            var _bundleConfig = Resources.Load<BundleConfig>("BundleConfig");
            BundleBuild.BuildBundles(_bundleConfig);
            string name = debug ? "debug" : "release";
            string Dir =
#if UNITY_STANDALONE
                "Windows";
#elif UNITY_ANDROID
                "Android";
#else
                "MacOS";
#endif
            string path = $"{Directory.GetCurrentDirectory()}/../{Dir}/{name}/";
            
            if (!Directory.Exists(path))
            {
                var directoryInfo = Directory.CreateDirectory(path);
            }
            BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions()
            {
                scenes = new string[] { "Assets/AVG/Scenes/AvgPlayer.unity" },
                
#if UNITY_STANDALONE
                locationPathName =$"{path}/{DateTime.Now.ToString("yy-MM-dd hh-mm-ss")}/MM.exe"  ,
#elif UNITY_ANDROID
                locationPathName =$"{path}/{DateTime.Now.ToString("yy-MM-dd hh-mm-ss")}.apk"  ,
#endif
                options = debug?BuildOptions.Development:BuildOptions.None,
#if UNITY_STANDALONE
                target = BuildTarget.StandaloneWindows64,
#elif UNITY_ANDROID
                target = BuildTarget.Android,
#endif
                targetGroup = BuildTargetGroup.Standalone,
            };

            var report = BuildPipeline.BuildPlayer(buildPlayerOptions);
            if (report.summary.result != UnityEditor.Build.Reporting.BuildResult.Succeeded)
            {
                Debug.LogError("打包失败");
            }
        }


        
        private class FontData
        {
            public string name;
            public string txt;
            public string asset;
            public int width;
            public int height;
        }
        
        private static List<FontData> _fontDatas = new List<FontData>()
        {
            new FontData()
            {
                name = "Assets/TextMesh Pro/Resources/Fonts & Materials/MainFont_CS.otf",
                txt = "Assets/AVG/FontData/ChineseSimplified.txt",
                asset = "Assets/TextMesh Pro/Resources/Fonts & Materials/MainFont_CS.asset",
                width = 2048,
                height = 4096
            },
            new FontData()
            {
                name = "Assets/TextMesh Pro/Resources/Fonts & Materials/MainFont_CS.otf",
                txt = "Assets/AVG/FontData/ChineseTraditional.txt",
                asset = "Assets/TextMesh Pro/Resources/Fonts & Materials/MainFont_CT.asset",
                width = 2048,
                height = 4096
            },
            new FontData()
            {
                name = "Assets/TextMesh Pro/Resources/Fonts & Materials/MainFont_CS.otf",
                txt = "Assets/AVG/FontData/English.txt",
                asset = "Assets/TextMesh Pro/Resources/Fonts & Materials/MainFont_EN.asset",
                width = 512,
                height = 512
            },
            new FontData()
            {
                name = "Assets/TextMesh Pro/Resources/Fonts & Materials/MainFont_CS.otf",
                txt = "Assets/AVG/FontData/Japanese.txt",
                asset = "Assets/TextMesh Pro/Resources/Fonts & Materials/MainFont_JP.asset",
                width = 2048,
                height = 2048
            },
        };
        
        [MenuItem("AVG/打包/打字")]
        public static void PackTyping()
        {
            foreach (var data in _fontDatas)
            {
                PackFont(data);
            }
        }

        private static void PackFont(FontData data)
        {
            Debug.Log(data.name);
            var creater = new TMPCreater();
            var mainFont = AssetDatabase.LoadAssetAtPath<Font>(data.name);
            var txt = AssetDatabase.LoadAssetAtPath<TextAsset>(data.txt);
            creater.m_AtlasWidth = data.width;
            creater.m_AtlasHeight = data.height;
            creater.Gen(mainFont,txt.text);
            creater.CreateFontAtlasTexture();
            creater.Save(data.asset);
        }
    }
}