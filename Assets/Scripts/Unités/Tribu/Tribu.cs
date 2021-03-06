﻿using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class Tribu : Pion
{
    public int idTribu;

    [HideInInspector] public bool estEntreCampement = false;
    
    [Header("Déplacements")]
    Stack<TuileManager> cheminASuivre = new Stack<TuileManager>();
    public List<TuileManager> tuilesAPortee;

    private bool traverseFleuve;

    [Header("Economie")]
    [SerializeField] public Expedition expedition;
    [SerializeField] public StockRessource stockRessources;

    [Header("Démographie")]
    [SerializeField] public Demographie demographie;

    [Header("Campement")]
    public Image banniere;
    public Campement campement;
    public InteractionTribu interactionTribu;

    [Header("Sprites")]
    [SerializeField] private Sprite sprCampementHiver;
    private Sprite sprTribuMvmt;
    private Sprite SprCampementEte;

    [Header("Combats")]
    public Guerrier guerrier;

    [Header("Bonus")]
    public BonusTribu bonus;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        Init();
        revendication.RevendiquerTerritoire(tuileActuelle, true);
        expedition.LancerExpeditions();

        campement.MonterCampement();
        EntrerCampement(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        Deplacement();
    }

    #region TOUR PAR TOUR
    /// <summary>
    /// Est appelée au début du tour des Tribus
    /// </summary>
    public override void DebutTour()
    {
        base.DebutTour();
    }

    /// <summary>
    /// Est appelée quand la tribu commence son tour
    /// </summary>
    public override void CommencerTour()
    {
        base.CommencerTour();

        ptsDeplacement = ptsDeplacementDefaut;
        guerrier.jetonAttaque = true;
        TrouverTuileActuelle();
        tuilesAPortee = pathFinder.CreerGrapheTuilesAPortee(tuileActuelle, ptsDeplacement, false);
        interactionTribu.ActiverBouton(true);
        interactionTribu.infobulle.texteInfoBulle = "Cliquez pour entrer en mode campement";
        expedition.GenererInteractions();
    }

    public void CheckerPasserTour()
    {
        if (stockRessources.Disette)
        {
            string description = "Vous n'avez pas assez de ressource pour maintenir votre population." +
                " A moins d'un miracle ou que vos alliés ne vous aident, vous perdrez une population au prochain tour." +
                " Êtes-vous sûr de vouloir passer votre tour ?" +
                "\n\nRAPPEL\nConsommation par population par tour ";
            if (Calendrier.Actuel.Hiver) description += "en hiver :\n" + stockRessources.consoParPopHiver.ToString(true);
            else description += "en été :\n" + stockRessources.consoParPop.ToString(true);
            FenetreValidation.OuvrirFenetreValidation(description, "Oui", "non", PasserTour);
        }
        else PasserTour();
    }

    /// <summary>
    /// Est appelée quand la tribu a fini de jouer et passe son tour
    /// </summary>
    public override void PasserTour()
    {
        base.PasserTour();
        expedition.RappelerInteractions();

        interactionTribu.ActiverBouton(false);
        interactionTribu.infobulle.texteInfoBulle = "Tribu Alliée\nApprochez-vous pour échanger des ressources";

        if (stockRessources.Disette) stockRessources.DeclancherDisette();
    }
    #endregion


    public void Init()
    {
        banniere.sprite = InfoTribus.Defaut.bannieresTribus[idTribu];
        sprTribuMvmt = InfoTribus.Defaut.pionTribus[idTribu];

        ptsDeplacement = ptsDeplacementDefaut;
        StartCoroutine(MAJTuilesAPortee());

        spriteRenderer = GetComponent<SpriteRenderer>();
        SprCampementEte = spriteRenderer.sprite;

        Calendrier.Actuel.EventChangementDeSaison.AddListener(RevetirSpriteSaison);  
    }

    private IEnumerator MAJTuilesAPortee()
    {
        while (Application.isPlaying)
        {
            if (!estEntreCampement && ControleSouris.Actuel.controleEstActif && this == InfoTribus.TribukiJoue)
            {
                tuilesAPortee = pathFinder.CreerGrapheTuilesAPortee(tuileActuelle, 
                    ptsDeplacement + bonus.bonusPointDeplacement, false);
                pathFinder.ColorerGraphe(tuilesAPortee, tuileActuelle.couleurTuileAPortee);
            }
            
            yield return new WaitForEndOfFrame();   
        }
    }

    public override void TrouverTuileActuelle()
    {
        if (tuileActuelle)
        {
            tuileActuelle.estOccupee = false;
        }

        LayerMask layerMaskTuile = LayerMask.GetMask("Tuile");

        Collider2D checkTuile = Physics2D.OverlapBox(transform.position, new Vector2(0.1f, 0.1f), 0, layerMaskTuile);

        if(checkTuile)
        {
            tuileActuelle = checkTuile.GetComponent<TuileManager>();
            transform.position = new Vector3(tuileActuelle.transform.position.x, tuileActuelle.transform.position.y, transform.position.z);
        }

        tuileActuelle.estOccupee = true;
    }

    #region GRAPHISMES
    public void RevetirSpriteSaison()
    {
        if(Calendrier.Actuel.Hiver)
        {
            spriteRenderer.sprite = sprCampementHiver;
        }
        else
        {
            spriteRenderer.sprite = SprCampementEte;
        }
    }
    #endregion

    #region INTERFACE
    public void EntrerCampement(bool selectionner)
    {
        estEntreCampement = selectionner;
        demographie.AfficherIntefacePop(selectionner);
        expedition.AfficherExploitations(selectionner);
        campement.AfficherInterfaceCampement(selectionner);

        if (selectionner)
        {
            CameraControle.Actuel.CentrerCamera(transform.position, true);
        }
        else
        {
            CameraControle.Actuel.controlesActives = true;
        }
    }
    #endregion

    #region DEPLACEMENTS
    public TuileManager Destination
    {
        set
        {
            cheminASuivre = pathFinder.TrouverChemin(tuileActuelle, value);
            expedition.RappelerExpeditions();
            revendication.RevendiquerTerritoire(tuileActuelle, false);
            EntrerCampement(false);
        }
    }

    private bool CheckerFleuve(Vector3 direction)
    {
        LayerMask maskFleuve = LayerMask.GetMask("Fleuve");

        RaycastHit2D checkFleuve = Physics2D.Raycast(transform.position, direction, tuileActuelle.GetComponent<TuileManager>().tailleTuile, maskFleuve);

        if(checkFleuve)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    private void Deplacement()
    {
        if(cheminASuivre.Count != 0)
        {
            Vector3 direction = cheminASuivre.Peek().transform.position;
            direction.z = transform.position.z;

            if (transform.position != direction)//Tant qu'il est pas arrivé à destination
            {
                if(CheckerFleuve(direction))
                {
                    traverseFleuve = true;
                }

                if (spriteRenderer.sprite != sprTribuMvmt) spriteRenderer.sprite = sprTribuMvmt;
                banniere.gameObject.SetActive(false);

                SeDeplacerALaProchaineTuile(direction);
            }
            else //Quand il est arrivé à destination
            {
                ptsDeplacement -= cheminASuivre.Peek().GetComponent<TuileManager>().terrainTuile.coutFranchissement;

                if(traverseFleuve)
                {
                    ptsDeplacement --;
                    traverseFleuve = false;
                }

                cheminASuivre.Pop();

                TrouverTuileActuelle();
                tuilesAPortee = pathFinder.CreerGrapheTuilesAPortee(tuileActuelle, ptsDeplacement, false);

                //Quand on est arrivé à bon port
                if(cheminASuivre.Count == 0)
                {
                    banniere.gameObject.SetActive(true);
                    spriteRenderer.sprite = Calendrier.Actuel.Hiver ? sprCampementHiver : SprCampementEte;

                    revendication.RevendiquerTerritoire(tuileActuelle, true);
                    expedition.LancerExpeditions();
                }
            }
        }
    }

    private void SeDeplacerALaProchaineTuile(Vector3 direction)
    {
        transform.position = Vector3.MoveTowards(transform.position, direction, vitesse*Time.deltaTime);
    }
    #endregion

    public IEnumerator CheckerGameOver()
    {
        yield return new WaitUntil(() => demographie.taillePopulation <= 0);
        InterfaceEvenement.Defaut.evenementGameoverTrib.LancerEvenement();
        expedition.RappelerExpeditions();
        InfoTribus.RetirerTribu(this);
    }
}
