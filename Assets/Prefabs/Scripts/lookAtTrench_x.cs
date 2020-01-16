using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookAtTrench_x : MonoBehaviour
{
    public GameObject trenchManagerGO;
    trenchManager getTrenchManager;
    public Transform target; // target GameObject
    public float maxAngle = 0f; // limit angle
    public float speedMove; // limit speed to move GameObject
    private Quaternion baseRotation;
    private Quaternion targetRotation;
    public bool activeLookAt;
    public bool activeVigilant;
    void Start()
    {
        getTrenchManager = trenchManagerGO.GetComponent<trenchManager>();
        target = getTrenchManager.target;
        activeLookAt = false;
        baseRotation = transform.rotation;
    }
    void Update()
    {   
        // Active LookAt
        if(activeLookAt == true)
            moveGUN();
        // Vigilant action
        if(activeVigilant == true)
        {
            vigilantBehavior();
        }
    }
    void moveGUN()
    {
        maxAngle = getTrenchManager.elevation;
        Vector3 look = target.transform.position - transform.position;
        look.z = 35; // angle defined to move
        Quaternion q = Quaternion.LookRotation(look);
        if (Quaternion.Angle (q, baseRotation) <= maxAngle)
            targetRotation = q;
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * speedMove);
    }
    void vigilantBehavior()
    {
        //transform.Rotate(0, 50 * Time.deltaTime, 0);
    }
}
