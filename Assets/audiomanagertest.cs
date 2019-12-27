using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audiomanagertest : MonoBehaviour
{
  public AudioClip music2;
  public AudioClip music3;


  [Button]
  public void test()
  {
   // AudioManager.Instance.PlayMusic(music2);
  }
  [Button]
  public void tests()
  {
    //AudioManager.Instance.PlayMusicWithCrossFade(music3);
  }
}
