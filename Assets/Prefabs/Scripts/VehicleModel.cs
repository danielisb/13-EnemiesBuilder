using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Models;

//------------------------------------------------------
using System;
namespace Models
{
	public interface IGameObject
	{
		object GetGameObject();
	}
}
public class GameObjectWrapper : IGameObject
{
	GameObject _gameObject;
	public GameObjectWrapper(GameObject gameObject)
	{
		_gameObject = gameObject;
	}
	public object GetGameObject()
	{
		return _gameObject;
	}
}
//------------------------------------------------------
public class VehicleModel : MonoBehaviour
{
	public GameObject carController;
	public GameObject headlights;
	public GameObject trajectory;
	public GameObject startPosition;
	public Transform gyroReference;

	[Range(-20, 90)]
	public float _targetVelocity;
	public bool lightsOn;
	public bool stop;
	public bool manualControl;
	public float velocity;
	CarController _carController;
	vehiclesManager _vehiclesManager;
	float _integral;
	bool _drive;

	//------------------------------
	void Awake()
	{
		//Log.Trace("VehicleModel.Awake()");
		if (carController != null)
			_carController = carController.GetComponent<CarController>();
		Brake(true);
	}
	//------------------------------
	void Start()
	{
		_vehiclesManager = carController.GetComponent<vehiclesManager>();
		
		//Log.Trace("VehicleModel.Start()");

		if (_carController != null)
			_carController.inputs.bypass = !manualControl;

		// if (trajectory != null)
		// 	SetTrajectory(new GameObjectWrapper(trajectory));

		if (startPosition != null)
			SetVehiclePosition(new GameObjectWrapper(startPosition));

		if (headlights != null)
			headlights.SetActive(false);
	}
	//------------------------------
	public void SetVehiclePosition(IGameObject reference)
	{
		//Log.Trace("VehicleModel.SetVehiclePosition(" + reference + ")");
		if (reference == null) return;
		var gobj = reference.GetGameObject() as GameObject;
		if (gobj == null) return;
		transform.position = gobj.transform.position;
		transform.rotation = gobj.transform.rotation;

		if (gyroReference != null)
		{
			gyroReference.position = gobj.transform.position;
			gyroReference.rotation = gobj.transform.rotation;
		}
	}
	//------------------------------
	public void SetTrajectory(IGameObject trajectory)
	{
		//Debug.Log("VehicleModel.SetTrajectory(" + trajectory + ")");
		//if (trajectory == null) return;
		var gobj = trajectory.GetGameObject() as GameObject;
		if (gobj == null || _carController == null) return;
		_carController.SetPath(gobj);
	}
	//------------------------------
	public void SetHeadlights(bool headlightsOn)
	{
		//Log.Ptrace("VehicleModel.SetHeadlights(" + headlightsOn + ")");
		lightsOn = headlightsOn;
	}
	//------------------------------
	public void SetDriveVelocity(float velocity)
	{
		//Log.Trace("VehicleModel.SetDriveVelocity(" + velocity + ")");
		_targetVelocity = velocity;
	}
	//------------------------------
	void Update()
	{
		if (stop && _drive)
		{
			stop = false;
			Stop();
		}
		ControlVehicleVelocity();

		if (headlights != null)
			headlights.SetActive(lightsOn);
	}
	//------------------------------
	public void Drive()
	{
		_drive = (_targetVelocity != 0);
	}
	//------------------------------
	public void Stop()
	{
		_drive = false;
	}
	//------------------------------
	float Round(float value, uint decimals = 0)
	{
		float mul = Mathf.Pow(10, decimals);
		return Mathf.Round(value * mul) / mul;
	}
	//------------------------------
	void Brake(bool brake)
	{
		if (_carController == null)
			return;

		_carController.brakeTorque = (brake) ? _carController.maxBrakeTorque : 0;
		_carController.brake = brake;

		const float volumeScaleStep = 0.05f;
		const float volumeScaleMin  = 0.3f;
		const float volumeScaleMax  = 0.5f;

		if (brake && _carController.sound.volumeScale > volumeScaleMin)
			_carController.sound.volumeScale -= volumeScaleStep;
		else if (!brake && _carController.sound.volumeScale < volumeScaleMax)
			_carController.sound.volumeScale += volumeScaleStep;

		//Log.Trash("VehicleModel.ControlVehicleVelocity(): brake = " + brake + ", volumeScale = " + _carController.sound.volumeScale);
	}
	//------------------------------
	void ControlVehicleVelocity()
	{
		//Log.Ptrace("VehicleModel.ControlVehicleVelocity()");
		
		if (!_carController)
			return;

		Brake(!_drive);

		// _carController.inputs.bypass = true;

		//Log.Disabled("VehicleModel.ControlVehicleVelocity(): drive = " + _drive + ", brake = " + _carController.brake + ", brakeTorque = " + _carController.brakeTorque);

		float target = (_drive) ? _targetVelocity : 0;

		const float kP = 0.7f;
		const float kD = 0.4f;
		const float kI = 0.1f;

		float velocity = _carController.velocity * 3.6f;  // from m/s to km/h
		velocity = Round(velocity, 1);

		float error = target - velocity;

		float p = error;
		float d = error / Time.deltaTime;
		float i = _integral + error;

		const float iMax = 500;
		
		i = Mathf.Clamp(i, -iMax, +iMax);

		float input = p * kP + i * kI + d * kD;

		_integral = i;

		input = Round(input, 1);
		input /= 100f;
		
		_carController.inputs.bypassY = input;

		velocity = Round(velocity, 2);
		
		input = 60f;

		//_targetVelocity = _vehiclesManager.velocity;
	}
	//------------------------------
	public float GetDriveVelocity()
	{
		return _targetVelocity;
	}
	//------------------------------
	public void GetPos(out float x, out float y, out float dir)
	{
		x = transform.position.x;
		y = transform.position.z;
		dir = transform.rotation.eulerAngles.y;
	}
}