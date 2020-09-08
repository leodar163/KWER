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
    public Production consoParPop;
    public Production consoParPopHiver;
    [SerializeField] private Production capaciteParPop;
    [Space]
    [Header("Consommables")]
    public int emplacementConsommable = 2;
    public List<Consommable> consommables = new List<Consommable>();

    public Production CapaciteDeStockage
    {
        set
        {
            capaciteDeStockage = value;
            LimiterStock();
        }
        get
        {
            return capaciteDeStockage * tribu.bonus.bonusMultStockage;
        }
    }
    public Production RessourcesEnStock
    {
        set
        {
            ressourcesEnStock = value;
            LimiterStock();
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

    public struct Inventaire
    {
        public Production stockRessource;
        public List<Consommable> consommables;

        public Inventaire(Production stock, List<Consommable> stockConsommable)
        {
            stockRessource = stock;
            consommables = stockConsommable;
        }
        public Inventaire(Production stock)
        {
            stockRessource = stock;
            consommables = new List<Consommable>();
        }

    }

    public bool Disette
    {
        get
        {
            Production conso = Calendrier.Actuel.Hiver ? consoParPopHiver : consoParPop;
            for (int i = 0; i < conso.gains.Length; i++)
            {
                if (conso.gains[i] != 0)
                {
                    if (RessourcesEnStock.gains[i] + ProjectionGain.gains[i] < 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }

    public void DeclancherDisette()
    {
        StartCoroutine(SouffrirDisette());
    }

    private IEnumerator SouffrirDisette()
    {
        yield return new AttendreFinTour();

        tribu.demographie.SupprimerPop();
    }

    private void Awake()
    {
        InstancierProduction();
    }
    // Start is called before the first frame update
    void Start()
    {
        LimiterStock();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AjouterInventaire(Inventaire inventaire)
    {
        ressourcesEnStock += inventaire.stockRessource;

        foreach(Consommable consommable in inventaire.consommables)
        {
            consommables.Add(consommable);
        }

        LimiterStock();
    }

    public void RetirerInventaire(Inventaire inventaire)
    {
        ressourcesEnStock -= inventaire.stockRessource;

        foreach(Consommable consommable in inventaire.consommables)
        {
            if (consommables.Contains(consommable)) consommables.Remove(consommable);
        }

        LimiterStock();
    }

    private void InstancierProduction()
    {
        Production capacite = ScriptableObject.CreateInstance<Production>();
        if (capaciteDeStockage)
        {
            capacite.gains = (float[])CapaciteDeStockage.gains.Clone();
        }
        CapaciteDeStockage = capacite;

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
        if (projectionGain)
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
                    projectionGain -= consoParPopHiver * tribu.demographie.taillePopulation;
                }
                else projectionGain -= consoParPop * tribu.demographie.taillePopulation;
            }

            LimiterStock();
        }
    }

    private void LimiterGain()
    {
        for (int i = 0; i < projectionGain.gains.Length; i++)
        {
            if(projectionGain.gains[i] > (CapaciteDeStockage.gains[i] - ressourcesEnStock.gains[i]))
            {
                projectionGain.gains[i] = (CapaciteDeStockage.gains[i] - ressourcesEnStock.gains[i]);
            }
        }
    }

    private void LimiterStock()
    {
        for (int i = 0; i < ressourcesEnStock.gains.Length; i++)
        {
            if(ressourcesEnStock.gains[i] > CapaciteDeStockage.gains[i])
            {
                ressourcesEnStock.gains[i] = CapaciteDeStockage.gains[i];
            }
            if (ressourcesEnStock.gains[i] < 0) ressourcesEnStock.gains[i] = 0;
        }
    }

    public void EncaisserGain()
    {
        for (int i = 0; i < ressourcesEnStock.gains.Length; i++)
        {
            ressourcesEnStock.gains[i] += projectionGain.gains[i];
        }

        LimiterStock();
    }

    public void EncaisserRessource(string nomRessource, float montant)
    {
        ressourcesEnStock.AugmenterGain(nomRessource, montant);
        LimiterStock();
    }

    public void AjouterCapacitePop()
    {
        for (int i = 0; i < capaciteDeStockage.gains.Length; i++)
        {
            capaciteDeStockage.gains[i] += capaciteParPop.gains[i];
        }
        LimiterStock();
    }

    public void RetirerCapacitePop()
    {
        for (int i = 0; i < capaciteDeStockage.gains.Length; i++)
        {
            capaciteDeStockage.gains[i] -= capaciteParPop.gains[i];
        }
        LimiterStock();
    }
}
