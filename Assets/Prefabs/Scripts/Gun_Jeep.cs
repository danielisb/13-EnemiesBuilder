using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun_Jeep : MonoBehaviour
{
    // LOOK AT CONTROL

    //--------------------------CONTROLS------------------------

    [Header("Objects")]
    public Transform BulletM4a1; // GameObject to instantiate bullet
    public GameObject enemyGO; // GameObject para alvo do inimigo
    public GameObject firing; // GameObject to instantiate firing particles

    [Header("LookAt")]
    public GameObject turretAxis; //move baseGun (Axises X, Z)
    public GameObject GunAxis; //move Gun (Axis Y)
    
    [Header("Do Not Use")]
    public bool activeShoot; // active shoot
    public bool activeLookAt; // active lookat

    //---------------------------------------------------------
    GameObject particles;

    void Start()
    {
        //activeShoot = false;
        activeLookAt = false;
    }
    void Update()
    {
        if (activeLookAt == true)
        {
            //Debug.Log("LOOKAT");
            lookAtJeep();
        }                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    
    }
    void lookAtJeep()
    {
        //move baseGun (Axises X, Z)
        Vector3 moveBase = new Vector3 (enemyGO.transform.position.x, transform.position.y, enemyGO.transform.position.z);
        turretAxis.transform.LookAt(moveBase);
        //move gun (Axis Y)
        Vector3 moveGun = new Vector3 (transform.position.x, enemyGO.transform.position.y, transform.position.z);
        GunAxis.transform.LookAt(moveGun);
    }
}
