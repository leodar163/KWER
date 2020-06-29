using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenetreEvenementCombat : FenetreEvenement
{
    [Header("Interface Combat")]
    private Combat combat;
    public Combat CombatActuel
    {
        get
        {
            return combat;
        }
    }
    [SerializeField] private StatsCombat statsJoueur;
    [SerializeField] private StatsCombat statsEnnemi;

    public override Evenement EvenementActuel
    {
        get => evenement;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LancerCombat(Combat combatALancer, EvenementCombat evenementCombat)
    {
        evenement = evenementCombat;
        DessinerEvenement();
        combat = combatALancer;
        MAJInterfaceCombat();
    }

    private void MAJInterfaceCombat()
    {
        statsJoueur.MAJStats(combat.Guerrier);
        statsEnnemi.MAJStats(combat.Hostile.nbrCombattant, combat.Hostile.attaque, combat.Hostile.defense);
    }

}
