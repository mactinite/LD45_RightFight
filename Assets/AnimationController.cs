using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour {

    public Animator anim;
    public ActorController actor;
    public SpriteRenderer sprite;
    public bool flip;

    // Update is called once per frame
    void Update() {
        anim.SetFloat("VerticalSpeed", actor.characterController.velocity.y);
        anim.SetBool("grounded", actor.characterController.isGrounded);
        anim.SetBool("moving", actor.characterController.velocity.magnitude > 0);

        if(actor.characterController.velocity.x > 0) {
            flip = false;
        } else if(actor.characterController.velocity.x < 0) {
            flip = true;
        }

        sprite.flipX = flip;
    }   
}

