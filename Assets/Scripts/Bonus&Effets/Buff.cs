using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[CreateAssetMenu(fileName = "NvBuff", menuName = "Bonus & Effets/Buff")]
public class Buff : ScriptableObject
{
    [SerializeField] private GameObject effet;

    [SerializeField] private UnityEvent effets;
    public AntiEffet antiEffets = new AntiEffet();
    public struct AntiEffet
    {
        public Tribu tribuAffectee;
        public List<Delegate> effets;

        public AntiEffet(Tribu tribu, List<Delegate> effet)
        {
            tribuAffectee = tribu;
            effets = effet;
        }

        public AntiEffet(AntiEffet antiEffets)
        {
            tribuAffectee = antiEffets.tribuAffectee;
            effets = antiEffets.effets;
        }
    }


    //[HideInInspector] public EventTribu antiEffets;

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
            Tribu.tribuQuiJoue.StartCoroutine(ActivationTPT());   
        }
    }

    private IEnumerator ActivationTPT()
    {
        effets.Invoke();

        AntiEffet nvAntiEffets = new AntiEffet(antiEffets);
        nvAntiEffets.tribuAffectee = Tribu.tribuQuiJoue;

        yield return new AttendreFinTour(nombreTour);

        foreach (Delegate effet in nvAntiEffets.effets)
        {
            effet.DynamicInvoke();
        }
    }
}
