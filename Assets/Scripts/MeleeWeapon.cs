using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "New Melee Weapon")]
public class MeleeWeapon : WeaponAsset {
    public Vector3 hitboxSize = Vector3.zero;
    public float hitDuration = 0.1f;

    public override WeaponAsset.WeaponUseStates Attack(int comboCount) {
        return WeaponAsset.WeaponUseStates.DEPLETED;
    }
}