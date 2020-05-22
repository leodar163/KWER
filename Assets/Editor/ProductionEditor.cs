using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Experimental.TerrainAPI;
using System.Data.SqlClient;
using System.Threading;

[CustomEditor(typeof(Production))]
public class ProductionEditor : Editor
{
    private EditorApplication.CallbackFunction callback;
    private Production production;
    

    public override void OnInspectorGUI()
    {
        production = (Production)target;
        
        DessinerGainsRessource();

        GUILayout.Space(20);
        if(GUILayout.Button("Sauvegarder"))
        {
            production.SauvegarderSO();
        }
    }




    private void DessinerGainsRessource()
    {
        Ressource[] listeRessources = ListeRessources.Defaut.listeDesRessources;

        float[] gains = new float[listeRessources.Length];


        for (int i = 0; i < gains.Length; i++)
        {
            GUILayout.BeginHorizontal();
            if (i < production.gains.Length)
            {
                 
                gains[i] = production.gains[i];
            }
            gains[i] = EditorGUILayout.FloatField("Gain en " + listeRessources[i].nom, gains[i]);
            GUILayout.EndHorizontal();
        }
        production.gains = (float[])gains.Clone();
        //Debug.Log(production.gains[0]);
    }

    
}
