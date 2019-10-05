using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using CustomInput;

[CustomEditor(typeof(PlayerInput))]
public class PlayerInputEditor : Editor {

    string[] _axes;
    int _moveXIndex = 0;
    int _moveYIndex = 0;


    public override void OnInspectorGUI() {
        _axes = ReadInputManager.ReadAxes();
        var p_input = target as PlayerInput;
        DrawDefaultInspector();

        // set editor indexes based on what is set in the PlayerInput component
        if (p_input.MoveXAxis != null)
            _moveXIndex = ArrayUtility.IndexOf(_axes, p_input.MoveXAxis);
        if (p_input.MoveYAxis != null)
            _moveYIndex = ArrayUtility.IndexOf(_axes, p_input.MoveYAxis);

        // Draw our dropdowns
        _moveXIndex = EditorGUILayout.Popup("X Movement Axis", _moveXIndex, _axes);
        _moveYIndex = EditorGUILayout.Popup("Y Movement Axis", _moveYIndex, _axes);

        // Update our PlayerInput component
        p_input.MoveXAxis = _axes[_moveXIndex];
        p_input.MoveYAxis = _axes[_moveYIndex];

        EditorUtility.SetDirty(target);
    }

}
