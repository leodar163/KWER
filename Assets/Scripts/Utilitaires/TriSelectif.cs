using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TriSelectif
{
    public static List<Terrains.TypeTerrain> TrierPetitGrand(List<Terrains.TypeTerrain> listeTerrains)
    {
        
        List<Terrains.TypeTerrain> listeRetour = new List<Terrains.TypeTerrain>();
        List<Terrains.TypeTerrain> listeOrigine = new List<Terrains.TypeTerrain>();
        listeOrigine = listeTerrains.GetRange(0,listeTerrains.Count);
       
        while (listeOrigine.Count > 0)
        {
            for (int i = 0; i < listeOrigine.Count; i++)
            {
                float minim = MiniMax.TrouverMinimum(listeOrigine).hauteur;
                if (listeOrigine[i].hauteur == minim)
                {
                    
                    listeRetour.Add(listeOrigine[i]);
                    listeOrigine.Remove(listeOrigine[i]);

                    
                }
            }
        }
        

        return listeRetour;
    }

    public static List<Terrains.TypeTerrain> TrierGrandPetit(List<Terrains.TypeTerrain> listeTerrains)
    {
        
        List<Terrains.TypeTerrain> listeRetour = new List<Terrains.TypeTerrain>();
        List<Terrains.TypeTerrain> listeOrigine = new List<Terrains.TypeTerrain>();
        listeOrigine = listeTerrains.GetRange(0, listeTerrains.Count);

        while (listeOrigine.Count > 0)
        {
            for (int i = 0; i < listeOrigine.Count; i++)
            {
                float max = MiniMax.TrouverMaximum(listeOrigine).hauteur;
                if (listeOrigine[i].hauteur == max)
                {
                    listeRetour.Add(listeOrigine[i]);
                    listeOrigine.Remove(listeOrigine[i]);
                    
                }
            }
        }

        return listeRetour;
    }

}
