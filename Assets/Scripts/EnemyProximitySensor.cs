using System.Collections;
using UnityEngine;

public class EnemyProximitySensor : MonoBehaviour {

    [Header("Proximity Parameter")]
    [SerializeField] private float radius = 3;
    [SerializeField] private LayerMask targetMask;
    [SerializeField] private LayerMask obstructionMask;

    [Header("Awareness Parameter")]
    //[SerializeField] private float proximityMinimumAwareness = 0f;
    [SerializeField] private float proximityAwarenessBuildRate = 20f;

    private EnemyAwareness enemyAwareness;

    void Start() {
        enemyAwareness = GetComponent<EnemyAwareness>();

        StartCoroutine(ProximityCheckRoutine());
    }

    private IEnumerator ProximityCheckRoutine() {

        while (true) {
            yield return new WaitForSeconds(0.2f);
            ProximityCheck();
        }
    }

    private void ProximityCheck() {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

        if (rangeChecks.Length != 0) {

            foreach (Collider target in rangeChecks) {
                Vector3 directionToTarget = (target.transform.position - transform.position).normalized;
                float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask)) {
                    float awareness = proximityAwarenessBuildRate * Time.deltaTime;
                    enemyAwareness.UpdateAwarenessAbout(target.gameObject, awareness);
                }
            }
        }
    }
}
