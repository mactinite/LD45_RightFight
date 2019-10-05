using UnityEngine;

public class Fists : WeaponBehaviour {

    Animator anim;

    private void Awake() {
        anim = GetComponent<Animator>();
        if(anim == null){
            Debug.LogWarning("Weapon doesn't have an animator");
        }
    }
    public override void Attack(int comboCount) {
        anim.SetTrigger("Attack");
    }

    public override void Equip() {
        throw new System.NotImplementedException();
    }
}