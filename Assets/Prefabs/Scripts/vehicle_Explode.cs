using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vehicle_Explode : MonoBehaviour
{
    public GameObject vehicleGO; // GameObject vehicle
    public GameObject explosionGO; // particle of vehicle explosion
    public GameObject shootDetector;
    public GameObject wreckedGO;
    GameObject particles;
    GameObject goodVehicle;
    GameObject wreckedVehicle;
    shootVehicleDetector detectorBool;
    public bool explodeBool;
    void Start()
    {
        detectorBool = shootDetector.GetComponent<shootVehicleDetector>();
        explodeBool = false;
    }
    void Update()
    {
        // Explosion
        if(detectorBool.GODead == true)
        {
            explosionVehicle();
            Destroy(particles, 5);
            //Destroy(wreckedVehicle, 10);
        }
    }
    void explosionVehicle()
    {
        particles = Instantiate(explosionGO, vehicleGO.transform.position, vehicleGO.transform.rotation);
        wreckedVehicle = Instantiate(wreckedGO, vehicleGO.transform.position, vehicleGO.transform.rotation);
        goodVehicle = vehicleGO;
        Destroy(goodVehicle);
    }
}
