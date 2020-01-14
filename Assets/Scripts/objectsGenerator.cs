using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectsGenerator : MonoBehaviour
{
    [Header("Settings")]
    public bool move;

    [Header("Objects")]
    public GameObject player;
    public GameObject machineGun;
    public GameObject vehicle;
    public GameObject soldiers;
    
    GameObject prefabGenerator;
    GameObject genObject;
    public struct Coordinates
    {
        public float x;
        public float y;
        public float z;
    };
    public enum Type
    {
        Player,
        MachineGun,
        Vehicle,
        Soldier,
    }
    public enum Behavior
    {
        Idle,        
        Vigilant,

        Move,
    }
    public struct Enemy
    {
        public Type type;
        public Coordinates position;
        public float identificationRange;
        public float weaponRange;
        public Behavior identificationAction;
        public Behavior effectiveAction;
        public Coordinates[] trajectory;
    }
    Enemy[] enemies;
    int i = 0; // indice 
    int arraySize = 0; // armazena tamanho do Array
    void inputData() // Recebe dados de entrada
    {        
        enemies[i].type = Type.Player;
        enemies[i].position.x = 161f;
        enemies[i].position.y = 0f;
        enemies[i].position.z = -87f;
        enemies[i].identificationRange = 100f;
        enemies[i].weaponRange = 50f;
        enemies[i].identificationAction = Behavior.Vigilant;        
        i++;

        enemies[i].type = Type.MachineGun;
        enemies[i].position.x = 171.3f;
        enemies[i].position.y = 0f;
        enemies[i].position.z = 64.2f;
        enemies[i].identificationRange = 200f;
        enemies[i].weaponRange = 100f;
        enemies[i].identificationAction = Behavior.Vigilant;        
    }
    void processData() // processa os dados de entrada
    {
        for(i=0; i<arraySize; i++)
        {
            switch (enemies[i].type)
            {
                case Type.Player:
                    spawnPlayer(enemies[i]);
                    break;
                case Type.MachineGun:
                    spawnMachineGun(enemies[i]);
                    break;
            }
            Debug.Log("arraySize: " + arraySize);
        }
    }
    // Player
    void spawnPlayer(Enemy enemy)
    {
        genObject = new GameObject();
        genObject.transform.position = new Vector3(enemies[i].position.x, enemies[i].position.y, enemies[i].position.z);
        prefabGenerator = Instantiate(player, genObject.transform.position, Quaternion.identity);
        Destroy(genObject);
    }
    // ManhineGun
    void spawnMachineGun(Enemy enemy) // Instancia inimigo de acordo com os parâmetros de entrada
    {
        genObject = new GameObject();
        genObject.transform.position = new Vector3(enemies[i].position.x, enemies[i].position.y, enemies[i].position.z);
        prefabGenerator = Instantiate(machineGun, genObject.transform.position, Quaternion.identity);
        Destroy(genObject);
        var settings = machineGun.GetComponent<trenchManager>();
            settings.detectionRadius = enemies[i].identificationRange;
            settings.effectiveDistance = enemies[i].weaponRange;
            //settings.recognitionAction = enemies[i].identificationAction;
            //settings.effectiveAction = enemies[i].effectiveAction;
    }
    void Start()
    {
        enemies = new Enemy[2];
        arraySize = enemies.Length; // conta os indices do Array

        inputData();
        processData();
    }
    void Update()
    {
        
    }
}
        // foreach(Enemy e in enemies)
        // {
        //     switch (enemies[i].type)
        //     {
        //         case Type.Player:
        //             spawnPlayer(enemies[i]);
        //             break;
        //         case Type.MachineGun:
        //             spawnMachineGun(enemies[i]);
        //             break;
        //     }
        //     Debug.Log("arraySize: " + arraySize);
        //     Debug.Log("enemies: " + enemies);
        // }
        // i++;
        // enemies[i].type = Type.Vehicle;
        // enemies[i].position.x = 50f;
        // enemies[i].position.y = 0f;
        // enemies[i].position.z = 70f;
        // enemies[i].identificationRange = 200f;
        // enemies[i].weaponRange = 100f;
        // enemies[i].identificationAction = Behavior.Move;        
        // enemies[i].trajectory = new Coordinates[3];

        // enemies[i].trajectory[0].x = 5f;
        // enemies[i].trajectory[0].y = 5f;
        // enemies[i].trajectory[0].z = 5f;
        // i++;
        // enemies[i].type = Type.Vehicle;
        // enemies[i].position.x = -67f;
        // enemies[i].position.y = 0f;
        // enemies[i].position.z = -76f;
        // enemies[i].identificationRange =200f;
        // enemies[i].weaponRange = 100f;
        // enemies[i].identificationAction = Behavior.Vigilant;        
        // //enemies[i].trajectory = 

        // i++;
        // enemies[i].type = Type.Soldier;
        // enemies[i].position.x = -87f;
        // enemies[i].position.y = 0f;
        // enemies[i].position.z = 79;
        // enemies[i].identificationRange = 150;
        // enemies[i].weaponRange = 80f;
        // enemies[i].identificationAction = Behavior.Vigilant;        
        // i++;

        // enemies[i].type = Type.Soldier;
        // enemies[i].position.x = -137f;
        // enemies[i].position.y = 0f;
        // enemies[i].position.z = 37f;
        // enemies[i].identificationRange = 150;
        // enemies[i].weaponRange = 80f;
        // enemies[i].identificationAction = Behavior.Vigilant;        
