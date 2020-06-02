using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;
using UnityEngine.UI;

public class SlotCraft : MonoBehaviour
{
    public PanelRecette panelRecette;
    public Pop pop;
    private Demographie demo;
    [SerializeField] private Image iconePop;
    public bool estActif
    {
        get
        {
            if (estOccupe && aAssezRessource)
            {
                return true;
            }
            else return false;
        }
    }

    private bool estOccupe
    {
        get
        {
            if (pop == null)
            {
                return false;
            }
            else return true;
        }
    }

    public bool aAssezRessource
    {
        get
        {
            if (RecupRessourcesInsuffisantes().Count > 0)
            {
                return false;
            }
            else return true;
        }
    }

    private void OnValidate()
    {
        MiseAJourIconePop();
    }

    // Start is called before the first frame update
    void Start()
    {
        demo = panelRecette.craft.campement.tribu.demographie;
        MiseAJourIconePop();
        iconePop.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public List<Ressource> RecupRessourcesInsuffisantes()
    {
        List<Ressource> ressourcesInsuffisantes = new List<Ressource>();
        StockRessource stocks = panelRecette.craft.campement.tribu.stockRessources;

        if (ListeRessources.Defaut)
        {
            for (int i = 0; i < panelRecette.Recette.cout.gains.Length; i++)
            {
                if (panelRecette.Recette.cout.gains[i] > 0)
                {
                    if (stocks.ProjectionGain.gains[i] > 0 || stocks.RessourcesEnStock.gains[i] > 0)
                    {
                        ressourcesInsuffisantes.Add(ListeRessources.Defaut.listeDesRessources[i]);
                    }
                }
            }
        }

        return ressourcesInsuffisantes;
    }

    public void CliquerSurSlot()
    {
        if(pop)
        {
            pop.gameObject.SetActive(true);
            demo.AjouterPop(pop);
            pop = null;
            iconePop.gameObject.SetActive(false);
        }
        else if(demo.listePopsCampement.Count > 0)
        {
            pop = demo.RetirerPop(false);
            pop.gameObject.SetActive(false);
            iconePop.gameObject.SetActive(true);
        }

        panelRecette.AfficherGainRessource();
    }

    private void MiseAJourIconePop()
    {
        if (iconePop.sprite != ListeIcones.Defaut.iconePopulation)
        {
            iconePop.sprite = ListeIcones.Defaut.iconePopulation;
        }
    }
}
