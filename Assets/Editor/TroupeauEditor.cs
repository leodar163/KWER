using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.ComponentModel;

[CustomEditor(typeof(Troupeau))]
public class TroupeauEditor : Editor
{
    Troupeau troupeau;
    Color typeSelectionne = new Color(20, 20,150);

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        troupeau = (Troupeau)target;

        GUILayout.Space(15);



        DessinerBoutonsTypeTroupeau();

        if (GUI.changed)
        {
            EditorUtility.SetDirty(troupeau);
        }
    }

    

    private void DessinerBoutonsTypeTroupeau()
    {
        GUILayoutOption[] options = new GUILayoutOption[2] {GUILayout.Width(150), GUILayout.Height(30) };

        GUILayout.Label("Type de troupeau");
        GUILayout.Space(10);

        ColorerSelection(troupeau.domesticable);
        if(GUILayout.Button("Domesticable", options))
        {
            troupeau.domesticable = true;
            troupeau.megaFaune = false;
            troupeau.predateur = false;
            troupeau.migration.cantonnerAuxPlaines = true;
        }
        ColorerSelection(troupeau.megaFaune);
        if (GUILayout.Button("MegaFaune", options))
        {
            troupeau.domesticable = false;
            troupeau.megaFaune = true;
            troupeau.predateur = false;
            troupeau.migration.cantonnerAuxPlaines = true;
        }
        ColorerSelection(troupeau.predateur);
        if (GUILayout.Button("Predateur", options))
        {
            troupeau.domesticable = false;
            troupeau.megaFaune = false;
            troupeau.predateur = true;
            troupeau.migration.cantonnerAuxPlaines = false;
        }
    }

    private void ColorerSelection(bool selectionne)
    {
        if(selectionne)
        {
            GUI.backgroundColor = typeSelectionne;
        }
        else
        {
            GUI.backgroundColor = Color.white;
        }
    }
}
