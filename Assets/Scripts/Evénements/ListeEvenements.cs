using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ListeEvenements : MonoBehaviour
{
    private static ListeEvenements cela;

    public static ListeEvenements Defaut
    {
        get
        {
            if(cela == null)
            {
                cela = FindObjectOfType<ListeEvenements>();
            }

            return cela;
        }
    }

    public List<EvenementTemporel> listeEvenementsTemporels = new List<EvenementTemporel>();


    // Start is called before the first frame update
    void Start()
    {
        TourParTour.Defaut.eventNouveauTour.AddListener(TirerEvenement);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void TirerEvenement()
    {
        float aleaJactata = 100 - Random.Range(0, 100);

        List<EvenementTemporel> evenementsRetenus = new List<EvenementTemporel>();

        foreach (EvenementTemporel evenementTemporel in listeEvenementsTemporels)
        {
            if(Calendrier.Actuel.Hiver == true)
            {
                if(evenementTemporel.probaHiver >= aleaJactata)
                {
                    evenementsRetenus.Add(evenementTemporel);
                }
            }
            else
            {
                if (evenementTemporel.probaEte >= aleaJactata)
                {
                    evenementsRetenus.Add(evenementTemporel);
                }
            }
        }

        if(evenementsRetenus.Count != 0)
        {
            int aleumJactatum = Random.Range(0, evenementsRetenus.Count -1);
            evenementsRetenus[aleumJactatum].LancerEvenement();
        }
    }
}
