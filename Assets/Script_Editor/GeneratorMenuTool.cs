#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using GameFunctions;
using GameFunctions.Editors;
using System.IO;

public static class GeneratorMenuTool {

    [MenuItem("Tools/Generator Save Model")]
    public static void Generator() {
        string str = Path.Combine(Application.dataPath, "Script", "SaveModel");
        GFEBufferEncoderGenerator.GenModel(str);
    }

}
#endif