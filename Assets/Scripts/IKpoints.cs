using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]

public class IKpoints : MonoBehaviour
{
    public Transform RightPoint;
    public Transform LeftPoint;
    private Animator animator;
    void OnAnimatorIK()
    {
            // Righ hand
            animator.SetIKPositionWeight(AvatarIKGoal.RightHand,1);
            animator.SetIKRotationWeight(AvatarIKGoal.RightHand,1);  
            animator.SetIKPosition(AvatarIKGoal.RightHand,RightPoint.position);
            animator.SetIKRotation(AvatarIKGoal.RightHand,RightPoint.rotation);
            // Left hand
            animator.SetIKPositionWeight(AvatarIKGoal.LeftHand,1);
            animator.SetIKRotationWeight(AvatarIKGoal.LeftHand,1);  
            animator.SetIKPosition(AvatarIKGoal.LeftHand,LeftPoint.position);
            animator.SetIKRotation(AvatarIKGoal.LeftHand,LeftPoint.rotation);
    }
    void Start()
    {
        animator = GetComponent<Animator>();
        OnAnimatorIK();
    }
    void Update()
    {
        
    }
}
