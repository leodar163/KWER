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
    [SerializeField] private int nbrSlotsParDeffaut;
    [SerializeField] float nourritureParSlotParDeffaut;
    [SerializeField] float pierreParSlotParDeffaut;
    [SerializeField] float peauParSlotParDeffaut;
    [SerializeField] float pigmentParSlotParDeffaut;
    
    public ProductionTuile.Production Production
    {
        get
        {
            return new ProductionTuile.Production(nbrSlotsParDeffaut, nourritureParSlotParDeffaut, pierreParSlotParDeffaut, 
                                                    peauParSlotParDeffaut, pigmentParSlotParDeffaut);
        }
    }
}
