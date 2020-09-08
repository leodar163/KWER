using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductionTroupeau : MonoBehaviour
{
    public int nbrSlot;
    public Production gainProduction;
    public Production bonusOutil;

    public Troupeau troupeau;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FertiliserTuile()
    {
        if(troupeau.domesticable)
        {
            troupeau.tuileActuelle.productionTuile.AjouterBonusOutil(bonusOutil);
            troupeau.tuileActuelle.productionTuile.AjouterProduction(gainProduction);
            troupeau.tuileActuelle.productionTuile.AjouterSlots(nbrSlot);
        }
    }

    public void ReinitTuile()
    {
        troupeau.tuileActuelle.productionTuile.ReinitProd();
        troupeau.tuileActuelle.productionTuile.ReinitBonusOutil();
        troupeau.tuileActuelle.productionTuile.AjouterSlots(-nbrSlot);
    }
}
