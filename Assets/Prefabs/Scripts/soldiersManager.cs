using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soldiersManager : MonoBehaviour
{
    //------------------------------------------- CONTROLS ----------------------------------------
    [Header("Objects")]
    public GameObject soldierGO; // GameObject do soldado
    public GameObject enemyTarget; // GameObject para alvo do inimigo
    public Transform scape; // GameObject com coordenadas para fuga
    public GameObject captTarget;

    [Header("Settings")]
    [Range(0, 700)]
    public float detectionRadius; // Raio de detecção/reconhecimento
    [Range(0, 700)]
    public float effectiveDistanceRay; // Raio de distância Efetiva
    public enum Animations
    {
        UpIdle,
        UpRun,
        CrIdle,
        CrFire,
        CrDeath,
        Vigilant,
    }
    public Animations recognitionAction; // seletor de animação específica 
    public Animations effectiveAction; // seletor de animação específica
    float startTime; // Time when the movement started
    float journeyLength; // Total distance between the markers  
    public bool externalControl; // desativa controles e habilita controle externo (STAND BY)
    //---------------------------------------------------------------------------------------------
    DetectTarget detection; // aponta para variável com distância do inimigo no script DetectTarget
    DetectTarget detection2;
    DetectTarget detection3;
    DetectTarget detection4;
    AnimatorManager animator_Manager; // AnimatorManager animator;
    bool lookAtTrigger; // variável temporária de gatilho que ativa a função lookAt
    bool scapeTrigger; // variável de gatilho que ativa a função Scape
    bool endScape; // Verifica se o soldado realizou o Scape()
    public bool shootTriggerManager; // Gatilho de disparo da GenBullet
    float distance; // distance from scape()
    float speed = 0.001f; // velocidade da fuga
    public bool enemyDead; // indica morte no lifeManager;

    // lifeManager _lifeManger;
    void Start()
    {
        // bool controls
        enemyDead = false;
        externalControl = false;
        lookAtTrigger = false;
        shootTriggerManager = false;
        scapeTrigger = false;
        endScape = false;

        // getting components
        animator_Manager = soldierGO.GetComponent<AnimatorManager>(); // Acive animations
        detection = soldierGO.GetComponent<DetectTarget>(); // Recebe componente RayCast

        //_lifeManger = soldierGO.GetComponent<lifeManager>();

        // settings to scape
        startTime = Time.time; // Keep a note of the time the movement started (scape function)
        journeyLength = Vector3.Distance(soldierGO.transform.position, scape.transform.position); // Calculate the journey length
        
        captTarget = GameObject.Find("Player");		
        enemyTarget = captTarget.gameObject;
        detection.enemiesTag = enemyTarget.tag; // passa a tag do GameObject enemyTarget para a string "enemiesTag" do script DetectTarget
    }
    void Update()
    {
        Behavior(); // inicia função de comportamento

        if(lookAtTrigger == true) // verifica se a função lookat pode ser ativada
        {
            LookAtTarget();
        }
        if(scapeTrigger == true && endScape == false) // verifica se a função Scape pode ser ativada
        {
            Scape();
        }
        if(scapeTrigger == true && distance >= 1 && distance <=2) // Mudança do modo Scape para Vigilante
        {
            endScape = true;
            scapeTrigger = false;
            speed = 0f;
            animator_Manager.state = AnimatorManager.Animations.Vigilant;
            //print("distance VECTOR3: --- "+ distance);
            Behavior();
        }
        if(enemyDead == true)
        {
            shootTriggerManager = false;
            animator_Manager.state = AnimatorManager.Animations.Vigilant;
        }
    }
    void Behavior() // Comportamento do soldado 
    {
        lookAtTrigger = false; // inicia gatilho do LookAt desativada
        shootTriggerManager = false; // inicia gatilho de disparo desativado
        scapeTrigger = false; // inicia gatilho de scape desativado

        //print("Enemy at "+ detection.currentDistance +" meters"); // imprime distância do alvo

        if(detection.currentDistance == 0) // Mantém o soldado em vigilância procurando alvo 
        {
            animator_Manager.state = AnimatorManager.Animations.Vigilant;
            //print("VIGILANT______1______");
        }
        if(detection.currentDistance <= detectionRadius && detection.currentDistance > effectiveDistanceRay
        && recognitionAction == Animations.UpRun && endScape == false) // Comportamento de reconhecimento e fuga
        {
            animator_Manager.state = GetCorrectAnimations(recognitionAction); // Fuga
            scapeTrigger = true;
            //Debug.Log("RUNNING_______2_______!!!");
        }else{
            if(detection.currentDistance <= detectionRadius && detection.currentDistance > effectiveDistanceRay
            && recognitionAction == Animations.UpRun && endScape == true) // Ação caso inimigo já tenha executado Scape
            {
                animator_Manager.state = AnimatorManager.Animations.CrIdle;
                lookAtTrigger = true;
                scapeTrigger = true;
                //Debug.Log("ENDSCAPE_______3_______!!!");
            }else{
                    if (detection.currentDistance <= detectionRadius && detection.currentDistance > effectiveDistanceRay) // Ação de reconhecimento
                    {
                    animator_Manager.state = GetCorrectAnimations(recognitionAction);
                    lookAtTrigger = true;
                    //Debug.Log("RECOGNITION_______4_______!!!");     
                    }
                }
            }
        if (detection.currentDistance <= detectionRadius && detection.currentDistance <= effectiveDistanceRay && detection.currentDistance > 0) // Ação de ataque
        {
            lookAtTrigger = true;
            animator_Manager.state = GetCorrectAnimations(effectiveAction); // chaveia animação de ataque
            //Debug.Log("FIRING_______5_______!!!");

            if(effectiveAction == Animations.CrFire) // condição para ativar disparo
                shootTriggerManager = true; // ativa disparo
        } else {
                return; // caso as condições não aceitem os teste, retorna para vigilant
                }    
    }
    AnimatorManager.Animations GetCorrectAnimations(Animations anim) // Função que chaveia animação do script AnimatorManager por parâmetro dentro da variável anim
    {
        switch(anim)
        { 
            case Animations.UpIdle:
            return AnimatorManager.Animations.UpIdle;
            break;
            case Animations.UpRun:
            return AnimatorManager.Animations.UpRun;
            break;
            case Animations.CrIdle:
            return AnimatorManager.Animations.CrIdle;
            break;              
            case Animations.CrFire:
            return AnimatorManager.Animations.CrFire;
            break;
            case Animations.CrDeath:
            return AnimatorManager.Animations.CrDeath;
            break;
            case Animations.Vigilant:
            return AnimatorManager.Animations.Vigilant;
            break;
            default: return AnimatorManager.Animations.UpIdle;
        } 
    }
     void LookAtTarget() // Função LookAt
    {
        soldierGO.transform.LookAt(enemyTarget.transform);
    }
    void Scape() // função chamada apenas quando o comportamento de reconhecimento for "RUN"
    {
        float distCovered = (Time.time - startTime) * speed; // Distance moved equals elapsed time times speed
        float fractionOfJourney = distCovered / journeyLength; // Fraction of journey completed equals current distance divided by total distance
        soldierGO.transform.position = Vector3.Lerp(soldierGO.transform.position, scape.transform.position, fractionOfJourney); // Set our position as a fraction of the distance between the markers      
        Vector3 newScape = new Vector3(scape.transform.position.x, transform.position.y, scape.transform.position.z); // LookAt
        soldierGO.transform.LookAt(newScape); // transform LookAt()
        distance = Vector3.Distance(scape.transform.position, soldierGO.transform.position); // Converte dados de Vetor3 para Float     
    }
}
