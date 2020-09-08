using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.LowLevel;

public class ProductionTuile : MonoBehaviour
{
    [SerializeField] private TuileManager tuile;

    private Production productionEte;
    private Production productionHiver;
    private Production bonusOutil;

    /// <summary>
    /// Ne pas assigner de valeur à aucune des propriétés de cet assesseur
    /// </summary>
    public Production ProductionTotale
    {
        get
        {
            if (productionEte == null ||productionHiver == null) InstancierProduction();
            if (Calendrier.Actuel && Calendrier.Actuel.Hiver) return productionHiver + tuile.tuileAmenagement.gainAmenagement;
            else return productionEte + tuile.tuileAmenagement.gainAmenagement;
        }
    }
    public Production BonusOutil
    {
        get
        {
            if (bonusOutil == null) InstancierProduction();
            return bonusOutil;
        }
    }

    private int nbrSlot;
    public int NbrSlot
    {
        get
        {
            return nbrSlot + tuile.tuileAmenagement.slotsAmenagement;
        }
    }

    private void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   

    }
    

    public void AjouterSlots(int nombre)
    {
        nbrSlot += nombre;
    }

    //Fait office d'initialisateur
    private void InstancierProduction()
    {
        bonusOutil = ScriptableObject.CreateInstance<Production>();
        bonusOutil.gains = (float[])tuile.terrainTuile.bonusOutil.gains.Clone();

        productionEte = ScriptableObject.CreateInstance<Production>();
        productionEte.gains = (float[])tuile.terrainTuile.productionEte.gains.Clone();

        productionHiver = ScriptableObject.CreateInstance<Production>();
        productionHiver.gains = (float[])tuile.terrainTuile.productionHiver.gains.Clone();

        nbrSlot = tuile.terrainTuile.nbrSlot;
    }

    public void ReinitBonusOutil()
    {
        bonusOutil.gains = (float[])tuile.terrainTuile.bonusOutil.gains.Clone();
    }
            

    public void ReinitProd()
    {
        if (productionEte == null || productionHiver == null) InstancierProduction();
        productionEte.gains = (float[])tuile.terrainTuile.productionEte.gains.Clone();
        productionHiver.gains = (float[])tuile.terrainTuile.productionHiver.gains.Clone();
    }

    public void AjouterProduction(Production prod)
    {
        for (int i = 0; i < prod.gains.Length; i++)
        {
            productionEte.gains[i] += prod.gains[i];
            productionHiver.gains[i] += prod.gains[i];
        }
    }

    public void AjouterBonusOutil(Production prod)
    {
        for (int i = 0; i < BonusOutil.gains.Length; i++)
        {
            bonusOutil.gains[i] += prod.gains[i];
        }
    }
}
