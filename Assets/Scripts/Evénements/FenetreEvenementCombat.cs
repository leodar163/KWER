using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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


    [SerializeField] private Image banniereJoueur;
    [SerializeField] private Image banniereHostile;
    private InfoBulle infoBulleBanniereJoueur;
    private InfoBulle infoBulleBanniereHostile;

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
        if (!infoBulleBanniereJoueur)
            infoBulleBanniereJoueur = banniereJoueur.GetComponent<InfoBulle>();
        if (!infoBulleBanniereHostile)
            infoBulleBanniereHostile = banniereHostile.GetComponent<InfoBulle>();

        evenement = evenementCombat;
        DessinerEvenement();
        combat = combatALancer;

        CameraControle.Actuel.CentrerCamera(combat.Guerrier.transform.position);

        banniereJoueur.sprite = combat.Guerrier.tribu.banniere.sprite;
        infoBulleBanniereJoueur.texteInfoBulle = combat.Guerrier.tribu.name;
        banniereHostile.sprite = combat.Hostile.pion.spriteRenderer.sprite;
        infoBulleBanniereHostile.texteInfoBulle = combat.Hostile.pion.name;

        Invoke("MAJInterfaceCombat", 0.2f);
    }

    private void MAJInterfaceCombat()
    {
        statsJoueur.MAJStats(combat.Guerrier);
        statsEnnemi.MAJStats(combat.Hostile.nbrCombattant, combat.Hostile.attaque, combat.Hostile.defense);
    }

}
