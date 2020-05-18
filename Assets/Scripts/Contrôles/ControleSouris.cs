﻿using System.Threading;
using UnityEngine;

public class ControleSouris : MonoBehaviour
{
    //Singleton
    private static ControleSouris actuel;
    public static ControleSouris Actuel
    {
        get
        {
            if (actuel == null)
            {
                actuel = FindObjectOfType<ControleSouris>();
            }

            return actuel;
        }

    }

    LayerMask maskUnite;
    LayerMask maskTuile;

    //GameObject objetSelectionne;
    Tribu tribuControlee;
    [SerializeField] private int idTribuControlee;
    CameraControle camControle;


    [HideInInspector] public Vector3 pointAccrocheSouris;

    public bool controlesActives = true;


    // Start is called before the first frame update
    void Start()
    {
        camControle = FindObjectOfType<CameraControle>();
        RecupererTribuControlee();
        InitMasks();
    }

    // Update is called once per frame
    void Update()
    {
        if(controlesActives)
        {
            QuandClique();
            SourisSurvol();
        }
    }

    private void QuandClique()
    {
        //Gestion du clique gauche
        if(Input.GetMouseButtonUp(0))
        {
            Collider2D check = Physics2D.OverlapBox(Camera.main.ScreenToWorldPoint(Input.mousePosition), new Vector2(0.01f, 0.01f), 0);

            if(check && check.CompareTag("Unite") && tribuControlee.modeCampement == false)
            {
                Tribu autre = check.GetComponent<Tribu>();

                if(autre == tribuControlee)
                {
                    tribuControlee.Selectionner(true);
                }
            }
            else if(check.CompareTag("Tuile") && tribuControlee.modeCampement == true)
            {
                tribuControlee.Selectionner(false);
            }

        }
        //Gestion clique droit
        else if(Input.GetMouseButtonUp(1))
        {
            Collider2D checkTuile = Physics2D.OverlapBox(Camera.main.ScreenToWorldPoint(Input.mousePosition), new Vector2(0.01f, 0.01f), 0, maskTuile);

            if (tribuControlee)
            {
                TuileManager tuileSelectionnee = checkTuile.GetComponent<TuileManager>();

                //On se déplace sur la tuile sur laquelle on a cliqué, si elle est à portée
                if(tribuControlee.tuilesAPortee.Contains(tuileSelectionnee))
                {
                    PathFinder pathfinder = tribuControlee.GetComponent<PathFinder>();

                    tribuControlee.Destination = tuileSelectionnee;
                }
            }
        }
        //Gestion clique milieux
        else if(Input.GetMouseButtonDown(2))
        {
            pointAccrocheSouris = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            camControle.sourisAccrochee = true;
        }
        else if (Input.GetMouseButtonUp(2))
        {
            camControle.sourisAccrochee = false;
        }
    }

    //Gestion de l'overing
    private void SourisSurvol()
    {
        Collider2D checkTuile = Physics2D.OverlapBox(Camera.main.ScreenToWorldPoint(Input.mousePosition), new Vector2(0.01f, 0.01f), 0, maskTuile);

        if (checkTuile && tribuControlee)
        {
            TuileManager tuileSurvolee = checkTuile.GetComponent<TuileManager>();
            
            PathFinder pathFinder = tribuControlee.GetComponent<PathFinder>();
            //Colore le chemin et le met à jour toutes les frames, si la tuile qu'on survole est à portee
            if (tuileSurvolee.aPortee)
            {
            pathFinder.ColorerGraphe(tribuControlee.tuilesAPortee, tuileSurvolee.couleurTuileAPortee);
                pathFinder.ColorerChemin(pathFinder.TrouverChemin(tribuControlee.tuileActuelle, tuileSurvolee), tuileSurvolee.couleurTuileSurChemin);
            }  
        }

    }

    private void RecupererTribuControlee()
    {
        Tribu[] listeTribus = FindObjectsOfType<Tribu>();

        foreach(Tribu trib in listeTribus)
        {
            if(trib.idTribu == idTribuControlee)
            {
                tribuControlee = trib;
                break;
            }
        }
    }

    private void InitMasks()
    {
        maskUnite = LayerMask.GetMask("Unite");
        maskTuile = LayerMask.GetMask("Tuile");
    }
}
