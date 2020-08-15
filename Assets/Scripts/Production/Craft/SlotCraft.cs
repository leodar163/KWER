using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;
using UnityEngine.UI;

public class SlotCraft : Slot
{
    public PanelRecette panelRecette;
    private StockRessource stocks;

    // Start is called before the first frame update
    void Start()
    {
        stocks = panelRecette.craft.campement.tribu.stockRessources;
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
        
        bool manqueRessource = false;
        string texteRetour = "";
        if(panelRecette.Recette)
        {   
            if (estOccupe)
            {
                for (int i = 0; i < panelRecette.Recette.inputParPop.gains.Length; i++)
                {
                    //écrit le coût dans l'infobulle
                    if (panelRecette.Recette.inputParPop.gains[i] > 0)
                    {
                        texteRetour += "\n <color=#" + ColorUtility.ToHtmlStringRGBA(ListeCouleurs.Defaut.couleurAlerteTexteInterface) + ">-"
                            + panelRecette.Recette.inputParPop.gains[i] + "<color=\"white\"> " + ListeRessources.Defaut.listeDesRessources[i].nom;


                        if (stocks.ProjectionGain.gains[i] < 0
                            && stocks.RessourcesEnStock.gains[i] < panelRecette.Recette.inputParPop.gains[i])
                        {
                            manqueRessource = true;
                            texteRetour += "<color=#" + ColorUtility.ToHtmlStringRGBA(ListeCouleurs.Defaut.couleurAlerteTexteInterface) + "> (insuffisant)";
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < panelRecette.Recette.inputParPop.gains.Length; i++)
                {
                    //écrit le coût dans l'infobulle
                    if (panelRecette.Recette.inputParPop.gains[i] > 0)
                    {
                        texteRetour += "\n <color=#" + ColorUtility.ToHtmlStringRGBA(ListeCouleurs.Defaut.couleurAlerteTexteInterface) + ">-"
                            + panelRecette.Recette.inputParPop.gains[i] + "<color=\"white\"> " + ListeRessources.Defaut.listeDesRessources[i].nom;
                        if (stocks.ProjectionGain.gains[i] < panelRecette.Recette.inputParPop.gains[i]
                            && stocks.RessourcesEnStock.gains[i] < panelRecette.Recette.inputParPop.gains[i])
                        {
                            manqueRessource = true;
                            texteRetour += "<color=#" + ColorUtility.ToHtmlStringRGBA(ListeCouleurs.Defaut.couleurAlerteTexteInterface) + "> (insuffisant)";
                        }
                    }
                }
            }
            if (manqueRessource) InterdireSlot("<color=#" + ColorUtility.ToHtmlStringRGBA(ListeCouleurs.Defaut.couleurAlerteTexteInterface)
                    + ">Pas assez de ressource" + texteRetour);
            else 
            {
                if(pop)
                {
                    AutoriserSlot("Cliquez pour retirer la population assignée" + texteRetour);
                }
                else
                {
                    AutoriserSlot(texteInfobulleDefaut + texteRetour);
                }
            }
               

            if (panelRecette.Recette.consommable && stocks.consommables.Count >= stocks.emplacementConsommable)
                InterdireSlot("<color=#" + ColorUtility.ToHtmlStringRGBA(ListeCouleurs.Defaut.couleurAlerteTexteInterface)
                                        + ">Pas d'emplacement de consommable disponible<color=\"white\">" + texteRetour);

        }

    }

    public override void CliquerSurSlot()
    {
        base.CliquerSurSlot();

        if(pop)
        {
            stocks.RessourcesEnStock -= panelRecette.Recette.inputParPop;
        }
        else
        {
            stocks.RessourcesEnStock += panelRecette.Recette.inputParPop;
        }

        panelRecette.AfficherGainRessource();
    }
}
