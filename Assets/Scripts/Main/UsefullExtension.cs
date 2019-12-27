using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class UsefullExtension : MonoBehaviour
{
  public static void SelectMainCamera()
  {
    if (Camera.main != null)
      Selection.activeObject = Camera.main.gameObject;
  }
  public static void SelectPlayer()
  {
    Selection.activeObject = GameObject.FindWithTag("Player");
  }
  public static void SelectGameController()
  {
    Selection.activeObject = GameObject.FindObjectOfType<Game>();
  }
  public static void SelectLights()
  {
    Light[] lights = FindObjectsOfType(typeof(Light)) as Light[];
    GameObject[] gos = new GameObject[lights.Length];

    for (int i = 0; i < lights.Length; i++)
    {
      gos[i] = (lights[i]).gameObject;
    }

    Selection.objects = gos;
  }
  public static void SelectCanvas()
  {

    Selection.objects = FindObjectsOfType<Canvas>();
  }

}
