﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceEchange : MonoBehaviour
{
    static private InterfaceEchange cela;

    static public InterfaceEchange Actuel
    {
        get
        {
            if (cela == null) cela = FindObjectOfType<InterfaceEchange>();
            return cela;
        }
    }
    [Header("Interface")]
    [SerializeField] private Button boutonValider;
    [SerializeField] private Button boutonAnnuler;
    [Space]
    [Header("Bannières")]
    [SerializeField] private Image banniereJoueur;
    [SerializeField] private Image banniereCible;
    [Space]
    [Header("Platos")]
    [SerializeField] private PlatoEchange platoEchangeJoueur;
    [SerializeField] private PlatoEchange platoEchangeCible;
    

    private Echange echange;

    // Start is called before the first frame update
    void Start()
    {
        cela = this;
        InterfaceRessource.Actuel.EventInterfaceMAJ.AddListener(MAJInterface);
        boutonAnnuler.onClick.AddListener(AnnulerEchange);
        boutonValider.onClick.AddListener(EffectuerEchange);
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void MAJInterface()
    {
        platoEchangeCible.MAJPlato();
        platoEchangeJoueur.MAJPlato();
    }

    

    public void OuvrirEchange(Echange mercosur)
    {
        echange = mercosur;

        platoEchangeCible.tribu = echange.tribuCible;
        platoEchangeCible.ActiverInteraction(true);

        platoEchangeJoueur.tribu = InfoTribus.TribukiJoue;
        platoEchangeJoueur.ActiverInteraction(true);

        banniereCible.sprite = echange.tribuCible.banniere.sprite;
        banniereJoueur.sprite = InfoTribus.TribukiJoue.banniere.sprite;

        gameObject.SetActive(true);
    }

    public void FermerEchange()
    {
        gameObject.SetActive(false);
        echange.EntrerEnInteraction(false);
        platoEchangeCible.NettoyerPlato();
        platoEchangeJoueur.NettoyerPlato();
    }

    public void AnnulerEchange()
    {
        platoEchangeCible.RendreToutLArgent();
        platoEchangeJoueur.RendreToutLArgent();
        FermerEchange();
    }

    public void EffectuerEchange()
    {
        StockRessource.Inventaire stockEngageJoueur = platoEchangeJoueur.StockEngage;
        StockRessource.Inventaire stockEngageCible = platoEchangeCible.StockEngage;

        InfoTribus.TribukiJoue.stockRessources.AjouterInventaire(stockEngageCible);
        echange.tribuCible.stockRessources.AjouterInventaire(stockEngageJoueur);

        FermerEchange();
    }
}
