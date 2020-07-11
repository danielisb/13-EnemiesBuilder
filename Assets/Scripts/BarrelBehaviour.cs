using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelBehaviour : MonoBehaviour
{
    public GameObject target;
    public Vector3 eulerangles;

    // Start is called before the first frame update
    void Start()
    {
        eulerangles = new Vector3(.0f,.0f,.0f);
    }

    // Update is called once per frame
    void Update()
    {
    //     transform.LookAt(target.transform);
    //     eulerangles = transform.localEulerAngles;
    //     eulerangles.x = (eulerangles.x > 0.0f) && (eulerangles.x <= 270.0f ) ? 0.0f: eulerangles.x;
    //     eulerangles.x = (eulerangles.x > 270.0f) && (eulerangles.x <= 340.0f )? 340.0f: eulerangles.x;
    //     transform.localEulerAngles = eulerangles;
    // }
    transform.LookAt(target.transform);
        eulerangles = transform.localEulerAngles;
        eulerangles.y = (eulerangles.y > 0.0f) && (eulerangles.y <= 270.0f ) ? 0.0f: eulerangles.y;
        eulerangles.y = (eulerangles.y > 270.0f) && (eulerangles.y <= 340.0f )? 340.0f: eulerangles.y;
        transform.localEulerAngles = eulerangles;
    }
}
