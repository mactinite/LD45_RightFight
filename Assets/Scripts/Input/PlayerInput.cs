using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CustomInput {
    public class PlayerInput : MonoBehaviour, IManagedInput {

        public Command<bool> JumpDown;
        public Command<bool> Jump;
        public Command<bool> JumpUp;
        public Command<float> MoveX;
        public Command<float> MoveY;

        public Command<bool> Pause;

        public KeyCode[] JumpKeys;
        public KeyCode[] PauseKeys;

        [SerializeField]
        [HideInInspector]
        private string moveXAxis;
        [SerializeField]
        [HideInInspector]
        private string moveYAxis;

        public const string PAUSE_BUTTON = "Pause";
        public const string JUMP_BUTTON = "Jump";
        public const string JUMP_BUTTON_DOWN = "JumpDown";
        public const string JUMP_BUTTON_UP = "JumpUp";
        public const string MOVE_X = "MoveX";
        public const string MOVE_Y = "MoveY";

        public string MoveXAxis {
            get {
                return moveXAxis;
            }

            set {
                moveXAxis = value;
            }
        }

        public string MoveYAxis {
            get {
                return moveYAxis;
            }

            set {
                moveYAxis = value;
            }
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
                case JUMP_BUTTON:
                    return this.Jump.State;
                case JUMP_BUTTON_DOWN:
                    return this.JumpDown.State;
                case JUMP_BUTTON_UP:
                    return this.JumpUp.State;
                case PAUSE_BUTTON:
                    return this.Pause.State;
                default:
                    Debug.LogError("Input " + name + " not implemented.");
                    return false;
            }
        }

        // Use this for initialization
        void Start() {
            this.JumpDown = new Command<bool>(JUMP_BUTTON_DOWN, () => { return GetAllKeysDown(JumpKeys); });
            this.Jump = new Command<bool>(JUMP_BUTTON, () => { return GetAllKeys(JumpKeys); });
            this.JumpUp = new Command<bool>(JUMP_BUTTON_UP, () => { return GetAllKeysUp(JumpKeys); });
            this.MoveX = new Command<float>(MOVE_X, () => { return Input.GetAxisRaw(MoveXAxis); });
            this.MoveY = new Command<float>(MOVE_Y, () => { return Input.GetAxisRaw(MoveYAxis); });
            this.Pause = new Command<bool>(PAUSE_BUTTON, () => { return GetAllKeysDown(PauseKeys); });

        }


        bool GetAllKeysDown(KeyCode[] keys) {
            foreach (KeyCode key in keys) {
                if (Input.GetKeyDown(key)) {
                    return true;
                }
            }
            return false;
        }

        bool GetAllKeysUp(KeyCode[] keys) {
            foreach (KeyCode key in keys) {
                if (Input.GetKeyUp(key)) {
                    return true;
                }
            }
            return false;
        }

        bool GetAllKeys(KeyCode[] keys) {

            foreach (KeyCode key in keys) {
                if (Input.GetKey(key)) {
                    return true;
                }
            }
            return false;
        }
    }
}