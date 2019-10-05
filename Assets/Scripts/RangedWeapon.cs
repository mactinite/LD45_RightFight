using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "New Ranged Weapon")]
public class RangedWeapon : WeaponAsset {
    public Transform projectile;
    public int projectileDamage;
}