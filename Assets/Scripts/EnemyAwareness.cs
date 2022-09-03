using System.Collections.Generic;
using UnityEngine;

public class EnemyAwareness : MonoBehaviour {

    [SerializeField] float awarenessDecayDelay = 1f;
    [SerializeField] float awarenessDecayRate = 0.1f;

    private Dictionary<GameObject, TrackedTarget> targets = new Dictionary<GameObject, TrackedTarget>();

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

    public void UpdateAwarenessAbout(GameObject targetGameObject, float awareness) {
        if (!targets.ContainsKey(targetGameObject)) {
            targets[targetGameObject] = new TrackedTarget();
        }
        targets[targetGameObject].UpdateTarget(targetGameObject, awareness);
    }
}

class TrackedTarget {
    private Vector3 position;
    private float lastSensedTime = -1f;
    public float AwarenessOfThisTarget { get; private set; }

    public void UpdateTarget(GameObject targetGameObject, float awareness) {
        position = targetGameObject.transform.position;
        lastSensedTime = Time.time;
        AwarenessOfThisTarget = Mathf.Clamp(AwarenessOfThisTarget + awareness, 0f, 2f);
    }

    public void DecayAwareness(float decayDelayTime, float amount) {
        if ((Time.time - lastSensedTime) < decayDelayTime) {
            return;
        }

        AwarenessOfThisTarget -= amount;
    }
}
