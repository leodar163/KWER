using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Experimental.TerrainAPI;

[CustomEditor(typeof(Production))]
public class ProductionEditor : Editor
{
    private Production production;

    public override void OnInspectorGUI()
    {
        production = (Production)target;
        Ressource[] listeRessources = ListeRessources.Defaut.listeDesRessources;

        float[] gains = new float[listeRessources.Length];

        for (int i = 0; i < gains.Length; i++)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("Gain en " + listeRessources[i].nom);
            if(i < production.gains.Length)
            {
                gains[i] = production.gains[i];
            }
            gains[i] = EditorGUILayout.FloatField(gains[i]);
            GUILayout.EndHorizontal();
        }

        production.gains = gains;
    }
}
