using UnityEngine;

namespace CustomInput {
    [System.Serializable]
    public class ButtonCommand {
        public string inputName;
        public KeyCode[] keys;
        private Command<bool> InputDown;
        private Command<bool> InputUp;
        private Command<bool> Input;
        public void Init() {
            InputDown = new Command<bool>(inputName, false, () => {
                return GetAllKeysDown(keys);
            });
            InputUp = new Command<bool>(inputName, false, () => {
                return GetAllKeysUp(keys);
            });
            Input = new Command<bool>(inputName, false, () => {
                return GetAllKeys(keys);
            });
        }

        public bool GetInputDown() {
            return InputDown.State;
        }
        public bool GetInputUp() {
            return InputUp.State;
        }
        public bool GetInput() {
            return Input.State;
        }

        bool GetAllKeysDown(KeyCode[] keys) {
            foreach (KeyCode key in keys) {
                if (UnityEngine.Input.GetKeyDown(key)) {
                    return true;
                }
            }
            return false;
        }

        bool GetAllKeysUp(KeyCode[] keys) {
            foreach (KeyCode key in keys) {
                if (UnityEngine.Input.GetKeyUp(key)) {
                    return true;
                }
            }
            return false;
        }

        bool GetAllKeys(KeyCode[] keys) {

            foreach (KeyCode key in keys) {
                if (UnityEngine.Input.GetKey(key)) {
                    return true;
                }
            }
            return false;
        }
    }


}