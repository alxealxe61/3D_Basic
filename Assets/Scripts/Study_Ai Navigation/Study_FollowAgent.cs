using UnityEngine;
using UnityEngine.AI;

public class Study_FollowAgent : MonoBehaviour
{
    [SerializeField] private NavMeshAgent targetAgent;

    private NavMeshAgent agent;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        if (targetAgent.isStopped == false)
        {
            agent.SetDestination(targetAgent.transform.position);
        }
    }
}