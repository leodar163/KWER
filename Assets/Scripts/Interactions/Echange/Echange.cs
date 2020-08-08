using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Echange : Interaction
{
    [HideInInspector] public Tribu tribuCible;

    protected override void Start()
    {
        base.Start();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void EntrerEnInteraction(bool entrer)
    {
        base.EntrerEnInteraction(entrer);

        if(enInteraction)
        {
            InterfaceEchange.Actuel.OuvrirEchange(this);
        }
    }

}
