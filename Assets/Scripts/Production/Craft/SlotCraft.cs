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
    private Button bouton;

    public bool estOccupe
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

    private void OnValidate()
    {
        MiseAJourIconePop();
    }

    // Start is called before the first frame update
    void Start()
    {
        bouton = GetComponent<Button>();
        InterfaceRessource.Actuel.EventInterfaceMAJ.AddListener(MAJSlots);
        demo = panelRecette.craft.campement.tribu.demographie;
        MiseAJourIconePop();
        iconePop.gameObject.SetActive(false);
        MAJSlots();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Active ou désactive le slot en fonction de s'il y a suffisament de ressource en stock
    private void MAJSlots()
    {
        StockRessource stocks = panelRecette.craft.campement.tribu.stockRessources;

        if(panelRecette.Recette)
        {
            if (estOccupe)
            {
                for (int i = 0; i < panelRecette.Recette.cout.gains.Length; i++)
                {
                    if (panelRecette.Recette.cout.gains[i] > 0)
                    {
                        if (stocks.ProjectionGain.gains[i] < 0 && stocks.RessourcesEnStock.gains[i] < 0)
                        {
                            CliquerSurSlot();
                            bouton.interactable = false;
                            MAJSlots();
                            return;
                        }
                    }
                }
                bouton.interactable = true;
            }
            else
            {
                for (int i = 0; i < panelRecette.Recette.cout.gains.Length; i++)
                {
                    if (panelRecette.Recette.cout.gains[i] > 0)
                    {
                        if (stocks.ProjectionGain.gains[i] <= 0 && stocks.RessourcesEnStock.gains[i] <= 0)
                        {
                            bouton.interactable = false;
                            return;
                        }
                    }
                }
                bouton.interactable = true;
            }
        }
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
