using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        InvokeRepeating("GoAfterPlayer", 1f, 2f);
    }

    private void GoAfterPlayer()
    {
        navMeshAgent.SetDestination(Player.Self.Transfrom.position);
    }
}
