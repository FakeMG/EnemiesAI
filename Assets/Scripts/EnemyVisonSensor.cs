using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(EnemyAwareness))]
public class EnemyVisonSensor : MonoBehaviour {

    public GameObject playerRef;
    public bool canSeePlayer;

    [Header("Vision Parameter")]
    [SerializeField] public float visionRadius = 15;
    [SerializeField][Range(0, 180)] public float angle;
    [SerializeField] private LayerMask targetMask;
    [SerializeField] private LayerMask obstructionMask;

    [Header("Awareness Parameter")]
    //[SerializeField] private float visionMinimumAwareness = 1f;
    [SerializeField] private float visionAwarenessBuildRate = 10f;

    private EnemyAwareness enemyAwareness;

    private void Start() {
        playerRef = GameObject.FindGameObjectWithTag("Player");
        enemyAwareness = GetComponent<EnemyAwareness>();

        StartCoroutine(VisionCheckRoutine());
    }

    private IEnumerator VisionCheckRoutine() {
        while (true) {
            yield return new WaitForSeconds(0.2f);
            VisionCheck();
        }
    }

    private void VisionCheck() {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, visionRadius, targetMask);

        if (rangeChecks.Length != 0) {

            foreach (Collider target in rangeChecks) {
                Vector3 directionToTarget = (target.transform.position - transform.position).normalized;

                if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2) {
                    float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);

                    if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask)) {
                        canSeePlayer = true;

                        float awareness = CalculateAwareness(directionToTarget);
                        enemyAwareness.UpdateAwarenessAbout(target.gameObject, awareness);
                    }
                }
            }
        }
    }

    private float CalculateAwareness(Vector3 directionToTarget) {
        float dot = Vector3.Dot(Vector3.forward, directionToTarget);
        return dot * visionAwarenessBuildRate * Time.deltaTime;
    }
}