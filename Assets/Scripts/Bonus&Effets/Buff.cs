using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[CreateAssetMenu(fileName = "NvBuff", menuName = "Bonus & Effets/Buff")]
public class Buff : ScriptableObject
{
    [SerializeField] private GameObject effet;

    [SerializeField] private UnityEvent effets;
    [HideInInspector] public UnityEvent antiEffets;

    [HideInInspector] public bool compteurTour;
    [HideInInspector] public bool tpsDunEvent;
    [HideInInspector] public bool tpsDuneTechno;
    [HideInInspector] public int nombreTour;
    [HideInInspector] public List<string> listeEffetsRetours;


    [HideInInspector]
    public string Retours
    {
        get
        {
            string retours = "";

            for (int i = 0; i < listeEffetsRetours.Count; i++)
            {
                if(i != 0)
                {
                    retours += '\n';
                }
                retours += listeEffetsRetours[i];
            }

            return retours;
        }
    }



    public void activerBuff()
    {
        effets.Invoke();


    }


}
