using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trenchManager : MonoBehaviour
{
    //------------------------------------------- CONTROLS ----------------------------------------
    [Header("Objects")]
    public GameObject trenchGO;
    public GameObject _particleSystem; // particles of shoot

    [Header("Settings")]
    public Transform target; // GameObject enemy
    [Range(0, 1400)]
    public float detectionRadius; // Raio de detecção/reconhecimento
    [Range(0, 1200)]
    public float effectiveDistance; // Raio de distância Efetiva
    
    // public enum Behavior // Comportamentos do veículo
    // {
    //     Idle,        
    //     Vigilant,
    //     Shoot,
    //     Move,
    // }
    public objectsGenerator.Behavior identificationAction; // select recognition action
    public objectsGenerator.Behavior effectiveAction; // select effective action
    public float elevation;
    public float azimuth;
    enum typeShoot // cadence shoot
    {
        Full,
    }
    typeShoot cadenceType; // select cadence
    //---------------------------------------------------------------------------------------------
    bool activeVigilant = false;
    shootMAG gettingTypeShoot; // Acessa script shootMAG
    lookAtTrench captGunbools; // capt Shoot bool from Gun_Jeep
    DetectTarget detection; // aponta para variável com distância do inimigo no script DetectTarget
    objectsGenerator.Behavior currentState;
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
        
        detection.enemiesTag = target.tag; // passa a tag do GameObject target para a string "enemiesTag" do script DetectTarget
        currentState = objectsGenerator.Behavior.Idle; // vehicle start in Vigilant
    }
    void Update()
    {
        trenchBehavior(); 

        switch(currentState)
        {
            case objectsGenerator.Behavior.Idle:
                // .activeVigilant = false;
                opTypeShoot(false);
                Debug.Log("IDLE");
            break;
            case objectsGenerator.Behavior.Vigilant:
                // rotate turret - find enemy
                captGunbools.activeVigilant = true;
                opTypeShoot(false);
                Debug.Log("VIGILANT");
            break;
            case objectsGenerator.Behavior.Shoot:
                // shoot weapon
                opTypeShoot(true);
                Debug.Log("SHOOT");
            break;
            default:
                currentState = objectsGenerator.Behavior.Idle;
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
            Debug.Log("RECOGNITION");
        }
        else
        // Effective Action
        if (detection.currentDistance <= effectiveDistance)
        {
            captGunbools.activeLookAt = true;
            currentState = effectiveAction;
            Debug.Log("EFFECTIVE");
        }
        else
        {   // back to Vigilant
            currentState = objectsGenerator.Behavior.Vigilant;
            Debug.Log("DEFAULT");
            opTypeShoot(false);
        }
    }
}