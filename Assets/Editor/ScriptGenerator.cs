using System;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UIElements.Button;

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
public class EnemyCreatorEditor : EditorWindow
{
    private Label weaponScriptSection;
    private Label shootingTypeDesc;
    private TextField scriptName;
    private FloatField speedField;

    private Button createButton;
    private Button burstButton, singleShotButton, automaticButton, boltButton, pumpButton;

    [MenuItem("Tools/EnemyCreator")]
    public static void ShowMyEditor()
    {
        EditorWindow editorWindow = GetWindow<EnemyCreatorEditor>();
        editorWindow.titleContent = new GUIContent("Enemy Creator");
    }

    private void OnEnable()
    {
        var mainContainer = new VisualElement();

        //Main container
        #region
        //Create style of the window
        mainContainer.style.flexDirection = FlexDirection.Column;

        mainContainer.style.paddingTop = 5;
        mainContainer.style.paddingBottom = 15;
        mainContainer.style.paddingLeft = 15;
        mainContainer.style.paddingRight = 15;

        mainContainer.style.alignItems = Align.FlexStart;

        //Labels
        #region
        weaponScriptSection = CreateLabel("New weapon characteristics", Align.Center, 20, 5, 20);
        weaponScriptSection.style.unityTextOutlineWidth = 1f;

        shootingTypeDesc = CreateLabel("Choose shooting type", Align.Center);
        #endregion

        scriptName = CreateInputField("Script name: ", marginTop: 15);
        scriptName.style.alignSelf = Align.Center;
        scriptName.style.width = 300;

        createButton = CreateButton("Create weapon script", () => ScriptGenerator.CreateScript(scriptName.text), Align.Center, marginTop: 10,buttonWidth: 250, buttonHeight: 25) ;
        createButton.style.alignSelf = Align.Center;
        createButton.SetEnabled(true);
        #endregion

        //Shooting type buttons container
        #region
        var shootingTypeContainer = new VisualElement();
        shootingTypeContainer.style.flexDirection = FlexDirection.Row;
        shootingTypeContainer.style.justifyContent = Justify.SpaceBetween;

        shootingTypeContainer.style.alignSelf = Align.Center;

        singleShotButton = CreateButton("Single Shot", () => { }, marginRight: 5, marginLeft: 5);
        automaticButton = CreateButton("Automatic", () => { }, marginLeft: 10, marginRight: 10);
        burstButton = CreateButton("Burst", () => { }, marginLeft: 10, marginRight: 10);
        pumpButton = CreateButton("Pump", () => { }, marginLeft: 10, marginRight: 10);
        boltButton = CreateButton("Bolt", () => { }, marginLeft: 10, marginRight: 5);
        
        shootingTypeContainer.Add(singleShotButton);
        shootingTypeContainer.Add(automaticButton);
        shootingTypeContainer.Add(burstButton);
        shootingTypeContainer.Add(pumpButton);
        shootingTypeContainer.Add(boltButton);
        #endregion

        //Distribution of UI elements
        #region

        //Section 1
        mainContainer.Add(weaponScriptSection);
        mainContainer.Add(shootingTypeDesc);
        mainContainer.Add(shootingTypeContainer);
        mainContainer.Add(scriptName);
        mainContainer.Add(createButton);
        #endregion

        //Display all UI elements in the editor window
        rootVisualElement.Add(mainContainer);
    }

    //UI Element methods
    #region
    private Label CreateLabel(string text, Align alignStyle, int fontSize = 13,
        int marginTop = 5, int marginBottom = 5, int marginLeft = 0, int marginRight = 0)
    {
        Label label = new Label(text);

        label.style.alignSelf = alignStyle;

        label.style.marginTop = marginTop;
        label.style.marginBottom = marginBottom;
        label.style.marginLeft = marginLeft;
        label.style.marginRight = marginRight;

        label.style.fontSize = fontSize;

        return label;
    }

    private Button CreateButton(string text, Action buttonCallback, Align alignStyle = default,
        int marginTop = 5, int marginBottom = 5, int marginLeft = 0, int marginRight = 0, 
        int buttonWidth = 100, int buttonHeight = 35)
    {
        Button button = new Button(buttonCallback) { text = text };

        button.style.marginTop = marginTop;
        button.style.marginBottom = marginBottom;
        button.style.marginLeft = marginLeft;
        button.style.marginRight = marginRight;

        button.style.width = buttonWidth;
        button.style.height = buttonHeight;

        return button;
    }

    private TextField CreateInputField(string text, Align alignStyle = default, 
        int marginTop = 5, int marginBottom = 5, int marginLeft = 0, int marginRight = 0,
        int inputBoxWidth = 100, int inputBoxHeight = 25)
    {
        TextField textField = new TextField(text);

        textField.style.marginTop = marginTop;
        textField.style.marginBottom = marginBottom;
        textField.style.marginLeft = marginLeft;
        textField.style.marginRight = marginRight;

        textField.style.width = inputBoxWidth;
        textField.style.height = inputBoxHeight;

        return textField;
    }
    #endregion
}