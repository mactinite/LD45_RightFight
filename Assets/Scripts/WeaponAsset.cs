using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class WeaponAsset : ScriptableObject {
    public Sprite sprite;

    public Transform handRig;

    public enum WeaponType {
        Melee,
        Ranged
    }

    public WeaponType weaponType;
}
