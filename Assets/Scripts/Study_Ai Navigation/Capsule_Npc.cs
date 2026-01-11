using System;
using UnityEngine;
using UnityEngine.AI;


public class Capsule_Npc : MonoBehaviour
{
    public Collider MainCollider {get; private set;}
    [SerializeField] private Transform[] goal;
    [SerializeField] private float detectRadius = 5f;
    [SerializeField] private LayerMask playerLayer;
    
    private NavMeshAgent agent;
    private int currentIndex = 0;
    private Transform targetPlayer;
    public GameObject end;
    void Start()
    {
        MainCollider = GetComponent<Collider>();
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(goal[0].position);
    }
    
    void Update()
    {
        // 주변에 플레이어가 감지 된다면 플레이어한테 다가 가는 로직 
        DetectPlayer();

        if (targetPlayer != null)
        {
            agent.SetDestination(targetPlayer.position);
            return;
        }
        
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            MoveToNextGoal();
        }
        
    }
    void MoveToNextGoal()
    {
        currentIndex =  (currentIndex + 1) % goal.Length;
        agent.SetDestination(goal[currentIndex].position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(other.gameObject);
            end.SetActive(true);
            Time.timeScale = 0;
        }
    }

    void DetectPlayer()
    {
        // OverlapSphere 주변 콜라이더를 탐색 하는 함수 
        Collider[] hits = Physics.OverlapSphere
            (transform.position,detectRadius, playerLayer);

        if (hits.Length > 0)
        {
            targetPlayer = hits[0].transform;
        }
        else
        {
            targetPlayer = null;
        }
    }
}
