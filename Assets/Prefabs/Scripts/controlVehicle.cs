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
	void Awake ()
	{
		objsGenerator = GameObject.Find("ObjectsCreator");
		_objsGenerator = objsGenerator.GetComponent<objectsGenerator>();
		
		trajectory = _objsGenerator.objTrajectory;
		initialized = false;
	}
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
