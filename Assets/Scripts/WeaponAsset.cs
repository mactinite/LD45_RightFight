using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class WeaponAsset : ScriptableObject {
    public Sprite sprite;
    public int uses = 20;

    public enum WeaponUseStates {
        HIT,
        MISS,
        DEPLETED,
    }
    public WeaponUseStates Use(int comboCount) {
        --uses;
        if (uses < 0) {
            return WeaponUseStates.DEPLETED;
        }

        return Attack(comboCount);
    }

    public virtual WeaponUseStates Attack(int comboCount) {
        return WeaponUseStates.DEPLETED;
    }
}
