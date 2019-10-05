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

    private IWEaponManager weaponManager;

    private bool jump = false;
    private bool jumpDown = false;

    public bool hit = false;

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

        weaponManager = GetComponent<IWEaponManager>();
        if (weaponManager == null) {
            Debug.LogWarning("No Weapon Manager on game object!");
        }
    }

    // Update is called once per frame
    void Update() {
        Move();
        HandleWeapons();
    }

    public void HandleWeapons() {
        hit = input.GetButtonInput(Constants.HIT_BUTTON);

        if (hit) {
            weaponManager.Attack();
        }
    }
    public void Move() {
        Vector3 moveVector = new Vector3(input.GetAxisInput(Constants.MOVE_X), 0, input.GetAxisInput(Constants.MOVE_Y));

        moveVector = moveVector.normalized * walkSpeed;

        jumpDown = input.GetButtonInput(Constants.JUMP_BUTTON_DOWN);
        jump = input.GetButtonInput(Constants.JUMP_BUTTON);

        if (characterController.isGrounded) {
            moveDirection.y = -1;
            if (jumpDown) {
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

        moveVector.y = moveDirection.y;

        characterController.Move(moveVector * Time.deltaTime);

    }

}
