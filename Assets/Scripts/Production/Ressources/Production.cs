using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Experimental.TerrainAPI;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using System;
using System.Security.Policy;
using UnityScript.Lang;

[CreateAssetMenu(fileName = "nvlleProduction", menuName = "Production")]
public class Production : ScriptableObject
{
    public float[] gains = new float[0];

    public static Production operator +(Production a, Production b)
    {
        Production prod = CreateInstance<Production>();
        float[] gains = (float[])a.gains.Clone();

        for (int i = 0; i < gains.Length; i++)
        {
            if (i < b.gains.Length)
            {
                gains[i] += b.gains[i];
            }
        }
        prod.gains = gains;
        return prod;
    }

    public static Production operator -(Production a, Production b)
    {
        Production prod = CreateInstance<Production>();
        float[] gains = (float[])a.gains.Clone();

        for (int i = 0; i < gains.Length; i++)
        {
            if (i < b.gains.Length)
            {
                gains[i] -= b.gains[i];
            }
        }
        prod.gains = gains;
        return prod;
    }

    public static Production operator *(Production a, float b)
    {
        Production prod = CreateInstance<Production>();
        float[] gains = (float[])a.gains.Clone();

        for (int i = 0; i < gains.Length; i++)
        {
            gains[i] *= b;
        }
        prod.gains = gains;
        return prod;
    }

    public static Production operator /(Production a, float b)
    {
        Production prod = CreateInstance<Production>();
        float[] gains = (float[])a.gains.Clone();

        for (int i = 0; i < gains.Length; i++)
        {
            gains[i] /= b;
        }
        prod.gains = gains;
        return prod;
    }


    public void AugmenterGain(string nomRessource, float augmentation)
    {
        for (int i = 0; i < ListeRessources.Defaut.listeDesRessources.Length; i++)
        {
            if(nomRessource == ListeRessources.Defaut.listeDesRessources[i].nom)
            {
                gains[i] += augmentation;
                return;
            }
        }
        Debug.LogError("Impossible d'augmenter le gain d'une ressource qui n'existe pas, ou le nom de la ressource est pas le bon. " +
            "Tip : Les Ressources commences par une majuscules et son en français");
    }

    public void MultiplierGain(string nomRessource, float multiplicateur)
    {
        for (int i = 0; i < ListeRessources.Defaut.listeDesRessources.Length; i++)
        {
            if (nomRessource == ListeRessources.Defaut.listeDesRessources[i].nom)
            {
                gains[i] *= multiplicateur;
                return;
            }
        }
        Debug.LogError("Impossible de multiplier le gain d'une ressource qui n'existe pas, ou le nom de la ressource est pas le bon. " +
            "Tip : Les Ressources commences par une majuscules et son en français");
    }

    public float RecupuererGainRessource(string nomRessource, float multiplicateur)
    {
        for (int i = 0; i < ListeRessources.Defaut.listeDesRessources.Length; i++)
        {
            if (nomRessource == ListeRessources.Defaut.listeDesRessources[i].nom)
            {
                gains[i] *= multiplicateur;
                return gains[i];
            }
        }
        Debug.LogError("Impossible de récupérer le gain d'une ressource qui n'existe pas, ou le nom de la ressource est pas le bon. " +
            "Tip : Les Ressources commences par une majuscules et son en français");
        return 0;
    }

    public void Sauvegarder()
    {
        AssetDatabase.Refresh();
        EditorUtility.SetDirty(this);
        AssetDatabase.SaveAssets();
    }
}
