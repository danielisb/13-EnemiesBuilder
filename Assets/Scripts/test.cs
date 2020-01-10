using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public Transform p1;
    public Transform p2;
    private float timeCount = 0.0f;
    void Start()
    {
        
    }
    void Update()
    {
        vigilantBehavior();
    }
     void vigilantBehavior()
    {
        transform.rotation = Quaternion.Euler(new Vector3(0f ,Random.Range(-20f, 20f), 0.01f)); 
    }
}
