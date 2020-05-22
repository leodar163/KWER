using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Experimental.TerrainAPI;

[CustomEditor(typeof(Recette))]
public class RecetteEditor : Editor
{
    Recette recette;

    public override void OnInspectorGUI()
    {
        recette = (Recette)target;
        base.OnInspectorGUI();
        GUILayout.Space(20);
        if(GUILayout.Button("Sauvegarder"))
        {
            recette.Sauvegarder();
        }
    }

}
