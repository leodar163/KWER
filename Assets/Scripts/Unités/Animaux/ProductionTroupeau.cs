﻿using System.Collections;
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
            for (int i = 0; i < troupeau.migration.tuileActuelle.productionTuile.BonusOutil.gains.Length; i++)
            {
                troupeau.migration.tuileActuelle.productionTuile.BonusOutil.gains[i] += bonusOutil.gains[i];
            }
            for (int i = 0; i < troupeau.migration.tuileActuelle.productionTuile.Production.gains.Length; i++)
            {
                troupeau.migration.tuileActuelle.productionTuile.Production.gains[i] += gainProduction.gains[i];
            }
            troupeau.migration.tuileActuelle.productionTuile.nbrSlot += nbrSlot;
        }
    }

    public void ReinitTuile()
    {
        troupeau.migration.tuileActuelle.productionTuile.ReinitProd();
        troupeau.migration.tuileActuelle.productionTuile.ReinitBonusOutil();
        troupeau.migration.tuileActuelle.productionTuile.nbrSlot -= nbrSlot;
    }
}
