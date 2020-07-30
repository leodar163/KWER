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
        
        InterfaceRessource.Actuel.EventInterfaceMAJ.AddListener(MAJSlot);
        demo = panelRecette.craft.campement.tribu.demographie;
        MAJSlot();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Active ou désactive le slot en fonction de s'il y a suffisament de ressource en stock
    private void MAJSlot()
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
                        if ((stocks.ProjectionGain.gains[i] < 0 && stocks.RessourcesEnStock.gains[i] <= 0) || stocks.consommables.Count >= stocks.emplacementConsommable)
                        {
                            InterdireSlot("<color=#" + ColorUtility.ToHtmlStringRGBA(ListeCouleurs.Defaut.couleurAlerteTexteInterface)
                                        + ">Pas assez de ressource<color=\"white\">");
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
                        if ((stocks.ProjectionGain.gains[i] <= 0 && stocks.RessourcesEnStock.gains[i] <= 0) || stocks.consommables.Count >= stocks.emplacementConsommable)
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
