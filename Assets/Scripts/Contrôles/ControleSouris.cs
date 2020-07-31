using System;
using System.Threading;
using UnityEngine;

public class ControleSouris : MonoBehaviour
{
    #region SINGLETON
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
    #endregion

    LayerMask maskUnite;
    LayerMask maskTuile;

    //GameObject objetSelectionne;
    CameraControle camControle;

    [HideInInspector] public Vector3 pointAccrocheSouris;

    public bool controleEstActif = true;
    private bool modeInteraction = false;
    private Interaction interactionEnCours;

    
    // Start is called before the first frame update
    void Start()
    {
        camControle = FindObjectOfType<CameraControle>();
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
                    if(tribu == Tribu.TribukiJoue)
                    {
                        return true;
                    }
                }
            }
        }   
        return false;
    }

    public void ActiverModeInteraction(Interaction interaction, bool activer)
    {
        modeInteraction = activer;
        controleEstActif = !activer;
        Interaction[] toutesInteractions = FindObjectsOfType<Interaction>();

        for (int i = 0; i < toutesInteractions.Length; i++)
        {
            if(toutesInteractions[i] != interaction)
            {
                if(activer)
                {
                    toutesInteractions[i].DesactiverInteraction();
                }
                else
                {
                    toutesInteractions[i].ActiverInteraction();
                }
            }
        }

        if(activer)
        {
            interactionEnCours = interaction;
        }
        else
        {
            //interactionEnCours.EntrerEnInteraction(false);
            interactionEnCours = null;
        }
    }

    private void QuandClique()
    {
        if (controleEstActif)
        {
            //Gestion du clique gauche
            if (Input.GetMouseButtonUp(0))
            {

            }
            //Gestion clique droit
            else if (Input.GetMouseButtonUp(1))
            {
                Collider2D checkTuile = Physics2D.OverlapBox(Camera.main.ScreenToWorldPoint(Input.mousePosition), new Vector2(0.01f, 0.01f), 0, maskTuile);

                if (Tribu.TribukiJoue && checkTuile && !modeInteraction)
                {
                    TuileManager tuileSelectionnee = checkTuile.GetComponent<TuileManager>();

                    //On se déplace sur la tuile sur laquelle on a cliqué, si elle est à portée
                    if (Tribu.TribukiJoue.tuilesAPortee.Contains(tuileSelectionnee) && !Tribu.TribukiJoue.estEntreCampement)
                    {
                        Tribu.TribukiJoue.Destination = tuileSelectionnee;
                    }
                    else if (Tribu.TribukiJoue.estEntreCampement)
                    {
                        Tribu.TribukiJoue.EntrerCampement(false);
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

        if (modeInteraction)
        {
            //Quand on fait un clique droit et qu'on clique pas sur un enfant de l'interaction, on sort de l'interaction
            if (Input.GetMouseButtonUp(1))
            {
                Collider2D check = Physics2D.OverlapBox(Camera.main.ScreenToWorldPoint(Input.mousePosition), new Vector2(0.01f, 0.01f), 0);

                if (check == null ||
                check.GetComponentInParent<Interaction>() == null ||
                check.GetComponentInParent<Interaction>() != interactionEnCours)
                {
                    ActiverModeInteraction(interactionEnCours, false);
                }
            }
        }
    }

    //Gestion de l'overing
    private void SourisSurvol()
    {
        Collider2D checkTuile = Physics2D.OverlapBox(Camera.main.ScreenToWorldPoint(Input.mousePosition), new Vector2(0.01f, 0.01f), 0, maskTuile);

        if (checkTuile && Tribu.TribukiJoue)
        {
            TuileManager tuileSurvolee = checkTuile.GetComponent<TuileManager>();

            //Colore le chemin et le met à jour toutes les frames, si la tuile qu'on survole est à portee
            if (tuileSurvolee.aPortee)
            {
                if (!Tribu.TribukiJoue.estEntreCampement && controleEstActif)
                {
                    Tribu.TribukiJoue.pathFinder.ColorerChemin(Tribu.TribukiJoue.pathFinder.
                        TrouverChemin(Tribu.TribukiJoue.tuileActuelle, tuileSurvolee), tuileSurvolee.couleurTuileSurChemin);
                }
            }
        }
    }

    private void InitMasks()
    {
        maskUnite = LayerMask.GetMask("Unite");
        maskTuile = LayerMask.GetMask("Tuile");
    }
}
