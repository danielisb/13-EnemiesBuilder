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
    };
    public enum Behavior
    {
        Idle,
        Fire,
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
    void spawnSoldier(Enemy enemy)
    {
        
    }
    void spawnmachineGun(Enemy enemy)
    {
        
    }
    void parseEnemy()
    {
        
    }
    void inputData()
    {
        int i = 0;
        enemies[i].type = Type.MachineGun;
        enemies[i].position.x = 161f;
        enemies[i].position.y = 0;
        enemies[i].position.z = -87f;
        enemies[i].identificationRange = 100f;
        enemies[i].weaponRange = 50f;
        enemies[i].identificationAction = Behavior.Vigilant;
        enemies[i].effectiveAction = Behavior.Fire;
        // i++;
        // enemies[i].type = Type.MachineGun;
        // enemies[i].position.x = 171.3f;
        // enemies[i].position.y = 0f;
        // enemies[i].position.z = 64.2f;
        // enemies[i].identificationRange = 100f;
        // enemies[i].weaponRange = 50f;
        // enemies[i].identificationAction = Behavior.Vigilant;
        // enemies[i].effectiveAction = Behavior.Fire;
        // i++;
        // enemies[i].type = Type.Vehicle;
        // enemies[i].position.x = 50f;
        // enemies[i].position.y = 0f;
        // enemies[i].position.z = 70f;
        // enemies[i].identificationRange = 200f;
        // enemies[i].weaponRange = 100f;
        // enemies[i].identificationAction = Behavior.Move;
        // enemies[i].effectiveAction = Behavior.Fire;
        // enemies[i].trajectory = new Coordinates[3];
        // enemies[i].trajectory[0].x = 5f;

        // i++;
        // enemies[i].type = Type.Vehicle;
        // enemies[i].position.x = -67f;
        // enemies[i].position.y = 0f;
        // enemies[i].position.z = -76f;
        // enemies[i].identificationRange =200f;
        // enemies[i].weaponRange = 100f;
        // enemies[i].identificationAction = Behavior.Vigilant;
        // enemies[i].effectiveAction = Behavior.Fire;
        // //enemies[i].trajectory = 
        // i++;
        // enemies[i].type = Type.Soldier;
        // enemies[i].position.x = -87f;
        // enemies[i].position.y = 0f;
        // enemies[i].position.z = 79;
        // enemies[i].identificationRange = 150;
        // enemies[i].weaponRange = 80f;
        // enemies[i].identificationAction = Behavior.Vigilant;
        // enemies[i].effectiveAction = Behavior.Fire;
        // i++;
        // enemies[i].type = Type.Soldier;
        // enemies[i].position.x = -137f;
        // enemies[i].position.y = 0f;
        // enemies[i].position.z = 37f;
        // enemies[i].identificationRange = 150;
        // enemies[i].weaponRange = 80f;
        // enemies[i].identificationAction = Behavior.Vigilant;
        // enemies[i].effectiveAction = Behavior.Fire;          
    }
    // void genObjects() // Cria gameobjects a partir dos parâmetros de inputData
	// {
	// 	int i;
	// 	for(i=0; i<7; i++)
	// 	{
    //         genObject = new GameObject();
    //         genObject.transform.position = new Vector3(inputPositions[i].x, 10f, inputPositions[i].z);

    //         spawnSoldier(Enemy[i])

    //         RaycastHit hit;
    //         Ray downRayCast = new Ray(genObject.transform.position, Vector3.down);

    //         if (Physics.Raycast (downRayCast, out hit, 10))
    //         {
    //             if(inputType[i].type == "Player")
    //             {
    //                 prefabGenerator = Instantiate(player, hit.point, Quaternion.identity);  
    //             }
    //             if(inputType[i].type == "MachineGun")
    //             {
    //                 prefabGenerator = Instantiate(machineGun, hit.point, Quaternion.identity);
    //             }
    //             if(inputType[i].type == "Vehicle")
    //             {
    //                 prefabGenerator = Instantiate(vehicle, hit.point, Quaternion.identity);    
    //             }
    //             if(inputType[i].type == "Soldier")
    //             {
    //                 prefabGenerator = Instantiate(soldiers, hit.point, Quaternion.identity); 
    //             }
    //             prefabGenerator.AddComponent<BoxCollider>();
    //             Vector3 objectsize = prefabGenerator.GetComponent<Collider>().bounds.size;
    //             prefabGenerator.transform.position = new Vector3(prefabGenerator.transform.position.x, (prefabGenerator.transform.position.y+objectsize.y/2)-0.01f, prefabGenerator.transform.position.z);
    //         }
	// 	}
	// }
    void Start()
    {
        inputData();
        //genObjects();
    }
    void Update()
    {
        
    }
}
