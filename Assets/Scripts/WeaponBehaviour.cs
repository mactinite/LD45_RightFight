using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class  WeaponBehaviour: MonoBehaviour
{
    public abstract void Attack(Sprite sprite, int comboCount);
}
