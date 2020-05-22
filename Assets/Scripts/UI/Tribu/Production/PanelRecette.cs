using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PanelRecette : MonoBehaviour
{
    public Craft craft;
    public TextMeshProUGUI nomRecette;
    public PanelGainRessources panelRessource;
    private Recette recette;
    int nbrslot = 0 ;

    public Recette Recette
    {
        set
        {
            recette = value;
            nomRecette.text = recette.nom;
            nbrslot = recette.slots;
        }
        get
        {
            return recette;
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void RecentrerSLots()
    {

    }

}
