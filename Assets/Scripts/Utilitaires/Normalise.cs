using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Normalise
{
    //Float en argument
    public static float[,,] Normaliser(float[,,] tablo)
    {

        float hauteurMinim = MiniMax.TrouverMinimum(tablo);
        float hauteurMax = MiniMax.TrouverMaximum(tablo);


        for (int z = 0; z < tablo.GetLength(2); z++)
        {
            for (int y = 0; y < tablo.GetLength(1); y++)
            {
                for (int x = 0; x < tablo.GetLength(0); x++)
                {
                    tablo[x, y, z] = (tablo[x, y, z] - hauteurMinim) / (hauteurMax - hauteurMinim);
                }
            }
        }
        return tablo;
    }

    public static float[,] Normaliser(float[,] tablo)
    {

        float hauteurMinim = MiniMax.TrouverMinimum(tablo);
        float hauteurMax = MiniMax.TrouverMaximum(tablo);

        for (int y = 0; y < tablo.GetLength(1); y++)
        {
            for (int x = 0; x < tablo.GetLength(0); x++)
            {
                tablo[x, y] = (tablo[x, y] - hauteurMinim) / (hauteurMax - hauteurMinim);
            }
        }
        return tablo;
    }

    
    public static float[] Normaliser(float[] tablo)
    {

        float hauteurMinim = MiniMax.TrouverMinimum(tablo);
        float hauteurMax = MiniMax.TrouverMaximum(tablo);


            for (int x = 0; x < tablo.GetLength(0); x++)
            {
                tablo[x] = (tablo[x] - hauteurMinim) / (hauteurMax - hauteurMinim);
        }
        return tablo;
    }

    public static List<float> Normaliser(List<float> liste)
    {

        float hauteurMinim = MiniMax.TrouverMinimum(liste);
        float hauteurMax = MiniMax.TrouverMaximum(liste);


        for (int x = 0; x < liste.Count; x++)
        {
            liste[x] = (liste[x] - hauteurMinim) / (hauteurMax - hauteurMinim);
        }
        return liste;
    }

    public static Queue<float> Normaliser(Queue<float> queue)
    {

        float hauteurMinim = MiniMax.TrouverMinimum(queue);
        float hauteurMax = MiniMax.TrouverMaximum(queue);

        float[] tabloMediateur = new float[queue.Count];


        for (int x = 0; x < queue.Count; x++)
        {
            tabloMediateur[x] = queue.Dequeue();
        }

        Normalise.Normaliser(tabloMediateur);

        for (int x = 0; x < queue.Count; x++)
        {
            queue.Enqueue(tabloMediateur[x]);
        }
        return queue;
    }

    public static Stack<float> Normaliser(Stack<float> stack)
    {

        float hauteurMinim = MiniMax.TrouverMinimum(stack);
        float hauteurMax = MiniMax.TrouverMaximum(stack);

        float[] tabloMediateur = new float[stack.Count];


        for (int x = 0; x < stack.Count; x++)
        {
            tabloMediateur[x] = stack.Pop();
        }

        Normalise.Normaliser(tabloMediateur);

        for (int x = 0; x < stack.Count; x++)
        {
            stack.Push(tabloMediateur[x]);
        }
        return stack;
    }

    //Int en Argument
    public static float[,,] Normaliser(int[,,] tablo)
    {

        float hauteurMinim = MiniMax.TrouverMinimum(tablo);
        float hauteurMax = MiniMax.TrouverMaximum(tablo);

        float[,,] tabloRetour = new float[tablo.GetLength(0), tablo.GetLength(1), tablo.GetLength(2)];

        for (int z = 0; z < tablo.GetLength(2); z++)
        {
            for (int y = 0; y < tablo.GetLength(1); y++)
            {
                for (int x = 0; x < tablo.GetLength(0); x++)
                {
                    tabloRetour[x, y, z] = (tablo[x, y, z] - hauteurMinim) / (hauteurMax - hauteurMinim);
                }
            }
        }
        return tabloRetour;
    }

    public static float[,] Normaliser(int[,] tablo)
    {

        float hauteurMinim = MiniMax.TrouverMinimum(tablo);
        float hauteurMax = MiniMax.TrouverMaximum(tablo);

        float[,] tabloRetour = new float[tablo.GetLength(0), tablo.GetLength(1)];

        
            for (int y = 0; y < tablo.GetLength(1); y++)
            {
                for (int x = 0; x < tablo.GetLength(0); x++)
                {
                    tabloRetour[x, y] = (tablo[x, y] - hauteurMinim) / (hauteurMax - hauteurMinim);
                }
            }
        return tabloRetour;
    }

    public static float[] Normaliser(int[] tablo)
    {

        float hauteurMinim = MiniMax.TrouverMinimum(tablo);
        float hauteurMax = MiniMax.TrouverMaximum(tablo);

        float[] tabloRetour = new float[tablo.GetLength(0)];

        
                for (int x = 0; x < tablo.GetLength(0); x++)
                {
                    tabloRetour[x] = (tablo[x] - hauteurMinim) / (hauteurMax - hauteurMinim);
            }
        return tabloRetour;
    }

    public static List<float> Normaliser(List<int> liste)
    {

        float hauteurMinim = MiniMax.TrouverMinimum(liste);
        float hauteurMax = MiniMax.TrouverMaximum(liste);


        List<float> listeRetour = new List<float>();

        for (int x = 0; x < liste.Count; x++)
        {
            listeRetour[x] = (liste[x] - hauteurMinim) / (hauteurMax - hauteurMinim);
            }
        return listeRetour;
    }

    public static Queue<float> Normaliser(Queue<int> queue)
    {

        float hauteurMinim = MiniMax.TrouverMinimum(queue);
        float hauteurMax = MiniMax.TrouverMaximum(queue);

        float[] tabloMediateur = new float[queue.Count];


        for (int x = 0; x < queue.Count; x++)
        {
            tabloMediateur[x] = queue.Dequeue();
        }

        Normalise.Normaliser(tabloMediateur);
        Queue<float> queueRetour = new Queue<float>();

        for (int x = 0; x < queue.Count; x++)
        {
            queueRetour.Enqueue(tabloMediateur[x]);
        }
        return queueRetour;
    }

    public static Stack<float> Normaliser(Stack<int> stack)
    {

        float hauteurMinim = MiniMax.TrouverMinimum(stack);
        float hauteurMax = MiniMax.TrouverMaximum(stack);

        float[] tabloMediateur = new float[stack.Count];


        for (int x = 0; x < stack.Count; x++)
        {
            tabloMediateur[x] = stack.Pop();
        }

        Normalise.Normaliser(tabloMediateur);
        Stack<float> stackretour = new Stack<float>();

        for (int x = 0; x < stack.Count; x++)
        {
            stackretour.Push(tabloMediateur[x]);
        }
        return stackretour;
    }
}
