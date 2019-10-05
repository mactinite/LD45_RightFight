using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class WeaponAsset : ScriptableObject {
    public Sprite sprite;
    public int uses = 20;
    public bool infiniteUses;

    public Transform handRig;

    public enum WeaponUseStates {
        HIT,
        MISS,
        DEPLETED,
    }
    public WeaponUseStates Use(int comboCount, WeaponManager manager) {
        if (!infiniteUses) {
            --uses;
            if (uses < 0) {
                return WeaponUseStates.DEPLETED;
            }
        }

        return Attack(comboCount, manager);
    }

    public virtual WeaponUseStates Attack(int comboCount, WeaponManager manager) {
        return WeaponUseStates.DEPLETED;
    }
}
