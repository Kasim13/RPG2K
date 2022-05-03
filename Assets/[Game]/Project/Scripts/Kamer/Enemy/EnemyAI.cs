using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public EnemyData data;
    [SerializeField] Transform target;
    NavMeshAgent agent;
    void Start()
    {
        target = FindObjectOfType<PlayerMovement>().transform;
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(target.position);
    }
}
