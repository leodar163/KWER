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
    public float bonusMultCoutPop;

    [Header("Stockage")]
    public float bonusMultStockage;
}
