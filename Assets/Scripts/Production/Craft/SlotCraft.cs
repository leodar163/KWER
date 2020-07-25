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
                for (int i = 0; i < panelRecette.Recette.inputParPop.gains.Length; i++)
                {
                    if (panelRecette.Recette.inputParPop.gains[i] > 0)
                    {
                        if (stocks.ProjectionGain.gains[i] < 0 && stocks.RessourcesEnStock.gains[i] < 0)
                        {
                            CliquerSurSlot();
                            InterdireSlot("Pas assez de ressource");
                            MAJSlots();
                            return;
                        }
                    }
                }
                AutoriserSlot();
            }
            else
            {
                for (int i = 0; i < panelRecette.Recette.inputParPop.gains.Length; i++)
                {
                    if (panelRecette.Recette.inputParPop.gains[i] > 0)
                    {
                        if (stocks.ProjectionGain.gains[i] <= 0 && stocks.RessourcesEnStock.gains[i] <= 0)
                        {
                            InterdireSlot("<color=#" + ColorUtility.ToHtmlStringRGBA(ListeCouleurs.Defaut.couleurAlerteTexteInterface)
                                        + ">Pas assez de ressource<color=\"white\">");
                            return;
                        }
                    }
                }
                AutoriserSlot();
            }
        }
    }

    public override void CliquerSurSlot()
    {
        base.CliquerSurSlot();

        panelRecette.AfficherGainRessource();
    }
}
