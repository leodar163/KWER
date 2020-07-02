using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[CreateAssetMenu(fileName = "NvBuff", menuName = "Bonus&Effets/Buff")]
public class Buff : ScriptableObject
{
    [SerializeField] private GameObject effet;

    [SerializeField] private UnityEvent Effets;

    [HideInInspector] public bool compteurTour;
    [HideInInspector] public bool tpsDunEvent;
    [HideInInspector] public bool tpsDuneTechno;

    [HideInInspector] public int nombreTour;
    public void activerBuff()
    {
        Effets.Invoke();
    }
}
