using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListeEvenementCombat : MonoBehaviour
{

    private static ListeEvenementCombat cela;

    public static ListeEvenementCombat Defaut
    {
        get
        {
            if(cela == null)
            {
                cela = FindObjectOfType<ListeEvenementCombat>();
            }
            return cela;
        }
    }

    [SerializeField] private List<EvenementCombat> strategiesCommunes = new List<EvenementCombat>();
    [Space]
    [SerializeField] private List<EvenementCombat> strategiesPillards = new List<EvenementCombat>();
    [Space]
    [SerializeField] private List<EvenementCombat> strategiesPredateurs = new List<EvenementCombat>();
    [Space]
    [SerializeField] private List<EvenementCombat> strategiesMegaFaune = new List<EvenementCombat>();


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public EvenementCombat PiocherEvenement(Combat combat)
    {
        List<EvenementCombat> listeAPiocher = new List<EvenementCombat>(strategiesCommunes);
        if(combat.Hostile.pion is Troupeau)
        {
            Troupeau troupeau = (Troupeau)combat.Hostile.pion;
            if (troupeau.megaFaune)
            {
                for (int i = 0; i < strategiesMegaFaune.Count; i++)
                {
                    listeAPiocher.Add(strategiesMegaFaune[i]);
                } 
            }
            else if (troupeau.predateur)
            {
                for (int i = 0; i < strategiesPredateurs.Count; i++)
                {
                    listeAPiocher.Add(strategiesPredateurs[i]);
                }
            }
        }
        else if(combat.Hostile.pion is Pillard)
        {
            for (int i = 0; i < strategiesPillards.Count; i++)
            {
                listeAPiocher.Add(strategiesPillards[i]);
            }
        }

        return listeAPiocher[Random.Range(0, listeAPiocher.Count - 1)];
    }
}
