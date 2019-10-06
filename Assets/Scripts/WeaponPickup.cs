using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour {
    bool inRange = false;
    public WeaponAsset pickup;
    public SpriteRenderer spriteRenderer;

    public GameObject topLevel;

    private void Start() {
        spriteRenderer.sprite = pickup.sprite;
        if(!topLevel){
            topLevel = gameObject;
        }
    }
    void OnTriggerStay(Collider collider) {
        if (collider.tag == "Player") {
            collider.GetComponent<WeaponManager>().pickupWeapon = this;
        }
    }

    private void OnTriggerExit(Collider collider) {
        if (collider.tag == "Player") {
            inRange = false;
            collider.GetComponent<WeaponManager>().pickupWeapon = null;
        }
    }

    public void PickUp(){
        Destroy(topLevel);
    }

}
