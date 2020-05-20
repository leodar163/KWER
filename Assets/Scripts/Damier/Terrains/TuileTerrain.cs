using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TuileTerrain : MonoBehaviour
{
    public string nom;

    [Header("Sprites")]
    public Sprite[] garnitures;
    public Sprite[] garnituresHivernales;

    public float coutFranchissement;
    public bool ettendueEau;

    [Header("Production")]
    [SerializeField] private int nbrSlots;
    [SerializeField] float gainNourriture;
    [SerializeField] float gainPierre;
    [SerializeField] float gainPeau;
    [SerializeField] float gainPigment;
    
    public ProductionTuile.Production Production
    {
        get
        {
            return new ProductionTuile.Production(nbrSlots, gainNourriture, gainPierre, 
                                                    gainPeau, gainPigment);
        }
    }
}
