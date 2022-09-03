using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HearingSensorsManager : MonoBehaviour {
    public static HearingSensorsManager Instance { get; private set; }

    private List<EnemyHearingSensor> hearingSensors = new List<EnemyHearingSensor>();

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(this);
        } else {
            Instance = this;
        }
    }

    public void RegisterSensor(EnemyHearingSensor sensor) {
        hearingSensors.Add(sensor);
    }

    public void DeregisterSensor(EnemyHearingSensor sensor) {
        hearingSensors.Remove(sensor);
    }

    public void NotifyAllHearingSensors(GameObject targetGameObject, float originalIntensity) {
        foreach (EnemyHearingSensor sensor in hearingSensors) {
            sensor.HearingCheck(targetGameObject, originalIntensity);
        }
    }
}
