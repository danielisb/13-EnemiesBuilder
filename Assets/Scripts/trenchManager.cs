﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trenchManager : MonoBehaviour
{
    //------------------------------------------- CONTROLS ----------------------------------------
    [Header("Objects")]
    public GameObject trenchGO;
    public GameObject _particleSystem; // particles of shoot
    public GameObject captTarget;

    [Header("Settings")]
    public Transform target; // GameObject enemy

    [Range(0, 1400)]
    public float detectionRadius; // Raio de detecção/reconhecimento

    [Range(0, 1200)]
    public float effectiveDistance; // Raio de distância Efetiva

    public sceneBuilder.Behavior identificationAction; // select recognition action
    public sceneBuilder.Behavior effectiveAction; // select effective action

    public float elevation_z;
    public float azimuth_y;

    enum typeShoot // cadence shoot
    {
        Full,
    }

    typeShoot cadenceType; // select cadence
    shootMAG gettingTypeShoot; // Acessa script shootMAG
    lookAtTrench captGunbools; // capt Shoot bool from Gun_Jeep
    DetectTarget detection; // aponta para variável com distância do inimigo no script DetectTarget
    sceneBuilder.Behavior currentState;
    
    //sceneBuilder playerScene;

    bool activeVigilant = false;
    int cadenceTypeShoot;
    void opTypeShoot(bool flag) // cadence Shoot
    {
        //Debug.Log("SHOOTING");
        switch(cadenceType)
        {
            case typeShoot.Full:
                cadenceTypeShoot = 15;
                break;
        }
        gettingTypeShoot.animator.SetBool("isShooting", flag);
        gettingTypeShoot.animator.SetInteger("opType", cadenceTypeShoot); // insere valor inteiro na variável opType no componente Animator de shootMAG
    }
    void Start()
    {
        captGunbools = trenchGO.GetComponent<lookAtTrench>();
        detection = trenchGO.GetComponent<DetectTarget>();
        gettingTypeShoot = _particleSystem.GetComponent<shootMAG>();
        
        captTarget = GameObject.Find("Player");		
        target = captTarget.transform;
        detection.enemiesTag = target.tag; 
        currentState = sceneBuilder.Behavior.Idle; // vehicle start in Vigilant
    }
    void Update()
    {
        trenchBehavior(); 

        switch(currentState)
        {
            case sceneBuilder.Behavior.Idle:
                // .activeVigilant = false;
                opTypeShoot(false);
                //Debug.Log("IDLE");
            break;
            case sceneBuilder.Behavior.Vigilant:
                captGunbools.activeVigilant = true;
                opTypeShoot(false);
                //Debug.Log("VIGILANT");
            break;
            case sceneBuilder.Behavior.Shoot:
                opTypeShoot(true);
                //Debug.Log("SHOOT");
            break;
            default:
                currentState = sceneBuilder.Behavior.Idle;
            break;
        }
    }
    void trenchBehavior()
    {
        // case raycast detect something
        if (!detection.hitDetect)
        {
            captGunbools.activeLookAt = false;
            return;
        }
        // Recognition Action
        if (detection.currentDistance <= detectionRadius && 
            detection.currentDistance > effectiveDistance)
        {
            captGunbools.activeLookAt = true;
            currentState = identificationAction;
            //Debug.Log("RECOGNITION");
        }
        else
        // Effective Action
        if (detection.currentDistance <= effectiveDistance)
        {
            captGunbools.activeLookAt = true;
            currentState = effectiveAction;
            //Debug.Log("EFFECTIVE");
        }
        else
        {   // back to Vigilant
            currentState = sceneBuilder.Behavior.Vigilant;
            //Debug.Log("DEFAULT");
            opTypeShoot(false);
        }
    }
}