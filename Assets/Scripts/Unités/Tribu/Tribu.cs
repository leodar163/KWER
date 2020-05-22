﻿using System.Collections.Generic;
using UnityEngine;

public class Tribu : MonoBehaviour
{
    public int idTribu;

    PathFinder pathFinder;
    public TuileManager tuileActuelle;
    SpriteRenderer spriteRenderer;
    Revendication revendication;

    [HideInInspector] public bool estEntreCampement = false;

    //couleurs
    public Color couleurSelectionne;

    List<Troupeau> troupeauxAPortee;
    
    [Header("Déplacements")]
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

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        Init();
        expedition.LancerExpeditions();
        campement.MonterCampement();
        EntrerCampement(false);
    }

    // Update is called once per frame
    void Update()
    {
        Deplacement();
    }

    public void PasserTour()
    {
        pointsAction = pointActionDeffaut;

        tuilesAPortee = pathFinder.CreerGrapheTuilesAPortee(tuileActuelle,pointsAction,false);
    }

    public void Init()
    {
        revendication = GetComponent<Revendication>();
        troupeauxAPortee = new List<Troupeau>();
        pointsAction = pointActionDeffaut;
        cheminASuivre = new Stack<TuileManager>();

        TrouverTuileActuelle();
        pathFinder = GetComponent<PathFinder>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        tuilesAPortee = pathFinder.CreerGrapheTuilesAPortee(tuileActuelle, pointsAction, false);
    }

    private void TrouverTuileActuelle()
    {
        LayerMask layerMaskTuile = LayerMask.GetMask("Tuile");

        Collider2D checkTuile = Physics2D.OverlapBox(transform.position, new Vector2(0.1f, 0.1f), 0, layerMaskTuile);

        if(checkTuile)
        {
            tuileActuelle = checkTuile.GetComponent<TuileManager>();
            transform.position = new Vector3(tuileActuelle.transform.position.x, tuileActuelle.transform.position.y, transform.position.z);
        }
    }



    #region INTERFACE

    public void EntrerCampement(bool selectionner)
    {
        if(!CameraControle.Actuel.camEnMouvmt)
        {
            estEntreCampement = selectionner;
            demographie.AfficherIntefacePop(selectionner);
            expedition.AfficherExploitations(selectionner);
            campement.AfficherInterfaceCampement(selectionner);

            if (selectionner)
            {
                CameraControle.Actuel.CentrerCamera(transform.position, true);
            }
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
