using System;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour {

    private StateMachine _stateMachine;

    private void Awake() {
        NavMeshAgent _navMeshAgent = GetComponent<NavMeshAgent>();
        EnemyAwareness _enemyAwareness = GetComponent<EnemyAwareness>();


        _stateMachine = new StateMachine();

        Wandering wandering = new Wandering(this, _navMeshAgent);
        Chasing chasing = new Chasing(this, _navMeshAgent, _enemyAwareness);

        _stateMachine.SetState(wandering);
    }

    void Update() {
        _stateMachine.Tick();
    }
}
