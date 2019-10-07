using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class WeaponManager : MonoBehaviour, IWEaponManager {
    public WeaponAsset unequippedWeapon;
    public WeaponAsset equippedWeapon;
    public HitboxMonitor meleeHitbox;

    public Transform handRigPosition;


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
        hudController = HUDController._instance as HUDController;
        Equip();
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

        attackTimer += Time.deltaTime;
        if (attackTimer > attackRate) {
            canAttack = true;
        }
    }

    float attackRate = 0;
    float attackTimer = 0;

    bool canAttack = true;
    public void Attack() {
        if (!canAttack) {
            return;
        }
        // handle attackRate
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
            int weaponUses = currentWeapon.uses;

            if(weaponUses < 0){
                char infinity = '\u221E';
                hudController.CurrentWeaponUses = infinity.ToString();
            } else {
                hudController.CurrentWeaponUses = weaponUses.ToString();
            }
            
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
            currentHandRig = Transform.Instantiate(equippedWeapon.handRig, handRigPosition);
            currentWeapon = currentHandRig.GetComponent<WeaponBehaviour>();
            currentWeapon.Equip(this);
            attackRate = equippedWeapon.attackRate;
            if (updateUI) {
                hudController.CurrentWeaponSprite = equippedWeapon.sprite;
                hudController.CurrentWeaponUses = currentWeapon.uses.ToString();
            }
        } else {
            if (currentHandRig) {
                Destroy(currentHandRig.gameObject);
            }
            currentHandRig = Transform.Instantiate(unequippedWeapon.handRig, handRigPosition);
            currentWeapon = currentHandRig.GetComponent<WeaponBehaviour>();
            attackRate = unequippedWeapon.attackRate;
            currentWeapon.Equip(this);
            if (updateUI) {
                hudController.CurrentWeaponSprite = unequippedWeapon.sprite;
                char infinity = '\u221E';
                hudController.CurrentWeaponUses = infinity.ToString();
            }
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

    public void PickUp() {
        if (pickupWeapon) {
            equippedWeapon = pickupWeapon.pickup;

            pickupWeapon.PickUp();
            Equip();
        }
    }

    public WeaponAsset GetEquippedWeapon() {
        return equippedWeapon;
    }
}
