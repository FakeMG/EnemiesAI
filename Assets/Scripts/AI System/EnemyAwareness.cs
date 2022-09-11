using System.Collections.Generic;
using UnityEngine;

public class EnemyAwareness : MonoBehaviour {

    [SerializeField] float awarenessDecayDelay = 1f;
    [SerializeField] float awarenessDecayRate = 0.1f;

    public Dictionary<GameObject, TrackedTarget> targets { get; private set; } = new Dictionary<GameObject, TrackedTarget>();

    private void Update() {
        List<GameObject> toCleanup = new List<GameObject>();

        foreach (var target in targets.Keys) {
            targets[target].DecayAwareness(awarenessDecayDelay, awarenessDecayRate * Time.deltaTime);

            if (targets[target].AwarenessOfThisTarget <= 0f) {
                toCleanup.Add(target);
            }
        }

        // cleanup targets that are no longer detected
        foreach (var target in toCleanup) {
            targets.Remove(target);
        }
    }

    public void UpdateAwarenessAbout(GameObject targetGameObject, float minimumAwareness, float addedAwareness) {
        if (!targets.ContainsKey(targetGameObject)) {
            targets[targetGameObject] = new TrackedTarget();
        }
        targets[targetGameObject].UpdateTarget(targetGameObject, minimumAwareness, addedAwareness);
    }
}

public class TrackedTarget {
    public Vector3 Posittion { get; private set; }
    private float lastSensedTime = -1f;
    public float AwarenessOfThisTarget { get; private set; }

    public void UpdateTarget(GameObject targetGameObject, float minimumAwareness, float addedAwareness) {
        Posittion = targetGameObject.transform.position;
        lastSensedTime = Time.time;
        AwarenessOfThisTarget = Mathf.Clamp(AwarenessOfThisTarget + addedAwareness, minimumAwareness, 2f);
    }

    public void DecayAwareness(float decayDelayTime, float amount) {
        if ((Time.time - lastSensedTime) < decayDelayTime) {
            return;
        }

        AwarenessOfThisTarget -= amount;
    }
}
