using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MeleeWeaponBehaviour : WeaponBehaviour {
    public MeleeWeapon weapon;
    public WeaponManager weaponManager;
    public override WeaponUseStates Attack(int comboCount, WeaponManager manager) {
        Collider hit = manager.CheckMeleeHitbox();
        this.OnAttack(manager);
        if (hit) {
            ActorController controller = hit.GetComponent<ActorController>();


            if (controller) {
                Vector3 hitBackDir = hit.transform.position - manager.transform.position;
                hitBackDir = hitBackDir.normalized;
                controller.SetHitback(hitBackDir);
                return WeaponUseStates.HIT;
            }
        }

        return WeaponUseStates.MISS;
    }

    public virtual void OnAttack(WeaponManager manager) {

    }
    public override void Equip(WeaponManager manager) {
        weaponManager = manager;
        weapon = (manager.equippedWeapon ? manager.equippedWeapon : manager.unequippedWeapon) as MeleeWeapon;
        manager.SetMeleeRange(weapon.range);
    }
}
