using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Models;

public class controlVehicle : MonoBehaviour 
{
	public GameObject vehicle;
	public bool init;

	VehicleModel model;

	bool initialized;

	public GameObject trajectory;

	// Use this for initialization
	void Start () {
		initialized = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (init == true && !initialized) {	
			model = vehicle.GetComponent<VehicleModel>();
			initialized = true;

			if (trajectory != null)
			  	model.SetTrajectory(new GameObjectWrapper(trajectory));

			drive();
		}
	}

	void drive()
	{
		model.SetDriveVelocity(20f);
		model.Drive();
	}
}

// SetVehiclePosition(GameObjectWrapper)
 //VehicleController.SetTrajectory(GameObjectWrapper)
