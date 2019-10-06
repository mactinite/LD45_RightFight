using UnityEngine;

public class Fists : MeleeWeaponBehaviour {

    private Animator anim;
    

    private void Update() {
        this.anim.SetBool("Combo", weaponManager.combo);
    }
    private void Start() {
        this.anim = transform.GetComponent<Animator>();
        if(this.anim == null){
            Debug.LogWarning("Weapon doesn't have an animator");
        }
    }
    
    public override void OnAttack(WeaponManager manager) {
        this.anim.SetTrigger("Attack");
    }
}