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

    [SerializeField] private List<EvenementCombat> strategiesContrePillard = new List<EvenementCombat>();
    [SerializeField] private List<EvenementCombat> strategiesContrePredateurs = new List<EvenementCombat>();
    [SerializeField] private List<EvenementCombat> strategiesContreMegaFaune = new List<EvenementCombat>();


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
        if(combat.Hostile.pion is Troupeau)
        {
            Troupeau troupeau = (Troupeau)combat.Hostile.pion;
            if (troupeau.megaFaune)
            {
                return strategiesContreMegaFaune[Random.Range(0, strategiesContreMegaFaune.Count - 1)];
            }
            else if (troupeau.predateur)
            {
                return strategiesContrePredateurs[Random.Range(0, strategiesContrePredateurs.Count - 1)];
            }
        }
        else if(combat.Hostile.pion is Pillard)
        {
            print("J'ai pas encore implémenter les pillard...");
            return null;
        }

        return null;
    }
}
