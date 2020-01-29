using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateOtherDisplays : MonoBehaviour
{
    private bool selectCam;
    private Camera otherCamera;
    private Camera thisCamera;
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        Debug.Log("Displays connected: "+ Display.displays.Length);
        if(Display.displays.Length > 1){
            Display.displays[1].Activate();
        }
        if(Display.displays.Length > 2){
            Display.displays[2].Activate();
        }

        selectCam = false;
        otherCamera = GameObject.Find("CameraCommanderOnGunner").GetComponent<Camera>();
        thisCamera = this.GetComponent<Camera>();
    }

    void Update(){
        if(Input.GetKeyDown("down")){
            selectCam = !selectCam;
        }

        otherCamera.enabled = selectCam;

    }
}
