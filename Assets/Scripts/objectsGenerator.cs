using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectsGenerator : MonoBehaviour
{
    public GameObject player;
    public GameObject machineGun;
    public GameObject vehicle;
    public GameObject soldiers;
    GameObject prefabGenerator;
    GameObject genObject;
    public struct Coordinates
    {
        public Vector3 positions;
        public float x;
        public float y;
        public float z;
    };
    Coordinates[] inputPositions;
    //------------------------------
    // public enum Type
    // {
    //     MachineGun,
    //     Vehicle,
    //     Soldier,
    // };
    // Type[] inputType;
    //------------------------------
    public enum Behavior
    {
        Idle,
        Fire,
        Vigilant,
        Move,
    }
    //------------------------------
    public struct Enemy
    {
        // Type type;
        public string type;
        Coordinates position;
        float identificationRange;
        float weaponRange;
        Behavior identificationAction;
        Behavior effectiveAction;
    }
    Enemy[] inputType;
    Enemy[] inputIdentificationRange;
    Enemy[] inputWeaponRange;
    Enemy[] inputIdentificationAction;
    Enemy[] inputEffectiveAction;
    //------------------------------
    void inputData()
    {   // positions
        inputPositions = new Coordinates[7];
        //inputPositions[0].positions = new Vector3(161f, 0, -87f);       // old version
        inputPositions[0].x = 161f;
        inputPositions[0].y = 0;
        inputPositions[0].z = -87f;
        inputPositions[1].x = 154.73f;
        inputPositions[1].y = 0;
        inputPositions[1].z = -87f;
        inputPositions[2].x = 50f;
        inputPositions[2].y = 0;
        inputPositions[2].z = 70f;
        inputPositions[3].x = 64f;
        inputPositions[3].y = 0;
        inputPositions[3].z = -76f;
        inputPositions[4].x = -67f;
        inputPositions[4].y = 0;
        inputPositions[4].z = -76f;
        inputPositions[5].x = -87f;
        inputPositions[5].y = 0;
        inputPositions[5].z = 79f;
        inputPositions[6].x = -137f;
        inputPositions[6].y = 0;
        inputPositions[6].z = 37f;
        // type
        inputType = new Enemy[7];
        inputType[0].type = "Player";
        inputType[1].type = "MachineGun";
        inputType[2].type = "MachineGun";
        inputType[3].type = "Vehicle";
        inputType[4].type = "Vehicle";
        inputType[5].type = "Soldier";
        inputType[6].type = "Soldier";
        // identificationRange
        // inputIdentificationRange = new Enemy[6];
        // inputIdentificationRange[0].identificationRange = 50;
        // inputIdentificationRange[1].identificationRange = 50;
        // inputIdentificationRange[2].identificationRange = 50;
        // inputIdentificationRange[3].identificationRange = 50;
        // inputIdentificationRange[4].identificationRange = 50;
        // inputIdentificationRange[5].identificationRange = 50;
        // weaponRange
        // inputWeaponRange = new Enemy[6];
        // inputWeaponRange[0].weaponRange = 25;
        // inputWeaponRange[1].weaponRange = 25;
        // inputWeaponRange[2].weaponRange = 25;
        // inputWeaponRange[3].weaponRange = 25;
        // inputWeaponRange[4].weaponRange = 25;
        // inputWeaponRange[5].weaponRange = 25;
        // identificationAction
        // inputIdentificationAction = new Enemy[6];
        // inputIdentificationAction[0].identificationAction = Idle;
        // inputIdentificationAction[1].identificationAction = Idle;
        // inputIdentificationAction[2].identificationAction = Move;
        // inputIdentificationAction[3].identificationAction = Move;
        // inputIdentificationAction[4].identificationAction = Idle;
        // inputIdentificationAction[5].identificationAction = Idle;
        // effectiveAction
        // inputEffectiveAction = new Enemy[6];
        // inputEffectiveAction[0].effectiveAction = Fire;
        // inputEffectiveAction[1].effectiveAction = Fire;
        // inputEffectiveAction[2].effectiveAction = Fire;
        // inputEffectiveAction[3].effectiveAction = Fire;
        // inputEffectiveAction[4].effectiveAction = Fire;
        // inputEffectiveAction[5].effectiveAction = Fire;
    }
    void genObjects() // Cria gameobjects a partir dos parâmetros de inputData
	{
		int i;
		for(i=0; i<7; i++)
		{
            genObject = new GameObject();
            genObject.transform.position = new Vector3(inputPositions[i].x, 10f, inputPositions[i].z);

            RaycastHit hit;
            Ray downRayCast = new Ray(genObject.transform.position, Vector3.down);

            if (Physics.Raycast (downRayCast, out hit, 10))
            {
                if(inputType[i].type == "Player")
                {
                    prefabGenerator = Instantiate(player, hit.point, Quaternion.identity);  
                }
                if(inputType[i].type == "MachineGun")
                {
                    prefabGenerator = Instantiate(machineGun, hit.point, Quaternion.identity);
                }
                if(inputType[i].type == "Vehicle")
                {
                    prefabGenerator = Instantiate(vehicle, hit.point, Quaternion.identity);    
                }
                if(inputType[i].type == "Soldier")
                {
                    prefabGenerator = Instantiate(soldiers, hit.point, Quaternion.identity); 
                }
                prefabGenerator.AddComponent<BoxCollider>();
                Vector3 objectsize = prefabGenerator.GetComponent<Collider>().bounds.size;
                prefabGenerator.transform.position = new Vector3(prefabGenerator.transform.position.x, (prefabGenerator.transform.position.y+objectsize.y/2)-0.01f, prefabGenerator.transform.position.z);
            }
		}
	}
    void Start()
    {
        inputPositions = new Coordinates[7];
        inputData();
        genObjects();
    }
    void Update()
    {
        
    }
}
