using UnityEngine;
using System.Collections.Generic;
using CustomInput;
public class ConfigurableInput : MonoBehaviour {
    public List<ButtonCommand> inputs;
    public List<AxisCommand> axes;

    public Dictionary<string, ButtonCommand> inputDictionary = new Dictionary<string, ButtonCommand>();
    public Dictionary<string, AxisCommand> axisDictionary = new Dictionary<string, AxisCommand>();


    void Awake() {
        foreach (ButtonCommand command in inputs) {
            command.Init();
            inputDictionary.Add(command.inputName, command);
        }

        foreach (AxisCommand command in axes) {
            command.Init();
            axisDictionary.Add(command.inputName, command);
        }
    }

    public float GetAxis(string name) {
        try {
            AxisCommand command;
            axisDictionary.TryGetValue(name, out command);
            return command.GetAxis();
        } catch {
            return 0;
        }
    }
    public bool GetButton(string name) {
        try {
            ButtonCommand command;
            inputDictionary.TryGetValue(name, out command);
            return command.GetInput();
        } catch {
            return false;
        }
    }

    public bool GetButtonDown(string name) {
        try {
            ButtonCommand command;
            inputDictionary.TryGetValue(name, out command);
            return command.GetInputDown();
        } catch {
            return false;
        }
    }

    public bool GetButtonUp(string name) {
        try {
            ButtonCommand command;
            inputDictionary.TryGetValue(name, out command);
            return command.GetInputUp();
        } catch {
            return false;
        }
    }
}