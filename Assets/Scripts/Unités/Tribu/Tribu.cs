using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Tribu : Pion
{
    public int idTribu;

    SpriteRenderer spriteRenderer;

    #region SINGLETON
    static public Tribu tribuQuiJoue
    {
        get
        {
            Tribu[] listeTribus = FindObjectsOfType<Tribu>();
            for (int i = 0; i < listeTribus.Length; i++)
            {
                if(listeTribus[i].idTribu == ControleSouris.Actuel.idTribuControlee)
                {
                    return listeTribus[i];
                }
            }
            return null;
        }
    }
    #endregion

    [HideInInspector] public bool estEntreCampement = false;
    
    [Header("Déplacements")]
    public PathFinder pathFinder;
    [HideInInspector]public TuileManager tuileActuelle;
    public float pointActionDeffaut = 3;
    public float pointsAction;
    public float vitesse = 10;
    Stack<TuileManager> cheminASuivre = new Stack<TuileManager>();
    public bool peutEmbarquer = false;
    public List<TuileManager> tuilesAPortee;

    private bool traverseFleuve;

    [Header("Economie")]
    [SerializeField] private Expedition expedition;
    [SerializeField] public StockRessource stockRessources;

    [Header("Démographie")]
    [SerializeField] public Demographie demographie;

    [Header("Campement")]
    public Campement campement;

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
        pointsAction = pointActionDeffaut;

        tuilesAPortee = pathFinder.CreerGrapheTuilesAPortee(tuileActuelle,pointsAction,false);
        expedition.GenererCombats();
        stockRessources.EncaisserGain();
    }

    public void Init()
    {
        TrouverTuileActuelle();

        pointsAction = pointActionDeffaut;
        StartCoroutine(MAJTuilesAPortee());

        spriteRenderer = GetComponent<SpriteRenderer>();
        SprCampementEte = spriteRenderer.sprite;

        Calendrier.Actuel.EventChangementDeSaison.AddListener(RevetirSpriteSaison);  
    }

    private IEnumerator MAJTuilesAPortee()
    {
        while(Application.isPlaying)
        {
            tuilesAPortee = pathFinder.CreerGrapheTuilesAPortee(tuileActuelle, pointsAction, false);
            
            if(!estEntreCampement && ControleSouris.Actuel.controlesActives)
            {
                pathFinder.ColorerGraphe(tuilesAPortee, tuileActuelle.couleurTuileAPortee);
            }
            else
            {
                pathFinder.ColorerGraphe(tuilesAPortee, Color.white);
            }

            yield return new WaitForEndOfFrame();
        }
    }

    private void TrouverTuileActuelle()
    {
        if (tuileActuelle) tuileActuelle.estOccupee = false;

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
                
                pointsAction -= cheminASuivre.Peek().GetComponent<TuileManager>().terrainTuile.coutFranchissement;

                if(traverseFleuve)
                {
                    pointsAction --;
                    traverseFleuve = false;
                }

                cheminASuivre.Pop();

                TrouverTuileActuelle();
                tuilesAPortee = pathFinder.CreerGrapheTuilesAPortee(tuileActuelle, pointsAction, false);

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
}
