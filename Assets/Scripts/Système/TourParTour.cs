﻿using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
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
    [SerializeField] public int idTribu = 0;

    [HideInInspector] public UnityEvent eventNouveauTour;

    public bool calendrierMAJ = false;

    private void Start()
    {
        cela = this;
        StartCoroutine(TourJoueur());
    }

    public void JoueurPasseTour()
    {
        Tribu.TribukiJoue.pathFinder.ReinitGraphe();
        Tribu.TribukiJoue.aPasseSonTour = true;
    }


    #region DEROULEMENT D'UN TOUR
    private IEnumerator TourJoueur()
    {
        tribus = Tribu.ListeOrdonneeDesTribus;
        idTribu = 0;
        
        if (nbrTour != 0) 
        {
            eventNouveauTour.Invoke();
            ControleSouris.Actuel.controlesActives = true;
            BoutonTourSuivant.Actuel.Activer(true);
            foreach(Tribu tribu in tribus)
            {
                tribu.DemarrerTour();
            }
        }

        for (int i = 0; i < tribus.Length; i++)
        {
            tribus[i].interactionTribu.EntrerEnInteraction(true);

            for (int j = 0; j < tribus.Length; j++)
            {
                if (tribus[j] != tribus[i]) tribus[j].interactionTribu.ActiverBouton(false);
                else tribus[j].interactionTribu.ActiverBouton(true);
            }

            yield return new WaitUntil(() => tribus[i].aPasseSonTour);

            if(idTribu < tribus.Length -1 )idTribu++;
        }

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
