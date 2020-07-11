using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtTurret : MonoBehaviour
{
    public GameObject gunGO;
    public GameObject turretAxis; // turret GameObject
    public Transform turretTarget; // target GameObject
    public float maxAngle = 360f; // limit angle
    public float speedMove; // limit speed to move GameObject
    private Quaternion baseRotation;
    private Quaternion targetRotation;
    LookAtGun getLookAtGUN;

    public bool activeTurret;
    void Start()
    {
        activeTurret = false;
        getLookAtGUN = gunGO.GetComponent<LookAtGun>();
        speedMove = getLookAtGUN.speedMove;
    }
    void Update()
    {
        if(activeTurret == true)
        {
            moveTurret();
        }
    }
    void moveTurret()
    {
        turretTarget = getLookAtGUN.target;
        Vector3 look = turretTarget.transform.position - transform.position;
        look.y = 0; // angle defined to move

        Quaternion q = Quaternion.LookRotation (look);
        if (Quaternion.Angle (q, baseRotation) <= maxAngle)
            targetRotation = q;
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * speedMove);
    }
}