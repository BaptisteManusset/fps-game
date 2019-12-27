using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class WeaponInteract : MonoBehaviour
{
  [BoxGroup("Arme Actuel")] public Weapon weapon;
  [BoxGroup("Inventaire")] public int selectWeapon;
  [Space(5)]
  [BoxGroup("Inventaire")] [Label("Liste des armes")] public Weapon[] weapons = null;

  private int recoilDuration = 0;
  [BoxGroup("Raycast")] public Camera cam;
  RaycastHit hit;
  [BoxGroup("Raycast")] public LayerMask layers;
  [BoxGroup("Raycast")] public float rayLength = 200;
  [BoxGroup("UI")] public TextMeshProUGUI ui;

  private AudioSource aus;

  private void Awake()
  {
    aus = GetComponent<AudioSource>();
  }

  private void Start()
  {
    ui = Game.uiManager.UIWeaponValue;
    DrawUi();
  }

  private void FixedUpdate()
  {
    if (Input.GetButton("Fire1"))
    {
      if (weapon.IsRaycast)
      {
        FireRaycast();
      }
      else
      {
        FireProjectile();
      }
    }
    if (recoilDuration < weapon.recoil)
    {
      recoilDuration++;
    }

    if (Input.GetAxis("Mouse ScrollWheel") != 0f)
    {
      if (Input.GetAxis("Mouse ScrollWheel") > 0f)
      {
        selectWeapon++;
        if (selectWeapon > weapons.Length - 1)
        {
          selectWeapon = 0;
        }

      }
      else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
      {
        selectWeapon--;
        if (selectWeapon < 0)
        {
          selectWeapon = weapons.Length - 1;
        }

      }

      weapon = weapons[selectWeapon];
      UpdateGraphisme();
    }


    WeaponSwitch();
  }

  #region Selection

  void GetGunsList()
  {
    if (weapons == null)
    {
      //weapons = null;
      weapons = new Weapon[transform.childCount];
      for (int i = 0; i < transform.childCount; ++i)
      {
        Debug.Log(transform.GetChild(i));
        weapons[i] = transform.GetChild(i).GetComponent<Weapon>();
      }
    }
  }

  void EnableWeapons()
  {
    if (weapons.Length == 0)
    {
      GetGunsList();
    }

    foreach (var item in weapons)
    {
      item.gameObject.SetActive(true);
    }
  }

  [Button]
  void EnableCurrentWeapon()
  {
    GetGunsList();
    SelectedWeapon();
    UpdateGraphisme();

    weapon = weapons[selectWeapon];
  }

  void UpdateGraphisme()
  {
    for (int i = 0; i < weapons.Length; i++)
    {
      weapons[i].gameObject.SetActive(false);
    }
    weapons[selectWeapon].gameObject.SetActive(true);
    DrawUi();
  }


  /**
 * retourne l'arme actuel et redéfinie l'arme selectionné à 0 si la valeur est fausse
 */
  public int SelectedWeapon()
  {
    if (selectWeapon > weapons.Length - 1)
    {
      Debug.Log("<color=red><b>L'arme selectionnée posséde un numéro supérieur à celui du nombre d'arme</b>\n Il a était redéfinie à 0</color>");
      return 0;
    }
    return selectWeapon;
  }

  /**
   * permet de remplace l'arme actuel par une nouvelle
   */
  public void Change(int newWeapon)
  {
    weapons[selectWeapon].gameObject.SetActive(false);
    GetGunsList();  //verifie que la liste d'arme existe
    selectWeapon = newWeapon;
    selectWeapon = SelectedWeapon();
    // selectWeapon = newWeapon;
    weapons[selectWeapon].gameObject.SetActive(true);
    EnableCurrentWeapon();
  }

  [Button("Reset All Stat")]
  public void noone()
  {
    weapon = null;
    weapons = null;
    selectWeapon = 0;
  }

  [Button]
  public void nextWeapon()
  {
    selectWeapon++;
    SelectedWeapon();
    EnableCurrentWeapon();
  }

  #endregion

  #region Tir


  void FireRaycast()
  {

    //Debug.Log("<color=red>FIRE !!!</color>");
    if (recoilDuration >= weapon.recoil)
    {
      //drawLine(true);
      //Invoke("drawLine(false)", 0.1f);


      //aus.clip = weapon.shootSound;

      aus.PlayOneShot(weapon.shootSound);
      Vector3 direction = transform.forward;
      for (int i = 0; i < weapon.quantity; i++)
      {

        #region ajout du spread
        direction.x += Random.Range(-weapon.spreadFactor, weapon.spreadFactor);
        direction.y += Random.Range(-weapon.spreadFactor, weapon.spreadFactor);
        direction.z += Random.Range(-weapon.spreadFactor, weapon.spreadFactor);
        Vector3 position = cam.ViewportToWorldPoint(new Vector3(0.5F, 0.5F, 0));
        #endregion

        if (Physics.Raycast(position, direction, out hit, rayLength, layers))
        {
          #region Apparition et disparition de la balle
          Debug.DrawRay(weapon.canon.transform.position, weapon.canon.transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);

          var bulletInstance = Instantiate(weapon.bullet, hit.point, Quaternion.identity);
          //Destroy(bulletInstance, 5f);
          float des = Random.Range(weapon.bulletDelayGone.x, weapon.bulletDelayGone.y);
          Destroy(bulletInstance, des);


          bulletInstance.transform.parent = hit.collider.gameObject.transform;
          #endregion

          #region Gestion de la vie

          //Deprecated
          StateController2 hl = hit.collider.gameObject.GetComponent<StateController2>();
          if (hl != null)
          {
            hl.takeDamage(weapon.damage);
          }

          StateMachineMain smm = hit.collider.gameObject.GetComponent<StateMachineMain>();
          if (smm != null)
          {
            smm.takeDamage(weapon.damage);
          }

          #endregion



          #region application de la force d'explosion
          Collider[] targets = Physics.OverlapSphere(hit.point, radius: 5);
          foreach (Collider nearObject in targets)
          {
            if (nearObject.CompareTag("Player"))
              continue;

            Rigidbody rb = nearObject.GetComponent<Rigidbody>();

            if (rb != null)
            {
              rb.AddExplosionForce(weapon.ExplosionForce, hit.point, weapon.ExplosionRadius);
            }


          }
          #endregion
        }
      }
      //recoilDuration = weapon.recoil;
      recoilDuration = 0;

    }
  }


  void FireProjectile()
  {
    if (recoilDuration >= weapon.recoil)
    {
      aus.clip = weapon.shootSound;

      aus.Play();
      Vector3 direction = transform.forward;
      for (int i = 0; i < weapon.quantity; i++)
      {
        var slt = Instantiate(weapon.bullet, weapon.canon.transform.position, transform.rotation);
        slt.GetComponent<Rigidbody>().AddForce(slt.transform.forward * 1000);
        //Destroy(Random.Range(0,3));
        float des = Random.Range(weapon.bulletDelayGone.x, weapon.bulletDelayGone.y);
        Destroy(slt, des);
      }
      recoilDuration = 0;

    }
  }

  #endregion


  void DrawUi()
  {
    ui.text = weapon.gunName;
  }
  void WeaponSwitch()
  {


    if (Input.GetKeyDown(KeyCode.Alpha1))
    {
      Change(0);
    }
    if (Input.GetKeyDown(KeyCode.Alpha2))
    {
      Change(1);
    }
    if (Input.GetKeyDown(KeyCode.Alpha3))
    {
      Change(2);
    }
    if (Input.GetKeyDown(KeyCode.Alpha4))
    {
      Change(3);
    }
    if (Input.GetKeyDown(KeyCode.Alpha5))
    {
      Change(4);
    }
    if (Input.GetKeyDown(KeyCode.Alpha6))
    {
      Change(5);
    }
    if (Input.GetKeyDown(KeyCode.Alpha7))
    {
      Change(6);
    }
    if (Input.GetKeyDown(KeyCode.Alpha8))
    {
      Change(7);
    }
    if (Input.GetKeyDown(KeyCode.Alpha9))
    {
      Change(8);
    }
    if (Input.GetKeyDown(KeyCode.Alpha0))
    {
      Change(9);
    }
  }
}
