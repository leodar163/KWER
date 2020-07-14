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
    Pillard[] pillards;
    int nbrTour;

    [HideInInspector] public UnityEvent eventNouveauTour;

    public bool calendrierMAJ = false;

    private void Start()
    {
        cela = this;
        StartCoroutine(TourJoueur());
    }

    public void JoueurPasseTour()
    {
        Tribu.tribuQuiJoue.aPasseSonTour = true;
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
                tribu.DemarrerTour();
            }
        }

        yield return new AttendreFiniDeJouer<Tribu>();

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

        for (int i = 0; i < animaux.Length; i++)
        {
            animaux[i].DemarrerTour();
            yield return new WaitUntil(() => animaux[i].aPasseSonTour);
        }

        print("Animaux ont fini leur tour");

        StartCoroutine(TourPillards());
    }

    private IEnumerator TourPillards()
    {
        pillards = FindObjectsOfType<Pillard>();

        for (int i = 0; i < pillards.Length; i++)
        {
            pillards[i].DemarrerTour();
            yield return new WaitUntil(() => pillards[i].aPasseSonTour);
        }

        print("Pillards ont fini leur tour");

        StartCoroutine(Evenements());
    }

    private IEnumerator Evenements()
    {
        yield return new WaitForSeconds(0.5f);

        StartCoroutine(TourJoueur());
    }
    #endregion
}
