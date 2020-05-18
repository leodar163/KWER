using System.Collections.Generic;
using UnityEngine;

public class Tribu : MonoBehaviour
{
    public int idTribu;

    PathFinder pathFinder;
    public TuileManager tuileActuelle;
    SpriteRenderer spriteRenderer;
    Revendication revendication;

    [HideInInspector] public bool modeCampement = false;

    //interface
    //PanelBouffeUnite panelBouffe;
    //PanelPopupaltionUnite panelPopu;
    BonusPeche bonusPeche;
    Vector3 positionBonusPeche;
    InterfaceNourriture interfaceNourriture;
    InterfacePopulation interfacePopu;
    InterfaceCroissance interfaceCroissance;
    //Interface


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

    

    [Header("Démographie")]
    public float gainNourriturePeche = 1;
    public float ptsCroissance;
    public float excedentNourriture;
    public float gainNourriture;
    public float population = 1;
    [SerializeField] private Demographie demographie;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        Deplacement();
    }

    public void PasserTour()
    {
        pointsAction = pointActionDeffaut;
        
        CalculerNourriture();

        if(excedentNourriture >= 0)
        {
            ptsCroissance += excedentNourriture;
        }
        else
        {
            population--;
        }


        tuilesAPortee = pathFinder.CreerGrapheTuilesAPortee(tuileActuelle,pointsAction,false);
        

        //AfficherInterfaceTribu();
        //CacherIntefaceTribu();
    }

    public void Init()
    {
        revendication = GetComponent<Revendication>();
        troupeauxAPortee = new List<Troupeau>();
        pointsAction = pointActionDeffaut;
        bonusPeche = FindObjectOfType<BonusPeche>();
        cheminASuivre = new Stack<TuileManager>();

        TrouverTuileActuelle();
        pathFinder = GetComponent<PathFinder>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        tuilesAPortee = pathFinder.CreerGrapheTuilesAPortee(tuileActuelle, pointsAction, false);
        print(tuilesAPortee[0].name);
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
    private void AfficherInterfaceTribu()
    {
        AfficherPopulation();
        AfficherNourriture();
        AfficherCroissance();
    }

    private void AfficherCroissance()
    {
        interfaceCroissance.MiseAJourCroissance();
    }

    private void AfficherNourriture()
    {
        TuileManager tM = tuileActuelle.GetComponent<TuileManager>();
        tM.AfficherInterfaceTuile();
        foreach (TuileManager tuile in tM.connections)
        {
            if(tuile)
            {
                tuile.AfficherInterfaceTuile();
            }
        }

        foreach (Troupeau troupeau in troupeauxAPortee)
        {
            troupeau.AfficherNourriture();
        }

        //panelBouffe.AfficherGainNourriture(gainNourriture);

        interfaceNourriture.MiseAJourTextesNourriture();

        bonusPeche.AfficherBonusPeche(positionBonusPeche, gainNourriturePeche);
    }

    private void AfficherPopulation()
    {
        interfacePopu.MiseAJourPopulation();
        //panelPopu.AfficherPopulation();
    }

    private void CacherIntefaceTribu()
    {
        //panelPopu.CacherPopulation();
        CacherNourriture();
    }

    private void CacherNourriture()
    {
        TuileManager tM = tuileActuelle.GetComponent<TuileManager>();
        tM.CacherInterfaceTuile();
        foreach (TuileManager tuile in tM.connections)
        {
            if(tuile)
            {
                tuile.CacherInterfaceTuile();
            }      
        }

        //panelBouffe.CacherGainNourriture();

        foreach (Troupeau troupeau in troupeauxAPortee)
        {
            troupeau.CacherNourriture();
        }

        positionBonusPeche = new Vector3();
        bonusPeche.CacherBonusPeche();
    }
    #endregion


    #region NOURRITURE
    private void CalculerNourriture()
    {
        gainNourriture = 0;
        gainNourriture += tuileActuelle.terrainTuile.nourriture;
        foreach (TuileManager tuile in tuileActuelle.connections)
        {
            if(tuile)
            {
                gainNourriture += tuile.GetComponent<TuileManager>().terrainTuile.nourriture;
            }
            
        }
        CheckerNourritureEau();
        CheckerNourritureTroupeau();

        excedentNourriture = gainNourriture - population;
    }

    private void CheckerNourritureTroupeau()
    {
        troupeauxAPortee.Clear();

        LayerMask maskTroupeau = LayerMask.GetMask("Troupeau");

        float index = 0;

        TuileManager tM = tuileActuelle.GetComponent<TuileManager>();

        for (int i = 0; i < tM.nombreConnections; i++)
        {
            float x = Mathf.Cos(index) * tM.tailleTuile;
            float y = Mathf.Sin(index) * tM.tailleTuile;

            Vector2 direction = new Vector2(x, y);

            RaycastHit2D checkTroupeau = Physics2D.Raycast(transform.position, direction, tM.tailleTuile, maskTroupeau);

            if (checkTroupeau)
            {
                Troupeau troupeau = checkTroupeau.collider.GetComponent<Troupeau>();
                troupeauxAPortee.Add(troupeau);
                gainNourriture += troupeau.nourriture;
            }

            index += Mathf.PI * 2 / tM.nombreConnections;
        }
    }

    private void CheckerNourritureEau()
    {
        bool fleuve = false;
        LayerMask maskRiviere = LayerMask.GetMask("Fleuve");
        LayerMask maskTuile = LayerMask.GetMask("Tuile");

        float index = 0;

        TuileManager tM = tuileActuelle.GetComponent<TuileManager>();

        for (int i = 0; i < tM.nombreConnections; i++)
        {
            float x = Mathf.Cos(index) * tM.tailleTuile;
            float y = Mathf.Sin(index) * tM.tailleTuile;

            Vector2 direction = new Vector2(x, y);

            RaycastHit2D checkFleuve = Physics2D.Raycast(transform.position, direction, tM.tailleTuile, maskRiviere);

            if (checkFleuve)
            {
                gainNourriture += gainNourriturePeche;
                positionBonusPeche = checkFleuve.point;
                fleuve = true;
                break;
            }
            index += Mathf.PI * 2 / tM.nombreConnections;
        }

        if (!fleuve)
        {
            for (int i = 0; i < tM.nombreConnections; i++)
            {
                float x = Mathf.Cos(index) * tM.tailleTuile;
                float y = Mathf.Sin(index) * tM.tailleTuile;

                Vector2 direction = new Vector2(x, y);

               
                RaycastHit2D checkTuile = Physics2D.Raycast(transform.position, direction, tM.tailleTuile, maskTuile);

                if (checkTuile)
                {
                    if(checkTuile.collider.GetComponent<TuileManager>().terrainTuile.ettendueEau)
                    {
                        gainNourriture += gainNourriturePeche;
                        positionBonusPeche = checkTuile.point;

                        break;
                    }
                }

                index += Mathf.PI * 2 / tM.nombreConnections;
            }
        }
    }
    #endregion


    public void Selectionner(bool selectionner)
    {
        modeCampement = selectionner;
        demographie.AfficherIntefacePop(modeCampement);
    }


    public TuileManager Destination
    {
        set
        {
            cheminASuivre = pathFinder.TrouverChemin(tuileActuelle, value);
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

                //EtreDeselectionne();
                //revendication.RevendiquerTerritoire(tuileActuelle, false);

                TrouverTuileActuelle();
                tuilesAPortee = pathFinder.CreerGrapheTuilesAPortee(tuileActuelle, pointsAction, false);
                
                //revendication.RevendiquerTerritoire(tuileActuelle, true);
                //EtreSelectionne();
            }
        }
    }



    private void SeDeplacerALaProchaineTuile(Vector3 direction)
    {
        transform.position = Vector3.MoveTowards(transform.position, direction, vitesse*Time.deltaTime);
    }
}
