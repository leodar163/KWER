using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Ressource), true)]
public class RessourceEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("SAUVEGARDER"))
        {
            Sauvegarder();
        }
    }

    private void Sauvegarder()
    {
        AssetDatabase.Refresh();
        EditorUtility.SetDirty(target);
        AssetDatabase.SaveAssets();
    }
}
