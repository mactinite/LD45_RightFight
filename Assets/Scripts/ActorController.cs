using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomInput;
public class ActorController : MonoBehaviour {

    public float gravity = 20.0f;
    public float walkSpeed = 2.0f;
    public float jumpVelocity = 2.0f;

    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    private IManagedInput input;
    [HideInInspector]
    public CharacterController characterController;

    private WeaponManager weaponManager;

    private bool jump = false;
    private bool jumpDown = false;

    [HideInInspector]
    public bool hitButton = false;

    [HideInInspector]
    public bool hit = false;

    [HideInInspector]
    public bool pickup = false;
    public float hitBackTime = 0.25f;
    public float hitBackSpeed = 12.0f;

    private Vector3 moveDirection = Vector3.zero;
    private void Awake() {
        input = GetComponent<IManagedInput>();
        if (input == null) {
            Debug.LogWarning("No Managed Input on game object!");
        }

        characterController = GetComponent<CharacterController>();
        if (characterController == null) {
            Debug.LogWarning("No Character Controller on game object!");
        }

        weaponManager = GetComponent<WeaponManager>();
        if (weaponManager == null) {
            Debug.LogWarning("No Weapon Manager on game object!");
        }
    }

    void Start() {
        hudController = HUDController._instance as HUDController;
    }

    // Update is called once per frame
    void Update() {
        Move();
        HandleWeapons();
    }

    public void HandleWeapons() {
        hitButton = input.GetButtonInput(Constants.HIT_BUTTON);
        pickup = input.GetButtonInput(Constants.PICKUP_WEAPON);
        if (hitButton) {
            weaponManager.Attack();
        }

        if (pickup) {
            weaponManager.PickUp();
        }
    }

    public int health = 100;
    public void SetHitback(Vector3 direction) {
        hit = true;
        hitbackTimer = 0;
        hitBack = direction;
        hitBack.y += hitBackSpeed;
        if (health < 0) {
            WeaponAsset equipped = weaponManager.GetEquippedWeapon();
            if (equipped) {
                Instantiate(equipped.pickupPrefab, transform.position, equipped.pickupPrefab.rotation);
            }
            Destroy(this.gameObject);
        }
    }

    private HUDController hudController;
    public void Damage(int damage, Vector3 hitbackDir) {
        SetHitback(hitbackDir);
        health -= damage;
        if (weaponManager.updateUI) {
            hudController.PlayerHealth = health;
        }
    }

    public Vector3 hitBack = Vector3.zero;
    float hitbackTimer = 0;
    public void Move() {
        Vector3 moveVector = new Vector3(input.GetAxisInput(Constants.MOVE_X), 0, input.GetAxisInput(Constants.MOVE_Y));

        moveVector = moveVector.normalized * walkSpeed;

        jumpDown = input.GetButtonInput(Constants.JUMP_BUTTON_DOWN);
        jump = input.GetButtonInput(Constants.JUMP_BUTTON);



        if (characterController.isGrounded) {
            moveDirection.y = -1;
            if (jumpDown && !hit) {
                moveDirection.y = jumpVelocity;
            }
        }



        if (characterController.velocity.y <= 0) {
            moveDirection.y -= gravity * fallMultiplier * Time.deltaTime;
        } else if (characterController.velocity.y > 0 && !jump) {
            moveDirection.y -= gravity * lowJumpMultiplier * Time.deltaTime;
        } else {
            moveDirection.y -= gravity * Time.deltaTime;
        }



        if (hit == true && hitbackTimer <= hitBackTime) {
            hitbackTimer += Time.deltaTime;
            moveVector = hitBack * hitBackSpeed;
        }

        if (hitbackTimer > hitBackTime) {
            hitbackTimer = 0;
            hit = false;
        }

        moveVector += moveDirection;

        characterController.Move(moveVector * Time.deltaTime);

    }

}
