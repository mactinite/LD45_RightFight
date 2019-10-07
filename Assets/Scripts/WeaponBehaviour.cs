using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBehaviour : MonoBehaviour {

    
    public int uses = 20;
    public bool infiniteUses;

    public enum WeaponUseStates {
        HIT,
        MISS,
        DEPLETED,
    }
    public WeaponUseStates Use(int comboCount, WeaponManager manager) {
        WeaponUseStates result = Attack(comboCount, manager);
        if (!infiniteUses && result == WeaponUseStates.HIT) {
            --uses;
            if (uses < 0) {
                return WeaponUseStates.DEPLETED;
            }
        }

        return result;
    }

    public virtual WeaponUseStates Attack(int comboCount, WeaponManager manager) {
        return WeaponUseStates.DEPLETED;
    }

    public virtual void Equip(WeaponManager manager) { }
}
