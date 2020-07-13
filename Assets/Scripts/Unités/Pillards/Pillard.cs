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
    public float ptsDeplacement;
    public float vitesse;

    private TuileManager prochaineTuile;
    private Stack<TuileManager> chemin;
    private bool traverseFleuve;

    // Start is called before the first frame update
    void Start()
    {
        TrouverTuileActuelle();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


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
            //puis on revendique le territoire maintenant à porté
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
