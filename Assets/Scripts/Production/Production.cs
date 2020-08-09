using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "nvlleProduction", menuName = "Economie/Production")]
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
            "Tip : Les Ressources commencent par une majuscule et sont en français");
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
            "Tip : Les Ressources commencent par une majuscule et sont en français");
    }

    public void MultiplierGain(float multiplicateur)
    {
        for (int i = 0; i < gains.Length; i++)
        {
            gains[i] *= multiplicateur;
        }
    }

    public float RecupuererGainRessource(string nomRessource)
    {
        for (int i = 0; i < ListeRessources.Defaut.listeDesRessources.Length; i++)
        {
            if (nomRessource == ListeRessources.Defaut.listeDesRessources[i].nom)
            {
               return gains[i];
            }
        }
        Debug.LogError("Impossible de récupérer le gain d'une ressource qui n'existe pas, ou le nom de la ressource est pas le bon. " +
            "Tip : Les Ressources commencent par une majuscule et sont en français");
        return 0;
    }


    public void Clear()
    {
        for (int i = 0; i < gains.Length; i++)
        {
            gains[i] = 0;
        }
    }

    public void Initialiser()
    {
        if(ListeRessources.Defaut)gains = new float[ListeRessources.Defaut.listeDesRessources.Length];
    }

    public Production Cloner()
    {
        Production clone = CreateInstance<Production>();

        clone.gains = (float[])gains.Clone();

        return clone;
    }

    public void AppliquerBonusProduction(BonusTribu bonus)
    {
        MultiplierGain(bonus.bonusMultProd);
        MultiplierGain("Nourriture", bonus.bonusMultProdNourriture);
        MultiplierGain("Pierre", bonus.bonusMultProdPierre);
        MultiplierGain("Peau", bonus.bonusMultProdPeau);
        MultiplierGain("Outil", bonus.bonusMultProdOutil);
        MultiplierGain("Pigment", bonus.bonusMultProdPigment);
    }
}
