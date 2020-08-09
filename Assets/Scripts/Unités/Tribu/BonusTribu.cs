using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusTribu : MonoBehaviour
{
    public Tribu tribu;
    
    [Header("Combat")]
    public int bonusAttaque;
    public int bonusDefense;
    public int bonusDegatMoral;

    [Header("Population")]
    public float bonusMultCoutPop = 1;

    [Header("Stockage")]
    public float bonusMultStockage = 1;

    [Header("Production")]
    public float bonusMultProd = 1;
    public float bonusMultProdNourriture = 1;
    public float bonusMultProdPierre = 1;
    public float bonusMultProdOutil = 1;
    public float bonusMultProdPeau = 1;
    public float bonusMultProdPigment = 1;
}
