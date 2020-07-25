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

        if (recette.typeOutput == Recette.TypeOutput.Consommable)
        {
            GUILayout.Space(10);
            GUILayout.Label("Consommable");
            recette.consommable = (Consommable)EditorGUILayout.ObjectField(recette.consommable, typeof(Consommable), true);
            GUILayout.Label("Cout");
            recette.cout = (Production)EditorGUILayout.ObjectField(recette.cout, typeof(Production), true);
        }
        else if (recette.typeOutput == Recette.TypeOutput.Ressources)
        {
            GUILayout.Space(10);
            GUILayout.Label("Production");
            recette.production = (Production)EditorGUILayout.ObjectField(recette.production, typeof(Production), true);
        }
        GUILayout.Space(20);
        if(GUILayout.Button("Sauvegarder"))
        {
            Sauvegarder();
        }
    }


    public void Sauvegarder()
    {
        AssetDatabase.Refresh();
        EditorUtility.SetDirty(target);
        AssetDatabase.SaveAssets();
    }
}
