using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookAtTrench : MonoBehaviour
{
    public GameObject trenchManagerGO;
    public GameObject _lookAtTrenchx;
    trenchManager getTrenchManager;
    public Transform target; // target GameObject
    public float maxAngle = 0f; // limit angle
    public float speedMove; // limit speed to move GameObject
    private Quaternion baseRotation;
    private Quaternion targetRotation;
    public bool activeLookAt;
    public bool activeVigilant;
    lookAtTrench_x activeLookAt_X;
    Quaternion qTo;
    float speed = 1.25f;
    float rotateSpeed = 3.0f;
    float timer = 0;

    public Transform p1;
    public Transform p2;

     private float timeCount = 0.0f;
    void Start()
    {
        getTrenchManager = trenchManagerGO.GetComponent<trenchManager>();
        activeLookAt_X = _lookAtTrenchx.GetComponent<lookAtTrench_x>();
        target = getTrenchManager.target;
        activeLookAt = false;
        baseRotation = transform.rotation;
        //qTo = Quaternion.Euler(Vector3(0.0,Random.Range(-180.0, 180.0), 0.0));
    }
    void Update()
    {   
        // Active LookAt
        // if(activeLookAt == true)
        //     activeLookAt_X.activeLookAt = true;
        //     moveGUN();
        // Vigilant action
        if(activeVigilant == true)
        {
            vigilantBehavior();
            //activeLookAt_X.activeVigilant = true;
        }
    }
    void moveGUN()
    {
        maxAngle = getTrenchManager.azimuth;
        Vector3 look = target.transform.position - transform.position;
        look.y = 0; // angle defined to move
        Quaternion q = Quaternion.LookRotation(look);
        if (Quaternion.Angle (q, baseRotation) <= maxAngle)
            targetRotation = q;
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * speedMove);
    }
    void vigilantBehavior()
    {
        //transform.Rotate(0, 50 * Time.deltaTime, 0);     
        //Vector3 rot = new Vector3(0f, Random.rotation.y, 0f);   
        transform.rotation = Quaternion.Euler(new Vector3(0f ,Random.Range(-20f, 20f), 0f)); 

       
        // Vector3 euler;
        // euler.y = Random.Range(-20f, 20f);    
        // transform.rotation = Random.rotation;

        // Vector3 euler = transform.eulerAngles;
        // euler.y = Random.Range(90f, 98);
        // transform.eulerAngles = euler * Time.deltaTime * 1;

        // int i;
        // for(i=0; i>5; i++)
        // {
        //     euler.y = Random.Range(-20, 20f);
        //     transform.rotation = euler.y;
        // }
        // Quaternion euler;
        // euler = 20;
        // transform.rotation = Quaternion.Lerp(transform.rotation, euler, Time.deltaTime * 1);
    }
}

//  #pragma strict
//  var noTarget = true;
//  var qTo : Quaternion;
//  var speed = 1.25;
//  var rotateSpeed = 3.0;
//  var timer = 0.0;
 
//  function Start() {
//    qTo = Quaternion.Euler(Vector3(0.0,Random.Range(-180.0, 180.0), 0.0));
 
//  }
 
//  function Update() {
 
//      timer += Time.deltaTime;
 
//      if(noTarget == true) {//when not targeting hero     
//          if(timer > 2) { // timer resets at 2, allowing .5 s to do the rotating
//                qTo = Quaternion.Euler(Vector3(0.0,Random.Range(-180.0, 180.0), 0.0));  
//                timer = 0.0;
//              }
//          transform.rotation = Quaternion.Slerp(transform.rotation, qTo, Time.deltaTime * rotateSpeed);
//          transform.Translate(Vector3.forward * speed * Time.deltaTime);
//      }
//  }