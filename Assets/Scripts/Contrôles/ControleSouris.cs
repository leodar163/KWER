using System;
using System.Threading;
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
        QuandClique();
        SourisSurvol();
    }

    //Détecte si on vient de cliquer sur un élément de la tribu ou pas
    private bool EstEnfantDeTribuSelectionnee(Collider2D check)
    {
        if (check)
        {
            foreach (Transform parent in check.GetComponentsInParent<Transform>())
            {
                Tribu tribu;
                if (parent.TryGetComponent<Tribu>(out tribu))
                {
                    if(tribu == tribuControlee)
                    {
                        return true;
                    }
                }
            }
        }   
        return false;
    }

    private void QuandClique()
    {
        if (controlesActives)
        {
            //Gestion du clique gauche
            if (Input.GetMouseButtonUp(0))
            {
                Collider2D check = Physics2D.OverlapBox(Camera.main.ScreenToWorldPoint(Input.mousePosition), new Vector2(0.01f, 0.01f), 0);

                if (check && check.CompareTag("Unite") && tribuControlee.estEntreCampement == false)
                {
                    Tribu autre = check.GetComponent<Tribu>();

                    if (autre == tribuControlee)
                    {
                        tribuControlee.EntrerCampement(true);
                    }
                }
                else if (check && check.CompareTag("Unite") && tribuControlee.estEntreCampement == true)
                {
                    Tribu autre = check.GetComponent<Tribu>();

                    if (autre == tribuControlee)
                    {
                        tribuControlee.EntrerCampement(false);
                    }
                }
                //Si on clique sur autre chose qu'un élément de la tribu, 
                //else if(!EstEnfantDeTribuSelectionnee(check) && tribuControlee.estEntreCampement == true)
                //{
                //    tribuControlee.EntrerCampement(false);
                //}
            }
            //Gestion clique droit
            else if (Input.GetMouseButtonUp(1))
            {
                Collider2D checkTuile = Physics2D.OverlapBox(Camera.main.ScreenToWorldPoint(Input.mousePosition), new Vector2(0.01f, 0.01f), 0, maskTuile);

                if (tribuControlee && checkTuile)
                {
                    TuileManager tuileSelectionnee = checkTuile.GetComponent<TuileManager>();

                    //On se déplace sur la tuile sur laquelle on a cliqué, si elle est à portée
                    if (tribuControlee.tuilesAPortee.Contains(tuileSelectionnee) && !tribuControlee.estEntreCampement)
                    {
                        tribuControlee.Destination = tuileSelectionnee;
                    }
                    else if (tribuControlee.estEntreCampement)
                    {
                        tribuControlee.EntrerCampement(false);
                    }
                }
            }
            //Gestion clique milieux
            else if (Input.GetMouseButtonDown(2))
            {
                pointAccrocheSouris = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                camControle.sourisAccrochee = true;
            }
            else if (Input.GetMouseButtonUp(2))
            {
                camControle.sourisAccrochee = false;
            }
        }
    }

    //Gestion de l'overing
    private void SourisSurvol()
    {
        Collider2D checkTuile = Physics2D.OverlapBox(Camera.main.ScreenToWorldPoint(Input.mousePosition), new Vector2(0.01f, 0.01f), 0, maskTuile);

        if (checkTuile && tribuControlee)
        {
            TuileManager tuileSurvolee = checkTuile.GetComponent<TuileManager>();

            //Colore le chemin et le met à jour toutes les frames, si la tuile qu'on survole est à portee
            if (tuileSurvolee.aPortee)
            {
                if (!tribuControlee.estEntreCampement && controlesActives)
                {
                    tribuControlee.pathFinder.ColorerChemin(tribuControlee.pathFinder.TrouverChemin(tribuControlee.tuileActuelle, tuileSurvolee), tuileSurvolee.couleurTuileSurChemin);
                }
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
