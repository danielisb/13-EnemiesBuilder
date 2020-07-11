using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookAtVehicle : MonoBehaviour
{
    //--------------------------CONTROLS------------------------
    [Header("Objects")]
    public Transform BulletM4a1; // GameObject to instantiate bullet
    public GameObject target; // GameObject para alvo do inimigo
    public GameObject firing; // GameObject to instantiate firing particles

    [Header("LookAt")]
    public GameObject turretAxis; //move baseGun (Axises X, Z)
    public GameObject GunAxis; //move Gun (Axis Y)
    float min = -0.7f;
    float max = -0.67f;
    float elevationx = 0;

    // Vector3 minVector3 = new Vector3( 0, 1f, 0);
    // Vector3 maxVector3 = new Vector3( 0, -0.6f, 0);
    // float limitY; // test
    
    [Header("Do Not Use")]
    public bool activeLookAt; // active lookat
    //---------------------------------------------------------
    //public Vector3 eulerangles; //test
    GameObject particles;
    void Start()
    {
        activeLookAt = false;
    }
    void Update()
    {
        if (activeLookAt == true)
        {
            //Debug.Log("LOOKAT TRUE");
            lookAtJeep();
        }                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    
    }
    void lookAtJeep()
    {
        //move baseGun (Axises X, Z)
        Vector3 moveBase = new Vector3 (target.transform.position.x, transform.position.y, target.transform.position.z);
        turretAxis.transform.LookAt(moveBase);
        //move gun (Axis Y)
        Vector3 moveGun = new Vector3 (target.transform.position.x, target.transform.position.y, target.transform.position.z);
        GunAxis.transform.LookAt(moveGun);
    }
}
