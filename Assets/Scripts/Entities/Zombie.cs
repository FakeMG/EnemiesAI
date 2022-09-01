using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour {

    private StateMachine _stateMachine;
    private NavMeshAgent _navMeshAgent;

    private void Awake() {
        _navMeshAgent = GetComponent<NavMeshAgent>();

        //State component
        // navMeshAgent
        // animator
    }

    void Update() {

    }
}
