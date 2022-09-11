using UnityEngine;
using UnityEngine.AI;

public class Chasing : IState {
    private readonly Zombie _zombie;
    private NavMeshAgent _navMeshAgent;
    private EnemyAwareness _awarenessSystem;

    private float _chasingSpeed = 3f;

    public Chasing(Zombie zombie, NavMeshAgent navMeshAgent, EnemyAwareness awarenessSystem) {
        _zombie = zombie;
        _navMeshAgent = navMeshAgent;
        _awarenessSystem = awarenessSystem;
    }

    public void OnEnter() {
        _navMeshAgent.enabled = true;
        _navMeshAgent.speed = _chasingSpeed;
    }

    public void Tick() {
        ChaseTarget();
    }

    private void ChaseTarget() {
        foreach (var target in _awarenessSystem.targets.Keys) {
            if (_awarenessSystem.targets[target].AwarenessOfThisTarget >= 2f) {
                _navMeshAgent.SetDestination(_awarenessSystem.targets[target].Posittion);
            }
        }
    }

    public void OnExit() {
        _navMeshAgent.enabled = false;
    }
}
