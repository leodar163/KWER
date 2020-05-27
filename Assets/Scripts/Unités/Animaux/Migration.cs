using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Migration : MonoBehaviour
{
    [SerializeField] private Troupeau troupeau;

    public TuileManager tuileActuelle;
    [SerializeField] float vitesse;
    [SerializeField] float ptsActionDefaut = 2;
    float ptsAction;
    TuileManager prochaineTuile = null;
    List<TuileManager> tuilesParcourues;
    bool traverseFleuve = false;
    public bool cantonnerAuxPlaines;

    private void Awake()
    {
        
    }

    void Start()
    {
        tuilesParcourues = new List<TuileManager>();

        if (troupeau == null)
        {
            troupeau = GetComponent<Troupeau>();
        }
        Invoke("TrouverTuileActuelle",0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        SeDeplacer();
    }

    private void TrouverTuileActuelle()
    {
        LayerMask layerMaskTuile = LayerMask.GetMask("Tuile");

        Collider2D checkTuile = Physics2D.OverlapBox(transform.position, new Vector2(0.1f, 0.1f), 0, layerMaskTuile);

        if (checkTuile)
        {
            tuileActuelle = checkTuile.gameObject.GetComponent<TuileManager>();
            transform.position = new Vector3(tuileActuelle.transform.position.x, tuileActuelle.transform.position.y, transform.position.z);
        }

        troupeau.productionTroupeau.FertiliserTuile();
    }


    public void Migrer()
    {
        ptsAction = ptsActionDefaut;
        tuilesParcourues.Add(tuileActuelle);
    }

    public void FinirMigration()
    {
        if (tuilesParcourues != null || tuilesParcourues.Count != 0)
        {
            tuilesParcourues.Clear();
        }
        ptsAction = 0;
    }

    private bool CheckerFleuve()
    {

        LayerMask maskFleuve = LayerMask.GetMask("Fleuve");

        RaycastHit2D checkFleuve = Physics2D.Raycast(transform.position, prochaineTuile.transform.position, tuileActuelle.GetComponent<TuileManager>().tailleTuile, maskFleuve);

        if (checkFleuve)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    private void SeDeplacer()
    {
        if (ptsAction > 0)
        {
            if (prochaineTuile == null)
            {
                

                prochaineTuile = ChoisirProchaineTuile();

                if (CheckerFleuve())
                {
                    traverseFleuve = true;
                }
            }

            if (!EstArrivePrichaineTuile())
            {

                SeDeplacerALaProchaineTuile();
            }
            else
            {

                if (traverseFleuve)
                {
                    ptsAction--;
                    traverseFleuve = false;
                }

                TrouverTuileActuelle();
                tuilesParcourues.Add(tuileActuelle);

                prochaineTuile = null;

                ptsAction--;
            }
        }
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

    private TuileManager ChoisirProchaineTuile()
    {
        TuileManager tuileCible;

        List<TuileManager> directionPossibles = new List<TuileManager>();

        foreach (TuileManager tuile in tuileActuelle.connections)
        {
            if (tuile)
            {
                TuileManager tMCible = tuile;
                if (!tuilesParcourues.Contains(tMCible))
                {
                    if(troupeau)
                    {
                        if (tMCible.terrainTuile.nom == "Plaine")
                        {
                            directionPossibles.Add(tMCible);
                        }
                    }
                    else
                    {
                        directionPossibles.Add(tMCible);
                    }
                    
                }
            }
        }
        if (directionPossibles.Count == 0)
        {
            foreach (TuileManager tuile in tuileActuelle.connections)
            {
                if (tuile)
                {
                    TuileManager tMCible = tuile;

                    if (tMCible.terrainTuile.nom == "Plaine")
                    {
                        directionPossibles.Add(tMCible);
                    }
                }
            }
        }
        int choixTuile = Random.Range(0, directionPossibles.Count);

        tuileCible = directionPossibles[choixTuile];

        troupeau.productionTroupeau.ReinitTuile();

        return tuileCible;
    }

    private void SeDeplacerALaProchaineTuile()
    {
        Vector3 direction = prochaineTuile.transform.position;
        direction.z = transform.position.z;

        transform.position = Vector3.MoveTowards(transform.position, direction, vitesse * Time.deltaTime);
    }
}
