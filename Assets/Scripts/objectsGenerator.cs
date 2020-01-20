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
        enemies[i].position.x = 161f;
        enemies[i].position.y = 0f;
        enemies[i].position.z = -87f;
        enemies[i].rotation.x = 0f;
        enemies[i].rotation.y = 358.7f;
        enemies[i].rotation.z = 0;
        enemies[i].trajectory = new Coordinates[12];
        enemies[i].trajectory[0].x = 159.2f;
        enemies[i].trajectory[0].y = 1.5f;
        enemies[i].trajectory[0].z = -79.7f;
        enemies[i].trajectory[1].x = 153.5f;
        enemies[i].trajectory[1].y = 1.5f;
        enemies[i].trajectory[1].z = 11.4f;
        enemies[i].trajectory[2].x = 128.7f;
        enemies[i].trajectory[2].y = 1.5f;
        enemies[i].trajectory[2].z = 68.2f;
        enemies[i].trajectory[3].x = 71.2f;        
        enemies[i].trajectory[3].y = 1.5f;
        enemies[i].trajectory[3].z = 21.5f;
        enemies[i].trajectory[4].x = 31.4f;
        enemies[i].trajectory[4].y = 1.5f;
        enemies[i].trajectory[4].z = -32.6f;
		enemies[i].trajectory[5].x = -11.3f;
        enemies[i].trajectory[5].y = 1.5f;
        enemies[i].trajectory[5].z = -53.9f;
		enemies[i].trajectory[6].x = -46.4f;
        enemies[i].trajectory[6].y = 1.5f;
        enemies[i].trajectory[6].z = -26.1f;
        enemies[i].trajectory[7].x = -39.3f;
        enemies[i].trajectory[7].y = 1.5f;
        enemies[i].trajectory[7].z = 21.5f;
        enemies[i].trajectory[8].x = -71.4f;
        enemies[i].trajectory[8].y = 1.5f;
        enemies[i].trajectory[8].z = 52.7f;
        enemies[i].trajectory[9].x = -109.9f;
        enemies[i].trajectory[9].y = 1.5f;
        enemies[i].trajectory[9].z = -1.2f;
        enemies[i].trajectory[10].x = -142f;        
        enemies[i].trajectory[10].y = 1.5f;
        enemies[i].trajectory[10].z = -29.9f;
        enemies[i].trajectory[11].x = -192.8f;
        enemies[i].trajectory[11].y = 1.5f;
        enemies[i].trajectory[11].z = -64.4f;
        i++;
        enemies[i].type = Type.MachineGun;
        enemies[i].position.x = 154f;
        enemies[i].position.y = 0f;
        enemies[i].position.z = 73f;
        enemies[i].rotation.x = 0f;
        enemies[i].rotation.y = 162.18f;
        enemies[i].rotation.z = 0;
        enemies[i].identificationRange = 200f;
        enemies[i].weaponRange = 100f;
        enemies[i].identificationAction = Behavior.Vigilant;
        enemies[i].effectiveAction = Behavior.Shoot;
        i++;
        enemies[i].type = Type.Vehicle;
        enemies[i].position.x = 50f;
        enemies[i].position.y = 0f;
        enemies[i].position.z = 70f;
        enemies[i].rotation.x = 0f;
        enemies[i].rotation.y = 104.38f;
        enemies[i].rotation.z = 0;
        enemies[i].identificationRange = 200f;
        enemies[i].weaponRange = 100f;
        enemies[i].identificationAction = Behavior.Move;
        enemies[i].effectiveAction = Behavior.Shoot;
        enemies[i].trajectory = new Coordinates[2];
        enemies[i].trajectory[0].x = 29.7f;
        enemies[i].trajectory[0].y = 1.5f;
        enemies[i].trajectory[0].z = 82.4f;
        enemies[i].trajectory[1].x = 25f;
        enemies[i].trajectory[1].y = 1.5f;
        enemies[i].trajectory[1].z = 19.6f;
        i++;
        enemies[i].type = Type.Soldier;
        enemies[i].position.x = 64f;
        enemies[i].position.y = 0f;
        enemies[i].position.z = -76f;
        enemies[i].rotation.x = 0f;
        enemies[i].rotation.y = 358.52f;
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
            Debug.Log("enemiesArraySize: " + enemies.Length);
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
}