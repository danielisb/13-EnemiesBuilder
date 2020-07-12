using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Models;

public class controlVehicle : MonoBehaviour 
{
	public GameObject sceneBuilderGO;
	public GameObject trajectory;
	public GameObject vehicle;
	public bool init;

	sceneBuilder _sceneBuilder;
	VehicleModel model;

	bool initialized;

	void Start ()
	{
		sceneBuilderGO = GameObject.Find("Builder");
		_sceneBuilder = sceneBuilderGO.GetComponent<sceneBuilder>();		
		initialized = false;
	}

	void Update () 
	{
		init = _sceneBuilder.move;
		trajectory = _sceneBuilder.objTrajectory;

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
		model.SetDriveVelocity(_sceneBuilder.playerVelocity);
		model.Drive();
	}
}
