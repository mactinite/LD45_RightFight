using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName="New Weapon")]
public class WeaponAsset : ScriptableObject
{
    public Sprite weaponSprite;
    public WeaponBehaviour weaponBehaviour;
}
