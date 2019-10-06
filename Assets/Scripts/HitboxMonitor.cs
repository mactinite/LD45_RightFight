﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxMonitor : MonoBehaviour {
    //TODO: Make this monitor the hitbox so we can easily check it
    Collider collidingWith = null;
    float closestCollider = Mathf.Infinity;

    BoxCollider self;

    public void Awake() {
        self = GetComponent<BoxCollider>();
    }
    public Collider checkStatus() {
        if (collidingWith) {
            return collidingWith;
        }
        return null;
    }

    public Vector3 GetSize() {
        return self.size;
    }

    public void SetHitboxSize(Vector3 size) {
        self.size = size;
    }
    public void SetHitboxCenter(Vector3 center) {
        self.center = center;
    }



    public Vector3 GetCenter() {
        return self.center;
    }
    void OnTriggerStay(Collider collider) {
        float distance = Vector3.Distance(transform.position, collider.transform.position);

        if (distance < closestCollider) {
            collidingWith = collider;
            closestCollider = distance;
        }
    }

    void OnTriggerExit(Collider collider) {
        if (collider == collidingWith) {
            collidingWith = null;
            closestCollider = Mathf.Infinity;
        }
    }
}