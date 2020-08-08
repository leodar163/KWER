using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class Tribu : Pion
{
    public int idTribu;

    SpriteRenderer spriteRenderer;

    #region SINGLETON
    static public Tribu TribukiJoue
    {
        get
        {
            return ListeOrdonneeDesTribus[TourParTour.Defaut.idTribu];
        }
    }

    static private Tribu[] listeOrdonneeTribus;
    static public Tribu[] ListeOrdonneeDesTribus
    {
        get
        {
            if(listeOrdonneeTribus == null)
            {
                Tribu[] tribus = FindObjectsOfType<Tribu>();
                listeOrdonneeTribus = new Tribu[tribus.Length];

                for (int i = 0; i < tribus.Length; i++)
                {
                    listeOrdonneeTribus[tribus[i].idTribu] = tribus[i];
                }
            }

            return listeOrdonneeTribus;
        }
    }
    #endregion

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
    [SerializeField] private Sprite SprCampementHiver;
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

    public override void DemarrerTour()
    {
        base.DemarrerTour();
        ptsDeplacement = ptsDeplacementDefaut;
        guerrier.jetonAttaque = true;

        tuilesAPortee = pathFinder.CreerGrapheTuilesAPortee(tuileActuelle,ptsDeplacement,false);
        expedition.GenererInteractions();
        stockRessources.EncaisserGain();
    }

    public void Init()
    {
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
            if (!estEntreCampement && ControleSouris.Actuel.controleEstActif && this == TribukiJoue)
            {
                tuilesAPortee = pathFinder.CreerGrapheTuilesAPortee(tuileActuelle, ptsDeplacement, false);
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
            spriteRenderer.sprite = SprCampementHiver;
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
            pathFinder.ReinitGraphe();
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

                if(cheminASuivre.Count == 0)
                {
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

    public void GameOver()
    {
        print("GameOver !");
        print(demographie.taillePopulation);
        expedition.RappelerExpeditions();
        Destroy(gameObject);
    }
}
