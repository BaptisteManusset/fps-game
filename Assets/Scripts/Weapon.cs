using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Weapon : MonoBehaviour
{
  [BoxGroup("Recoil")] public int recoil = 10;

  [BoxGroup("Technique")] public GameObject canon;
  [BoxGroup("Technique")] public string gunName = "Gun";
  [BoxGroup("Technique")] public float spreadFactor = 0;

  [BoxGroup("Degas")] public float damage = 1;
  [BoxGroup("Degas"), Slider(0f, 10000f)] public float ExplosionForce = 3;
  [BoxGroup("Degas")] public float ExplosionRadius = 5;

  public enum Ammo { little, shotgun, heavy, plasma };
  [BoxGroup("Bullet")] public GameObject bullet;
  [BoxGroup("Bullet"), MinMaxSlider(0,20)] public Vector2 bulletDelayGone;
  [BoxGroup("Bullet")] public Ammo ammo;
  [BoxGroup("Bullet")] [MinValue(1), Slider(1, 100)] public int quantity = 1;

  [BoxGroup("Sound")] public AudioClip shootSound;

  public bool IsRaycast = true;


}

