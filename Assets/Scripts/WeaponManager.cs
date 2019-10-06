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

    private HUDController hudController;
    public int comboCount = 0;
    public float comboBufferTime = 0.1f;
    public bool combo = false;
    private float comboTimer;

    public bool updateUI = false;

    public WeaponPickup pickupWeapon;


    void Start() {
        Equip();
        hudController = HUDController._instance as HUDController;
    }
    void Update() {
        if (combo) {
            comboTimer += Time.deltaTime;
        }

        if (comboTimer > comboBufferTime) {
            combo = false;
            comboCount = 0;
            comboTimer = 0;
            if (updateUI) {
                hudController.ComboCount = comboCount;
            }
        }
        if (updateUI) {
            hudController.ComboTimer = comboTimer;
            hudController.ComboBufferTime = comboBufferTime;
        }
    }
    public void Attack() {
        // keep track of combos
        // trigger weapon attack
        WeaponBehaviour.WeaponUseStates result;

        result = currentWeapon.Use(comboCount, this);

        if (result == WeaponBehaviour.WeaponUseStates.DEPLETED) {
            WeaponDestroyed();
        }

        if (result == WeaponBehaviour.WeaponUseStates.HIT && comboTimer <= comboBufferTime) {
            combo = true;
            comboTimer = 0;
            comboCount++;
        }

        if (result == WeaponBehaviour.WeaponUseStates.MISS) {
            combo = false;
            comboTimer = 0;
            comboCount = 0;
        }
        if (updateUI) {
            hudController.ComboCount = comboCount;
        }
    }

    public void Equip() {
        Equip(null);
    }

    public void Equip(WeaponAsset newWeapon) {

        if (newWeapon != null) {
            equippedWeapon = newWeapon;
        }

        // initialize newly picked up weapon
        if (equippedWeapon) {
            if (currentHandRig) {
                Destroy(currentHandRig.gameObject);
            }
            currentHandRig = Transform.Instantiate(equippedWeapon.handRig, handRigPosition, equippedWeapon.handRig.rotation, battlerRig);
            currentHandRig.localPosition = handRigPosition;
            currentHandRig.localRotation = Quaternion.identity;
            currentWeapon = currentHandRig.GetComponent<WeaponBehaviour>();
            currentWeapon.Equip(this);
        } else {
            if (currentHandRig) {
                Destroy(currentHandRig.gameObject);
            }
            currentHandRig = Transform.Instantiate(unequippedWeapon.handRig, handRigPosition, unequippedWeapon.handRig.rotation, battlerRig);
            currentHandRig.localPosition = handRigPosition;
            currentHandRig.localRotation = Quaternion.identity;
            currentWeapon = currentHandRig.GetComponent<WeaponBehaviour>();
            currentWeapon.Equip(this);
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

    public void PickUp(){
        if(pickupWeapon){
            equippedWeapon = pickupWeapon.pickup;

            pickupWeapon.PickUp();
            Equip();
        }
    }

}
