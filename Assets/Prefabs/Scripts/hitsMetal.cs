using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitsMetal : MonoBehaviour
{    
    int i = 0;
    public AudioSource audio1;
    public AudioSource audio2;
    public AudioSource audio3;
    AudioSource audioSelect;
    void Start()
    {
        audio1 = GetComponent<AudioSource>();
        audio2 = GetComponent<AudioSource>();
        audio3 = GetComponent<AudioSource>();
    }
    void hitAudio_1()
    {
        if(!audio1) return;
	    audio1.mute = false;
		audio1.Play();
    }
    void hitAudio_2()
    {
        if(!audio2) return;
	    audio2.mute = false;
		audio2.Play();
    }
    void hitAudio_3()
    {
        if(!audio3) return;
        audio3.mute = false;
        audio3.Play();
    }
    void OnCollisionEnter(Collision col)
    {                
        if(col.gameObject.CompareTag("bulletM4a1"))
        {
            i = (i + 1) % 3;           
            if(i == 1)
            {                
                hitAudio_1();
                print("AUDIO 1" + i);
            }
            if(i == 2)
            {                
                hitAudio_2();
                print("AUDIO 2" + i);
            }
            if(i == 3)
            {
                hitAudio_3();
                print("AUDIO 3" + i);
            }                        
        }   
    }
}