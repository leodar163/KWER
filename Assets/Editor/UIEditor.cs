using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(UIManager))]
public class UIEditor : Editor
{
    public override void OnInspectorGUI()
    {
        UIManager uiManager = (UIManager)target;
        
        if(DrawDefaultInspector())
        {
            uiManager.MaJTailleUI();
        }
    }
}
