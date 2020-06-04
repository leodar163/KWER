using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


public class InterfaceCombat : MonoBehaviour
{
    [SerializeField] private StatsCombat statsTribu;
    [SerializeField] private StatsCombat statsEnnemi;
    [HideInInspector] public Hostile ennemi;
    [HideInInspector] public Guerrier tribu;

    [HideInInspector] public UnityEvent eventMAJInterface;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        MAJInterface();
    }

    public void MAJInterface()
    {
        if(ennemi && tribu)
        {
            statsEnnemi.MAJStats(ennemi.nbrCombattant, ennemi.attaque, ennemi.defense);
            statsTribu.MAJStats(tribu.nbrGuerrier, tribu.attaque, tribu.defense);
            eventMAJInterface.Invoke();
        }
    }
}
