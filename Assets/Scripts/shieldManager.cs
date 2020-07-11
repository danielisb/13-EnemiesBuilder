using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shieldManager : MonoBehaviour
{
    public GameObject myGameObject;
    public GameObject _vehiclesManager;
    GameObject bullet;
    vehicle_Explode activeExplosion;
    vehiclesManager getTypeShield;
    int health;
    bool damage;
    public bool _normal_shield;
    public bool _medium_shield;
    public bool _hard_shield;
    void Start()
    {
        activeExplosion = myGameObject.GetComponent<vehicle_Explode>();
        getTypeShield = _vehiclesManager.GetComponent<vehiclesManager>();

        bullet = GameObject.Find("BulletM4a1");
        damage = false;

        // _normal_shield = false;
        // _medium_shield = false;
        // _hard_shield = false;

        _normal_shield = getTypeShield.normal_shield;
        _medium_shield = getTypeShield.medium_shield;
        _hard_shield = getTypeShield.hard_shield;

        // Debug.Log(" _normal_shield " + _normal_shield);
        // Debug.Log(" _medium_shield " + _medium_shield);
        // Debug.Log(" _hard_shield " + _hard_shield);

        if (_normal_shield == true)
        {
            //Debug.Log("NORMAL SHIELD");
            health = 300;
        }
        if (_medium_shield == true)
        {   
            //Debug.Log("MEDIUM SHIELD");
            health = 500;
        }            
        if (_hard_shield == true)
        {
            //ebug.Log("HARD SHIELD");
            health = 700;
        }
        //Debug.Log(" INIT HEALTH " + health);
    }
    void Update()
    {
        shield_Manager();

        // Debug.Log(" _normal_shield " + _normal_shield);
        // Debug.Log(" _medium_shield " + _medium_shield);
        // Debug.Log(" _hard_shield " + _hard_shield);
    }
    void shield_Manager()
    {
        if(damage == true)
        {
            health = health-15;
            //Debug.Log("HEALTH SHIELD = " + health);

            if(health <= 0){
                Debug.Log("DEAD " + health);
                activeExplosion.explodeBool = true;
                damage = false;
            }
            else{
                activeExplosion.explodeBool = false;
            }
        }
    }
    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.CompareTag("bulletM4a1"))
        {
            damage = true;
            //Debug.Log("COLLIDING");
        }
        else
        {
            damage = false;
        } 
    }
}
