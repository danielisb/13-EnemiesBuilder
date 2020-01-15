using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Models;

public class controlVehicle : MonoBehaviour 
{
	public GameObject objsGenerator;
	public GameObject trajectory;
	public GameObject vehicle;
	objectsGenerator _objsGenerator;
	public bool init;
	VehicleModel model;

	bool initialized;	

	// Use this for initialization
	void Start ()
	{
		_objsGenerator = objsGenerator.GetComponent<objectsGenerator>();
		initialized = false;
		trajectory = _objsGenerator.objTrajectory;
	}
	
	// Update is called once per frame
	void Update () 
	{
		init = _objsGenerator.move;
		
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
