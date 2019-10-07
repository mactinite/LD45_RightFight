using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CustomInput {
    public class CPUInput : MonoBehaviour, IManagedInput {

        public Command<float> MoveX;

        public Command<float> MoveY;
        public Command<bool> HitButton;
        public const string MOVE_X = "MoveX";
        public const string MOVE_Y = "MoveY";

        WeaponManager weaponManager;

        AnimationController animationController;

        void Awake() {
            weaponManager = GetComponent<WeaponManager>();
            animationController = GetComponent<AnimationController>();
        }

        // this is super ugly omg
        public float GetAxisInput(string name) {

            switch (name) {
                case MOVE_X:
                    return MoveX.State;
                case MOVE_Y:
                    return MoveY.State;
                default:
                    Debug.Log("Input type not implemented.");
                    return 0;

            }
        }
        // this is super ugly omg
        public bool GetButtonInput(string name) {
            switch (name) {
                case Constants.HIT_BUTTON:
                    return HitButton.State;
                default:
                    Debug.LogError("Input " + name + " not implemented.");
                    return false;
            }
        }

        // Use this for initialization
        void Start() {
            this.MoveX = new Command<float>(MOVE_X, () => moveDirection.x);
            this.MoveY = new Command<float>(MOVE_Y, () => moveDirection.z);
            this.HitButton = new Command<bool>(Constants.HIT_BUTTON, () => {
                bool result = hitButton;
                if(hitButton == true){
                    hitButton = false;
                }
                return result;
            });

        }
        Vector3 moveDirection = Vector3.zero;

        public Transform target;

        public bool hitButton = false;
        private bool hasHit = true;
        private bool inRange;
        public float hitTimeout = 0.5f;
        private float hitTimer = 0.0f;

        public float reactionTime = 0.75f;
        private float reactionTimer = 0;

        public Transform tellEffects;
        private void Update() {
            moveDirection = target.position - transform.position;

            WeaponAsset currentWeapon = (weaponManager.equippedWeapon ? weaponManager.equippedWeapon : weaponManager.unequippedWeapon);

            if (currentWeapon.weaponType == WeaponAsset.WeaponType.Melee) {
                MeleeWeapon weapon = currentWeapon as MeleeWeapon;

                float dist = Vector3.Distance(target.position, transform.position);

                Collider col = weaponManager.meleeHitbox.checkStatus();

                if(col && col.tag == "Enemy"){
                    moveDirection = Vector3.Reflect(moveDirection, animationController.flip ? transform.right : -transform.right);
                }
                
                if(col && col.tag == "Player"){
                    inRange = true;
                    reactionTimer+= Time.deltaTime;
                } else{
                    inRange = false;
                    reactionTimer = 0;
                }
        

                if (!hasHit && inRange && reactionTimer > reactionTime) {
                    moveDirection = moveDirection * 0.1f;
                    hasHit = true;
                    hitButton = true;
                    tellEffects.gameObject.SetActive(false);
                }

                if (hasHit) {
                    hitTimer += Time.deltaTime;
                }

                if (hitTimer > hitTimeout) {
                    hitTimer = 0;
                    hasHit = false;
                }

                if(hitTimer > hitTimeout / 2){
                    tellEffects.gameObject.SetActive(true);
                }
            }

        }

    }
}