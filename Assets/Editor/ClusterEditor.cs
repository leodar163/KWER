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

        NormaliserProba();
        cluster = (ClusterEvenement)target;
        if(GUILayout.Button("SAUVEGARDER"))
        {
            Sauvegarder();
        }
    }

    private void Sauvegarder()
    {
        AssetDatabase.Refresh();
        EditorUtility.SetDirty(target);
        AssetDatabase.SaveAssets();
        NormaliserProba();
    }

    private void NormaliserProba()
    {
        cluster = (ClusterEvenement)target;

        float denominateur = 0;

        foreach(ClusterEvenement.EvenementProbable proba in cluster.listeEvenements)
        {
            denominateur += proba.proba;
        }
        if(denominateur == 0)
        {
            for (int i = 0; i < cluster.listeEvenements.Count; i++)
            {
                ClusterEvenement.EvenementProbable evement = cluster.listeEvenements[i];
                evement.proba = 100/cluster.listeEvenements.Count;
                cluster.listeEvenements[i] = evement;
            }
        }
        else
        {
            for (int i = 0; i < cluster.listeEvenements.Count; i++)
            {
                ClusterEvenement.EvenementProbable evement = cluster.listeEvenements[i];
                evement.proba = (evement.proba * 100) / denominateur;
                cluster.listeEvenements[i] = evement;
            }
        }
    }
}
