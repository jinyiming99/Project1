using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEditor;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Debug = UnityEngine.Debug;
using UnityEngine;

namespace GameFrameWork.Network.Editor
{
    public class NetworkProtoFileCreater
    {
        private static string FolderPath = "ProtoFoldPath";
        [MenuItem("GameTools/Proto/ProtoCreater")]
        public static void ProtoCreate()
        {
            string oldPath = PlayerPrefs.GetString(FolderPath);
            string folderPath = oldPath;
            if (string.IsNullOrEmpty(oldPath))
                folderPath = EditorUtility.OpenFolderPanel("打开PB路径", "", "");
            else
            {
                var b =EditorUtility.DisplayDialog("","是否重新选择路径","ok","cancal");
                if (b)
                {
                    folderPath = EditorUtility.OpenFolderPanel("打开PB路径", "", "");
                }
            }
            string tempPath = Path.Combine(Application.temporaryCachePath, "tmp");
            // if (!folderPath.EndsWith("client/v2/dto"))
            // {
            //     EditorUtility.DisplayDialog("error","选错文件夹了，选client","ok");
            //     return;
            // }
            ClearFolder(tempPath);
            if (!Directory.Exists(tempPath))
            {
                Directory.CreateDirectory(tempPath);
            }

            tempPath = SamePath(tempPath);
            PlayerPrefs.SetString(FolderPath,folderPath);
            ///proto的路径C:\Projects\hero-im-proto\client\v2
            var protoPath = folderPath;
            ///获取路径下的全部文件
            var allFile = GetPathAllFilePath(protoPath,"*.proto");

            //输出路径root
            var outPath = Path.Combine( Directory.GetCurrentDirectory() , "Assets/Scr/Game/Network/Protos");

            ///需要执行的命令
            string command = $"--proto_path {folderPath}  --csharp_out={outPath} --csharp_opt=file_extension=.cs ";
            //return;

            foreach (var newFilePath in allFile)
            {
                Task.Run(() =>
                {
                    var trueOutPath = outPath;
                    var processCommand = command;
                    processCommand = string.Format(processCommand,trueOutPath + " ");
                    var s = (processCommand + newFilePath).Replace("\\","/");
                
                    Process process = new Process();
                    process.StartInfo.UseShellExecute  = false;
                    process.StartInfo.RedirectStandardError = true;
                    process.StartInfo.RedirectStandardInput  = true;
                    process.StartInfo.RedirectStandardOutput = true;
                    process.StartInfo.FileName = Directory.GetCurrentDirectory() + "/protoc/bin/protoc.exe";
                    process.StartInfo.Arguments = s;
                    process.Start();
                
                    var error = process.StandardError.ReadToEnd();
                    process.WaitForExit();
                    Debug.Log(process.StartInfo.Arguments);
                    if (!string.IsNullOrEmpty(error))
                    {
                        
                        Debug.Log("error " +error);
                    }
                });
            }
        }

        static string SamePath(string str)
        {
            return str.Replace("\\", "/");
        }

        public static void ProtoChange(string path,string outPath)
        {

            StringBuilder builder = new StringBuilder();
            using (TextReader reader = File.OpenText(path))
            {
                string line = string.Empty;
                
                while ((line = reader.ReadLine()) != null)
                {
                    var list = Regex.Matches(line, Regex.Escape("validate"));
                    if (list.Count > 0)
                    {
                        if (line.Contains("import"))
                        {
                            builder.Append("//").AppendLine(line);
                        }
                        else
                        {
                            builder.AppendLine(line.Replace("[(", ";//[("));
                        }
                    }
                    else
                    {
                        builder.AppendLine(line);
                    }
                }
            }
            
            using (FileStream stream = new FileStream(outPath, FileMode.OpenOrCreate))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(builder);
                }
            }




        }
        
        public static List<string> GetPathAllFilePath(string path,string searchPattern = "")
        {
            List<string> pathList = new List<string> { path };
            List<string> needFileDir = new List<string>();
            while (pathList.Count > 0)
            {
                needFileDir.AddRange(pathList);
                List<string> list = new List<string>();
                foreach (var dir in pathList)
                {
                    list.AddRange(Directory.GetDirectories(dir));
                }

                pathList.Clear();
                pathList.AddRange(list);
            
            }
            List<string> fileList = new List<string>();

            foreach (var p in needFileDir)
            {
                fileList.AddRange(Directory.GetFiles(p,searchPattern));
            }

            return fileList;
        }
        
        public static List<string> GetFilePathBySuffix(List<string> data, List<string> suffixs)
        {

            List<string> outList = new List<string>();
            foreach (var suffix in suffixs)
            {
                var list = from d in data where d.EndsWith(suffix) select d;
                outList.AddRange(list);
            }
            return outList;
        }
        
        public static void CreatePath(string path)
        {
            if (!Directory.Exists(path))
            {
                string p = Path.Combine(path, "..");
                string root = Path.GetFullPath(p);
                if (!Directory.Exists(root))
                    CreatePath(root);
            
                Directory.CreateDirectory(path);
            }
        }
        /// <summary>
        /// 清空文件夹
        /// </summary>
        /// <param name="path"></param>
        public static void ClearFolder(string path)
        {
            if (!Directory.Exists(path))
                return;
            try
            {
                //去除文件夹和子文件的只读属性
                //去除文件夹的只读属性
                System.IO.DirectoryInfo fileInfo = new DirectoryInfo(path);
                fileInfo.Attributes = FileAttributes.Normal & FileAttributes.Directory;

                //去除文件的只读属性
                System.IO.File.SetAttributes(path, System.IO.FileAttributes.Normal);

                //判断文件夹是否还存在
                if (Directory.Exists(path))
                {
                    foreach (string f in Directory.GetFileSystemEntries(path))
                    {
                        if (File.Exists(f))
                        {
                            //如果有子文件删除文件
                            File.Delete(f);
                        }
                        else
                        {
                            //循环递归删除子文件夹
                            ClearFolder(f);
                        }
                    }
                    //删除空文件夹
                    Directory.Delete(path);
                }
            }
            catch (Exception ex) // 异常处理
            {
                Debug.Log(ex.Message.ToString());// 异常信息
            }
        }
    }
}