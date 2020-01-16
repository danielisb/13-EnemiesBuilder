using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vehiclesManager : MonoBehaviour
{
    //------------------------------------------- CONTROLS ----------------------------------------
    [Header("Objects")]
    public GameObject vehicleGO; // GameObject vehicle
    public GameObject LookAtGO; // GameObject LookAt
    public GameObject turretControlGO; // GameObject to control Gun
    public Transform enemyTarget; // GameObject enemy
    //public Transform scape; // GameObject with coordinates to scape
    public GameObject trajectory; // path to move
    //public GameObject trigger; // test
    public GameObject particleSystem; // particles of shoot
    public GameObject objsGenerator;

    [Header("Settings")]
    [Range(0, 3000)]
    public float detectionRadius; // Raio de detecção/reconhecimento
    [Range(0, 1800)]
    public float effectiveDistance; // Raio de distância Efetiva
    public enum vehicleActions // Comportamentos do veículo
    {
        Vigilant,
        Move,
        Shoot,
        Stop,
    }
    public vehicleActions recognitionAction; // select recognition action
    public new vehicleActions effectiveAction; // select effective action

    [Range (-30,100)]
    public float velocity; // VERIFICAR SE É NECESSÁRIO
    bool moveVehicle;
    public bool explode; // explodir veículo
    public bool activeVigilant = false;
    public enum typeShoot // cadence shoot
    {
        Single,
        Burst,
        Full,
    }
    public typeShoot cadenceType; // select cadence
    public bool shoot; // VERIFICAR SE É NECESSÁRIO
    public enum Shield // types of shield
    {
        Normal,
        Medium,
        Hard,
    }
    public Shield shieldType; // Armazena enum Shield
    public bool externalControl; // disable manager controls and enable external controls
    //---------------------------------------------------------------------------------------------
    VehicleModel model; // Acessa script VehicleModel
    shootMAG gettingTypeShoot; // Acessa script shootMAG
    LookAtGun captGunbools; // capt Shoot bool from Gun_Jeep
    DetectTarget detection; // aponta para variável com distância do inimigo no script DetectTarget
    TurretControl _TurretControl;
    vehicle_Explode gettingExplosion;
    vehicleActions currentState;
    objectsGenerator _objsGenerator;

    // to manage Shield in shieldManager
    public bool normal_shield;
    public bool medium_shield;
    public bool hard_shield;
    void chooseShield()
    {
        switch(shieldType)
        {
            case Shield.Normal:
                normal_shield = true;
                break;
            case Shield.Medium:
                medium_shield = true;
                break;
            case Shield.Hard:
                hard_shield = true;
                break;
            default:
                normal_shield = false;
                medium_shield = false;
                hard_shield = false;
            break;
        }
        Debug.Log(" manager normal_shield " + normal_shield);
        Debug.Log(" manager _medium_shield " + medium_shield);
        Debug.Log(" manager _hard_shield " + hard_shield);
    }
    int cadenceTypeShoot;
    void opTypeShoot(bool flag) // cadence Shoot
    {
        //Debug.Log("SHOOTING");
        switch(cadenceType)
        {
            case typeShoot.Single:
                cadenceTypeShoot = 1;
                break;
            case typeShoot.Burst:
                cadenceTypeShoot = 5;
                break;
            case typeShoot.Full:
                cadenceTypeShoot = 15;
                break;
        }
        gettingTypeShoot.animator.SetBool("isShooting", flag);
        gettingTypeShoot.animator.SetInteger("opType", cadenceTypeShoot); // insere valor inteiro na variável opType no componente Animator de shootMAG
    }
    void Start()
    {
        externalControl = false;
        moveVehicle = false;

        explode = false;

        model = vehicleGO.GetComponent<VehicleModel>();
        captGunbools = LookAtGO.GetComponent<LookAtGun>();
        detection = vehicleGO.GetComponent<DetectTarget>();
        _TurretControl = turretControlGO.GetComponent<TurretControl>();
        gettingTypeShoot = particleSystem.GetComponent<shootMAG>();
        gettingExplosion = vehicleGO.GetComponent<vehicle_Explode>();

        objsGenerator = GameObject.Find("ObjectsCreator");
        _objsGenerator = objsGenerator.GetComponent<objectsGenerator>();

        detection.enemiesTag = enemyTarget.tag; // passa a tag do GameObject enemyTarget para a string "enemiesTag" do script DetectTarget
        currentState = vehicleActions.Vigilant; // vehicle start in Vigilant

        enemyTrajectory();
        
        // Set trajectory to vehicle
        if (trajectory != null) 
            model.SetTrajectory(new GameObjectWrapper(trajectory));
    }
    void Update()
    {
        //chooseShield(); // test

        vehicleBehavior();        

        //Debug.Log("move vehicle " + moveVehicle);

        // Explode vehicle
        if(explode == true)
        {
            gettingExplosion.explodeBool = explode;
        }
        // //External Turret Controls
        if(externalControl == true)
        {
            _TurretControl.activeExternalControl = true;
            _TurretControl.activeVigilant = false;
            opTypeShoot(false);
        }else{
            _TurretControl.activeExternalControl = false;
        }

        //Debug.Log("currentState = " + currentState);
        switch(currentState)
        {
            case vehicleActions.Vigilant:
                // rotate turret - find enemy
                //_TurretControl.activeVigilant = true;
                Debug.Log("VIGILANT");
            break;
            case vehicleActions.Move:
                // move vehicle
                moveVehicle = true;
                Debug.Log("MOVE");
            break;
            case vehicleActions.Shoot:
                // shoot weapon
                opTypeShoot(true);
                Debug.Log("SHOOT");
            break;
            case vehicleActions.Stop:
                // stop vehicle
                moveVehicle = false;
                Debug.Log("STOP");
            break;
            default:
                currentState = vehicleActions.Vigilant;
            break;
        }
        // active drive to move vehicle
        if (moveVehicle)
            model.Drive();
        else
            model.Stop();
    }
    void vehicleBehavior()
    {
        // if raycast detect something
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
            currentState = recognitionAction;
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
            currentState = vehicleActions.Vigilant;
            Debug.Log("DEFAULT");
            opTypeShoot(false);
        }
    }
    void drive()
	{
		model.SetDriveVelocity(20f);
		model.Drive();
	}
    void enemyTrajectory()
    {
        if(_objsGenerator.objEnemyTrajectory != null)
        {
            //print("NOT NULL ----------");
            trajectory = _objsGenerator.objEnemyTrajectory;
        }
    }
}