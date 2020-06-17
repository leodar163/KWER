using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NvlEvenementTempo", menuName = "Evénements/Evément Temporel")]
public class EvenementTemporel : Evenement
{
    [Header("Probabilité")]
    [Range(0, 100)]
    public float probaEte;
    [Range(0, 100)]
    public float probaHiver;

    [Header("Notification")]
    public string infobulle;
}
