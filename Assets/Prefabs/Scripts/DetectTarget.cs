using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectTarget : MonoBehaviour{

    [Header("General")]
    public GameObject my_Body; // pega componentes do soldado
    public Transform objToRayCast; // object transform que utiliza o RayCast
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
    float checkTimer = 0;
    public float currentDistance;
    lookAtVehicle captGun; // aponta para script Gun_Jeep
    public bool hitDetect;
    private void Start()
    {
        captGun = my_Body.GetComponent<lookAtVehicle>();

        hitDetect = false;

        checkTimer = 0;
        if (!objToRayCast) {
            objToRayCast = transform;
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
                    Vector3 directionMultipl = (-objToRayCast.right) + (objToRayCast.up * y * LayersDistance);
                    Vector3 rayDirection = Quaternion.AngleAxis(angleToRay, objToRayCast.up) * directionMultipl;
                    //
                    RaycastHit hitRaycast;
                    if (Physics.Raycast(objToRayCast.position, rayDirection, out hitRaycast, lookDistance))
                    {
                        //Debug.Log("ENEMY TAG ----------" + enemiesTag);
                        //if (!hitRaycast.transform.IsChildOf(transform.root) && !hitRaycast.collider.isTrigger)
                         //{
                            if (hitRaycast.collider.gameObject.CompareTag(enemiesTag))
                            {
                                //print("Enemy at "+hitRaycast.distance+" meters"); // distância de detecção
                                Debug.DrawLine(objToRayCast.position, hitRaycast.point, Color.red); // linha vermelha de detecção do inimigo
                                currentDistance = hitRaycast.distance;
                                hitDetect = true;
                                //captGun.initShoot = true;

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
                         Debug.DrawRay(objToRayCast.position, rayDirection * lookDistance, Color.green); // raios verdes RayCast
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
