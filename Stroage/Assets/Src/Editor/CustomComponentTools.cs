using UI.Base;
using UnityEditor;
using UnityEngine;

namespace Src.Editor
{
    public class CustomComponentTools
    {
        [MenuItem("Assets/CustomComponentTools/GenerateExpressionObject")]
        public static void GenerateExpressionObject()
        {
            var select = Selection.activeObject;
            if (select == null)
                return;
            var path = AssetDatabase.GetAssetPath(select);
            if (string.IsNullOrEmpty(path))
                return;
            var name = "NewExpressionObject";
            ScriptableObject obj = ScriptableObject.CreateInstance<AnimationScriptableObject>();
            AssetDatabase.CreateAsset(obj, path + "/" + name + ".asset");

            AssetDatabase.Refresh();
        }
    }
}