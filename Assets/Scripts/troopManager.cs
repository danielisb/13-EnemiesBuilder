﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class troopManager : MonoBehaviour
{
    //------------------------------------------- CONTROLS ----------------------------------------
    [Header("Objects")]
    public GameObject soldierGO1;
    public GameObject soldierGO2;
    public GameObject enemyTarget;
    
    public soldiersManager settings1;
    public soldiersManager settings2;
    
    [Header("Settings")]
    [Range(0, 700)]
    public float detectionRadius; // Raio de detecção/reconhecimento
    
    [Range(0, 700)]
    public float effectiveDistance; // Raio de distância Efetiva

    public soldiersManager.Animations recognitionAction; // seletor de animação específica 
    public soldiersManager.Animations effectiveAction; // seletor de animação específica

    public sceneBuilder.Behavior recognitionActionOBJ; // seletor de animação específica 
    public sceneBuilder.Behavior effectiveActionOBJ; // seletor de animação específica

    //---------------------------------------------------------------------------------------------

    void Awake()
    {
        settings1 = soldierGO1.GetComponent<soldiersManager>();
        settings2 = soldierGO2.GetComponent<soldiersManager>();

        settings1.enemyTarget = enemyTarget;
        settings2.enemyTarget = enemyTarget;

        settings1.detectionRadius = detectionRadius;
        settings2.detectionRadius = detectionRadius;

        settings1.effectiveDistanceRay = effectiveDistance;        
        settings2.effectiveDistanceRay = effectiveDistance;                        

        settings1.recognitionAction = recognitionAction;
        settings2.recognitionAction = recognitionAction;

        settings1.effectiveAction = effectiveAction;
        settings2.effectiveAction = effectiveAction;

        soldierGO1.GetComponent<soldiersManager>().enabled =true;
        soldierGO2.GetComponent<soldiersManager>().enabled =true;
        
        selectBehavior();
    }
    void selectBehavior()
    {
        switch (recognitionActionOBJ)
        {
            case sceneBuilder.Behavior.Idle:
                recognitionAction = soldiersManager.Animations.CrIdle;
                break;
            case sceneBuilder.Behavior.Vigilant:
                recognitionAction = soldiersManager.Animations.Vigilant;
                break;
            case sceneBuilder.Behavior.Shoot:
                recognitionAction = soldiersManager.Animations.CrFire;
                break;
            case sceneBuilder.Behavior.Move:
                recognitionAction = soldiersManager.Animations.UpRun;
                break;
        }
        
        switch (effectiveActionOBJ)
        {
            case sceneBuilder.Behavior.Idle:
                effectiveAction = soldiersManager.Animations.CrIdle;
                break;
            case sceneBuilder.Behavior.Vigilant:
                effectiveAction = soldiersManager.Animations.Vigilant;
                break;
            case sceneBuilder.Behavior.Shoot:
                effectiveAction = soldiersManager.Animations.CrFire;
                break;
            case sceneBuilder.Behavior.Move:
                effectiveAction = soldiersManager.Animations.UpRun;
                break;
        }
    }
}
