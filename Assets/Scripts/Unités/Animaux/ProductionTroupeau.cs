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
            for (int i = 0; i < troupeau.tuileActuelle.productionTuile.BonusOutil.gains.Length; i++)
            {
                troupeau.tuileActuelle.productionTuile.BonusOutil.gains[i] += bonusOutil.gains[i];
            }
            for (int i = 0; i < troupeau.tuileActuelle.productionTuile.Production.gains.Length; i++)
            {
                troupeau.tuileActuelle.productionTuile.Production.gains[i] += gainProduction.gains[i];
            }
            troupeau.tuileActuelle.productionTuile.nbrSlot += nbrSlot;
        }
    }

    public void ReinitTuile()
    {
        troupeau.tuileActuelle.productionTuile.ReinitProd();
        troupeau.tuileActuelle.productionTuile.ReinitBonusOutil();
        troupeau.tuileActuelle.productionTuile.nbrSlot -= nbrSlot;
    }
}
