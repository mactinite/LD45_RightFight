using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class WeaponAsset : ScriptableObject {
    public Sprite sprite;

    public Transform handRig;

    public Transform pickupPrefab;

    public int baseDamage;

    public float attackRate;
    public enum WeaponType {
        Melee,
        Ranged
    }

    public WeaponType weaponType;
}
