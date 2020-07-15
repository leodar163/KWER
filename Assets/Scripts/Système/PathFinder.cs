using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    //Génère le graphe de toutes les tuiles à portée
    public List<TuileManager> CreerGrapheTuilesAPortee(TuileManager tuileOrigine, float portee)
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

                        if (!tuileFille.parcouru && tuileFille.estOccupee == false)//On demande si la tuile a déjà été explorée par l'algo et si elle est occupée
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

                        if (!tuileFille.parcouru && tuileFille.estOccupee == false)//On demande si la tuile a déjà été explorée par l'algo et si elle est occupée
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

    //algo de Dijkstra
    public Stack<TuileManager> TrouverCheminPlusCourt(TuileManager tuileDepart, TuileManager tuileCible)
    {
        ReinitGraphe(); // Juste par sécurité
        List<TuileManager> graphe = new List<TuileManager>(FindObjectsOfType<TuileManager>());

        TuileManager tuileObservee;

        //Retire les tuiles qui sont déjà occupées et donc infranchissables
        List<TuileManager> listeTampon = new List<TuileManager>(graphe);
        foreach (TuileManager noeud in graphe)
        {
            if (noeud.estOccupee) listeTampon.Remove(noeud);
        }
        graphe = listeTampon;
        graphe.Add(tuileDepart);
        graphe.Add(tuileCible);

        //On initialise toutes les distance à infini sauf la tuile de départ
        for (int i = 0; i < graphe.Count; i++)
        {
            graphe[i].distance = Mathf.Infinity;
        }
        tuileDepart.distance = 0;

        while (graphe.Count > 0)
        {
            tuileObservee = trouverTuileConnectPlusProche(graphe);
            if (!tuileObservee) Debug.LogError("Pourquoi la tuile observée est null ?");
            graphe.Remove(tuileObservee);

            //On met à jour les distances
            for (int i = 0; i < tuileObservee.connections.Count; i++)
            {
                TuileManager connection = tuileObservee.connections[i];

                if(connection.distance > tuileObservee.distance + 
                    tuileObservee.connectionsDistance[i])
                {
                    connection.distance = tuileObservee.distance +
                    tuileObservee.connectionsDistance[i];

                    connection.predecesseur = tuileObservee;
                }
            }
        }

        return TrouverChemin(tuileDepart, tuileCible);
    }

    public Stack<TuileManager> TrouverCheminPlusCourt(TuileManager tuileDepart, TuileManager tuileCible, bool traversEau)
    {
        ReinitGraphe(); // Juste par sécurité
        List<TuileManager> graphe = new List<TuileManager>(FindObjectsOfType<TuileManager>());

        TuileManager tuileObservee;

        //Retire les tuiles qui sont déjà occupées et donc infranchissables
        List<TuileManager> listeTampon = new List<TuileManager>(graphe);
        foreach (TuileManager noeud in graphe)
        {
            if (noeud.estOccupee) listeTampon.Remove(noeud);
        }
        graphe = listeTampon;
        graphe.Add(tuileDepart);
        graphe.Add(tuileCible);

        //On initialise toutes les distance à infini
        for (int i = 0; i < graphe.Count; i++)
        {
            graphe[i].distance = Mathf.Infinity;
        }
        tuileDepart.distance = 0;

        while (graphe.Count > 0)
        {
            tuileObservee = trouverTuileConnectPlusProche(graphe);
            if (!tuileObservee) Debug.LogError("Pourquoi la tuile observée est null ?");
            graphe.Remove(tuileObservee);

            if (tuileObservee.terrainTuile.ettendueEau && !traversEau) continue;
            
            //On met à jour les distances
            for (int i = 0; i < tuileObservee.connections.Count; i++)
            {
                TuileManager connection = tuileObservee.connections[i];

                if (connection.distance > 
                        tuileObservee.distance +
                        tuileObservee.connectionsDistance[tuileObservee.RecupIndexConnection(connection)])
                {
                    connection.distance = 
                        tuileObservee.distance +
                        tuileObservee.connectionsDistance[tuileObservee.RecupIndexConnection(connection)];

                    connection.predecesseur = tuileObservee;
                }
            }
        }

        return TrouverChemin(tuileDepart, tuileCible);
    }

    private TuileManager trouverTuileConnectPlusProche(List<TuileManager> graphe)
    {
        float minim = Mathf.Infinity;
        TuileManager sommet = null;

        for (int i = 0; i < graphe.Count; i++)
        {
            if(graphe[i].distance < minim)
            {
                minim = graphe[i].distance;
                sommet = graphe[i];
            }
        }
        return sommet;
    }

    // Génère le chemin à partir du graphe de "prédécesseur" créé par la fonction CreerGrapheTuilesAPortee() et TrouverCheminPlusCourt()
    public Stack<TuileManager> TrouverChemin(TuileManager tuileOrigine, TuileManager tuileCible)
    {
        Stack<TuileManager> chemin = new Stack<TuileManager>();

        if (tuileCible.predecesseur != null)//Ne calcule ce faisant pas une tuile cible si elle n'est pas à portée. 
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

    public float CalculerLongeurChemin(Stack<TuileManager> chemin)
    {
        float distance = 0;
        List<TuileManager> listeTuile = new List<TuileManager>(chemin);

        for (int i = 0; i < listeTuile.Count -1; i++)
        {
            distance += listeTuile[i].connectionsDistance[listeTuile[i].RecupIndexConnection(listeTuile[i + 1])];
        }

        return distance;
    }

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

