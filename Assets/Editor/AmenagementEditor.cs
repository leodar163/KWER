using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[CustomEditor(typeof(Amenagement), true)]
public class AmenagementEditor : Editor
{
    Amenagement amenagement;

    public override void OnInspectorGUI()
    {
        amenagement = (Amenagement)target;
        GUILayout.Space(30);
        amenagement.gainAmenagementEte = (Production)EditorGUILayout.ObjectField("Gain Amenagement Eté", amenagement.gainAmenagementEte, typeof(Production), true);
        amenagement.gainAmenagementHiver = (Production)EditorGUILayout.ObjectField("Gain Amenagement Hiver", amenagement.gainAmenagementHiver, typeof(Production), true);
        amenagement.Slots = EditorGUILayout.IntField("nombre de slots",amenagement.Slots);
        GUILayout.Space(20);
        amenagement.palier = EditorGUILayout.IntField("Nombre de palier", amenagement.palier);
        
        GUILayout.Space(10);
        GUILayout.Label("Sprite d'Eté");
        for (int i = 0; i < amenagement.palier; i++)
        {
            if (amenagement.spritesEte.Count == i) amenagement.spritesEte.Add(null);
            amenagement.spritesEte[i] = (Sprite)EditorGUILayout.ObjectField("Sprite Palier " + (i + 1), amenagement.spritesEte[i], typeof(Sprite), true);
        }
        GUILayout.Space(10);
        GUILayout.Label("Sprites d'Hiver");
        for (int i = 0; i < amenagement.palier; i++)
        {
            if (amenagement.spritesHiver.Count == i) amenagement.spritesHiver.Add(null); 
            amenagement.spritesHiver[i] = (Sprite)EditorGUILayout.ObjectField("Sprite Palier " + (i + 1), amenagement.spritesHiver[i], typeof(Sprite), true);
        }

        if(amenagement.spritesEte.Count > amenagement.palier)
        {
            for (int i = 0; i < amenagement.spritesEte.Count - amenagement.palier; i++)
            {
                amenagement.spritesEte.RemoveAt(amenagement.palier);
            }
        }
        if (amenagement.spritesHiver.Count > amenagement.palier)
        {
            for (int i = 0; i < amenagement.spritesHiver.Count - amenagement.palier; i++)
            {
                amenagement.spritesHiver.RemoveAt(amenagement.palier);
            }
        }
        GUILayout.Space(15);

        DessinerSelecteurTerrainsAmenagables();

        GUILayout.Space(25);
        GUILayout.Label("Texte Infobulle");
        amenagement.texteInfobulle = EditorGUILayout.TextArea(amenagement.texteInfobulle);

        GUILayout.Space(30);
        if (GUILayout.Button("SAUVEGARDER"))
        {
            Sauvegarder();
        }
    }


    private void DessinerSelecteurTerrainsAmenagables()
    {
        GUILayout.Label("Terrains Amenageables");
        GUILayout.Space(10);
        Color defaut = GUI.backgroundColor;
        for (int i = 0; i < ListeTerrains.TousTerrains.Length; i++)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label(ListeTerrains.TousTerrains[i].nom);

            if(!amenagement.terrainsAmenageables.Contains(ListeTerrains.TousTerrains[i]))
                GUI.backgroundColor = ListeCouleurs.Defaut.couleurAlerteTexteInterface;
            if (amenagement.terrainsAmenageables.Contains(ListeTerrains.TousTerrains[i]))
                GUI.backgroundColor = ListeCouleurs.Defaut.couleurTexteBonus;
            GUILayoutOption[] options = new GUILayoutOption[2] { GUILayout.Width(40), GUILayout.Height(40) };
            if(GUILayout.Button("", options))
            {
                if(amenagement.terrainsAmenageables.Contains(ListeTerrains.TousTerrains[i]))
                {
                    amenagement.terrainsAmenageables.Remove(ListeTerrains.TousTerrains[i]);
                }
                else
                {
                    amenagement.terrainsAmenageables.Add(ListeTerrains.TousTerrains[i]);
                }
            }
            GUILayout.Space(5);
        GUILayout.EndHorizontal();
        }
        GUI.backgroundColor = defaut;
    }
    private void Sauvegarder()
    {
        AssetDatabase.Refresh();
        EditorUtility.SetDirty(target);
        AssetDatabase.SaveAssets();
    }
}
