using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;
using System;
#if UNITY_EDITOR
public class Clippy : EditorWindow
{
  public Texture logo;

  private int tab = 0;
  #region test
  string myString = "Hello World";
  bool groupEnabled;
  bool myBool = true;
  float myFloat = 1.23f;
  #endregion

  [MenuItem("?/Controller")]
  static void ShowWindow()
  {
    EditorWindow window = GetWindow(typeof(Clippy));
    window.Show();
  }

  Texture2D image;
  void Awake()
  {
    image = Resources.Load("Assets/test.png", typeof(Texture2D)) as Texture2D;

  }

  void OnGUI()
  {
    tab = GUILayout.Toolbar(tab, new string[] { "Scene", "Tools", "Etat", "Note", "Test" });
    switch (tab)
    {
      #region Scene
      default:
      case 0:
        ToolbarScene();
        break;
      #endregion

      case 1:
        ToolbarTools();
        break;
      #region etat
      case 2:
        ToolbarEtat();
        break;
      #endregion
      #region etat
      case 3:
        ToolbarNote();
        break;
      #endregion
      #region test
      case 4:
        GUILayout.Label("Test");
        GUILayout.BeginHorizontal();
        GUILayout.Button("Mini Button", EditorStyles.miniButton);
        GUILayout.Button("Mini Button");
        GUILayout.Button("Mini Button", EditorStyles.popup);
        GUILayout.Button("Mini Button", EditorStyles.miniButton);
        GUILayout.EndHorizontal();


        GUILayout.Label("Base Settings", EditorStyles.boldLabel);
        myString = EditorGUILayout.TextField("Text Field", myString);

        groupEnabled = EditorGUILayout.BeginToggleGroup("Optional Settings", groupEnabled);
        myBool = EditorGUILayout.Toggle("Toggle", myBool);
        myFloat = EditorGUILayout.Slider("Slider", myFloat, -3, 3);
        EditorGUILayout.EndToggleGroup();
        break;
        #endregion
    }
  }

  private void ToolbarNote()
  {
    GUILayout.Label("Notes", EditorStyles.boldLabel);
    GUILayout.BeginHorizontal();
    GUILayout.Label("Quick Search:");
    GUILayout.Label("Ctrl + Shift + O", EditorStyles.boldLabel );
    GUILayout.EndHorizontal();


  }

  public string sceneController = "";

  private void ToolbarScene()
  {
    GUILayout.Label("Scene", EditorStyles.boldLabel);
    sceneController = EditorGUILayout.TextField("Text Field", sceneController);
    if (GUILayout.Button("Load controllers"))
    {
      // SceneManager.LoadScene(SceneController.Instance.controlScene, LoadSceneMode.Additive);
      EditorSceneManager.OpenScene("Assets/Scenes/ControlScene.unity", OpenSceneMode.Additive);
      //EditorSceneManager.OpenScene(SceneController.Instance.controlScene, OpenSceneMode.Additive);
    }
  }
  private void ToolbarEtat()
  {
    GUILayout.Label("Scene", EditorStyles.boldLabel);
  }
  private void ToolbarTools()
  {
    GUILayout.Label("Select", EditorStyles.boldLabel);
    GUILayout.BeginHorizontal();
    if (GUILayout.Button("Main Camera"))
    {
      UsefullExtension.SelectMainCamera();
    }
    if (GUILayout.Button("Joueur"))
    {
      UsefullExtension.SelectPlayer();
    }
    if (GUILayout.Button("Game"))
    {
      UsefullExtension.SelectGameController();
    }
    GUILayout.EndHorizontal();
    GUILayout.BeginHorizontal();
    if (GUILayout.Button("Lights"))
    {
      UsefullExtension.SelectLights();
    }
    if (GUILayout.Button("Canvas"))
    {
      UsefullExtension.SelectCanvas();
    }

    GUILayout.EndHorizontal();

  }
}
#endif
