using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEngine;

[CustomEditor(typeof(Buff), true)]
public class BuffEditor : Editor 
{
    Buff buff;
    Color typeSelectionne = new Color(20, 20, 150);

    public override void OnInspectorGUI()
    {
        buff = (Buff)target;

        base.OnInspectorGUI();

        if(buff.compteurTour)
        {
            GUILayout.Space(10);
            GUILayout.Label("Nombre de tour que dure l'effet");
            buff.nombreTour = EditorGUILayout.IntField(buff.nombreTour);
            GUILayout.Space(30);
        }

        DessinerBoutonType();

        GUILayout.Space(30);

        if (GUILayout.Button("SAUVEGARDER"))
        {
            Sauvegarder();
        }
    }

    private void DessinerBoutonType()
    {
        GUILayoutOption[] options = new GUILayoutOption[2] { GUILayout.Width(150), GUILayout.Height(30) };

        GUILayout.Label("Type de décompte");
        GUILayout.Space(10);

        ColorerSelection(buff.compteurTour);
        if (GUILayout.Button("Compteur de Tour", options))
        {
            buff.compteurTour = true;
            buff.tpsDuneTechno = false;
            buff.tpsDunEvent = false;
        }

        ColorerSelection(buff.tpsDunEvent);
        if (GUILayout.Button("Buff d'événement", options))
        {
            buff.compteurTour = false;
            buff.tpsDuneTechno = false;
            buff.tpsDunEvent = true;
        }

        ColorerSelection(buff.tpsDuneTechno);
        if (GUILayout.Button("Buff de technologie", options))
        {
            buff.compteurTour = false;
            buff.tpsDuneTechno = true;
            buff.tpsDunEvent = false;
        }
        ColorerSelection(false);
    }

    private void ColorerSelection(bool selectionne)
    {
        if (selectionne)
        {
            GUI.backgroundColor = typeSelectionne;
        }
        else
        {
            GUI.backgroundColor = Color.white;
        }
    }

    private void Sauvegarder()
    {
        AssetDatabase.Refresh();
        EditorUtility.SetDirty(target);
        AssetDatabase.SaveAssets();
    }
}
