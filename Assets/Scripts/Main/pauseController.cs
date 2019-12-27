using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pauseController : MonoBehaviour
{
  public GameObject menuPauseUi;

  private void Awake()
  {
    DontDestroyOnLoad(gameObject);
  }


  private void Start()
  {
    Resume();
  }

  void Update()
  {
    if (Input.GetButtonDown("Cancel"))
    {
      if (Game.IsPaused)
      {
        Resume();
      }
      else
      {
        Pause();
      }
    }
  }
  [Button]
  void Pause()
  {
    Time.timeScale = 0f;
    menuPauseUi.SetActive(true);
    Game.IsPaused = true;
    Debug.Log("<color=#695B51><b><size=13> [Pause] </size></b></color>");
  }

  [Button]
  public void Resume()
  {
    Time.timeScale = 1f;
    menuPauseUi.SetActive(false);
    Game.IsPaused = false;
    Debug.Log("<color=#695B51><b><size=13> [Resume] </size></b></color>");
  }


  public void MainMenu()
  {
    Time.timeScale = 1f;
    Debug.Log("Main menu");
  }

  public void QuitGame()
  {
    Debug.Log("quit game");
    Application.Quit();
  }

}
