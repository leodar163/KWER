using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.LowLevel;

public class ProductionTuile : MonoBehaviour
{
    [SerializeField] private TuileManager tuile;
    [HideInInspector] public Production production;
    
    public int nbrSlot;

    private void Awake()
    {
        production = ScriptableObject.CreateInstance<Production>();
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
        production.gains = tuile.terrainTuile.production.gains;
        
        nbrSlot = tuile.terrainTuile.nbrSlot;
    }

    // Update is called once per frame
    void Update()
    {
    }
    
    public void ReinitProd()
    {
        production.gains = tuile.terrainTuile.production.gains;
    }
}
