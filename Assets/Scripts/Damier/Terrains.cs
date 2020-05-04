using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terrains : MonoBehaviour
{
    [SerializeField]public List<TypeTerrain> listeDesTerrains;

    [System.Serializable]
    public struct TypeTerrain
    {
        public string nom;
        public float hauteur;
        public Sprite sprite;
        public float coutfranchissement;
        public bool ettendueEau;

        //constructeur de structure
        public TypeTerrain(string nomTerrain, float hauteurMinimale, Sprite spriteTerrain, float coutFranchissementTerrain, bool estEttendueDEau)
            {
            nom = nomTerrain;
            hauteur = hauteurMinimale;
            sprite = spriteTerrain;
            coutfranchissement = coutFranchissementTerrain;
            ettendueEau = estEttendueDEau;
            }

    }

    

    //Attribue un terrain à une tuile en fonction de sa hauteur 
    /*  OBSOLET
    public void AttribuerTerrain(TuileManager[,] damier)
    {
        List<TypeTerrain> listeTriee = TriSelectif.TrierGrandPetit(listeDesTerrains);

        for (int y = 0; y < damier.GetLength(1); y++)
        {
            for (int x = 0; x < damier.GetLength(0); x++)
            {
                for (int i = 0; i < listeTriee.Count; i++)
                {
                    if (damier[x,y].hauteur >= listeTriee[i].hauteur)
                    {
                        damier[x, y].SetTerrain(listeTriee[i]);
                        break;
                    }
                }
            }
        }
    }

    public void AttribuerTerrain(TuileManager[,] damier, List<TypeTerrain> listeTerrains)
    {
        List<TypeTerrain> listeTriee = TriSelectif.TrierGrandPetit(listeTerrains);

        for (int y = 0; y < damier.GetLength(1); y++)
        {
            for (int x = 0; x < damier.GetLength(0); x++)
            {
                for (int i = 0; i < listeTriee.Count; i++)
                {
                    if (damier[x, y].hauteur >= listeTriee[i].hauteur)
                    {
                        damier[x, y].SetTerrain(listeTriee[i]);
                        break;
                    }
                }
            }
        }
    }
    */



}
