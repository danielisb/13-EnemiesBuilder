using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dinamicCoordinates : MonoBehaviour 
{
	public Color rayColor = Color.white; // cor dos gizmos
	ArrayList path;
	public struct Config
    {
       public Vector3 positions; 
    }
    Config[] configuration;
    // void initCoordinates()
    // {
    //     configuration = new Config[12];
    //     configuration[0].positions = new Vector3(159.2f, 1.5f, -79.7f);
    //     configuration[1].positions = new Vector3(153.5f, 1.5f, 11.4f);
    //     configuration[2].positions = new Vector3(128.7f, 1.5f, 68.2f);
    //     configuration[3].positions = new Vector3(71.2f, 1.5f, 21.5f);
	// 	configuration[4].positions = new Vector3(31.4f, 1.5f, -32.6f);
	// 	configuration[5].positions = new Vector3(-11.3f, 1.5f, -53.9f);
	// 	configuration[6].positions = new Vector3(-46.4f, 1.5f, -26.1f);
	// 	configuration[7].positions = new Vector3(-39.3f, 1.5f, 21.5f);
	// 	configuration[8].positions = new Vector3(-71.4f, 1.5f, 52.7f);
	// 	configuration[9].positions = new Vector3(-109.9f, 1.5f, -1.2f);
	// 	configuration[10].positions = new Vector3(-142f, 1.5f, -29.9f);
	// 	configuration[11].positions = new Vector3(-192.8f, 1.5f, -64.4f);
    // }
	void genObject() // Cria gameobjects a apartir das coordenadas da struct
	{
		int i;
		for(i=0; i<12; i++)
		{
			var obj = new GameObject();				
			obj.transform.position = configuration[i].positions; //var obj recebe a localização de positions 
			obj.transform.parent = transform;        //transforma objeto em filho  no objeto pai
		}
	}
	void Start()
	{
		//configuration = new Config[12];
		//initCoordinates();
		//genObject();
	}
	void OnDrawGizmos() // Desenha a linha e os gizmos entre os gameobjects
	{
		Gizmos.color = rayColor;
		// all child objects
		var path_objs = transform.GetComponentsInChildren<Transform>();
		path = new ArrayList();

		foreach (var path_obj in path_objs) 
		{
			// se igual eh o parent e nao queremos incluir como elemento
			if (path_obj != transform)
				path.Add(path_obj);
		}
		for (var i=0; i<path.Count; i++) 
		{
			Vector3 pos = ((Transform) path[i]).position;
			if (i > 0)
			{
				Vector3 prev = ((Transform) path[i-1]).position;
				Gizmos.DrawLine(prev, pos);
				Gizmos.DrawWireSphere(pos, (float)0.3);
			}
		}
	}	
}
