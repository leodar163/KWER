using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControleSouris : MonoBehaviour
{
    LayerMask maskUnite;
    LayerMask maskTuile;

    GameObject objetSelectionne;
    CameraControle camControle;


    [HideInInspector] public Vector3 pointAccrocheSouris;

    public bool controlesActives = true;


    // Start is called before the first frame update
    void Start()
    {
        camControle = FindObjectOfType<CameraControle>();
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
            ReinitSelection();

            Collider2D checkUnite = Physics2D.OverlapBox(Camera.main.ScreenToWorldPoint(Input.mousePosition), new Vector2(0.01f, 0.01f), 0, maskUnite);
            Collider2D checkTuile = Physics2D.OverlapBox(Camera.main.ScreenToWorldPoint(Input.mousePosition), new Vector2(0.01f, 0.01f), 0, maskTuile);


            if(checkUnite)
            {
                GameObject autre = checkUnite.gameObject;
                autre.GetComponent<UniteManager>().EtreSelectionne();

                objetSelectionne = autre;
            }   
            else if(checkTuile)
            {
                GameObject autre = checkTuile.gameObject;
                TuileManager tM = autre.GetComponent<TuileManager>();

                /*
                foreach(GameObject tuile in tM.connections)
                {
                    tuile.GetComponent<TuileManager>().ColorerTuile(Color.blue);
                }
                */
            }
        }
        //Gestion clique droit
        else if(Input.GetMouseButtonUp(1))
        {
            

            Collider2D checkUnite = Physics2D.OverlapBox(Camera.main.ScreenToWorldPoint(Input.mousePosition), new Vector2(0.01f, 0.01f), 0, maskUnite);
            Collider2D checkTuile = Physics2D.OverlapBox(Camera.main.ScreenToWorldPoint(Input.mousePosition), new Vector2(0.01f, 0.01f), 0, maskTuile);

            if (checkUnite)
            {
                GameObject autre = checkUnite.gameObject;
            }
            else if (checkTuile && objetSelectionne && objetSelectionne.CompareTag("Unite"))
            {

                TuileManager tuileSelectionnee = checkTuile.GetComponent<TuileManager>();
                PathFinder pathfinder = objetSelectionne.GetComponent<PathFinder>();
                UniteManager Tribu = objetSelectionne.GetComponent<UniteManager>();

                Tribu.ImporterCheminASuivre(pathfinder.TrouverChemin(Tribu.tuileActuelle, tuileSelectionnee));
            }
            else
            {
                ReinitSelection();
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

    private void ReinitSelection()
    {
        if(objetSelectionne)
        {
            if (objetSelectionne.CompareTag("Tuile"))
            {

                objetSelectionne.GetComponent<TuileManager>().EtreDeselectionne();
                
            }

            else if (objetSelectionne.CompareTag("Unite"))
            {

                objetSelectionne.GetComponent<UniteManager>().EtreDeselectionne();
                
            }
            objetSelectionne = null;
        }
        
    }


    //Gestion de l'overing
    private void SourisSurvol()
    {
        Collider2D checkUnite = Physics2D.OverlapBox(Camera.main.ScreenToWorldPoint(Input.mousePosition), new Vector2(0.01f, 0.01f), 0, maskUnite);
        Collider2D checkTuile = Physics2D.OverlapBox(Camera.main.ScreenToWorldPoint(Input.mousePosition), new Vector2(0.01f, 0.01f), 0, maskTuile);

        if (checkUnite)
        {
            GameObject autre = checkUnite.gameObject;
        }
        else if (checkTuile && objetSelectionne && objetSelectionne.CompareTag("Unite"))
        {
            TuileManager tuileSurvolee = checkTuile.GetComponent<TuileManager>();
            

            PathFinder pathFinder = objetSelectionne.GetComponent<PathFinder>();
            UniteManager uM = objetSelectionne.GetComponent<UniteManager>();


            //Colore le chemin et le met à jour toutes les frames, si la tuile qu'on survole est à portee
            if (tuileSurvolee.aPortee)
            {
                pathFinder.ColorerGraphe(uM.tuilesAPortee, tuileSurvolee.couleurTuileAPortee);
                pathFinder.ColorerChemin(pathFinder.TrouverChemin(uM.tuileActuelle, tuileSurvolee), tuileSurvolee.couleurTuileSurChemin);
            }
           
            
           
        }
        else
        {

        }

    }

    private void InitMasks()
    {
        maskUnite = LayerMask.GetMask("Unite");
        maskTuile = LayerMask.GetMask("Tuile");
    }
}
