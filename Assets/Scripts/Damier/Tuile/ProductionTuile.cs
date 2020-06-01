using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.LowLevel;

public class ProductionTuile : MonoBehaviour
{
    [SerializeField] private TuileManager tuile;
    [HideInInspector] public Production production;
    [HideInInspector] public Production bonusOutil;
    
    public int nbrSlot;

    private void Awake()
    {
        production = ScriptableObject.CreateInstance<Production>();
        production.gains = (float[])tuile.terrainTuile.production.gains.Clone();

        bonusOutil = ScriptableObject.CreateInstance<Production>();
        bonusOutil.gains = (float[])tuile.terrainTuile.bonusOutil.gains.Clone();

    }
    // Start is called before the first frame update
    void Start()
    {
        
        
        
        nbrSlot = tuile.terrainTuile.nbrSlot;
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    public void ReinitBonusOutil()
    {
        bonusOutil.gains = (float[])tuile.terrainTuile.bonusOutil.gains.Clone();
    }
            

    public void ReinitProd()
    {
        production.gains = (float[])tuile.terrainTuile.production.gains.Clone();
    }
}
