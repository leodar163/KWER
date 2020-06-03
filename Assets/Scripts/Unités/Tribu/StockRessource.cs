using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StockRessource : MonoBehaviour
{
    [SerializeField] private Tribu tribu;
    [SerializeField] private Production capaciteDeStockage;
    [SerializeField] private Production ressourcesEnStock;
    
    private Production projectionGain;
    private List<Production> gaineurs = new List<Production>();

    [Header("Population")]
    [SerializeField] private Production consoParPop;
    [SerializeField] private Production consoParPopHiver;
    [SerializeField] private Production capaciteParPop;

    public Production CapaciteDeStockage
    {
        set
        {
            capaciteDeStockage = value;
            MiseAJourInterfaceRessource();
        }
        get
        {
            return capaciteDeStockage;
        }
    }
    public Production RessourcesEnStock
    {
        set
        {
            ressourcesEnStock = value;
            MiseAJourInterfaceRessource();
        }
        get
        {
            return ressourcesEnStock;
        }
    }

    public Production ProjectionGain
    {
        get
        {
            return projectionGain;
        }
    }

    private void Awake()
    {
        InstancierProduction();
    }
    // Start is called before the first frame update
    void Start()
    {
        LimiterStock();
        Invoke("MiseAJourInterfaceRessource",0.5f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void InstancierProduction()
    {
        Production capacite = ScriptableObject.CreateInstance<Production>();
        if (capaciteDeStockage)
        {
            capacite.gains = (float[])capaciteDeStockage.gains.Clone();
        }
        capaciteDeStockage = capacite;

        Production stock = ScriptableObject.CreateInstance<Production>();
        if (ressourcesEnStock)
        {
            stock.gains = (float[])ressourcesEnStock.gains.Clone();
        }
        ressourcesEnStock = stock;

        projectionGain = ScriptableObject.CreateInstance<Production>();
        projectionGain.gains = new float[ListeRessources.Defaut.listeDesRessources.Length];
    }

    public void AjouterGain(Production gain)
    {
        if(!gaineurs.Contains(gain))
        {
            gaineurs.Add(gain);
        }
        CalculerGain();
    }

    public void RetirerGain(Production gain)
    {
        if (gaineurs.Contains(gain))
        {
            gaineurs.Remove(gain);
        }
        CalculerGain();
    }

    public void CalculerGain()
    {
        projectionGain.Clear();
        foreach (Production gain in gaineurs)
        {   
            projectionGain += gain;
        }
        if(Calendrier.Actuel)
        {
            if(Calendrier.Actuel.Hiver)
            {
                projectionGain += consoParPopHiver * tribu.demographie.taillePopulation;
            }
            else projectionGain += consoParPop * tribu.demographie.taillePopulation;
        }

        MiseAJourInterfaceRessource();
    }

    private void LimiterGain()
    {
        for (int i = 0; i < projectionGain.gains.Length; i++)
        {
            if(projectionGain.gains[i] > (capaciteDeStockage.gains[i] - ressourcesEnStock.gains[i]))
            {
                projectionGain.gains[i] = (capaciteDeStockage.gains[i] - ressourcesEnStock.gains[i]);
            }
        }
    }

    private void LimiterStock()
    {
        for (int i = 0; i < ressourcesEnStock.gains.Length; i++)
        {
            if(ressourcesEnStock.gains[i] > capaciteDeStockage.gains[i])
            {
                ressourcesEnStock.gains[i] = capaciteDeStockage.gains[i];
            }
        }
    }

    public void EncaisserGain()
    {
        for (int i = 0; i < ressourcesEnStock.gains.Length; i++)
        {
            ressourcesEnStock.gains[i] += projectionGain.gains[i];
        }

        LimiterStock();
        MiseAJourInterfaceRessource();
    }

    private void MiseAJourInterfaceRessource()
    {
        if(InterfaceRessource.Actuel)
        {
            InterfaceRessource.Actuel.MiseAJourCapacite(capaciteDeStockage);
            InterfaceRessource.Actuel.MiseAJourStock(ressourcesEnStock);
            InterfaceRessource.Actuel.MiseAjourGain(projectionGain);
        }
    }

    public void AjouterCapacitePop()
    {
        for (int i = 0; i < capaciteDeStockage.gains.Length; i++)
        {
            capaciteDeStockage.gains[i] += capaciteParPop.gains[i];
        }
        MiseAJourInterfaceRessource();
    }

    public void RetirerCapacitePop()
    {
        for (int i = 0; i < capaciteDeStockage.gains.Length; i++)
        {
            capaciteDeStockage.gains[i] -= capaciteParPop.gains[i];
        }
        MiseAJourInterfaceRessource();
    }
}
