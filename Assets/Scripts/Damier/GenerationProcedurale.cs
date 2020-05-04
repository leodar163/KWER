using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerationProcedurale : MonoBehaviour
{
    public float[,] CreerMappeHauteur(int colonnes, int lignes, float zoom, int octaves, float lacunarite, float persistance, int seed, Vector2 decalage)
    {
        float[,] mappeHauteur = new float[colonnes,lignes];

        Vector2[] decalageOctaves = DecalerOctaves(octaves, seed);

        //On génère la mappe des hauteurs
        for (int y = 0; y < lignes; y++)
        {
            for (int x = 0; x < colonnes; x++)
            {
                float hauteurBruit = 0;
                float frequence = 1;
                float amplitude = 1;

                //On génère toutes les octaves et on les additionne
                for (int i = 0; i < octaves; i++)
                {
                   //On divise par le zoom pour que plus le zoom soit élevé, plus on zoome fort
                    float bruitX = x / zoom * frequence + decalageOctaves[i].x + decalage.x; // on multiplie les arguments par la fréquence pour influencer le x (donc la largeur) de la fonction BruitDePerlin
                    float bruitY = y / zoom * frequence + decalageOctaves[i].y + decalage.y;



                    //On additionne entre eux les octaves à une coordonnée donnée de la mappe des hauteurs
                    hauteurBruit += Mathf.PerlinNoise(bruitX, bruitY) * 2 -1;
                    hauteurBruit *= amplitude; // On multiplie la sortie par l'amplitude pour influencer le y (donc la hauteur) de la fonction.

                    //Fréquence gère le niveau de détail, la lacunarité l'influence de manière exponentielle à chaque octave
                    frequence *= lacunarite;
                    //Amplitude gère la hauteur de la.. hauteur, la persistance l'influence de manière logarithmique à chaque octave (enfin il me semble que c'est logarithmique) Ca se rapproche de 0 au lieu de se rapprocher de l'infini
                    amplitude *= persistance;

                }
                mappeHauteur[x, y] = hauteurBruit;
            }
        }

        Normalise.Normaliser(mappeHauteur);
        return mappeHauteur;
    }

    private Vector2[] DecalerOctaves(int octaves, int seed)
    {

        System.Random rng = new System.Random(seed); // Génère un nombre aléatoire qui sera toujours le même en fonction de la seed
        Vector2[] decalageOctaves = new Vector2[octaves];

        for (int i = 0; i < octaves; i++)
        {
            decalageOctaves[i].x = rng.Next(-100000, 100000);
            decalageOctaves[i].y = rng.Next(-100000, 100000);
        }

        return decalageOctaves;


    }

    public void AttribuerHauteur(TuileManager[,] damier, float[,] mappeHauteur)
    {
        for (int y = 0; y < damier.GetLength(1); y++)
        {
            for (int x = 0; x < damier.GetLength(0); x++)
            {
                damier[x, y].hauteur = mappeHauteur[x, y];
            }
        }
        
    }

}


