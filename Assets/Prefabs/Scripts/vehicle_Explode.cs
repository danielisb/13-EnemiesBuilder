using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vehicle_Explode : MonoBehaviour
{
    public GameObject vehicleGO; // GameObject vehicle
    public GameObject explosionGO; // particle of vehicle explosion
    public GameObject wreckedGO;
    GameObject particles;
    GameObject goodVehicle;
    GameObject wreckedVehicle;
    public bool explodeBool;
    void Start()
    {
        explodeBool = false;
    }
    void Update()
    {
        // Explosion
        if(explodeBool == true)
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
