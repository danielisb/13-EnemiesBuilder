using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    public GameObject characther; // gameobject do personagem específico
    Animator anim;
    public enum Animations
    {
        UpIdle,
        UpWalk,
        UpRun,
        UpFire,
        UpDieFront,
        UpDieBehind,
        CrIdle,
        CrWalking,
        CrFire,
        CrDeath,
        PrIdle,
        PrWalking,
        PrFire,
        PrDeath,
        Vigilant,
    }
    public Animations state;

    
    void Start()
    {
        anim = GetComponent<Animator>(); // Active animator
    }
    void Update()
    {
        var animator = characther.GetComponent<Animator>();

        //Debug.Log("Update" + state);       

        switch(state)
        {
            case Animations.UpIdle:
                characther.GetComponent<Animator>().SetInteger("State", (int) Animations.UpIdle); // Select animation from animator
                break;
            case Animations.UpWalk:
                characther.GetComponent<Animator>().SetInteger("State", (int) Animations.UpWalk);
                break;
            case Animations.UpRun:
                characther.GetComponent<Animator>().SetInteger("State", (int) Animations.UpRun);
                break;
            case Animations.UpFire:
                characther.GetComponent<Animator>().SetInteger("State", (int) Animations.UpFire);
                break;
            case Animations.UpDieFront:
                characther.GetComponent<Animator>().SetInteger("State", (int) Animations.UpDieFront);
                break;
            case Animations.UpDieBehind:
                characther.GetComponent<Animator>().SetInteger("State", (int) Animations.UpDieBehind);
                break;
            case Animations.CrIdle:
                characther.GetComponent<Animator>().SetInteger("State", (int) Animations.CrIdle);
                break;              
            case Animations.CrWalking:
                characther.GetComponent<Animator>().SetInteger("State", (int) Animations.CrWalking);
                break;
            case Animations.CrFire:
                characther.GetComponent<Animator>().SetInteger("State", (int) Animations.CrFire);
                break;
            case Animations.CrDeath:
                characther.GetComponent<Animator>().SetInteger("State", (int) Animations.CrDeath);
                break;
            case Animations.PrIdle:
                characther.GetComponent<Animator>().SetInteger("State", (int) Animations.PrIdle);
                break;              
            case Animations.PrWalking:
                characther.GetComponent<Animator>().SetInteger("State", (int) Animations.PrWalking);
                break;
            case Animations.PrFire:
                characther.GetComponent<Animator>().SetInteger("State", (int) Animations.PrFire);
                break;
            case Animations.PrDeath:
                characther.GetComponent<Animator>().SetInteger("State", (int) Animations.PrDeath);
                break;
            case Animations.Vigilant:
                characther.GetComponent<Animator>().SetInteger("State", (int) Animations.Vigilant);
                break;
        }
    }
}
