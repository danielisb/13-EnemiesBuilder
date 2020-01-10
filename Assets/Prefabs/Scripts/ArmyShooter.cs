using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmyShooter : MonoBehaviour
{
    public GameObject soldiersManager_GO;
    public Transform BulletM4a1;
    soldiersManager shootTrigger;
    void Shoot()
    {
        var bullet = Instantiate(BulletM4a1);
        bullet.parent = transform;
        bullet.transform.localPosition = new Vector3();
        bullet.rotation = new Quaternion();
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 50f;
        bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * 5f);     
    }    
    void Start()
    {
        shootTrigger = soldiersManager_GO.GetComponent<soldiersManager>();
        shootTrigger.shootTriggerManager = false;
    }
    void Update()
    {
        if(shootTrigger.shootTriggerManager == true)
        {
            Shoot();
        }else{
            shootTrigger.shootTriggerManager = false;
        }
    }
}
