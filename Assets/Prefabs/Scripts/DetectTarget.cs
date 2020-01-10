using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectTarget : MonoBehaviour{

    [Header("General")]
    public GameObject soldierGO; // pega componentes do soldado
    public Transform soldier;
    public enum Collision
    {
        RayCast
    };
    public Collision _Collision = Collision.RayCast;
    public enum CheckTime 
    {
        _10perSecond, _20perSecond, fullTime
    };
    public CheckTime _CheckTime = CheckTime.fullTime;
    [Range(1, 1000)]
    public float lookDistance = 10;

    [Header("Raycast")]
    public string enemiesTag;
    [Range(2,500)]
    public float RaysPerLayer = 20;
    [Range(5, 180)]
    public float LookAngle = 120;
    [Range(1,10)]
    public int NumberOfLayers = 3;
    [Range(0.02f, 0.15f)]
    public float LayersDistance = 0.1f;
    //
    [Space(10)]
    public List<Transform> VisibleEnemies = new List<Transform>();
    List<Transform> collisionList = new List<Transform>();
    //LayerMask layerObstacles;
    float checkTimer = 0;
    public float currentDistance;
    public bool hitDetect;
    private void Start()
    {
        hitDetect = false;

        checkTimer = 0;
        if (!soldier) {
            soldier = transform;
        }
    }
    void Update()
    {
        if (_CheckTime == CheckTime._10perSecond) {
            checkTimer += Time.deltaTime;
            if (checkTimer >= 0.1f) {
                checkTimer = 0;
                lookEnemies();
            }
        }
        if (_CheckTime == CheckTime._20perSecond) {
            checkTimer += Time.deltaTime;
            if (checkTimer >= 0.05f) {
                checkTimer = 0;
                lookEnemies();
            }
        }
        if (_CheckTime == CheckTime.fullTime) {
            lookEnemies();
        }
    }
    private void lookEnemies() 
    {
        if (_Collision == Collision.RayCast)
        {
            float limitLayers = NumberOfLayers * 0.5f;
            for (int x = 0; x <= RaysPerLayer; x++)
            {
                for (float y = -limitLayers + 0.5f; y <= limitLayers; y++)
                {
                    float angleToRay = x * (LookAngle / RaysPerLayer) + ((180.0f - LookAngle) * 0.5f);
                    Vector3 directionMultipl = (-soldier.right) + (soldier.up * y * LayersDistance);
                    Vector3 rayDirection = Quaternion.AngleAxis(angleToRay, soldier.up) * directionMultipl;
                    //
                    RaycastHit hitRaycast;
                    if (Physics.Raycast(soldier.position, rayDirection, out hitRaycast, lookDistance))
                    {
                        //Debug.Log("ENEMY TAG ----------" + enemiesTag);
                        //if (!hitRaycast.transform.IsChildOf(transform.root) && !hitRaycast.collider.isTrigger)
                         //{
                            if (hitRaycast.collider.gameObject.CompareTag(enemiesTag))
                            {
                                //print("Enemy at "+hitRaycast.distance+" meters"); // distância de detecção
                                Debug.DrawLine(soldier.position, hitRaycast.point, Color.red); // linha vermelha de detecção do inimigo
                                currentDistance = hitRaycast.distance;

                                if (!collisionList.Contains(hitRaycast.transform)) {
                                    collisionList.Add(hitRaycast.transform);
                                }   
                                if (!VisibleEnemies.Contains(hitRaycast.transform)) {
                                    VisibleEnemies.Add(hitRaycast.transform);
                                }
                            }
                        //}
                    } 
                    else {
                         Debug.DrawRay(soldier.position, rayDirection * lookDistance, Color.green); // raios verdes RayCast
                     }
                }
            }
        }
        //remove da lista inimigos que não estão visiveis
        for (int x = 0; x < VisibleEnemies.Count; x++) {
            if (!collisionList.Contains(VisibleEnemies[x])) {
                VisibleEnemies.Remove(VisibleEnemies[x]);
            }
        }
        collisionList.Clear();
    }
}
