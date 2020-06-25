using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ClusterEvenement))]
public class ClusterEditor : Editor
{
    ClusterEvenement cluster;

    protected override void OnHeaderGUI()
    {
        base.OnHeaderGUI();


        cluster = (ClusterEvenement)target;
        if(GUILayout.Button("SAUVEGARDER"))
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
