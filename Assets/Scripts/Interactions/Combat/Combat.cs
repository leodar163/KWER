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
            infobulle.texteInfoBulle = hostile.name + "\nCliquer pour ouvrir l'interface de combat";
        }
    }
    [SerializeField] private InfoBulle infobulle;
    public InterfaceCombat interfaceCombat;

    public struct RecapCombat
    {
        public int attaqueGuerrier;
        public int defenseGuerrier;
        public int attaqueHostile;
        public int defenseHostile;
        public int mortsGuerrier;
        public int mortsHostile;
        public bool ennemiFuit;

        public RecapCombat(int attGuerrier, int defGerrier, int attHost, int defHost, int mortsGuer, int mortsHost, bool fuiteEnnemi)
        {
            attaqueGuerrier = attGuerrier;
            defenseGuerrier = defGerrier;
            attaqueHostile = attHost;
            defenseHostile = defHost;
            mortsGuerrier = mortsGuer;
            mortsHostile = mortsHost;
            ennemiFuit = fuiteEnnemi;
        }
    }

    private void OnDestroy()
    {
        guerrier.DesengagementGeneral();
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        interfaceCombat.gameObject.SetActive(false);
        interfaceCombat.eventMAJInterface.AddListener(MAJBouton);
        InterfaceEvenement.Defaut.eventFinEvenement.AddListener(TerminerCombat);
        
    }

    // Update is called once per frame
    void Update()
    {
   
    }

    public void joueurFuit()
    {
        int guerriersTues = 0;
        for (int i = 0; i < guerrier.nbrGuerrier; i++)
        {
            int alea = Random.Range(0, 99);
            
            if(alea < 25)
            {
                guerriersTues++;
            }
        }

        EvenementCombat fuiteJoueur = ScriptableObject.CreateInstance<EvenementCombat>();

        string texteInfobulle;
        string descriptionChoix;

        if (guerriersTues > 0)
        {
            texteInfobulle = "<color=#" + ColorUtility.ToHtmlStringRGBA(ListeCouleurs.Defaut.couleurAlerteTexteInterface)
            + ">" + guerriersTues + " <color=\"white\">guerriers meurent";
            descriptionChoix = "Nous avons subit des pertes";
            fuiteJoueur.description = "En plus de l'humilation de la défaite, certains de nos guerriers ont dû mourrir sans se battre. La fuite ne s'est pas bien passée";
            fuiteJoueur.titre = "ECHEC DE LA FUITE";
        }
        else
        {
            texteInfobulle = "Dans les fourrés !";
            descriptionChoix = "Aucun guerriere ne meurt";
            fuiteJoueur.description = "Il semble que nos guerriers courent mieux qu'ils se battent." +
                "A défaut d'avoir remporté la victoire, la retraite a été un succès.";
            fuiteJoueur.titre = "RETRAITE REUSSIE";
        }
        
        Evenement.Choix choix = new Evenement.Choix(descriptionChoix, texteInfobulle);

        choix.effets.AddListener(InterfaceEvenement.Defaut.FermerFenetreEvenement);
        choix.effets.AddListener(() => fuiteJoueur.TuerGuerrier(guerriersTues));
        
        fuiteJoueur.listeChoix.Add(choix);
        fuiteJoueur.combat = this;
        fuiteJoueur.LancerEvenement();
    }

    public void EnnemiFuit()
    {
        int hostilesTues = 0;
        for (int i = 0; i < hostile.nbrCombattant; i++)
        {
            int alea = Random.Range(0, 99);

            if (alea < 25)
            {
                hostilesTues++;
            }
        }

        EvenementCombat fuiteHostile = ScriptableObject.CreateInstance<EvenementCombat>();

        string texteInfobulle;
        string descriptionChoix;

        if (hostilesTues > 0)
        {
            texteInfobulle = "<color=#" + ColorUtility.ToHtmlStringRGBA(ListeCouleurs.Defaut.couleurTexteBonus)
            + ">" + hostilesTues + " <color=\"white\">ennemis meurent";
            descriptionChoix = "L'ennemi goute à nos pointes !";
            fuiteHostile.description = "L'ennemi a tenté de fuire, mais nous avons réussi à tuer certains d'entre eux avant !";
            fuiteHostile.titre = "ENNEMIS RATTRAPES";
        }
        else
        {
            texteInfobulle = "Maudits soient-ils !";
            descriptionChoix = "Aucun ennemi ne meurt";
            fuiteHostile.description = "L'ennemi a fuit et nous avons été incapable de l'intercepter." +
                "A l'heure qu'il est ils doivent être en train de se rassembler pour préparer un nouvel assaut.";
            fuiteHostile.titre = "L'ENNEMI S'ENFUIT";
        }

        Evenement.Choix choix = new Evenement.Choix(descriptionChoix, texteInfobulle);

        choix.effets.AddListener(InterfaceEvenement.Defaut.FermerFenetreEvenement);
        choix.effets.AddListener(() => fuiteHostile.TuerEnnemis(hostilesTues));
        

        fuiteHostile.listeChoix.Add(choix);
        fuiteHostile.combat = this;
        fuiteHostile.LancerEvenement();
    }

    //Se lance automatiquement quand un evenement combat prend fin
    private void TerminerCombat()
    {
        if(hostile.combatEstEnCours)
        {
            if (hostile.nbrCombattant <= 0) Destroy(hostile.gameObject);
            else
            {
                if (interfaceCombat)
                {
                    interfaceCombat.ReinitSlots();
                    guerrier.DesengagementGeneral();
                }
                hostile.combatEstEnCours = false;
            }
        }
    }

    public void looterEnnemi()
    {
        guerrier.tribu.stockRessources.RessourcesEnStock += hostile.loot;
    }

    public override void EntrerEnInteraction(bool entrer)
    {
        base.EntrerEnInteraction(entrer);
        AfficherInterfaceCombat(entrer);
        if (entrer)
        {
            boutonInteraction.onClick.AddListener(CommencerCombat);
        }
        else
        {
            boutonInteraction.onClick.RemoveListener(CommencerCombat);
            boutonInteraction.interactable = true;
            infobulle.texteInfoBulle = hostile.name + "\nCliquer pour ouvrir l'interface de combat";
        }
    }

    private void AfficherInterfaceCombat(bool afficher)
    {
        interfaceCombat.gameObject.SetActive(afficher);
    }

    private void CommencerCombat()
    {
        if(!OptionsJeu.Defaut.modeCombatsSimplifies)
            FenetreValidation.OuvrirFenetreValidation("Êtes-vous sûr de vouloir démarer un combat avec des " + Hostile.name + "s ?"
            , "Attaquer !", "Renoncer", () => InterfaceEvenement.Defaut.OuvrirFenetreEvenementCombat(this)
            , Hostile.pion.spriteRenderer.sprite, Hostile.name);
        else
            FenetreValidation.OuvrirFenetreValidation("Êtes-vous sûr de vouloir démarer un combat avec des " + Hostile.name + "s ?"
            , "Attaquer !", "Renoncer", LancerCombat
            , Hostile.pion.spriteRenderer.sprite, Hostile.name);
    }

    private void MAJBouton()
    {
        if(enInteraction)
        {
            if(guerrier.nbrGuerrier <= 0)
            {
                ActiverBouton(false);
                infobulle.texteInfoBulle = "<color=#"+ColorUtility.ToHtmlStringRGBA(ListeCouleurs.Defaut.couleurAlerteTexteInterface)
                    +">Il faut assigner au moins une population pour commencer un combat";
            }
            else
            {
                ActiverBouton(true);
                infobulle.texteInfoBulle = "Cliquer pour commencer le combat";
            }
        }
    }

    public void LancerCombat()
    {
        hostile.combatEstEnCours = true;

        int attaqueGuerrier = 0;
        int defenseGuerrier = 0;

        int attaqueHostile = 0;
        int defenseHostile = 0;

        for (int i = 0; i < guerrier.nbrGuerrier; i++)
        {
            for (int j = 0; j < guerrier.attaqueTotale; j++)
            {
                if (Random.Range(0, 2) == 1)
                {
                    attaqueGuerrier++;
                }
            }
            for (int j = 0; j < guerrier.defenseTotale; j++)
            {
                if (Random.Range(0, 2) == 1)
                {
                    defenseGuerrier++;
                }
            }
        }

        for (int i = 0; i < hostile.nbrCombattant; i++)
        {
            for (int j = 0; j < hostile.attaque; j++)
            {
                if (Random.Range(0, 2) == 1) attaqueHostile++;
            }
            for (int j = 0; j < hostile.defense; j++)
            {
                if (Random.Range(0, 2) == 1) defenseHostile++;
            }
        }

        int mortsGuerrier = defenseGuerrier < attaqueHostile ? attaqueHostile - defenseGuerrier : 0;
        int mortsHostile = defenseHostile < attaqueGuerrier ? attaqueGuerrier - defenseHostile : 0;

        mortsGuerrier = mortsGuerrier < guerrier.nbrGuerrier ? mortsGuerrier : guerrier.nbrGuerrier;
        mortsHostile = mortsHostile < hostile.nbrCombattant ? mortsHostile : hostile.nbrCombattant;

        bool ennemiFuit = false;

        for (int i = 0; i < mortsHostile; i++)
        {
            if (Random.Range(0, 100) <= guerrier.degatMoralTotal - hostile.resistanceMorale) ennemiFuit = true;
        }

        int nbrGuerrierATuer = guerrier.nbrGuerrier > mortsGuerrier ? mortsGuerrier : guerrier.nbrGuerrier;
        for (int i = 0; i < nbrGuerrierATuer; i++)
        {
            guerrier.tribu.demographie.DesengagerGuerrier(true);
        }

        hostile.nbrCombattant = hostile.nbrCombattant > mortsHostile ? hostile.nbrCombattant - mortsHostile : 0;

        InterfaceEvenement.Defaut.OuvrirRecapCombat(new RecapCombat(attaqueGuerrier, defenseGuerrier, attaqueHostile, defenseHostile, mortsGuerrier, mortsHostile, ennemiFuit), this);
    }
}
