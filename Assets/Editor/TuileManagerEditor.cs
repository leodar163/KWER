using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TuileManager))]
public class TuileManagerEditor : Editor
{
    Color couleurTuileAPortee = Color.white;
    Color couleurTuileSurChemin = Color.white;

    public override void OnInspectorGUI()
    {
        TuileManager tuileManager = (TuileManager)target;

        
        if (DrawDefaultInspector())
        {

        }


    }
}
