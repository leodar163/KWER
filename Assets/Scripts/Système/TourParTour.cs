using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TourParTour : MonoBehaviour
{
    ControleSouris controles;

    UniteManager[] toutesUnites;
    Migration[] tousMigrateurs; //Loups et troupeaux
    Hostile[] tousHostiles;
    int nbrTour;
    InterfaceNourriture interfaceNourriture;
    InterfacePopulation interfacePopulation;
    InterfaceCroissance interfaceCroissance;
    Calendrier calendrier;
    public bool calendrierMAJ;
    bool hostilesOntAttaque;
    bool saisonChangee;
    bool passageTour;

    private void Start()
    {
        

        controles = FindObjectOfType<ControleSouris>();
        calendrier = FindObjectOfType<Calendrier>();
        interfaceNourriture = FindObjectOfType<InterfaceNourriture>();
        interfaceCroissance = FindObjectOfType<InterfaceCroissance>();
        interfacePopulation = FindObjectOfType<InterfacePopulation>();
    }

    public void PasserTour()
    {
        if(!passageTour)
        {
            passageTour = true;
            nbrTour++;

            StartCoroutine(PasserLeTour());

        }
    }

    private IEnumerator PasserLeTour()
    {
        ActiverControles(false);

        StartCoroutine(AttaquesHostiles());
        yield return new WaitUntil(() => hostilesOntAttaque);
        StartCoroutine(ChangementSaison());
        yield return new WaitUntil(() => saisonChangee);
        //Fin du tour

        ActiverControles(true);
        passageTour = false;
    }

    private IEnumerator AttaquesHostiles()
    {
        tousHostiles = FindObjectsOfType<Hostile>();
        bool tousHostilesOntAttaque = false;

        while(!tousHostilesOntAttaque)
        {
            for (int i = 0; i < tousHostiles.Length; i++)
            {
                if (tousHostiles[i].peutAttaquer)
                {
                    tousHostilesOntAttaque = false;
                    break;
                }
                tousHostilesOntAttaque = true;
            }
            yield return new WaitForEndOfFrame();
        }

        hostilesOntAttaque = true;
    }

    private IEnumerator ChangementSaison()
    {
        calendrier.MiseAJourCalendrier(nbrTour);

        if (!calendrierMAJ)
        {
            yield return new WaitForEndOfFrame();
        }

        toutesUnites = FindObjectsOfType<UniteManager>();
        tousMigrateurs = FindObjectsOfType<Migration>();
        

        foreach (UniteManager unite in toutesUnites)
        {
            unite.PasserTour();
        }
        if(calendrier.hiver)
        {
            foreach (Migration migrateur in tousMigrateurs)
            {
                migrateur.Migrer();
            }
        }
        else if (!calendrier.hiver)
        {
            foreach (Migration migrateur in tousMigrateurs)
            {
                migrateur.FinirMigration();
            }
        }
        calendrierMAJ = false;
        saisonChangee = true;
    }

    private void ActiverControles(bool activer)
    {
        controles.controlesActives = activer;
    }
}
