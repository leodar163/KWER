using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;
using UnityEngine.UI;

public class SlotCraft : Slot
{
    public PanelRecette panelRecette;


    // Start is called before the first frame update
    void Start()
    {
        
        InterfaceRessource.Actuel.EventInterfaceMAJ.AddListener(MAJSlots);
        demo = panelRecette.craft.campement.tribu.demographie;
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

    public override void CliquerSurSlot()
    {
        base.CliquerSurSlot();

        panelRecette.AfficherGainRessource();
    }
}
