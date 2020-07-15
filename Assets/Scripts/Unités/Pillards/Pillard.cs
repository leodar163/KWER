using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pillard : Pion
{
    [Header("Interactions")]
    public Hostile hostile;

    [Header("Déplacements")]
    public PathFinder pathFinder;
    public TuileManager tuileActuelle;
    public float ptsDeplacementDefaut;
    private float ptsDeplacement;
    public float vitesse;
    public bool peutTraverserEau;

    private TuileManager prochaineTuile;
    private Stack<TuileManager> chemin;
    private bool traverseFleuve;

    private Tribu tribuCible;
    private int entetementDefaut = 4;
    private int entetement;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        entetement = entetementDefaut;
        TourParTour.Defaut.eventNouveauTour.AddListener(() => entetement--);
        TrouverTuileActuelle();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    #region IA

    public override void DemarrerTour()
    {
        base.DemarrerTour();
        ptsDeplacement = ptsDeplacementDefaut;
        StartCoroutine(DeroulerTour());
    }

    private IEnumerator DeroulerTour()
    {
        while (ptsDeplacement > 0)
        {
            if(hostile.PeutAttaquer)
            {
                StartCoroutine(hostile.Attaquer());
            }
            else
            {
                if(!tribuCible || entetement <= 0)
                {
                    entetement = entetementDefaut;
                    tribuCible = TrouverTribuPlusProche();
                }

                chemin = pathFinder.TrouverCheminPlusCourt(tuileActuelle, tribuCible.tuileActuelle, peutTraverserEau);

                StartCoroutine(SeDeplacer());
            }

            yield return new WaitUntil(() => aFaitUneAction);
            aFaitUneAction = false;
        }

        aPasseSonTour = true;
    }

    private Tribu TrouverTribuPlusProche()
    {
        Tribu[] tribus = FindObjectsOfType<Tribu>();
        float[] distances = new float[tribus.Length];

        for (int i = 0; i < tribus.Length; i++)
        {
            Stack<TuileManager> cheminTribu = pathFinder.TrouverCheminPlusCourt(tuileActuelle, tribus[i].tuileActuelle, peutTraverserEau);
            distances[i] = pathFinder.CalculerLongeurChemin(cheminTribu);
        }

        float minim = MiniMax.TrouverMinimum(distances);
        int indexMinim = 0;

        for (int i = 0; i < distances.Length; i++)
        {
            if(distances[i] == minim)
            {
                indexMinim = i;
                break;
            }
        }

        return tribuCible = tribus[indexMinim];
    }

    #endregion

    #region DEPLACEMENTS
    private void TrouverTuileActuelle()
    {
        if (tuileActuelle) tuileActuelle.estOccupee = false;

        LayerMask layerMaskTuile = LayerMask.GetMask("Tuile");

        Collider2D checkTuile = Physics2D.OverlapBox(transform.position, new Vector2(0.1f, 0.1f), 0, layerMaskTuile);

        if (checkTuile)
        {
            tuileActuelle = checkTuile.GetComponent<TuileManager>();
            transform.position = new Vector3(tuileActuelle.transform.position.x, tuileActuelle.transform.position.y, transform.position.z);
        }

        tuileActuelle.estOccupee = true;
    }

    private bool EstArrivePrichaineTuile()
    {
        Vector3 direction = prochaineTuile.transform.position;
        direction.z = transform.position.z;

        if (transform.position != direction)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private bool CheckerFleuve(Vector3 direction)
    {
        LayerMask maskFleuve = LayerMask.GetMask("Fleuve");

        RaycastHit2D checkFleuve = Physics2D.Raycast(transform.position, direction, tuileActuelle.GetComponent<TuileManager>().tailleTuile, maskFleuve);

        if (checkFleuve)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    public IEnumerator SeDeplacer()
    {
        if (ptsDeplacement > 0)
        {
            prochaineTuile = chemin.Pop();

            if (CheckerFleuve(prochaineTuile.transform.position))
            {
                traverseFleuve = true;
            }

            //Tant qu'on est pas arrivé à la prochaine tuile, on avance vers elle
            //simule un Update() avec le WaitForEndOfFrame()
            while (!EstArrivePrichaineTuile())
            {
                SeDeplacerALaProchaineTuile();
                yield return new WaitForEndOfFrame();
            }

            //On diminue les points de deplacements disponibles fonction de si on a traversé un fleuve ou pas
            if (traverseFleuve)
            {
                ptsDeplacement--;
            }
            ptsDeplacement -= tuileActuelle.connectionsDistance[tuileActuelle.RecupIndexConnection(prochaineTuile)];

            //On abandonne le territoire tantôt à portée, 
            //puis on trouve la tuile actuelle, 
            //puis on revendique le territoire maintenant à portée
            revendication.RevendiquerTerritoire(tuileActuelle, false);
            TrouverTuileActuelle();
            revendication.RevendiquerTerritoire(tuileActuelle, true);
        }

        aFaitUneAction = true;
    }

    private void SeDeplacerALaProchaineTuile()
    {
        Vector3 direction = prochaineTuile.transform.position;
        direction.z = transform.position.z;

        transform.position = Vector3.MoveTowards(transform.position, direction, vitesse * Time.deltaTime);
    }
    #endregion
}
