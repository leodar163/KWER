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
    public int nbrSlot;
    public Production production;
    public Production bonusOutil;
}
