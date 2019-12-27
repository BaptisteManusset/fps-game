using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sensor
{
  public Transform capteur;
  [Range(0, 10)]
  public float distance;
}


public class HoverMotor : MonoBehaviour
{

  public float speed = 90f;
  public float turnSpeed = 5f;
  public float hoverForce = 65f;
  public float hoverHeight = 3.5f;
  private float powerInput;
  private float turnInput;
  private Rigidbody carRigidbody;
  public Sensor[] sensors;
  public Transform offset;
  bool brake;
  public float brakePower = 5;
  float force;

  void Awake()
  {
    carRigidbody = GetComponent<Rigidbody>();
  }

  void Update()
  {





    if (Input.GetAxis("Vertical") > 0)
    {
    }
    powerInput = Input.GetAxis("Vertical");

    turnInput = Input.GetAxis("Horizontal");
    brake = Input.GetKey("space");
    if (brake) powerInput = 0;

  }

  void FixedUpdate()
  {


    Ray ray = new Ray(offset.position, -offset.up);

    hover(ray, hoverHeight);

    force = powerInput * speed;

    carRigidbody.AddRelativeForce(0f, 0f, force);

    if (brake)
    {
      carRigidbody.AddRelativeForce(0f, 0f, -brakePower);
    }

    carRigidbody.AddRelativeTorque(0f, turnInput * turnSpeed, 0f);


    if (sensors.Length > 0)
    {
      for (int i = 0; i < sensors.Length; i++)
      {

        Ray sensorRay = new Ray(sensors[i].capteur.position, sensors[i].capteur.forward);
        hover(sensorRay, sensors[i].distance);
        RaycastHit sensorsHit;
        if (Physics.Raycast(sensorRay, out sensorsHit, sensors[i].distance))
        {
          Debug.DrawRay(sensors[i].capteur.position, sensors[i].capteur.forward * sensors[i].distance, Color.green);
        }
      }
    }
  }

  void hover(Ray ray, float hoverHeight)
  {
    RaycastHit hit;
    if (Physics.Raycast(ray, out hit, hoverHeight))
    {
      Debug.DrawRay(offset.position, -offset.up * hoverHeight, Color.green);
      float proportionalHeight = (hoverHeight - hit.distance) / hoverHeight;
      Vector3 appliedHoverForce = Vector3.up * proportionalHeight * hoverForce;
      carRigidbody.AddForce(appliedHoverForce, ForceMode.Acceleration);
    }
  }


}

