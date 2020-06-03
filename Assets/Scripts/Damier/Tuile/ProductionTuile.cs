using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.LowLevel;

public class ProductionTuile : MonoBehaviour
{
    [SerializeField] private TuileManager tuile;

    private Production production;
    private Production bonusOutil;

    [HideInInspector] public Production Production
    {
        get
        {
            if (production == null) InstancierProduction();
            return production;
        }
        set
        {
            production = value;
        }
    }
    [HideInInspector] public Production BonusOutil
    {
        get
        {
            if (production == null) InstancierProduction();
            return production;
        }
        set
        {
            production = value;
        }
    }
    
    public int nbrSlot;

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
        Production.gains = (float[])tuile.terrainTuile.production.gains.Clone();
    }
}
