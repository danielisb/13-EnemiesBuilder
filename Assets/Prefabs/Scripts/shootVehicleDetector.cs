using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootVehicleDetector : MonoBehaviour
{
    public GameObject vehicle_Explode;
    public GameObject particle_System; // particles of shoot        
    shootMAG MAGdead; // Acessa script shootMAG
    GameObject bullet;
    public bool GODead;
    int Health;
    void Start()
    {                        
        MAGdead = particle_System.GetComponent<shootMAG>();
        Health = 100;        
        GODead = false;
    }
    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.CompareTag("bulletM4a1"))
        {
            Health = Health-20;
            Debug.Log("Hit Vehicle=" + Health);
            if(Health <= 0)
            {
                GODead = true;                
                if(GODead == true)
                    MAGdead.animator.SetBool("isShooting", false);
            }
        }   
    }
}
