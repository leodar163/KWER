using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.LowLevel;

public class ProductionTuile : MonoBehaviour
{
    [SerializeField] private TuileManager tuile;

    private Production production;
    private Production bonusOutil;

    /// <summary>
    /// Ne pas assigner de valeur à aucune des propriétés de cet assesseur
    /// </summary>
    [HideInInspector] public Production ProductionTotale
    {
        get
        {
            if (production == null) InstancierProduction();
            return production + tuile.tuileAmenagement.gainAmenagement;
        }
    }
    [HideInInspector] public Production BonusOutil
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

        production = ScriptableObject.CreateInstance<Production>();
        production.gains = (float[])tuile.terrainTuile.production.gains.Clone();

        nbrSlot = tuile.terrainTuile.nbrSlot;
    }

    public void ReinitBonusOutil()
    {
        bonusOutil.gains = (float[])tuile.terrainTuile.bonusOutil.gains.Clone();
    }
            

    public void ReinitProd()
    {
        ProductionTotale.gains = (float[])tuile.terrainTuile.production.gains.Clone();
    }

    public void AjouterProduction(Production prod)
    {
        for (int i = 0; i < production.gains.Length; i++)
        {
            production.gains[i] += prod.gains[i];
        }
    }

    public void AjouterBonusOutil(Production prod)
    {
        for (int i = 0; i < bonusOutil.gains.Length; i++)
        {
            bonusOutil.gains[i] += prod.gains[i];
        }
    }
}
