using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MiniMax
{

    //Minimum
    static public float TrouverMinimum(float[,,] tablo)
    {
        float minim = Mathf.Infinity;

        foreach (float nbr in tablo)
        {
            if (nbr < minim)
            {
                minim = nbr;
            }
        }
        return minim;
    }

    static public float TrouverMinimum(float[,] tablo)
    {
        float minim = Mathf.Infinity;

        foreach (float nbr in tablo)
        {
            if (nbr < minim)
            {
                minim = nbr;
            }
        }
        return minim;
    }

    static public float TrouverMinimum(float[] tablo)
    {
        float minim = Mathf.Infinity;

        foreach (float nbr in tablo)
        {
            if (nbr < minim)
            {
                minim = nbr;
            }
        }
        return minim;
    }

    static public float TrouverMinimum(List<float> liste)
    {
        float minim = Mathf.Infinity;

        foreach (int nbr in liste)
        {
            if (nbr < minim)
            {
                minim = nbr;
            }
        }
        return minim;
    }

    static public float TrouverMinimum(Queue<float> liste)
    {
        float minim = Mathf.Infinity;

        foreach (int nbr in liste)
        {
            if (nbr < minim)
            {
                minim = nbr;
            }
        }
        return minim;
    }

    static public float TrouverMinimum(Stack<float> liste)
    {
        float minim = Mathf.Infinity;

        foreach (int nbr in liste)
        {
            if (nbr < minim)
            {
                minim = nbr;
            }
        }
        return minim;
    }

    static public int TrouverMinimum(int[,,] tablo)
    {
        int minim = int.MaxValue;

        foreach (int nbr in tablo)
        {
            if (nbr < minim)
            {
                minim = nbr;
            }
        }
        return minim;
    }

    static public int TrouverMinimum(int[,] tablo)
    {
        int minim = int.MaxValue;

        foreach (int nbr in tablo)
        {
            if (nbr < minim)
            {
                minim = nbr;
            }
        }
        return minim;
    }

    static public int TrouverMinimum(int[] tablo)
    {
        int minim = int.MaxValue;

        foreach (int nbr in tablo)
        {
            if (nbr < minim)
            {
                minim = nbr;
            }
        }
        return minim;
    }

    static public int TrouverMinimum(List<int> liste)
    {
        int minim = int.MaxValue;

        foreach (int nbr in liste)
        {
            if (nbr < minim)
            {
                minim = nbr;
            }
        }
        return minim;
    }

    static public int TrouverMinimum(Queue<int> liste)
    {
        int minim = int.MaxValue;

        foreach (int nbr in liste)
        {
            if (nbr < minim)
            {
                minim = nbr;
            }
        }
        return minim;
    }

    static public int TrouverMinimum(Stack<int> stack)
    {
        int minim = int.MaxValue;

        foreach (int nbr in stack)
        {
            if (nbr < minim)
            {
                minim = nbr;
            }
        }
        return minim;
    }

    static public Terrains.TypeTerrain TrouverMinimum(List<Terrains.TypeTerrain> liste)
    {
        Terrains.TypeTerrain minim = new Terrains.TypeTerrain();
        minim.hauteur = Mathf.Infinity;

        foreach (Terrains.TypeTerrain terrain in liste)
        {
            if (terrain.hauteur < minim.hauteur)
            {
                minim = terrain;
            }
        }
        return minim;
    }

    //Maximum
    static public float TrouverMaximum(float[,,] tablo)
    {
        float maximum = -Mathf.Infinity;

        foreach (float nbr in tablo)
        {
            if (nbr > maximum)
            {
                maximum = nbr;
            }
        }
        return maximum;
    }

    static public float TrouverMaximum(float[,] tablo)
    {
        float maximum = -Mathf.Infinity;

        foreach (float nbr in tablo)
        {
            if (nbr > maximum)
            {
                maximum = nbr;
            }
        }
        return maximum;
    }

    static public float TrouverMaximum(float[] tablo)
    {
        float maximum = -Mathf.Infinity;

        foreach (float nbr in tablo)
        {
            if (nbr > maximum)
            {
                maximum = nbr;
            }
        }
        return maximum;
    }

    static public float TrouverMaximum(List<float> liste)
    {
        float maximum = -Mathf.Infinity;

        foreach (int nbr in liste)
        {
            if (nbr > maximum)
            {
                maximum = nbr;
            }
        }
        return maximum;
    }

    static public float TrouverMaximum(Queue<float> liste)
    {
        float maximum = -Mathf.Infinity;

        foreach (int nbr in liste)
        {
            if (nbr > maximum)
            {
                maximum = nbr;
            }
        }
        return maximum;
    }

    static public float TrouverMaximum(Stack<float> liste)
    {
        float maximum = -Mathf.Infinity;

        foreach (int nbr in liste)
        {
            if (nbr > maximum)
            {
                maximum = nbr;
            }
        }
        return maximum;
    }

    static public int TrouverMaximum(int[,,] tablo)
    {
        int maximum = int.MinValue;

        foreach (int nbr in tablo)
        {
            if (nbr > maximum)
            {
                maximum = nbr;
            }
        }
        return maximum;
    }

    static public int TrouverMaximum(int[,] tablo)
    {
        int maximum = int.MinValue;

        foreach (int nbr in tablo)
        {
            if (nbr > maximum)
            {
                maximum = nbr;
            }
        }
        return maximum;
    }

    static public int TrouverMaximum(int[] tablo)
    {
        int maximum = int.MinValue;

        foreach (int nbr in tablo)
        {
            if (nbr > maximum)
            {
                maximum = nbr;
            }
        }
        return maximum;
    }

    static public int TrouverMaximum(List<int> liste)
    {
        int maximum = int.MinValue;

        foreach (int nbr in liste)
        {
            if (nbr > maximum)
            {
                maximum = nbr;
            }
        }
        return maximum;
    }

    static public int TrouverMaximum(Queue<int> liste)
    {
        int maximum = int.MinValue;

        foreach (int nbr in liste)
        {
            if (nbr > maximum)
            {
                maximum = nbr;
            }
        }
        return maximum;
    }

    static public int TrouverMaximum(Stack<int> liste)
    {
        int maximum = int.MinValue;

        foreach (int nbr in liste)
        {
            if (nbr > maximum)
            {
                maximum = nbr;
            }
        }
        return maximum;
    }

    static public Terrains.TypeTerrain TrouverMaximum(List<Terrains.TypeTerrain> liste)
    {
        Terrains.TypeTerrain maximum = new Terrains.TypeTerrain();
        maximum.hauteur = -Mathf.Infinity;

        foreach (Terrains.TypeTerrain terrain in liste)
        {
            if (terrain.hauteur > maximum.hauteur)
            {
                maximum = terrain;
            }
        }
        return maximum;
    }
}

