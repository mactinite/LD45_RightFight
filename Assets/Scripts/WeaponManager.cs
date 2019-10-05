using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class WeaponManager : MonoBehaviour, IWEaponManager {
    public WeaponAsset unequippedWeapon;
    public WeaponAsset equippedWeapon;

    public BoxCollider meleeHitbox;

    public int comboCount = 0;
    public float comboBufferTime = 0.1f;
    private bool combo = false;
    private float comboTimer;
    void Update() {
        if (combo) {
            comboTimer += Time.deltaTime;
        }
    }
    public void Attack() {
        // keep track of combos
        // trigger weapon attack
        WeaponAsset.WeaponUseStates result;

        if (equippedWeapon) {
            result = equippedWeapon.Use(comboCount, this);
        } else {
            result = unequippedWeapon.Use(comboCount, this);
        }

        if (result == WeaponAsset.WeaponUseStates.DEPLETED) {
            WeaponDestroyed();
        }

        if (result == WeaponAsset.WeaponUseStates.HIT && comboTimer <= comboBufferTime) {
            comboTimer = 0;
            comboCount++;
        }

        if (result == WeaponAsset.WeaponUseStates.MISS) {
            comboTimer = 0;
            comboCount = 0;
        }
    }

    public void Equip() {
        // initialize newly picked up weapon
    }

    private void WeaponDestroyed() {

    }

    public bool CheckMeleeHitbox() {
        //TODO: write melee hitbox checking code
        return true;
    }

    public void SetMeleeRange(float range) {
        Vector3 size = meleeHitbox.size;
        Vector3 center = meleeHitbox.center;

        size.x = range;
        center.x = range * 0.5f + 0.5f;
    }

}
