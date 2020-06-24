using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.WSA.Input;

public class SlotCombat : Slot
{
    private Guerrier guerrier;

    public Guerrier Guerrier
    {
        get
        {
            return guerrier;
        }
        set
        {
            guerrier = value;
            demo = guerrier.tribu.demographie;
        }
    }
    [SerializeField] private Combat combat;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void AssignerPop()
    {
        //base.AssignerPop();
        //guerrier.nbrGuerrier++;
        pop = guerrier.tribu.demographie.EngagerGuerrier();
        pop.gameObject.SetActive(false);
        iconePop.gameObject.SetActive(true);
        combat.interfaceCombat.MAJInterface();
    }

    protected override void retirerPop()
    {
        //base.retirerPop();
        //guerrier.nbrGuerrier--;
        pop.gameObject.SetActive(true);
        pop = null;
        iconePop.gameObject.SetActive(false);
        guerrier.tribu.demographie.DesengagerGuerrier(false);
        combat.interfaceCombat.MAJInterface();
    }
}
