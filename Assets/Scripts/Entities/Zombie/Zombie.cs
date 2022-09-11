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

        AddTransititons();
        _stateMachine.SetState(wandering);

        void AddTransititons() {
            bool Chasing() {
                foreach (var target in _enemyAwareness.targets.Keys) {
                    if (_enemyAwareness.targets[target].AwarenessOfThisTarget >= 2f)
                        return true;
                }
                return false;
            }

            bool Wandering() {
                foreach (var target in _enemyAwareness.targets.Keys) {
                    if (_enemyAwareness.targets[target].AwarenessOfThisTarget >= 2f)
                        return false;
                }
                return true;
            }

            _stateMachine.AddTransition(wandering, chasing, Chasing);
            _stateMachine.AddTransition(chasing, wandering, Wandering);
        }
    }

    void Update() {
        _stateMachine.Tick();
    }
}
