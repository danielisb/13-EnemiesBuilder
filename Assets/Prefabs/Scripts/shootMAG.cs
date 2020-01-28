using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootMAG : MonoBehaviour
{
    private ParticleSystem MAGfire;
    public Animator animator;
    public Transform BulletM4a1;
    public GameObject localBullet;
    AudioSource _shootingSound;
    void Start()
    {
        _shootingSound = GetComponent<AudioSource>();
        MAGfire = GetComponent<ParticleSystem>();
        animator = GetComponent<Animator>();
    }
    void Update()
        {
            if(this.gameObject.tag == "Player")
            {
                if(Input.GetKeyDown(KeyCode.L))
                {
                    shoot();
                }
            }            
        }
    void shoot()
    {
        print("is shooting");
        MAGfire.Play();
        var bullet = Instantiate(BulletM4a1, localBullet.transform.position, localBullet.transform.rotation);
            //bullet.parent = transform;
            //bullet = bullet.transform.localPosition;
            //bullet.transform.localPosition = new Vector3();
            //bullet.rotation = new Quaternion();
            bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 200f;
            bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * 300f);
        PlayShootingAudio();
    }
    void PlayShootingAudio()
	{
		if (!_shootingSound) return;
		_shootingSound.mute = false;
		_shootingSound.Play();
	}
}
