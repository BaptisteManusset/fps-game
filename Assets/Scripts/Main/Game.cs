using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
  public static bool IsPaused = false;

  #region référence
  public static UIManager uiManager;
  [Tooltip("Pause Controller")] public pauseController PC;
  #endregion

  #region Scenes
  public static SceneReference sceneController;
  #endregion


  private void Awake()
  {
    DontDestroyOnLoad(gameObject);
    uiManager = FindObjectOfType<UIManager>();
  }


  void OnEnable()
  {
    SceneManager.sceneLoaded += OnSceneLoaded;
  }

  void OnDisable()
  {
    SceneManager.sceneLoaded -= OnSceneLoaded;
  }

  void OnSceneLoaded(Scene scene, LoadSceneMode mode)
  {
    //player = GameObject.FindWithTag("Player");
  }
}
