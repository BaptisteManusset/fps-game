using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(Rigidbody))]
[ExecuteInEditMode]
public class Levitation : MonoBehaviour
{
  #region private
  private RaycastHit hit;
  private LineRenderer lineRenderer;
  private Rigidbody rb;
  private Vector3 power;
  float minDistance = 99999999;
  #endregion

  #region dashboard

  [BoxGroup("Dashboard"), Label("Toggle Graph.")] public bool togglePlot = false;
  [BoxGroup("Dashboard"), ShowIf("togglePlot"), Label("Interval")] public int intervalTime = 10;


  #endregion

  #region hover
  [BoxGroup("Hover control")] public float rayLength = 200;
  [BoxGroup("Hover control"), Label("Masque de Layer")] public LayerMask layers;

  //bool hovering = false;
  [BoxGroup("Hover control")] public float repulsionPower = 2000;
  [BoxGroup("Hover control")] Vector3 hoverForce = new Vector3(0, 0, 0);
  [BoxGroup("Hover control")] public GameObject hoverPosition;

  #endregion

  #region vitesse
  [BoxGroup("Vitesse")] public float speed = 5;
  [Header("Information")]
  [BoxGroup("Vitesse"), ShowIf("togglePlot")] public AnimationCurve accelerationPlot = new AnimationCurve();
  [BoxGroup("Vitesse"), ShowIf("togglePlot"), Label("Acceleration")] public float displayAcceleration;
  #endregion

  #region rotation
  [BoxGroup("Rotation")] public float rotationSpeed = 5;
  [Header("Information")]
  [BoxGroup("Rotation"), ShowIf("togglePlot")] public AnimationCurve rotationPlot = new AnimationCurve();
  [BoxGroup("Rotation"), ShowIf("togglePlot"), Label("Rotation")] public float rotationFinal;
  #endregion

  [BoxGroup("Input")] public float boost = 2;

  [BoxGroup("Capteurs")] public GameObject[] capteurs;


  void Awake()
  {
    lineRenderer = GetComponent<LineRenderer>();
    lineRenderer.useWorldSpace = true;
    rb = GetComponent<Rigidbody>();
  }

  private void Update()
  {
    #region recupération des input
    float translation = Input.GetAxis("Vertical") * speed;
    float rotation = Input.GetAxis("Horizontal") * rotationSpeed;
    float sprint = Input.GetAxis("Sprint") * boost;
    Debug.Log(sprint);
    #endregion

    minDistance = 9999999;

    BetterRaycast();
   /* for (int i = 0; i < capteurs.Length; i++)
    {
      GameObject capteur = capteurs[i];
      Capteurs(capteur);
    }*/

    #region calcul des forces
    hoverForce = new Vector3(0, repulsionPower / hit.distance, 0);
    hoverForce.x = translation + sprint * translation;
    hoverForce.y = Mathf.Clamp(hoverForce.y, 0, 100000);
    hoverForce.z = 0;


    rotationFinal = rotationSpeed * rotation;
    #endregion

    #region visual
    if (togglePlot)
    {
      if (Time.frameCount % intervalTime == 0)
      {
        accelerationPlot.AddKey(Time.realtimeSinceStartup, hoverForce.y);
      }
    }
    displayAcceleration = hoverForce.x;
    lineRenderer.widthMultiplier = hoverForce.y / 1000;
    #endregion

    #region application des forces
    if (hoverForce.y != Mathf.Infinity)
    {
      rb.AddRelativeForce(hoverForce);
    }
    rb.rotation = Quaternion.Euler(0, rotationFinal, 0) * transform.rotation;
    #endregion
  }

  // function qui execute le raycast
  void BetterRaycast()
  {

    lineRenderer.SetPosition(0, hoverPosition.transform.position);
    if (Physics.Raycast(hoverPosition.transform.position, -hoverPosition.transform.up, out hit, rayLength, layers))
    {
      lineRenderer.SetPosition(1, hit.point);
      if(minDistance > hit.distance)
      {
        minDistance = hit.distance;
      }

      //hovering = true;
    }
    else
    {
      lineRenderer.SetPosition(1, transform.TransformPoint(Vector3.down * 20));
    }
    //hovering = false;
  }

  void Capteurs(GameObject capteur)
  {
   


    if (Physics.Raycast(capteur.transform.position, -transform.up, out hit, rayLength, layers))
    {
      DebugExtension.DebugArrow(capteur.transform.position, hit.point - capteur.transform.position, Color.red);
      if (minDistance > hit.distance)
      {
        minDistance = hit.distance;
      }
    }

  }
}
