using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectsGenerator : MonoBehaviour
{
    [Header("Player Settings")]
    public bool move; // faz o veículo (Player) andar

    [Range(-20, 90)]
	public float playerVelocity;

    [Header("Objects")]
    public GameObject player; // armazena prefab dos players
    public GameObject enemyMachineGun; // armazena prefab das trincheiras
    public GameObject enemyVehicle; // armazena prefab dos veículos
    public GameObject enemiesSoldiers; // armazena prefab dos soldados
    public GameObject objTrajectory; // armazena trajectory criado na execução
    public GameObject objEnemyTrajectory; // armazena enemyTrajectory criado na execução
    //-----------------------------------
    GameObject genObject; // gameObject criado dinamicamente para instanciar prefabs nos locais predefinidos
    GameObject prefabGenerator; // armazena e instancia prefabs dinamicamente

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
        Shoot,
        Move,
    }
    public struct Enemy
    {
        public Type type;
        public Coordinates position;
        public Coordinates rotation;
        public float identificationRange;
        public float weaponRange;
        public Behavior identificationAction;
        public Behavior effectiveAction;
        public Coordinates[] trajectory;
    }
    Enemy[] enemies;    
    void Start()
    {
        enemies = new Enemy[4];
        inputData();
        processData();
    }
    void inputData() // Recebe dados de entrada
    {
        int i = 0;        
        enemies[i].type = Type.Player;
        enemies[i].position.x = 205f;
        enemies[i].position.y = 10f;
        enemies[i].position.z = 94f;
        enemies[i].rotation.x = 0f;
        enemies[i].rotation.y = 370f;
        enemies[i].rotation.z = 0f;
        enemies[i].trajectory = new Coordinates[12];
        enemies[i].trajectory[0].x = 173f;
        enemies[i].trajectory[0].y = 10f;
        enemies[i].trajectory[0].z = 118f;
        enemies[i].trajectory[1].x = 153.5f;
        enemies[i].trajectory[1].y = 10f;
        enemies[i].trajectory[1].z = 212f;
        enemies[i].trajectory[2].x = 85f;
        enemies[i].trajectory[2].y = 10f;
        enemies[i].trajectory[2].z = 395f;
        enemies[i].trajectory[3].x = 153f;        
        enemies[i].trajectory[3].y = 10f;
        enemies[i].trajectory[3].z = 514f;
        enemies[i].trajectory[4].x = 265f;
        enemies[i].trajectory[4].y = 10f;
        enemies[i].trajectory[4].z = 597f;
		enemies[i].trajectory[5].x = 339f;
        enemies[i].trajectory[5].y = 10f;
        enemies[i].trajectory[5].z = 554f;
		enemies[i].trajectory[6].x = 410f;
        enemies[i].trajectory[6].y = 10f;
        enemies[i].trajectory[6].z = 484f;
        enemies[i].trajectory[7].x = 379f;
        enemies[i].trajectory[7].y = 10f;
        enemies[i].trajectory[7].z = 380f;
        enemies[i].trajectory[8].x = 354f;
        enemies[i].trajectory[8].y = 10f;
        enemies[i].trajectory[8].z = 308f;
        enemies[i].trajectory[9].x = 365f;
        enemies[i].trajectory[9].y = 10f;
        enemies[i].trajectory[9].z = 254f;
        enemies[i].trajectory[10].x = 435f;        
        enemies[i].trajectory[10].y = 10f;
        enemies[i].trajectory[10].z = 247f;
        enemies[i].trajectory[11].x = 621f;
        enemies[i].trajectory[11].y = 10f;
        enemies[i].trajectory[11].z = 261f;
        i++;
        enemies[i].type = Type.MachineGun;
        enemies[i].position.x = 83.3f;
        enemies[i].position.y = 4.7f;
        enemies[i].position.z = 482f;
        enemies[i].rotation.x = 0f;
        enemies[i].rotation.y = 102f;
        enemies[i].rotation.z = 0;
        enemies[i].identificationRange = 200f;
        enemies[i].weaponRange = 100f;
        enemies[i].identificationAction = Behavior.Vigilant;
        enemies[i].effectiveAction = Behavior.Shoot;
        i++;
        enemies[i].type = Type.Vehicle;
        enemies[i].position.x = 576.3f;
        enemies[i].position.y = 5.2f;
        enemies[i].position.z = 612f;
        enemies[i].rotation.x = 0f;
        enemies[i].rotation.y = 265.4f;
        enemies[i].rotation.z = 0;
        enemies[i].identificationRange = 200f;
        enemies[i].weaponRange = 100f;
        enemies[i].identificationAction = Behavior.Move;
        enemies[i].effectiveAction = Behavior.Shoot;
        enemies[i].trajectory = new Coordinates[5];
        enemies[i].trajectory[0].x = 540f;
        enemies[i].trajectory[0].y = 10f;
        enemies[i].trajectory[0].z = 613f;
        enemies[i].trajectory[1].x = 435f;
        enemies[i].trajectory[1].y = 10f;
        enemies[i].trajectory[1].z = 585f;
        enemies[i].trajectory[2].x = 365f;
        enemies[i].trajectory[2].y = 10f;
        enemies[i].trajectory[2].z = 650f;
        enemies[i].trajectory[3].x = 256f;
        enemies[i].trajectory[3].y = 10f;
        enemies[i].trajectory[3].z = 670f;
        enemies[i].trajectory[4].x = 169f;
        enemies[i].trajectory[4].y = 10f;
        enemies[i].trajectory[4].z = 646f;
        i++;
        enemies[i].type = Type.Soldier;
        enemies[i].position.x = 441f;
        enemies[i].position.y = 5f;
        enemies[i].position.z = 184f;
        enemies[i].rotation.x = 0f;
        enemies[i].rotation.y = 341f;
        enemies[i].rotation.z = 0;
        enemies[i].identificationRange = 200f;
        enemies[i].weaponRange = 100f;
        enemies[i].identificationAction = Behavior.Shoot;
        enemies[i].effectiveAction = Behavior.Shoot;
        // i++;
        // enemies[i].type = Type.Vehicle;
        // enemies[i].position.x = -67f;
        // enemies[i].position.y = 0f;
        // enemies[i].position.z = -76f;
        // enemies[i].rotation.x = 0f;
        // enemies[i].rotation.y = 40.34f;
        // enemies[i].rotation.z = 0;
        // enemies[i].identificationRange =200f;
        // enemies[i].weaponRange = 100f;
        // enemies[i].identificationAction = Behavior.Vigilant;        
        // i++;
        // enemies[i].type = Type.Soldier;
        // enemies[i].position.x = -87f;
        // enemies[i].position.y = 0f;
        // enemies[i].position.z = 79;
        // enemies[i].rotation.x = 0f;
        // enemies[i].rotation.y = 125.45f;
        // enemies[i].rotation.z = 0;
        // enemies[i].identificationRange = 150;
        // enemies[i].weaponRange = 80f;
        // enemies[i].identificationAction = Behavior.Vigilant;        
        // i++;
        // enemies[i].type = Type.Soldier;
        // enemies[i].position.x = -137f;
        // enemies[i].position.y = 0f;
        // enemies[i].position.z = 37f;
        // enemies[i].rotation.x = 0f;
        // enemies[i].rotation.y = 29f;
        // enemies[i].rotation.z = 0;
        // enemies[i].identificationRange = 150;
        // enemies[i].weaponRange = 80f;
        // enemies[i].identificationAction = Behavior.Vigilant;        
    }
    void processData()
    {
        for(int i=0; i<enemies.Length; i++)
        {
            switch (enemies[i].type)
            {
                case Type.Player:
                    spawnPlayer(enemies[i]);
                    break;
                case Type.MachineGun:
                    spawnMachineGun(enemies[i]);
                    break;
                case Type.Vehicle:
                    spawnVehicle(enemies[i]);
                    break;
                case Type.Soldier:
                    spawnSoldiers(enemies[i]);
                    break;
            }
            Debug.Log("Enemies: " + (enemies.Length-1));
        }
    }

    void spawnPlayerTrajectory(Enemy enemy)
    {
        objTrajectory = new GameObject();
        objTrajectory.name = "Trajectory";
        objTrajectory.AddComponent<dinamicCoordinates>();
        for(int i=0; i<enemy.trajectory.Length; i++)
        {
            var childrensOBJ = new GameObject();
                childrensOBJ.name = "Point";			
			    childrensOBJ.transform.position = new Vector3(enemy.trajectory[i].x, enemy.trajectory[i].y, enemy.trajectory[i].z);
			    childrensOBJ.transform.parent = objTrajectory.transform; // transforma objeto em filho dentro do objeto pai
        }
    }    

    void spawnPlayer(Enemy enemy)
    {
        genObject = new GameObject();
        genObject.transform.position = new Vector3(enemy.position.x, enemy.position.y, enemy.position.z);
        prefabGenerator = Instantiate(player, genObject.transform.position, Quaternion.identity);
        prefabGenerator.transform.eulerAngles = new Vector3(0f, enemy.rotation.y, 0f);
        prefabGenerator.name = "Player";
        Destroy(genObject);
        var playerOBJ = player.GetComponent<controlVehicle>();
            playerOBJ.objsGenerator = this.gameObject;
        spawnPlayerTrajectory(enemy);        
    }

    void spawnSoldiers(Enemy enemy)
    {
        genObject = new GameObject();
        genObject.transform.position = new Vector3(enemy.position.x, enemy.position.y, enemy.position.z);
        prefabGenerator = Instantiate(enemiesSoldiers, genObject.transform.position, Quaternion.identity);
        prefabGenerator.transform.eulerAngles = new Vector3(0f, enemy.rotation.y, 0f);
        prefabGenerator.name = "Soldiers";
        Destroy(genObject);
        var settings = enemiesSoldiers.GetComponent<troopManager>();
            settings.detectionRadius = enemy.identificationRange;
            settings.effectiveDistance = enemy.weaponRange;
            settings.recognitionActionOBJ = enemy.identificationAction;
            settings.effectiveActionOBJ = enemy.effectiveAction;
    }

    void spawnVehicle(Enemy enemy)
    {
        genObject = new GameObject();
        genObject.transform.position = new Vector3(enemy.position.x, enemy.position.y, enemy.position.z);
        prefabGenerator = Instantiate(enemyVehicle, genObject.transform.position, Quaternion.identity);
        prefabGenerator.transform.eulerAngles = new Vector3(0f, enemy.rotation.y, 0f);
        prefabGenerator.name = "Vehicle";
        Destroy(genObject);        
        var settings = enemyVehicle.GetComponent<vehiclesManager>();
            settings.detectionRadius = enemy.identificationRange;
            settings.effectiveDistance = enemy.weaponRange;
            settings.recognitionAction = enemy.identificationAction;
            settings.effectiveAction = enemy.effectiveAction;
        spawnEnemyTrajectory(enemy); // spawn enemy trajectory inside vehicleManger            
    }

    void spawnMachineGun(Enemy enemy)
    {
        genObject = new GameObject();
        genObject.transform.position = new Vector3(enemy.position.x, enemy.position.y, enemy.position.z);
        prefabGenerator = Instantiate(enemyMachineGun, genObject.transform.position, Quaternion.identity);
        prefabGenerator.transform.eulerAngles = new Vector3(0f, enemy.rotation.y, 0f);
        prefabGenerator.name = "MachineGun";
        Destroy(genObject);
        var settings = enemyMachineGun.GetComponent<trenchManager>();
            settings.detectionRadius = enemy.identificationRange;
            settings.effectiveDistance = enemy.weaponRange;            
            settings.identificationAction = enemy.identificationAction;
            settings.effectiveAction = enemy.effectiveAction;
    }

    void spawnEnemyTrajectory(Enemy enemy)
    {
        objEnemyTrajectory = new GameObject();
        objEnemyTrajectory.name = "Enemy Trajectory";
        objEnemyTrajectory.AddComponent<dinamicCoordinates>();
        for(int i=0; i<enemy.trajectory.Length; i++)
        {
            var childrensOBJ = new GameObject();
                childrensOBJ.name = "Point";		
			    childrensOBJ.transform.position = new Vector3(enemy.trajectory[i].x, enemy.trajectory[i].y, enemy.trajectory[i].z);
                childrensOBJ.transform.parent = objEnemyTrajectory.transform;
        }
    }
}