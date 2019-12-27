using NaughtyAttributes;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "gunStat", menuName = "PERSO/GunProfil", order = 1)]
public class GunStat : ScriptableObject
{
  //public GameObject bullet;
  public GameObject canon;
  public string gunName = "Gun";
  [MinValue(1), Slider(1, 100)] public int quantity = 1;
  //[BoxGroup("technique")] public float bulletSpeed = 30;
  [BoxGroup("Recoil")] public int recoil = 10;
  [BoxGroup("Technique"), Slider(0, 1)] public float spreadFactor = 0;
  [BoxGroup("Raycast")] public float rayLength = 200;
  [BoxGroup("Degas")] public float damage = 1;
  [BoxGroup("Explosion"), Slider(0f, 10000f)] public float ExplosionForce = 3;
  [BoxGroup("Explosion")] public float ExplosionRadius = 5;

}


