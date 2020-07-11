using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootDetector : MonoBehaviour
{
    public GameObject myBody;
    public GameObject trenchDead;
    public GameObject _trenchManager;
    public GameObject particleSystem; // particles of shoot
    trenchManager stopShoot;
    trenchExplode activeDeadBool;
    shootMAG MAGdead; // Acessa script shootMAG
    GameObject bullet;
    public bool GODead;
    int Health;
    void Start()
    {
        stopShoot = _trenchManager.GetComponent<trenchManager>();
        activeDeadBool = trenchDead.GetComponent<trenchExplode>();
        MAGdead = particleSystem.GetComponent<shootMAG>();

        Health = 100;
        bullet = GameObject.Find("BulletM4a1");
        GODead = false;
    }
    // void Update()
    // {
        
    // }
    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.CompareTag("bulletM4a1"))
        {
            Health = Health-60;
            Debug.Log("HEALTH =" + Health);

            if(Health <= 0)
            {
                GODead = true;
                activeDeadBool.deadBool = true;
                if(GODead == true)
                    MAGdead.animator.SetBool("isShooting", false);
            }
        }   
    }
}
