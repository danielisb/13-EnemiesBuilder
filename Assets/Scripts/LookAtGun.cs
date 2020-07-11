using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtGun : MonoBehaviour
{
    public GameObject vehiclesManagerGO;
    public GameObject turretGO;
    public Transform target; // target GameObject
    public float maxAngle = 0f; // limit angle
    public float speedMove; // limit speed to move GameObject
    private Quaternion baseRotation;
    private Quaternion targetRotation;
    LookAtTurret getTurret;
    vehiclesManager getVehiclesManagerGO;
    public bool activeLookAt;
    void Start()
    {
        activeLookAt = false;
        baseRotation = transform.rotation;
        getTurret = turretGO.GetComponent<LookAtTurret>();
        getVehiclesManagerGO = vehiclesManagerGO.GetComponent<vehiclesManager>();
        target = getVehiclesManagerGO.enemyTarget;
    }
    void Update()
    {   
       if(activeLookAt)
       {
           moveGUN();
           getTurret.activeTurret = true;
       }else
       {
           getTurret.activeTurret = false;
       }
    }
    void moveGUN()
    {
        Vector3 look = target.transform.position - transform.position; 
        look.x = 0; // angle defined to move
         
        Quaternion q = Quaternion.LookRotation (look);
        if (Quaternion.Angle (q, baseRotation) <= maxAngle)
            targetRotation = q;
         
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * speedMove);
    }
}
