using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Migration : MonoBehaviour
{
    [SerializeField] private Troupeau troupeau;

    TuileManager prochaineTuile = null;
    List<TuileManager> tuilesParcourues;
    bool traverseFleuve = false;

    public bool PeutBouger
    {
        get
        {
            if (troupeau.ptsDeplacement > 0 && Calendrier.Actuel.Hiver) return true;
            else return false;
        }
    }

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

        Calendrier.Actuel.EventChangementDeSaison.AddListener(TerminerMigration);
    }

    // Update is called once per frame
    void Update()
    {

    }

    


    public void InitialiserPointsDeplacement()
    {
        troupeau.ptsDeplacement = troupeau.ptsDeplacementDefaut;
        tuilesParcourues.Add(troupeau.tuileActuelle);
    }

    //Boucle de déplacement
    public IEnumerator Migrer()
    {
        if (troupeau.ptsDeplacement > 0)
        {
            tuilesParcourues.Add(troupeau.tuileActuelle);
            prochaineTuile = ChoisirProchaineTuile();

            if (CheckerFleuve())
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
            if(traverseFleuve)
            {
                troupeau.ptsDeplacement--;
            }
            troupeau.ptsDeplacement--;

            //On abandonne le territoire tantôt à portée, 
            //puis on trouve la tuile actuelle, 
            //puis on revendique le territoire maintenant à porté
            troupeau.revendication.RevendiquerTerritoire(troupeau.tuileActuelle, false);
            troupeau.revendication.RevendiquerTerritoire(troupeau.tuileActuelle, true);
            troupeau.TrouverTuileActuelle();
            tuilesParcourues.Add(troupeau.tuileActuelle);
        }

        troupeau.aFaitUneAction = true;
    }

    private void TerminerMigration()
    {
        tuilesParcourues.Clear();
    }

    private bool CheckerFleuve()
    {
        LayerMask maskFleuve = LayerMask.GetMask("Fleuve");

        RaycastHit2D checkFleuve = Physics2D.Raycast(transform.position, prochaineTuile.transform.position, troupeau.tuileActuelle.GetComponent<TuileManager>().tailleTuile, maskFleuve);

        if (checkFleuve)
        {
            return true;
        }
        else
        {
            return false;
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

        foreach (TuileManager tuile in troupeau.tuileActuelle.connections)
        {
            if (tuile && tuile.estOccupee == false)
            {
                if (!tuilesParcourues.Contains(tuile))
                {
                    if(!troupeau.predateur)
                    {
                        if (tuile.terrainTuile.nom == "Plaine")
                        {
                            directionPossibles.Add(tuile);
                        }
                    }
                    else
                    {
                        directionPossibles.Add(tuile);
                    }
                }
            }
        }

        if (directionPossibles.Count == 0)
        {
            foreach (TuileManager tuile in troupeau.tuileActuelle.connections)
            {
                if (tuile && tuile.estOccupee == false)
                {
                    if(!troupeau.predateur)
                    {
                        if (tuile.terrainTuile.nom == "Plaine")
                        {
                            directionPossibles.Add(tuile);
                        }
                    }
                    else
                    {
                        directionPossibles.Add(tuile);
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

        transform.position = Vector3.MoveTowards(transform.position, direction, troupeau.vitesse * Time.deltaTime);
    }
}
