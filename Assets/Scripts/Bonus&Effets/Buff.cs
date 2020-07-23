using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[CreateAssetMenu(fileName = "NvBuff", menuName = "Bonus & Effets/Buff")]
public class Buff : ScriptableObject
{
    [SerializeField] private EffetBonus effet;

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
        if(compteurTour)
        {
            Tribu.TribukiJoue.StartCoroutine(ActivationTPT());   
        }
        else if (tpsDunEvent)
        {
            Tribu.TribukiJoue.StartCoroutine(ActivationEvenement());
        }
    }

    private IEnumerator ActivationTPT()
    {
        effets.Invoke();

        Tribu tribuKiSubit = Tribu.TribukiJoue;

        yield return new AttendreFinTour(nombreTour);

        effet.TribuKiSubit = tribuKiSubit;

        antiEffets.Invoke();

        effet.TribuKiSubit = null;
    }

    private IEnumerator ActivationEvenement()
    {
        effets.Invoke();

        bool eventFini = false;
        Tribu tribuKiSubit = Tribu.TribukiJoue;
        InterfaceEvenement.Defaut.eventFinEvenement.AddListener(() => eventFini = true);

        yield return new WaitUntil(() => eventFini);

        effet.TribuKiSubit = tribuKiSubit;

        antiEffets.Invoke();

        effet.TribuKiSubit = null;

    }
}
