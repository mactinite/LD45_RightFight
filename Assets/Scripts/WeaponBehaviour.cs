using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponBehaviour : MonoBehaviour {
    //TODO: Is this even necessary?
    public abstract void Attack(int comboCount);
    public abstract void Equip();
}
