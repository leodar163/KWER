using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor (typeof(DamierGen))]
public class Mappe : Editor
{
    string typeTuileGeneree;
    Color couleurBoutonTypeTuile = new Color(0.9f, 0.5f, 0.4f);
    Color couleurBoutonReinit = new Color(0.5f, 0.5f, 0.8f);
    Color couleurSauvegarde = new Color(0.5f, 0.8f, 0.5f);
    string nomMappe;

    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();

        DamierGen damierGen = (DamierGen)target;

        if (damierGen.genererEnTuileCarree)
        {
            typeTuileGeneree = "Damier à tuiles carrées";
        }
        else
        {
            typeTuileGeneree = "Damier à tuiles héxagonales";
        }

        if (DrawDefaultInspector())
        {
            if (damierGen.genererEnTuileCarree)
            {
                damierGen.ClearDamier(true);
                damierGen.GenDamierCarre(damierGen.colonnes, damierGen.lignes);
            }
            else
            {
                damierGen.ClearDamier(true);
                damierGen.GenDamierHexa(damierGen.colonnes, damierGen.lignes);
            }
        }

        GUI.backgroundColor = couleurBoutonTypeTuile;
        if (GUILayout.Button(typeTuileGeneree))
        {
            damierGen.genererEnTuileCarree = !damierGen.genererEnTuileCarree;
            damierGen.genererEnTuileHexa = !damierGen.genererEnTuileHexa;

            if (damierGen.genererEnTuileCarree)
            {
                damierGen.ClearDamier(true);
                damierGen.GenDamierCarre(damierGen.colonnes, damierGen.lignes);
            }
            else
            {
                damierGen.ClearDamier(true);
                damierGen.GenDamierHexa(damierGen.colonnes, damierGen.lignes);
            }

        }

        GUI.backgroundColor = couleurBoutonReinit;
        if(GUILayout.Button("Réinitialiser le damier"))
        {
            if (damierGen.genererEnTuileCarree)
            {
                damierGen.ClearDamier(true);
                damierGen.GenDamierCarre(damierGen.colonnes, damierGen.lignes);
            }
            else
            {
                damierGen.ClearDamier(true);
                damierGen.GenDamierHexa(damierGen.colonnes, damierGen.lignes);
            }
        }

        GUILayout.Space(15);
        GUI.backgroundColor = Color.white;

        GUILayout.Label("Sauvegarde : ");

        GUILayout.Space(5);

        GUILayout.Label("Nom de la mappe à sauvegarder");
        nomMappe = GUILayout.TextField(nomMappe);

        GUILayout.Space(7);

        GUI.backgroundColor = couleurSauvegarde;
        if (GUILayout.Button("Sauvegarder Mappe"))
        {
            SauvegardesMappe svgMappe = FindObjectOfType<SauvegardesMappe>();
            TuileManager[] listeTuile = FindObjectsOfType<TuileManager>();


            SauvegardesMappe.Mappe mappeASvger = new SauvegardesMappe.Mappe(nomMappe, damierGen.colonnes, damierGen.lignes, CreerMappeTerrain(damierGen.RecupDamier()));
            svgMappe.SauvegarderMappe(mappeASvger);
        }

    }

    private TuileTerrain[,] CreerMappeTerrain(TuileManager[,] damier)
    {
        
        TuileTerrain[,] mappeTerrain = new TuileTerrain[damier.GetLength(0), damier.GetLength(1)];


            for (int y = 0; y < damier.GetLength(1); y++)
            {
                for (int x = 0; x < damier.GetLength(0); x++)
                {
                    damier[x, y].Init();
                    mappeTerrain[x,y] = damier[x,y].terrainTuile;
                }
            }
            

        return mappeTerrain;
    }

    [CustomEditor(typeof(Terrains))]
    public class TerrainsEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            Terrains terrains = (Terrains)target;

            if (DrawDefaultInspector())
            {

            }
                if (GUILayout.Button("Ranger Terrains"))
            {
                terrains.listeDesTerrains = TriSelectif.TrierGrandPetit(terrains.listeDesTerrains);
            }
        }
    }

}

