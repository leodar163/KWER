using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionTribu : Interaction
{
    [SerializeField] private Tribu tribu;
    public InfoBulle infobulle;
    // Start is called before the first frame update
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
            tribu.EntrerCampement(true);
            boutonInteraction.gameObject.SetActive(false);
        }
        else
        {
            tribu.EntrerCampement(false);
            boutonInteraction.gameObject.SetActive(true);
        }
    }
}
