using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SauvegardesMappe : MonoBehaviour
{
    [SerializeField] public List<Mappe> mappes;

    [System.Serializable]
    public struct Mappe
    {
        public string nom;
        public Terrains.TypeTerrain[,] terrains;
        public TuileTerrain[,] mappeTerrains;
        public int colonnes;
        public int lignes;

        public Mappe(string nomMappe,int nbrColonnes, int nbrLignes, Terrains.TypeTerrain[,] listeTerrains) //OBSOLET
        {
            nom = nomMappe;
            terrains = listeTerrains;
            colonnes = nbrColonnes;
            lignes = nbrLignes;
            mappeTerrains = null;
        }

        public Mappe(string nomMappe, int nbrColonnes, int nbrLignes, TuileTerrain[,] listeTerrains)
        {
            nom = nomMappe;
            terrains = null;
            colonnes = nbrColonnes;
            lignes = nbrLignes;
            mappeTerrains = listeTerrains;
        }
    }

    public void SauvegarderMappe(Mappe mappe)
    {
        if (mappes.Count > 0)
        {
            for (int i = 0; i < mappes.Count; i++)
            {
                    if (mappes[i].nom == mappe.nom)//On vérifie que le nom de la mappe a pas déjà été donné
                    {
                        mappes[i] = mappe;//Si c'est le cas, on écrase l'ancienne sauvegarde.
                    }
                    else
                    {
                        mappes.Add(mappe);
                    }
            }
        }
        else if (mappes.Count == 0)
        {
            print("mappe Sauvgardée");
            mappes.Add(mappe);
        }
    }

    public void ChargerMappe(Mappe mappe)
    {
        DamierGen damierGen = FindObjectOfType<DamierGen>();
        damierGen.ClearDamier(true);
        //damierGen.GenDamierHexa(mappe);
    }
}
