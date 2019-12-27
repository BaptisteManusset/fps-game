using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SceneController : Singleton<SceneController>
{
  public SceneReference scene;
  public SceneReference controlScene;
  // (Optional) Prevent non-singleton constructor use.
  protected SceneController() { }

  private void Awake()
  {
    DontDestroyOnLoad(gameObject);
  }
}





