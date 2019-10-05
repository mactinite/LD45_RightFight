using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CustomInput {
    public class CPUInput : MonoBehaviour, IManagedInput {

        public Command<float> MoveX;
        public Command<float> MoveY;
        public const string MOVE_X = "MoveX";
        public const string MOVE_Y = "MoveY";

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
                default:
                    Debug.LogError("Input " + name + " not implemented.");
                    return false;
            }
        }

        // Use this for initialization
        void Start() {
            this.MoveX = new Command<float>(MOVE_X, () => moveDirection.x);
            this.MoveY = new Command<float>(MOVE_Y, () => moveDirection.z);

        }
        Vector3 moveDirection = Vector3.zero;

        public Transform target;
        private void Update() {
            moveDirection = target.position - transform.position;
        }

    }
}