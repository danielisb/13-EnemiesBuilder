using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lifeManager : MonoBehaviour
{
    public GameObject myBody;
    public GameObject soldiersManagerGO;
    GameObject bullet;
    AnimatorManager animator_Manager; // AnimatorManager animator;
    soldiersManager _soldiersManager;
    int Health;
    
    void Start()
    {
        animator_Manager = myBody.GetComponent<AnimatorManager>(); // Acive animations
        _soldiersManager = soldiersManagerGO.GetComponent<soldiersManager>();
        Health = 100;
        bullet = GameObject.Find("BulletM4a1");
    }
    // void Update()
    // {
        
    // }
    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.CompareTag("bulletM4a1"))
        {
            Health = Health-15;
            Debug.Log("HEALTH =" + Health);

            if(Health <= 0)
            {
                animator_Manager.state = AnimatorManager.Animations.UpDieFront;
                _soldiersManager.enemyDead = true;
                print("DEAAAAAD");
            }
        }   
    }
}
