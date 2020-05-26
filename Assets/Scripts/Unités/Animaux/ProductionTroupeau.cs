using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductionTroupeau : MonoBehaviour
{
    public int nbrSlot;
    public Production gainProduction;

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
        troupeau.migration.tuileActuelle.productionTuile.production += gainProduction;
        troupeau.migration.tuileActuelle.productionTuile.nbrSlot += nbrSlot;
    }

    public void ReinitTuile()
    {
        troupeau.migration.tuileActuelle.productionTuile.production -= gainProduction;
        troupeau.migration.tuileActuelle.productionTuile.nbrSlot -= nbrSlot;
    }
}
