using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FenetreRecapCombat : MonoBehaviour
{   
    
    [Header("Récap'")]
    [SerializeField] private TextMeshProUGUI attaqueGuerrier = null;
    [SerializeField] private TextMeshProUGUI defenseGuerrier = null;
    [Space]
    [SerializeField] private TextMeshProUGUI attaqueHostile = null;
    [SerializeField] private TextMeshProUGUI defenseHostile = null;
    [Space]
    [SerializeField] private TextMeshProUGUI mortsGuerrier = null;
    [SerializeField] private TextMeshProUGUI mortsHostile = null;
    [Space]
    [Header("Stats de Combat")]
    [SerializeField] private StatsCombat statsGuerrier = null;
    [SerializeField] private StatsCombat statsHostile = null;
    [Space]
    [Header("Evénement")]
    [SerializeField] private GameObject objChoix = null;
    public Image illustration = null;
    [Space]
    [Header("Interface")]
    [SerializeField] private Image banniereJoueur;
    [SerializeField] private Image banniereHostile;
    private InfoBulle infoBulleBanniereJoueur;
    private InfoBulle infoBulleBanniereHostile;
    [Space]
    [Header("Mode Simplifié")]
    [SerializeField] private Button choixFuite;

    public void AfficherRecap(Combat.RecapCombat recap, Combat combat, Evenement.Choix choix)
    {

        if (!infoBulleBanniereJoueur)
            infoBulleBanniereJoueur = banniereJoueur.GetComponent<InfoBulle>();
        if (!infoBulleBanniereHostile)
            infoBulleBanniereHostile = banniereHostile.GetComponent<InfoBulle>();

        attaqueGuerrier.text = recap.attaqueGuerrier.ToString();
        defenseGuerrier.text = recap.defenseGuerrier.ToString();
        attaqueHostile.text = recap.attaqueHostile.ToString();
        defenseHostile.text = recap.defenseHostile.ToString();
        mortsGuerrier.text = recap.mortsGuerrier.ToString();
        mortsHostile.text = recap.mortsHostile.ToString();

        statsGuerrier.MAJStats(combat.Guerrier);
        statsHostile.MAJStats(combat.Hostile);

        banniereJoueur.sprite = combat.Guerrier.tribu.banniere.sprite;
        infoBulleBanniereJoueur.texteInfoBulle = combat.Guerrier.tribu.name;
        banniereHostile.sprite = combat.Hostile.pion.spriteRenderer.sprite;
        infoBulleBanniereHostile.texteInfoBulle = combat.Hostile.pion.name;

        AssignationChoix(choix);

        if (OptionsJeu.Defaut.modeCombatsSimplifies && combat.Hostile.nbrCombattant > 0 && combat.Guerrier.nbrGuerrier > 0)
        {
            choixFuite.onClick.AddListener(InterfaceEvenement.Defaut.FermerFenetreEvenement);
            choixFuite.onClick.AddListener(combat.joueurFuit);
            choixFuite.gameObject.SetActive(true);
        }
        else
            choixFuite.gameObject.SetActive(false);
    }

    private void AssignationChoix(Evenement.Choix choix)
    {
        objChoix.GetComponent<TextMeshProUGUI>().text = choix.description;
        objChoix.GetComponent<InfoBulle>().texteInfoBulle = choix.infobulle;
        objChoix.GetComponent<Button>().onClick.AddListener(choix.effets.Invoke);
    }
}
