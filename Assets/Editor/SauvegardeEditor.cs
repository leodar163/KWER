using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SauvegardesMappe))]
public class SauvegardeEditor : Editor
{
    public override void OnInspectorGUI()
    {

        SauvegardesMappe svgMappe = (SauvegardesMappe)target;

        GUILayout.Label("Liste des sauvegardes : ");
        GUILayout.Space(15);

        AfficherInterfacesSauvegarde(svgMappe.mappes);
    }

    private void AfficherInterfacesSauvegarde(List<SauvegardesMappe.Mappe> listeMappes)
    {
        for (int i = 0; i < listeMappes.Count; i++)
        {
            CreerIntefaceSauvegarde(listeMappes[i]);
        }
    }

    private void CreerIntefaceSauvegarde(SauvegardesMappe.Mappe mappe)
    {
        GUILayout.BeginHorizontal();

        GUILayout.Label(mappe.nom);

        GUI.backgroundColor = new Color(0.3f, 0.8f, 0.3f);
        if(GUILayout.Button("charger"))
        {
            SauvegardesMappe svgMappe = (SauvegardesMappe)target;
            svgMappe.ChargerMappe(mappe);
        }

        GUI.backgroundColor = new Color(0.8f, 0.3f, 0.3f);
        if (GUILayout.Button("Supprimer"))
        {
            SauvegardesMappe svgMappe = (SauvegardesMappe)target;
            svgMappe.mappes.Remove(mappe);
        }

        GUILayout.EndHorizontal();
    }
}
