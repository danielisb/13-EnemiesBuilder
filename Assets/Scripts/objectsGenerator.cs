using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectsGenerator : MonoBehaviour
{
    [Header("Settings")]
    public bool move; // faz o veículo (Player) andar

    [Header("Objects")]
    public GameObject player; // armazena prefab dos players
    public GameObject enemyMachineGun; // armazena prefab das trincheiras
    public GameObject enemyVehicle; // armazena prefab dos veículos
    public GameObject enemiesSoldiers; // armazena prefab dos soldados
    public GameObject objTrajectory; // armazena trajectory criado na execução
    public GameObject objEnemyTrajectory; // armazena enemyTrajectory criado na execução
    //-------------------------------
    GameObject genObject; // gameObject criado dinamicamente para instanciar prefabs nos locais predefinidos
    GameObject prefabGenerator; // armazena e instancia prefabs dinamicamente
    public struct Coordinates
    {
        public float x;
        public float y;
        public float z;
    };
    Coordinates[] vehicleTrajectory;
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
    int i = 0; // indice
    int t = 0; // indice trajectory
    int trajectoryArray = 0; // armazena tamanho do Array vehicleTrajectory
    int trajectoryEnemyArray = 0; // armazena tamanho do Array enemy Trajectory
    int enemiesArraySize = 0; // armazena tamanho do Array enemies
    void Start()
    {
        vehicleTrajectory = new Coordinates[12];
        enemies = new Enemy[4];
        trajectoryArray = vehicleTrajectory.Length; // indica tamanho do Array trajectory
        enemiesArraySize = enemies.Length; // indica os indices do Array enemies

        inputData();
        spawnTrajectory();
        processData();
    }
    void Update()
    {
        
    }
    void inputData() // Recebe dados de entrada
    {
        vehicleTrajectory[0].x = 159.2f;
        vehicleTrajectory[0].y = 1.5f;
        vehicleTrajectory[0].z = -79.7f;
        vehicleTrajectory[1].x = 153.5f;
        vehicleTrajectory[1].y = 1.5f;
        vehicleTrajectory[1].z = 11.4f;
        vehicleTrajectory[2].x = 128.7f;
        vehicleTrajectory[2].y = 1.5f;
        vehicleTrajectory[2].z = 68.2f;
        vehicleTrajectory[3].x = 71.2f;        
        vehicleTrajectory[3].y = 1.5f;
        vehicleTrajectory[3].z = 21.5f;
        vehicleTrajectory[4].x = 31.4f;
        vehicleTrajectory[4].y = 1.5f;
        vehicleTrajectory[4].z = -32.6f;
		vehicleTrajectory[5].x = -11.3f;
        vehicleTrajectory[5].y = 1.5f;
        vehicleTrajectory[5].z = -53.9f;
		vehicleTrajectory[6].x = -46.4f;
        vehicleTrajectory[6].y = 1.5f;
        vehicleTrajectory[6].z = -26.1f;
        vehicleTrajectory[7].x = -39.3f;
        vehicleTrajectory[7].y = 1.5f;
        vehicleTrajectory[7].z = 21.5f;
        vehicleTrajectory[8].x = -71.4f;
        vehicleTrajectory[8].y = 1.5f;
        vehicleTrajectory[8].z = 52.7f;
        vehicleTrajectory[9].x = -109.9f;
        vehicleTrajectory[9].y = 1.5f;
        vehicleTrajectory[9].z = -1.2f;
        vehicleTrajectory[10].x = -142f;        
        vehicleTrajectory[10].y = 1.5f;
        vehicleTrajectory[10].z = -29.9f;
        vehicleTrajectory[11].x = -192.8f;
        vehicleTrajectory[11].y = 1.5f;
        vehicleTrajectory[11].z = -64.4f;

        enemies[i].type = Type.Player;
        enemies[i].position.x = 161f;
        enemies[i].position.y = 0f;
        enemies[i].position.z = -87f;
        enemies[i].rotation.x = 0f;
        enemies[i].rotation.y = 358.7f;
        enemies[i].rotation.z = 0;
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
        enemies[i].identificationAction = Behavior.Vigilant;
        enemies[i].effectiveAction = Behavior.Shoot;
        i++;
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
        for(i=0; i<enemiesArraySize; i++)
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
            Debug.Log("enemiesArraySize: " + enemiesArraySize);
        }
    }
    void spawnSoldiers(Enemy enemy)
    {
        genObject = new GameObject();
        genObject.transform.position = new Vector3(enemies[i].position.x, enemies[i].position.y, enemies[i].position.z);
        prefabGenerator = Instantiate(enemiesSoldiers, genObject.transform.position, Quaternion.identity);
        prefabGenerator.transform.eulerAngles = new Vector3(0f, enemies[i].rotation.y, 0f);
        prefabGenerator.name = "Soldiers";
        Destroy(genObject);
        var settings = enemiesSoldiers.GetComponent<troopManager>();
            settings.detectionRadius = enemies[i].identificationRange;
            settings.effectiveDistance = enemies[i].weaponRange;            
            //settings.identificationAction = enemies[i].identificationAction;
            //settings.effectiveAction = enemies[i].effectiveAction;
    }
    void spawnVehicle(Enemy enemy)
    {
        genObject = new GameObject();
        genObject.transform.position = new Vector3(enemies[i].position.x, enemies[i].position.y, enemies[i].position.z);
        prefabGenerator = Instantiate(enemyVehicle, genObject.transform.position, Quaternion.identity);
        prefabGenerator.transform.eulerAngles = new Vector3(0f, enemies[i].rotation.y, 0f);
        prefabGenerator.name = "Vehicle";
        Destroy(genObject);
        var settings = enemyVehicle.GetComponent<vehiclesManager>();
            settings.detectionRadius = enemies[i].identificationRange;
            settings.effectiveDistance = enemies[i].weaponRange;
            //settings.effectiveAction = enemies[i].identificationAction;
            //settings.effectiveAction = enemies[i].identificationAction;
        spawnEnemyTrajectory(enemies[i]); //spawn enemy trajectory inside vehicleManger
    }
    void spawnEnemyTrajectory(Enemy enemy)
    {
        objEnemyTrajectory = new GameObject();
        objEnemyTrajectory.name = "Enemy Trajectory";
        objEnemyTrajectory.AddComponent<dinamicCoordinates>();
        for(t=0; t<2; t++)
        {
            var childrensOBJ = new GameObject();
                childrensOBJ.name = "Point";		
			    childrensOBJ.transform.position = new Vector3(enemies[i].trajectory[t].x, enemies[i].trajectory[t].y, enemies[i].trajectory[t].z);
                childrensOBJ.transform.parent = objEnemyTrajectory.transform;
        }
    }
    void spawnTrajectory()
    {
        objTrajectory = new GameObject();
        objTrajectory.name = "Trajectory";
        objTrajectory.AddComponent<dinamicCoordinates>();
        int t;
        for(t=0; t<trajectoryArray; t++)
        {
            var childrensOBJ = new GameObject();
                childrensOBJ.name = "Point";			
			    childrensOBJ.transform.position = new Vector3(vehicleTrajectory[t].x, vehicleTrajectory[t].y, vehicleTrajectory[t].z);
			    childrensOBJ.transform.parent = objTrajectory.transform; // transforma objeto em filho dentro do objeto pai
        }
    }
    void spawnPlayer(Enemy enemy)
    {
        genObject = new GameObject();
        genObject.transform.position = new Vector3(enemies[i].position.x, enemies[i].position.y, enemies[i].position.z);
        prefabGenerator = Instantiate(player, genObject.transform.position, Quaternion.identity);
        prefabGenerator.transform.eulerAngles = new Vector3(0f, enemies[i].rotation.y, 0f);
        prefabGenerator.name = "Player";
        Destroy(genObject);
        var playerOBJ = player.GetComponent<controlVehicle>();
            playerOBJ.objsGenerator = this.gameObject;
    }
    void spawnMachineGun(Enemy enemy)
    {
        genObject = new GameObject();
        genObject.transform.position = new Vector3(enemies[i].position.x, enemies[i].position.y, enemies[i].position.z);
        prefabGenerator = Instantiate(enemyMachineGun, genObject.transform.position, Quaternion.identity);
        prefabGenerator.transform.eulerAngles = new Vector3(0f, enemies[i].rotation.y, 0f);
        prefabGenerator.name = "MachineGun";
        Destroy(genObject);
        var settings = enemyMachineGun.GetComponent<trenchManager>();
            settings.detectionRadius = enemies[i].identificationRange;
            settings.effectiveDistance = enemies[i].weaponRange;            
            //settings.identificationAction = enemies[i].identificationAction;
            //settings.effectiveAction = enemies[i].effectiveAction;
    }
}