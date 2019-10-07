using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MeleeWeaponBehaviour : WeaponBehaviour {
    public MeleeWeapon weapon;
    public WeaponManager weaponManager;

    public AudioClip[] hitSounds;
    public AudioClip[] missSounds;
    AudioSource hitSource;
    private void OnEnable() {
        hitSource = GetComponent<AudioSource>();
    }

    public override WeaponUseStates Attack(int comboCount, WeaponManager manager) {
        Collider hit = manager.CheckMeleeHitbox();
        this.OnAttack(manager);
        if (hit) {
            hitSource.PlayOneShot(hitSounds[Random.Range(0,hitSounds.Length)]);
            ActorController controller = hit.GetComponent<ActorController>();
            if (controller && !controller.shielding) {
                Vector3 hitBackDir = hit.transform.position - manager.transform.position;
                hitBackDir = hitBackDir.normalized;
                controller.Damage(weapon.baseDamage, hitBackDir);
                return WeaponUseStates.HIT;
            } else if (controller.shielding){
                return WeaponUseStates.MISS;
            }
        }
        hitSource.PlayOneShot(missSounds[Random.Range(0,missSounds.Length)]);
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
