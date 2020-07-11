using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trenchExplode : MonoBehaviour
{
    public GameObject soldier_GO;
    public GameObject machineGun_GO; // GameObject vehicle
    public GameObject wreckedGO;
    GameObject particles;
    GameObject goodVehicle;
    GameObject wreckedVehicle;
    animatorSoldiersManager soldierGetAnimations;
    public bool deadBool;
    void Start()
    {
        soldierGetAnimations = soldier_GO.GetComponent<animatorSoldiersManager>();
        deadBool = false;
    }
    void Update()
    {
        // Explosion
        if(deadBool == true)
        {
            explosionVehicle();
            Destroy(particles, 5);
        }
    }
    void explosionVehicle()
    {
        soldierGetAnimations.state = animatorSoldiersManager.Animations.CrDeath;
        //particles = Instantiate(explosionGO, machineGun_GO.transform.position, machineGun_GO.transform.rotation);
        wreckedVehicle = Instantiate(wreckedGO, machineGun_GO.transform.position, machineGun_GO.transform.rotation);
        goodVehicle = machineGun_GO;
        Destroy(goodVehicle);
    }
}
