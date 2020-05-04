using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;

public class Calendrier : MonoBehaviour
{
    TextMeshProUGUI compteur;
    [SerializeField] int dureeSaison;
    public bool hiver = true;
    int decompte;
    RectTransform roueCalendrier;
    TuileManager[] damier;
    TuileTerrain[] terrains;
    TourParTour tourParTour;

    // Start is called before the first frame update
    void Start()
    {
        damier = FindObjectsOfType<TuileManager>();
        compteur = GetComponentInChildren<TextMeshProUGUI>();
        terrains = FindObjectsOfType<TuileTerrain>();
        tourParTour = FindObjectOfType<TourParTour>();

        RecupererRoueCalendrier();

        MiseAJourCalendrier(0);
    }

    public void MiseAJourCalendrier(int nbrTour)
    {
        MiseAJourCompteurCalendrier(nbrTour);
        MiseAJourRoueCalendrier();
    }

    private void MiseAJourCompteurCalendrier(int nbrTour)
    {
        decompte = dureeSaison - nbrTour % dureeSaison;

        compteur.text = decompte.ToString();

        if (decompte == 4)
        {
            hiver = !hiver;
            ChangementSaison(hiver);
            //print(hiver);
        }
        else
        {
            tourParTour.calendrierMAJ = true;
        }
    }

    private void MiseAJourRoueCalendrier()
    {
        roueCalendrier.Rotate(0, 0, 180/dureeSaison);
    }
    
    private void RecupererRoueCalendrier()
    {
        RectTransform[] listeEnfants = GetComponentsInChildren<RectTransform>();

        foreach(RectTransform enfant in listeEnfants)
        {
            if(enfant.gameObject.name == "RoueCalendrier")
            {
                roueCalendrier = enfant;
                break;
            }
        }
    }

    private void ChangementSaison(bool hiver)
    {
        if(hiver)
        {
            for (int i = 0; i < terrains.Length; i++)
            {
                if(terrains[i].nom == "Foret")
                {
                    terrains[i].nourriture = 0;
                }
            }
        }
        else
        {
            for (int i = 0; i < terrains.Length; i++)
            {
                if (terrains[i].nom == "Foret")
                {
                    terrains[i].nourriture = 1;
                }
            }
        }

        for (int i = 0; i < damier.Length; i++)
        {
            damier[i].RevetirManteauHiver(hiver);
            if(i == damier.Length - 1)
            {
                tourParTour.calendrierMAJ = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
