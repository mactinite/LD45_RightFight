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

    private AudioSource hitSource;
    private ScoreManager scoreManager;
    public int comboCount = 0;
    public float comboBufferTime = 0.1f;
    public bool combo = false;
    private float comboTimer;

    public bool updateUI = false;

    private WeaponPickup pickupWeapon;
    private ActorController actor;

    private float startingXpos;

    void Start() {
        hudController = HUDController._instance as HUDController;
        scoreManager = ScoreManager._instance as ScoreManager;
        actor = GetComponent<ActorController>();
        startingXpos = transform.position.x;
        hudController.ComboCount = comboCount;
        scoreManager.CurrentMultiplier = Mathf.Max(1, comboCount);
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
            

            if (transform.position.x - startingXpos > 1) {
                scoreManager.AddScore(Mathf.RoundToInt(transform.position.x - startingXpos));
                startingXpos = transform.position.x;
            }

        }

        attackTimer += Time.deltaTime;
        if (attackTimer > attackRate) {
            canAttack = !actor.shielding;
        }
    }

    float attackRate = 0;
    float attackTimer = 0;

    bool canAttack = true;

    public WeaponPickup PickupWeapon { get => pickupWeapon; set => pickupWeapon = value; }

    public void Attack() {
        if (!canAttack) {
            return;
        }
        canAttack = false;
        attackTimer = 0;

        WeaponBehaviour.WeaponUseStates result;

        result = currentWeapon.Use(comboCount, this);

        if (result == WeaponBehaviour.WeaponUseStates.DEPLETED) {
            WeaponDestroyed();
        }

        if (result == WeaponBehaviour.WeaponUseStates.HIT && comboTimer <= comboBufferTime) {
            combo = true;
            comboTimer = 0;
            comboCount++;
            scoreManager.CurrentMultiplier = Mathf.Max(1, comboCount);
            scoreManager.AddScore(getCurrentWeaponAsset().baseDamage);
        }

        if (result == WeaponBehaviour.WeaponUseStates.MISS) {
            combo = false;
            comboTimer = 0;
            comboCount = 0;
            scoreManager.CurrentMultiplier = Mathf.Max(1, comboCount); ;
        }
        if (updateUI) {
            hudController.ComboCount = comboCount;
            int weaponUses = currentWeapon.uses;

            if (weaponUses < 0) {
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
        if (PickupWeapon) {
            equippedWeapon = PickupWeapon.pickup;
            PickupWeapon.PickUp();
            PickupWeapon = null;
            Equip();
        }
    }

    public WeaponAsset GetEquippedWeapon() {
        return equippedWeapon;
    }

    public WeaponAsset getCurrentWeaponAsset() {
        return equippedWeapon ? equippedWeapon : unequippedWeapon;
    }
}
