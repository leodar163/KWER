using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TourParTour : MonoBehaviour
{
    #region SINGLETON
    private static TourParTour cela;
    
    public static TourParTour Defaut
    {
        get
        {
            if (cela == null) cela = FindObjectOfType<TourParTour>();
            return cela;
        }
    }
    #endregion

    Tribu[] tribus;
    Troupeau[] animaux;
    Migration[] tousMigrateurs; //Loups et troupeaux
    int nbrTour;

    public UnityEvent eventNouveauTour;

    public bool calendrierMAJ = false;
    bool hostilesOntAttaque;
    bool passageTour;

    private int nbrJoueurAyantPasseTour = 0;
    private int nbrAnimalAyantPasseTour = 0;

    private void Start()
    {
        cela = this;
        StartCoroutine(TourJoueur());
    }

    public void JoueurPasseTour()
    {
        nbrJoueurAyantPasseTour++;
    }

    public void AnimalPasseTour()
    {
        nbrAnimalAyantPasseTour++;
    }



    #region DEROULEMENT D'UN TOUR
    private IEnumerator TourJoueur()
    {
        tribus = FindObjectsOfType<Tribu>();

        
        if(nbrTour != 0) 
        {
            eventNouveauTour.Invoke();
            ControleSouris.Actuel.controlesActives = true;
            BoutonTourSuivant.Actuel.Activer(true);
            foreach(Tribu tribu in tribus)
            {
                tribu.PasserTour();
            }
        }

        yield return new WaitWhile(() => nbrJoueurAyantPasseTour < tribus.Length);

        nbrJoueurAyantPasseTour = 0;
        BoutonTourSuivant.Actuel.Activer(false);
        ControleSouris.Actuel.controlesActives = false;
        nbrTour++;

        print("Joueurs ont fini leur tour");

        StartCoroutine(TourCalendrier());
    }

    private IEnumerator TourCalendrier()
    {
        Calendrier.Actuel.MiseAJourCalendrier(nbrTour);

        yield return new WaitUntil(() => calendrierMAJ);

        calendrierMAJ = false;

        print("Calendrier à jour");

        StartCoroutine(TourAnimaux());
    }

    private IEnumerator TourAnimaux()
    {
        animaux = FindObjectsOfType<Troupeau>();
        
        foreach(Troupeau animal in animaux)
        {
            animal.DemarrerTour();
        }

        yield return new WaitWhile(() => nbrAnimalAyantPasseTour < animaux.Length);

        nbrAnimalAyantPasseTour = 0;

        print("Animaux ont fini leur tour");

        StartCoroutine(Evenements());
    }

    private IEnumerator Evenements()
    {
        yield return new WaitForSeconds(0.5f);

        StartCoroutine(TourJoueur());
    }
    #endregion
}
