using UnityEngine;

[RequireComponent(typeof(EnemyAwareness))]
public class EnemyHearingSensor : MonoBehaviour {

    [SerializeField] private float hearingMinimumAwareness = 0f;
    [SerializeField] private float hearingAwarenessBuildRate = 5f;
    [SerializeField] private float hearRange = 5f;

    private EnemyAwareness enemyAwareness;

    private void Start() {
        HearingSensorsManager.Instance.RegisterSensor(this);
        enemyAwareness = GetComponent<EnemyAwareness>();
    }

    public void HearingCheck(GameObject targetGameObject, float originalIntensity) {
        if (Vector3.Distance(transform.position, targetGameObject.transform.position) > hearRange) {
            return;
        }

        float awareness = CalculateAwareness(targetGameObject.transform.position, originalIntensity);
        enemyAwareness.UpdateAwarenessAbout(targetGameObject, hearingMinimumAwareness, awareness);
    }

    private float CalculateAwareness(Vector3 targetPos, float originalIntensity) {
        float distanceToTarget = Vector3.Distance(transform.position, targetPos);
        float intensityRelativeToSensor = CalculateIntensity(originalIntensity, distanceToTarget);
        return intensityRelativeToSensor * hearingAwarenessBuildRate * Time.deltaTime;
    }

    private float CalculateIntensity(float originalIntensity, float distanceToTarget) {
        float intensityMultiplier = Mathf.Clamp(1 - (distanceToTarget / hearRange), 0.2f, 1);
        return originalIntensity * intensityMultiplier;
    }
}
