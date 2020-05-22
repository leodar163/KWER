using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    //Génère le graphe de toutes les tuiles à portée
    public  List<TuileManager> CreerGrapheTuilesAPortee(TuileManager tuileOrigine, float portee)
    {
        ReinitGraphe();
        List<TuileManager> grapheAPortee = new List<TuileManager>();

        Queue<TuileManager> fileTuiles = new Queue<TuileManager>();

        fileTuiles.Enqueue(tuileOrigine);
        tuileOrigine.parcouru = true;

        //Parcours en largeur (BFS)
        while(fileTuiles.Count > 0)
        {
            TuileManager tuileObservee = fileTuiles.Dequeue();

            tuileObservee.EstAPortee();

            if(tuileObservee.distance < portee)
            {
                for (int i = 0; i < tuileObservee.connections.Count; i++)
                {
                    if (tuileObservee.connections[i] != null)
                    {
                        TuileManager tuileFille = tuileObservee.connections[i];

                        if (!tuileFille.parcouru)//On demande si la tuile a déjà été explorée par l'algo
                        {

                            tuileFille.parcouru = true;
                            tuileFille.predecesseur = tuileObservee;
                            tuileFille.distance += tuileObservee.connectionsDistance[i] + tuileObservee.distance;
                            fileTuiles.Enqueue(tuileFille);

                            grapheAPortee.Add(tuileFille);


                        }
                    }
                }
            }
            
        }
        return grapheAPortee;
    }

    public List<TuileManager> CreerGrapheTuilesAPortee(TuileManager tuileOrigine, float portee, bool peutEmbarquer)
    {
        ReinitGraphe();
        List<TuileManager> grapheAPortee = new List<TuileManager>();

        Queue<TuileManager> fileTuiles = new Queue<TuileManager>();

        fileTuiles.Enqueue(tuileOrigine);
        tuileOrigine.parcouru = true;

        //Parcours en largeur (BFS)
        while (fileTuiles.Count > 0)
        {
            TuileManager tuileObservee = fileTuiles.Dequeue();

            tuileObservee.EstAPortee();

            if (tuileObservee.distance < portee)
            {

                for (int i = 0; i < tuileObservee.connections.Count; i++)
                {
                    if (tuileObservee.connections[i] != null)
                    {
                        TuileManager tuileFille = tuileObservee.connections[i];

                        if (!tuileFille.parcouru)//On demande si la tuile a déjà été explorée par l'algo
                        {
                            if (peutEmbarquer)// si on peut embarquer, aucune restriction concernant l'eau
                            {
                                tuileFille.parcouru = true;
                                tuileFille.predecesseur = tuileObservee;
                                tuileFille.distance += tuileObservee.connectionsDistance[i] + tuileObservee.distance;
                                fileTuiles.Enqueue(tuileFille);

                                grapheAPortee.Add(tuileFille);
                            }
                            else if (!tuileFille.terrainTuile.ettendueEau)// si on ne peut peux pas embarquer, alors l'algo ne prend pas en compte les tuiles d'eau
                            {
                                tuileFille.parcouru = true;
                                tuileFille.predecesseur = tuileObservee;
                                tuileFille.distance += tuileObservee.connectionsDistance[i] + tuileObservee.distance;
                                fileTuiles.Enqueue(tuileFille);

                                grapheAPortee.Add(tuileFille);
                            }
                        }
                    }
                } 
            }

        }
        return grapheAPortee;
    }


    // Génère le chemin à partir du graphe de "prédécesseur" créé par la fonction CreerGrapheTuilesAPortee()
    public Stack<TuileManager> TrouverChemin(TuileManager tuileOrigine, TuileManager tuileCible)
    {
        Stack<TuileManager> chemin = new Stack<TuileManager>();


        if(tuileCible.predecesseur != null)//Ne calcule ce faisant pas une tuile cible si elle n'est pas à portée. 
        {
            TuileManager tuileObservee = tuileCible;
            while (tuileObservee != tuileOrigine)
            {
                chemin.Push(tuileObservee);
                tuileObservee = tuileObservee.predecesseur;
            }
        }

        return chemin;
    }

    /* Obsolète ; préferez utiliser MiniMax.TrouverMinim();
    private TuileManager TrouverMinimum(List<TuileManager> listeNoeuds)
    {
        float minim = Mathf.Infinity;
        TuileManager noeudRetour = null;

        foreach(TuileManager tM in listeNoeuds)
        {
            print("Distance de : " +tM.gameObject.name + " : " + tM.distance);
            if (tM.distance < minim)
            {
                
                minim = tM.distance;
                noeudRetour = tM;
            }
        }
        print(noeudRetour.gameObject.name);
        return noeudRetour;
    }
    */

    public void ColorerGraphe(List<TuileManager> graphe, Color couleur)
    {
        foreach (TuileManager tuile in graphe)
        {
            tuile.ColorerTuile(couleur);
        }
    }


    public void ColorerChemin(Stack<TuileManager> chemin, Color couleur)
    {
        foreach (TuileManager tuile in chemin)
        {
            tuile.ColorerTuile(couleur);
        }
    }


    public void ReinitGraphe()
        {
            TuileManager[] graphe = FindObjectsOfType<TuileManager>();

            foreach (TuileManager noeud in graphe)
            {

                noeud.TuileReinit();
            }
        }
 }

