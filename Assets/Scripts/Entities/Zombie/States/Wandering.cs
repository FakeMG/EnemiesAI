using UnityEngine;
using UnityEngine.AI;

public class Wandering : IState {
    private readonly Zombie _zombie;
    private NavMeshAgent _navMeshAgent;

    private float _wanderingSpeed = 1.5f;
    private float _wanderingRadius = 10f;
    private float counter = 0f;

    public Wandering(Zombie zombie, NavMeshAgent navMeshAgent) {
        _zombie = zombie;
        _navMeshAgent = navMeshAgent;
    }

    public void OnEnter() {
        _navMeshAgent.enabled = true;
        _navMeshAgent.speed = _wanderingSpeed;
    }

    public void Tick() {
        MoveToRandomPosition();
    }

    private void MoveToRandomPosition() {
        if (_navMeshAgent.remainingDistance < 1f) {
            counter += Time.deltaTime;
        }

        if (counter > Random.Range(4f, 10f)) {
            counter = 0f;
            Vector3 destination = GetRandomPoint();
            _navMeshAgent.SetDestination(destination);
        }
    }

    private Vector3 GetRandomPoint() {
        Vector3 pos = (Random.insideUnitSphere * _wanderingRadius) + _zombie.transform.position;
        NavMesh.SamplePosition(pos, out NavMeshHit hit, _navMeshAgent.height * 2, NavMesh.AllAreas);
        return hit.position;
    }

    public void OnExit() {
        _navMeshAgent.enabled = false;
    }
}
