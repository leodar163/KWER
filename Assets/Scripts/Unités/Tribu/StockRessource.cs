using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StockRessource : MonoBehaviour
{
    [SerializeField] private Tribu tribu;

    public Production capaciteDeStockage;
    public Production ressourcesEnStock;

    private void Awake()
    {
        InstancierProduction();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InstancierProduction()
    {
        Production capacite = ScriptableObject.CreateInstance<Production>();
        if(capaciteDeStockage)
        {
            capacite += capaciteDeStockage;
        }
        capaciteDeStockage = capacite;

        Production stock = ScriptableObject.CreateInstance<Production>();
        if(ressourcesEnStock)
        {
            stock += ressourcesEnStock;
        }
        ressourcesEnStock = stock;
    }
}
