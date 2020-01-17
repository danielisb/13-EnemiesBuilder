using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class troopManager : MonoBehaviour
{
    //------------------------------------------- CONTROLS ----------------------------------------
    [Header("Objects")]
    public GameObject soldierGO1;
    public GameObject soldierGO2;
    public GameObject soldierGO3;
    public GameObject soldierGO4;
    public GameObject enemyTarget; // GameObject para alvo do inimigo
    public soldiersManager settings1;
    public soldiersManager settings2;
    public soldiersManager settings3;
    public soldiersManager settings4;
    
    [Header("Settings")]
    [Range(0, 700)]
    public float detectionRadius; // Raio de detecção/reconhecimento
    [Range(0, 700)]
    public float effectiveDistance; // Raio de distância Efetiva
    public soldiersManager.Animations recognitionAction; // seletor de animação específica 
    public soldiersManager.Animations effectiveAction; // seletor de animação específica
    //---------------------------------------------------------------------------------------------
    void Awake()
    {
        settings1 = soldierGO1.GetComponent<soldiersManager>();
        settings2 = soldierGO2.GetComponent<soldiersManager>();
        settings3 = soldierGO3.GetComponent<soldiersManager>();
        settings4 = soldierGO4.GetComponent<soldiersManager>();

        settings1.enemyTarget = enemyTarget;
        settings2.enemyTarget = enemyTarget;
        settings3.enemyTarget = enemyTarget;
        settings4.enemyTarget = enemyTarget;

        settings1.detectionRadius = detectionRadius;
        settings2.detectionRadius = detectionRadius;
        settings3.detectionRadius = detectionRadius;
        settings4.detectionRadius = detectionRadius;

        settings1.effectiveDistanceRay = effectiveDistance;        
        settings2.effectiveDistanceRay = effectiveDistance;                        
        settings3.effectiveDistanceRay = effectiveDistance;                                
        settings4.effectiveDistanceRay = effectiveDistance;

        settings1.recognitionAction = recognitionAction;
        settings2.recognitionAction = recognitionAction;
        settings3.recognitionAction = recognitionAction;
        settings4.recognitionAction = recognitionAction;

        settings1.effectiveAction = effectiveAction;
        settings2.effectiveAction = effectiveAction;
        settings3.effectiveAction = effectiveAction;
        settings4.effectiveAction = effectiveAction;

        soldierGO1.GetComponent<soldiersManager>().enabled =true;
        soldierGO2.GetComponent<soldiersManager>().enabled =true;
        soldierGO3.GetComponent<soldiersManager>().enabled =true;
        soldierGO4.GetComponent<soldiersManager>().enabled =true;
    }
    void Update()
    {

    }
}
