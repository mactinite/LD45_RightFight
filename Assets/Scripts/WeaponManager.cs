using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class WeaponManager : MonoBehaviour, IWEaponManager {
    public WeaponAsset unequippedWeapon;
    public WeaponAsset equippedWeapon;


    void Update() {

    }

    public void Attack() {
        // keep track of combos
        // trigger weapon attack
    }

    public void Equip() {
        // initialize newly picked up weapon
    }

}
