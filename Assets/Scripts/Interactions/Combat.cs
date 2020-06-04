using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CapsuleCollider2D))]
public class Combat : Interaction
{
    private Guerrier tribu;
    private Hostile hostile;

    public Guerrier Tribu
    {
        get
        {
            return tribu;
        }
        set
        {
            tribu = value;
            interfaceCombat.tribu = tribu;
        }
    }
    public Hostile Hostile
    {
        get
        {
            return hostile;
        }
        set
        {
            hostile = value;
            interfaceCombat.ennemi = hostile;
        }
    }

    [SerializeField] private InterfaceCombat interfaceCombat;

    // Start is called before the first frame update
    void Start()
    {
        interfaceCombat.gameObject.SetActive(false);
        interfaceCombat.eventMAJInterface.AddListener(MAJBouton);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void EntrerEnInteraction(bool entrer)
    {
        base.EntrerEnInteraction(entrer);
        AfficherInterfaceCombat(entrer);
        if (entrer == true) boutonInteraction.onClick.AddListener(CommencerCombat);
        else boutonInteraction.onClick.RemoveListener(CommencerCombat);
    }

    private void AfficherInterfaceCombat(bool afficher)
    {
        interfaceCombat.gameObject.SetActive(afficher);
    }

    private void CommencerCombat()
    {
        print("C le combat !");
    }

    private void MAJBouton()
    {
        if(tribu.nbrGuerrier <= 0)
        {
            ActiverBouton(false);
        }
        else
        {
            ActiverBouton(true);
        }
    }
}
