using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class WeaponManager : MonoBehaviour, IWEaponManager {
    public WeaponAsset unequippedWeapon;
    public WeaponAsset equippedWeapon;
    public HitboxMonitor meleeHitbox;

    public Vector3 handRigPosition = Vector3.zero;


    public Transform battlerRig;
    public Transform currentHandRig;

    public WeaponBehaviour currentWeapon;

    public int comboCount = 0;
    public float comboBufferTime = 0.1f;
    private bool combo = false;
    private float comboTimer;


    void Start() {
        Equip();
    }
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

        currentWeapon.Attack(1);

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
        if (equippedWeapon) {
            if (currentHandRig) {
                Destroy(currentHandRig.gameObject);
            }
            currentHandRig = Transform.Instantiate(equippedWeapon.handRig, handRigPosition, equippedWeapon.handRig.rotation, battlerRig);
            currentHandRig.localPosition = handRigPosition;
            currentHandRig.localRotation = Quaternion.identity;
            currentWeapon = currentHandRig.GetComponent<WeaponBehaviour>();
        } else {
            currentHandRig = Transform.Instantiate(unequippedWeapon.handRig, handRigPosition, unequippedWeapon.handRig.rotation, battlerRig);
            currentHandRig.localPosition = handRigPosition;
            currentHandRig.localRotation = Quaternion.identity;
            currentWeapon = currentHandRig.GetComponent<WeaponBehaviour>();
        }
    }

    private void WeaponDestroyed() {
        if (currentHandRig) {
            Destroy(currentHandRig.gameObject);
        }
        equippedWeapon = null;
        Equip();
    }

    public Collider CheckMeleeHitbox() {
        //TODO: write melee hitbox checking code
        return meleeHitbox.checkStatus();
    }

    public void SetMeleeRange(float range) {
        Vector3 size = meleeHitbox.GetSize();
        Vector3 center = meleeHitbox.GetCenter();

        size.x = range;
        center.x = range * 0.5f + 0.5f;

        meleeHitbox.SetHitboxSize(size);
        meleeHitbox.SetHitboxCenter(center);
    }

}
