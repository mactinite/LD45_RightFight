using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "New Melee Weapon")]
public class MeleeWeapon : WeaponAsset {
    public Vector3 hitboxSize = Vector3.zero;
    public float hitDuration = 0.1f;

    public override WeaponAsset.WeaponUseStates Attack(int comboCount, WeaponManager manager) {
        Collider hit = manager.CheckMeleeHitbox();
        if (hit) {
            ActorController controller = hit.GetComponent<ActorController>();


            if (controller) {
                Vector3 hitBackDir = hit.transform.position - manager.transform.position;
                hitBackDir = hitBackDir.normalized;
                controller.SetHitback(hitBackDir);
                return WeaponAsset.WeaponUseStates.HIT;
            }
        }

        return WeaponAsset.WeaponUseStates.MISS;
    }
}