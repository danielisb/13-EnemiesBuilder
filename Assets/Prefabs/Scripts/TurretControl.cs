using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretControl : MonoBehaviour
{
    // This script allows the user to rotate the turret,
    // and raise and lower the barrel elevation.
    public Transform barrel;
    public Transform vehicle;
    float min = -20;
    float max = 6.5f;
    float speedBarrel = 8.0f;
    float elevationx = 0;
    float elevationy;
    float elevationz;
    public bool activeExternalControl;
    public bool activeVigilant;
    void Start()
    {
        activeExternalControl = false;
        activeVigilant = false;
    }
    void Update()
    {
        // External Control
        if(activeExternalControl == true)
        {
            externalControlActive();
        }
        // Vigilant action
        if(activeVigilant == true)
        {
            vigilantBehavior();
        }
    }
    void externalControlActive()
    {
        // horizontal rotation control
        transform.Rotate(0.0f, Input.GetAxis("Horizontal") * speedBarrel, 0.0f);
        // barrel elevation control
        float v = -Input.GetAxis("Vertical");
        elevationx = Mathf.Clamp(elevationx+v,min,max);
        barrel.localRotation = Quaternion.Euler(elevationx, elevationy, elevationz);
    }
    void vigilantBehavior()
    {
        transform.Rotate(0, 50 * Time.deltaTime, 0);
    }
}
