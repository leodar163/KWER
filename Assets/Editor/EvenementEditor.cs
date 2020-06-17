using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

    [CustomEditor(typeof(Evenement), true)]
public class EvenementEditor : Editor
{
    private Evenement evenement;
    public override void OnInspectorGUI()
    {
        evenement = (Evenement)target;

        base.OnInspectorGUI();

        GUILayout.Space(20);
        if(GUILayout.Button("SAUVEGARDER"))
        {
            evenement.Sauvegarder();
        }
    }
}
