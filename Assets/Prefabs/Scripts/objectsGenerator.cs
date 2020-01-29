using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectsGenerator : MonoBehaviour
{
    [Header("Player Settings")]
    public bool move;
    public bool stop;

    [Range(-20, 90)]
	public float playerVelocity;

    [Header("Camera Settings")]
    public bool dayCamera;
    public bool thermalCamera;    

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
    GameObject _selectCamera;
    GameObject playerController;
    GameObject optic;
    selectCamera selectCamera;
    VehicleModel _controller;
           
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

        dayCamera = true;
        thermalCamera = false;
        _selectCamera = GameObject.Find("Player");
        selectCamera = _selectCamera.GetComponent<selectCamera>();

        playerController = GameObject.Find("Player");        
        _controller = playerController.GetComponent<VehicleModel>();        
    }
    void Update()
    {
        playerControllers();
        cameraController();
    }
    void inputData() // Recebe dados de entrada
    {
        int i = 0;        
        enemies[i].type = Type.Player;
        enemies[i].position.x = 401.2f;
        enemies[i].position.y = 1.1f;
        enemies[i].position.z = -299f;
        enemies[i].rotation.x = -05f;
        enemies[i].rotation.y = 5.9f;
        enemies[i].rotation.z = -299;
        enemies[i].trajectory = new Coordinates[14];
        enemies[i].trajectory[0].x = 392f;
        enemies[i].trajectory[0].y = 2f;
        enemies[i].trajectory[0].z = -238f;
        enemies[i].trajectory[1].x = 356f;
        enemies[i].trajectory[1].y = 2f;
        enemies[i].trajectory[1].z = -98f;
        enemies[i].trajectory[2].x = 303f;
        enemies[i].trajectory[2].y = 2f;
        enemies[i].trajectory[2].z = 42f;
        enemies[i].trajectory[3].x = 258f;        
        enemies[i].trajectory[3].y = 2f;
        enemies[i].trajectory[3].z = 137f;
        enemies[i].trajectory[4].x = 169f;
        enemies[i].trajectory[4].y = 2f;
        enemies[i].trajectory[4].z = 195f;
		enemies[i].trajectory[5].x = 97f;
        enemies[i].trajectory[5].y = 2f;
        enemies[i].trajectory[5].z = 162f;
		enemies[i].trajectory[6].x = -1f;
        enemies[i].trajectory[6].y = 2f;
        enemies[i].trajectory[6].z = 114f;
        enemies[i].trajectory[7].x = -73f;
        enemies[i].trajectory[7].y = 2f;
        enemies[i].trajectory[7].z = 24f;
        enemies[i].trajectory[8].x = -153f;
        enemies[i].trajectory[8].y = 2f;
        enemies[i].trajectory[8].z = -56f;
        enemies[i].trajectory[9].x = -298f;
        enemies[i].trajectory[9].y = 2f;
        enemies[i].trajectory[9].z = -128f;
        enemies[i].trajectory[10].x = -395f;        
        enemies[i].trajectory[10].y = 2f;
        enemies[i].trajectory[10].z = -128f;
        enemies[i].trajectory[11].x = -417f;
        enemies[i].trajectory[11].y = 2f;
        enemies[i].trajectory[11].z = -24f;
        enemies[i].trajectory[12].x = -417f;
        enemies[i].trajectory[12].y = 2f;
        enemies[i].trajectory[12].z = 115f;
        enemies[i].trajectory[13].x = -409f;
        enemies[i].trajectory[13].y = 2f;
        enemies[i].trajectory[13].z = 288f;
        i++;
        enemies[i].type = Type.MachineGun;
        enemies[i].position.x = 356f;
        enemies[i].position.y = 0f;
        enemies[i].position.z = 57f;
        enemies[i].rotation.x = 0f;
        enemies[i].rotation.y = 162.18f;
        enemies[i].rotation.z = 0;
        enemies[i].identificationRange = 200f;
        enemies[i].weaponRange = 70f;
        enemies[i].identificationAction = Behavior.Vigilant;
        enemies[i].effectiveAction = Behavior.Shoot;
        i++;
        enemies[i].type = Type.Vehicle;
        enemies[i].position.x = 255f;
        enemies[i].position.y = 0f;
        enemies[i].position.z = 188f;
        enemies[i].rotation.x = 0f;
        enemies[i].rotation.y = 104.38f;
        enemies[i].rotation.z = 0;
        enemies[i].identificationRange = 100f;
        enemies[i].weaponRange = 80f;
        enemies[i].identificationAction = Behavior.Move;
        enemies[i].effectiveAction = Behavior.Shoot;
        enemies[i].trajectory = new Coordinates[2];
        enemies[i].trajectory[0].x = 265.7f;
        enemies[i].trajectory[0].y = 1.5f;
        enemies[i].trajectory[0].z = 184.4f;
        enemies[i].trajectory[1].x = 197.7f;
        enemies[i].trajectory[1].y = 1.5f;
        enemies[i].trajectory[1].z = 103.1f;
        i++;
        enemies[i].type = Type.Soldier;
        enemies[i].position.x = 63.2f;
        enemies[i].position.y = 0f;
        enemies[i].position.z = 209.2f;
        enemies[i].rotation.x = 0f;
        enemies[i].rotation.y = 501.5f;
        enemies[i].rotation.z = 0;
        enemies[i].identificationRange = 200f;
        enemies[i].weaponRange = 70f;
        enemies[i].identificationAction = Behavior.Shoot;
        enemies[i].effectiveAction = Behavior.Shoot;
        // i++;
        // enemies[i].type = Type.Vehicle;
        // enemies[i].position.4xx = -67f;
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
    void playerControllers()
    {
        _controller.trajectory = objTrajectory; // get gameobject of player trajectory
        _controller._targetVelocity = playerVelocity; // velocity in run time

        if (_controller.trajectory != null && move) // set player trajectory in VehicleModel
		{
			_controller.SetTrajectory(new GameObjectWrapper(_controller.trajectory));
			_controller.Drive();
		}
        if (move)        
            _controller._drive = true;  
               
        if (stop && move)
        {
            move = false;
            stop = false;
            _controller._drive = false;
        }
    }
    void cameraController()
    {
        Camera cam;

        if (Input.GetKeyDown(KeyCode.O))
        {            
            dayCamera = !dayCamera;
            thermalCamera = !thermalCamera;
        }        
        if (dayCamera)
        {            
            selectCamera._day.SetActive(true);                
            selectCamera._thermal.SetActive(false);            
            cam = selectCamera._day.GetComponent<Camera>();
            
            if (Input.GetKeyDown(KeyCode.I))
                cam.fieldOfView = 4;
            if (Input.GetKeyDown(KeyCode.K))
                cam.fieldOfView = cam.fieldOfView +10;
        }
        if (thermalCamera)
        {            
            selectCamera._thermal.SetActive(true);                
            selectCamera._day.SetActive(false);
            cam = selectCamera._thermal.GetComponent<Camera>();
            
            if (Input.GetKeyDown(KeyCode.I))
                cam.fieldOfView = 4;                
            if (Input.GetKeyDown(KeyCode.K))
                cam.fieldOfView = cam.fieldOfView +10;
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
        genObject.transform.position = new Vector3(enemy.position.x, 10f, enemy.position.z);

        RaycastHit hit;
            Ray newRay = new Ray(genObject.transform.position, Vector3.down);
            Debug.DrawRay (genObject.transform.position, Vector3.down*10, Color.black);
            Physics.Raycast (newRay, out hit, 10);
        
        prefabGenerator = Instantiate(player, hit.point, Quaternion.identity);
        prefabGenerator.transform.eulerAngles = new Vector3(0f, enemy.rotation.y, 0f);
        prefabGenerator.name = "Player";
        Destroy(genObject);
        spawnPlayerTrajectory(enemy);        
    }
    void spawnSoldiers(Enemy enemy)
    {        
        genObject = new GameObject();
        genObject.transform.position = new Vector3(enemy.position.x, 10f, enemy.position.z);

        RaycastHit hit;
            Ray newRay = new Ray(genObject.transform.position, Vector3.down);
            Debug.DrawRay (genObject.transform.position, Vector3.down*10, Color.black);
            Physics.Raycast (newRay, out hit, 10);

        prefabGenerator = Instantiate(enemiesSoldiers, hit.point, Quaternion.identity);
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
        genObject.transform.position = new Vector3(enemy.position.x, 10f, enemy.position.z);

        RaycastHit hit;
            Ray newRay = new Ray(genObject.transform.position, Vector3.down);
            Debug.DrawRay (genObject.transform.position, Vector3.down*10, Color.black);
            Physics.Raycast (newRay, out hit, 10);

        prefabGenerator = Instantiate(enemyVehicle, hit.point, Quaternion.identity);
        prefabGenerator.transform.eulerAngles = new Vector3(0f, enemy.rotation.y, 0f);
        prefabGenerator.name = "Vehicle";
        Destroy(genObject);
        var settings = enemyVehicle.GetComponent<vehiclesManager>();
            settings.detectionRadius = enemy.identificationRange;
            settings.effectiveDistance = enemy.weaponRange;
            settings.recognitionAction = enemy.identificationAction;
            settings.effectiveAction = enemy.effectiveAction;
        spawnEnemyTrajectory(enemy);
    }
    void spawnMachineGun(Enemy enemy)
    {
        genObject = new GameObject();        
        genObject.transform.position = new Vector3(enemy.position.x, 10f, enemy.position.z);

        RaycastHit hit;
            Ray newRay = new Ray(genObject.transform.position, Vector3.down);
            Debug.DrawRay (genObject.transform.position, Vector3.down*10, Color.black);
            Physics.Raycast (newRay, out hit, 10);

        prefabGenerator = Instantiate(enemyMachineGun, hit.point, Quaternion.identity);
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