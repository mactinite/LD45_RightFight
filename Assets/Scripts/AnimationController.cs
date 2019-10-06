﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour {

    public Animator anim;
    public ActorController actor;
    public Transform sprite;
    bool flip;

    // Update is called once per frame
    void Update() {
        bool moving = Mathf.Abs(actor.characterController.velocity.x) > 0.25f || Mathf.Abs(actor.characterController.velocity.z) > 0.25f;
        anim.SetFloat("VerticalSpeed", actor.characterController.velocity.y);
        anim.SetBool("grounded", actor.characterController.isGrounded);
        anim.SetBool("moving", moving);

        anim.SetBool("hit", actor.hit);

        if (actor.characterController.velocity.x > 0) {
            flip = false;
        } else if (actor.characterController.velocity.x < 0) {
            flip = true;
        }

        if (flip) {
            sprite.localScale = new Vector3(-1, 1, 1);
        } else {
            sprite.localScale = Vector3.one;
        }

    }
}
