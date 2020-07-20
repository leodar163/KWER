using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CapsuleCollider2D))]
public class Combat : Interaction
{
    private Guerrier guerrier;
    private Hostile hostile;

    public Guerrier Guerrier
    {
        get
        {
            return guerrier;
        }
        set
        {
            guerrier = value;
            interfaceCombat.guerrier = guerrier;
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

    public InterfaceCombat interfaceCombat;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
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
        if (entrer) boutonInteraction.onClick.AddListener(CommencerCombat);
        else boutonInteraction.onClick.RemoveListener(CommencerCombat);

        if(!entrer)
        {
            boutonInteraction.interactable = true;
        }
    }

    private void AfficherInterfaceCombat(bool afficher)
    {
        interfaceCombat.gameObject.SetActive(afficher);
    }

    private void CommencerCombat()
    {
        InterfaceEvenement.Defaut.OuvrirFenetreEvenementCombat(this);
    }

    private void MAJBouton()
    {
        if(guerrier.nbrGuerrier <= 0)
        {
            ActiverBouton(false);
        }
        else
        {
            ActiverBouton(true);
        }
    }

    public void LancerCombat()
    {
        int attaqueGuerrier = 0;
        int defenseGuerrier = 0;

        int attaqueHostile = 0;
        int defenseHostile = 0;

        for (int i = 0; i < guerrier.nbrGuerrier; i++)
        {
            for (int j = 0; j < guerrier.attaqueTotale; j++)
            {
                if (Random.Range(0, 1) == 0)
                {
                    attaqueGuerrier++;
                }
            }
            for (int j = 0; j < guerrier.defenseTotale; j++)
            {
                if (Random.Range(0, 1) == 0)
                {
                    defenseGuerrier++;
                }
            }
        }

        for (int i = 0; i < hostile.nbrCombattant; i++)
        {
            for (int j = 0; j < hostile.attaque; j++)
            {
                if (Random.Range(0, 1) == 1) attaqueHostile++;
            }
            for (int j = 0; j < hostile.defense; j++)
            {
                if (Random.Range(0, 1) == 1) defenseHostile++;
            }
        }

        int mortsGuerrier = defenseGuerrier < attaqueHostile ? attaqueHostile - defenseGuerrier : 0;
        int mortsHostile = defenseHostile < attaqueGuerrier ? attaqueGuerrier - defenseHostile : 0;

        bool ennemiFuit = false;

        for (int i = 0; i < mortsHostile; i++)
        {
            if (Random.Range(0, 100) <= guerrier.degatsMoraux - hostile.resistanceMorale) ennemiFuit = true;
        }

        guerrier.nbrGuerrier = guerrier.nbrGuerrier > mortsGuerrier ? guerrier.nbrGuerrier - mortsGuerrier : 0;
        hostile.nbrCombattant = hostile.nbrCombattant > mortsHostile ? hostile.nbrCombattant - mortsHostile : 0;

        InterfaceEvenement.Defaut.OuvrirRecapCombat(attaqueGuerrier, defenseGuerrier, attaqueHostile, defenseHostile, mortsGuerrier, mortsHostile, ennemiFuit, this);
    }
}
