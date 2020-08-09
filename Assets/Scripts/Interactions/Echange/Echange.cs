using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Echange : Interaction
{
    [HideInInspector] public Tribu tribuCible;

    protected override void Start()
    {
        base.Start();
        boutonInteraction.onClick.AddListener(() => EntrerEnInteraction(true));
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
