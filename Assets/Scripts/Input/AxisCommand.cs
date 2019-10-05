using UnityEngine;

namespace CustomInput {
    [System.Serializable]
    public class AxisCommand {
        public string inputName;
        public string axisName = "Horizontal";
        private Command<float> AxisInput;
        public void Init() {
            AxisInput = new Command<float>(inputName, 0, () => {
                return Input.GetAxisRaw(axisName);
            });
        }
        public float GetAxis() {
            return AxisInput.State;
        }
    }


}