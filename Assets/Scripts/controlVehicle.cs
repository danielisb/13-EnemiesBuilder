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
	void Start ()
	{
		objsGenerator = GameObject.Find("Builder");
		_objsGenerator = objsGenerator.GetComponent<objectsGenerator>();		
		initialized = false;
	}
	void Update () 
	{
		init = _objsGenerator.move;
		trajectory = _objsGenerator.objTrajectory;

		if (init == true && !initialized)
		{	
			model = vehicle.GetComponent<VehicleModel>();
			initialized = true;
			if (trajectory != null)
			{
				model.SetTrajectory(new GameObjectWrapper(trajectory));
				drive();
			}
		}
	}
	void drive()
	{
		model.SetDriveVelocity(_objsGenerator.playerVelocity);
		model.Drive();
	}
}
