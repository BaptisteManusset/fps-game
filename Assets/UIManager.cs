using System;
using Homebrew;
using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using TagAttribute = NaughtyAttributes.TagAttribute;

public class UIManager : MonoBehaviour
{
  [BoxGroup("Health"),Tag] public string UIHealthTag;
  [BoxGroup("Health")] public GameObject UIHealthParent;
  [BoxGroup("Health")] public TextMeshProUGUI UIHealthValue;
  [BoxGroup("Health")] public Image UIHealthFill;


  [BoxGroup("Weapon")] public GameObject UIWeaponParent;
  [BoxGroup("Weapon")] public TextMeshProUGUI UIWeaponValue;
  [BoxGroup("Weapon")] public Image UIWeaponFill;

  internal void FindUI()
  {
    FindUIHealth();


  }
  [Button]
  public void FindUIHealth()
  {
  /*  UIHealthParent = GameObject.FindGameObjectWithTag(UIHealthTag);
    foreach (Transform item in UIHealthParent.transform.parent)
    {
      Debug.Log(item.name, item);
      if (item.name == "value") UIHealthValue = item.gameObject.GetComponent<TextMeshProUGUI>();
      if (item.name == "fill") UIHealthFill = item.gameObject.GetComponent<Image>();
    }*/
  }
}


