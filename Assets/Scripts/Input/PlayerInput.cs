using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CustomInput {
    public class PlayerInput : MonoBehaviour, IManagedInput {


        public List<Command<bool>> buttonCommands = new List<Command<bool>>();
        public List<Command<float>> axisCommands = new List<Command<float>>();
        public Command<bool> JumpDown;
        public Command<bool> Jump;
        public Command<bool> JumpUp;
        public Command<float> MoveX;
        public Command<float> MoveY;

        public Command<bool> Pause;

        public KeyCode[] JumpKeys;
        public KeyCode[] PauseKeys;
        public KeyCode[] HitKeys;

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

        public const string HIT_BUTTON = "HIT";

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
        public float GetAxisInput(string name) {

            foreach (Command<float> cmd in axisCommands) {
                if (cmd.Name == name) {
                    return cmd.State;
                }
            }

            Debug.Log("Input type not implemented.");
            return 0;

        }
        public bool GetButtonInput(string name) {

            foreach (Command<bool> cmd in buttonCommands) {
                if (cmd.Name == name) {
                    return cmd.State;
                }
            }

            Debug.LogError("Input " + name + " not implemented.");
            return false;
        }

        // Use this for initialization
        void Start() {

            buttonCommands.Add(new Command<bool>(JUMP_BUTTON_DOWN, () => { return GetAllKeysDown(JumpKeys); }));
            buttonCommands.Add(new Command<bool>(JUMP_BUTTON, () => { return GetAllKeys(JumpKeys); }));
            buttonCommands.Add(new Command<bool>(JUMP_BUTTON_UP, () => { return GetAllKeysUp(JumpKeys); }));
            buttonCommands.Add(new Command<bool>(PAUSE_BUTTON, () => { return GetAllKeysDown(PauseKeys); }));
            buttonCommands.Add(new Command<bool>(HIT_BUTTON, () => { return GetAllKeys(HitKeys); }));

            axisCommands.Add(new Command<float>(MOVE_X, () => { return Input.GetAxisRaw(MoveXAxis); }));
            axisCommands.Add(new Command<float>(MOVE_Y, () => { return Input.GetAxisRaw(MoveYAxis); }));
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