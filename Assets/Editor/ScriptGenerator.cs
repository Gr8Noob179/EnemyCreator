using UnityEditor;
using System.IO;
using UnityEngine;
using UnityEditor.TerrainTools;
namespace UnityEditor
{
    public class ScriptGenerator : Editor
    {
        [MenuItem("Assets/Create/Weapon C# Script")]
        public static void CreateScript(string scriptName = null)
        {
            //Path where template content stores
            string templatePath = "Assets/Editor/WeaponScriptTemplate.txt";

            //Convert content from txt file
            string templateContent = File.ReadAllText(templatePath);

            //Script name if "scriptName" is null
            string defaultName = "WeaponScript";

            //Replace class name in the template
            string finalScriptContent = templateContent.Replace("#SCRIPTNAME#", scriptName ?? defaultName);

            //Path where created script will store
            string scriptPath = $"Assets/Scripts/Weapons/{scriptName ?? defaultName}.cs";

            //Create script
            File.WriteAllText(scriptPath, finalScriptContent);

            AssetDatabase.Refresh();
        }
    }
}

[ExecuteInEditMode]
[CustomEditor(typeof(WeaponCreator))]
public class WeaponCreatorInspector : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
         
        if (GUILayout.Button("Create new weapon script"))
        {
            ScriptGenerator.CreateScript("TestWeaponScript");
        }
    }
}