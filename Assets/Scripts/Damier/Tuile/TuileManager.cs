using UnityEngine;


public class TuileManager : MonoBehaviour
{
    public bool tuileHexa = false;

    [SerializeField]GameObject garniture;
    SpriteRenderer spriteGarniture;
    SpriteRenderer spriteBase;
    int indexGarniture = -1;
    [SerializeField] Sprite[] spritesBase;
    [SerializeField] Sprite[] spritesBaseHiver;
    int indexBase = -1;

    [HideInInspector]public TuileManager predecesseur;
    public int nombreConnections;
    public TuileManager[] connections; //Liste des noeuds directement relié à celui-ci
    public float[] connectionsDistance; //Poids entre les noeuds / l'index est le même que celui des connections

    [HideInInspector]public float tailleTuile;

    public float hauteur = 0;
    
    public float distance = 0;
    public bool parcouru = false;
    public bool aPortee = false;

    private bool jeuLance = false; 

    public Color couleurTuileAPortee = Color.white;
    public Color couleurTuileSurChemin = Color.white;


    private PanelNourriture panelNourriture;
    private PanelInterdictionTuile panelInterdition;


    [SerializeField]public TuileTerrain terrainTuile;
    


    public int decalage;

    public Revendication revendicateur; //Celui qui revendique la tuile
    public bool estRevendiquee = false; //une tuile interdite ne produit pas de nourriture. Un tuile est interdite quand un ennemi la contrôle.

    private void Awake()
    {
        //Init();

    }

    // Start is called before the first frame update
    void Start()
    {
        jeuLance = true;
        
        TrouverConnections(nombreConnections);
        CacherInterfaceTuile();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    } 

    public void Init()
    {
        panelInterdition = GetComponentInChildren<PanelInterdictionTuile>();
        panelNourriture = GetComponentInChildren<PanelNourriture>();
        revendicateur = null;

        if(!terrainTuile)
        {
            terrainTuile = FindObjectOfType<TerrainDefaut>();
        }
    
        spriteBase = GetComponent<SpriteRenderer>();
        indexBase = Random.Range(0, spritesBase.Length);
        spriteBase.sprite = spritesBase[indexBase];

        spriteGarniture = garniture.GetComponent<SpriteRenderer>();
        indexGarniture = Random.Range(0, terrainTuile.garnitures.Length);
        spriteGarniture.sprite = terrainTuile.garnitures[indexGarniture];

        tailleTuile = spriteBase.bounds.size.x;
    }

    public void RevetirManteauHiver(bool hiver)
    {
        if (hiver)
        {
            if (terrainTuile.garnituresHivernales.Length == terrainTuile.garnitures.Length)
            {
                spriteBase.sprite = spritesBaseHiver[indexBase];
                spriteGarniture.sprite = terrainTuile.garnituresHivernales[indexGarniture];
            }
        }
        else
        {
            spriteBase.sprite = spritesBase[indexBase];
            spriteGarniture.sprite = terrainTuile.garnitures[indexGarniture];
        }
    }

    private void TrouverConnections(int nbrConnectionAttendue, float decalageDegre)
    {

        connections = new TuileManager[nbrConnectionAttendue];
        connectionsDistance = new float[nbrConnectionAttendue];

        LayerMask maskRiviere = LayerMask.GetMask("Fleuve");
        LayerMask maskTuile = LayerMask.GetMask("Tuile");

        float index = 0 + decalageDegre;

        for (int i = 0; i < nbrConnectionAttendue; i++)
        {
            float x = Mathf.Cos(index) * tailleTuile;
            float y = Mathf.Sin(index) * tailleTuile;

            Vector2 direction = new Vector2(x, y);

            //Debug.DrawRay(transform.position, direction, Color.white, Time.deltaTime);



            RaycastHit2D checkRiviere = Physics2D.Raycast(transform.position, direction,tailleTuile, maskRiviere);
            RaycastHit2D checkTuile = Physics2D.Raycast(transform.position, direction, tailleTuile, maskTuile);

            if (checkTuile)
            {
                connections[i] = checkTuile.collider.GetComponent<TuileManager>();
                connectionsDistance[i] = checkTuile.collider.GetComponent<TuileManager>().terrainTuile.coutFranchissement; 

                if (checkRiviere)
                {

                    connectionsDistance[i]++;
                }
            }
            

            index += Mathf.PI * 2 / nbrConnectionAttendue;
        }
    }

    private void TrouverConnections(int nbrConnectionAttendue)
    {

        connections = new TuileManager[nbrConnectionAttendue];
        connectionsDistance = new float[nbrConnectionAttendue];

        LayerMask maskRiviere = LayerMask.GetMask("Fleuve");
        LayerMask maskTuile = LayerMask.GetMask("Tuile");

        float index = 0;

        for (int i = 0; i < nbrConnectionAttendue; i++)
        {
            float x = Mathf.Cos(index) * tailleTuile;
            float y = Mathf.Sin(index) * tailleTuile;

            Vector2 direction = new Vector2(x, y);

            //Debug.DrawRay(tran  sform.position, direction, Color.white, Time.deltaTime);



            RaycastHit2D checkRiviere = Physics2D.Raycast(transform.position, direction, tailleTuile, maskRiviere);
            RaycastHit2D checkTuile = Physics2D.Raycast(transform.position, direction, tailleTuile, maskTuile);

            if (checkTuile)
            {
                connections[i] = checkTuile.collider.GetComponent<TuileManager>();
                connectionsDistance[i] = checkTuile.collider.GetComponent<TuileManager>().terrainTuile.coutFranchissement;

                if (checkRiviere)
                {

                    connectionsDistance[i]++;
                }
            }


            index += Mathf.PI * 2 / nbrConnectionAttendue;
        }
    }

    


    public void SetTerrain(TuileTerrain terrain)
    {
        terrainTuile = terrain;
        //print(terrainTuile.nom);
        //print(terrain.nom);

            Init();
    }

    /* OBSOLET
    private void TrouverConnections()//     /!\  Penser à transformer le tablo de connection pour intégrer les poids des arrêtes /!\
    {

        if (tuileHexa)// On pourrait transformer àa en algorithme qui suit les rayons d'un cercle, un truc dans le genre. Avec sinus et cosinus.
        {
            connections = new GameObject[6];

            Vector2 positionObjet = transform.position;

            LayerMask layerMaskTuile = LayerMask.GetMask("Tuile");

            Collider2D checkDroite = Physics2D.OverlapBox(positionObjet + Vector2.right * tailleTuile, new Vector2(tailleTuile / 4, tailleTuile / 4), 0, layerMaskTuile);
            Collider2D checkGauche = Physics2D.OverlapBox(positionObjet - Vector2.right * tailleTuile, new Vector2(tailleTuile / 4, tailleTuile / 4), 0, layerMaskTuile);
            Collider2D checkDroiteHaut = Physics2D.OverlapBox(positionObjet + new Vector2(tailleTuile/2, tailleTuile), new Vector2(tailleTuile / 4, tailleTuile / 4), 0, layerMaskTuile);
            Collider2D checkDroiteBas = Physics2D.OverlapBox(positionObjet + new Vector2(tailleTuile / 2, -tailleTuile), new Vector2(tailleTuile / 4, tailleTuile / 4), 0, layerMaskTuile);
            Collider2D checkGaucheHaut = Physics2D.OverlapBox(positionObjet + new Vector2(-tailleTuile / 2, tailleTuile), new Vector2(tailleTuile / 4, tailleTuile / 4), 0, layerMaskTuile);
            Collider2D checkGaucheBas = Physics2D.OverlapBox(positionObjet + new Vector2(-tailleTuile / 2, -tailleTuile), new Vector2(tailleTuile / 4, tailleTuile / 4), 0, layerMaskTuile);


            if (checkDroite)
            {
                connections[0] = checkDroite.gameObject;
            }
            if (checkGauche)
            {
                connections[1] = checkGauche.gameObject;
            }
            if (checkDroiteHaut)
            {
                connections[2] = checkDroiteHaut.gameObject;
            }
            if (checkDroiteBas)
            {
                connections[3] = checkDroiteBas.gameObject;
            }
            if (checkGaucheHaut)
            {
                connections[4] = checkGaucheHaut.gameObject;
            }
            if (checkGaucheBas)
            {
                connections[5] = checkGaucheBas.gameObject;
            }
        }
        else
        {
            connections = new GameObject[4];

            Vector2 positionObjet = transform.position;

            LayerMask layerMaskTuile = LayerMask.GetMask("Tuile");

            Collider2D checkHaut = Physics2D.OverlapBox(positionObjet + Vector2.up * tailleTuile, new Vector2(tailleTuile / 2, tailleTuile / 2), 0, layerMaskTuile);
            Collider2D checkBas = Physics2D.OverlapBox(positionObjet + -Vector2.up * tailleTuile, new Vector2(tailleTuile / 2, tailleTuile / 2), 0, layerMaskTuile);
            Collider2D checkDroite = Physics2D.OverlapBox(positionObjet + Vector2.right * tailleTuile, new Vector2(tailleTuile / 2, tailleTuile / 2), 0, layerMaskTuile);
            Collider2D checkGauche = Physics2D.OverlapBox(positionObjet + -Vector2.right * tailleTuile, new Vector2(tailleTuile / 2, tailleTuile / 2), 0, layerMaskTuile);


            if (checkHaut)
            {
                connections[0] = checkHaut.gameObject;
            }
            if (checkBas)
            {
                connections[1] = checkBas.gameObject;
            }
            if (checkDroite)
            {
                connections[2] = checkDroite.gameObject;
            }
            if (checkGauche)
            {
                connections[3] = checkGauche.gameObject;
            }
        }
    }
    */

    #region INTERFACE


    public void AfficherInterdiction(bool afficher)
    {
        if(panelInterdition)
        {
            panelInterdition.gameObject.SetActive(afficher);
        }
    }

    private void AfficherNourriture(bool afficher)
    {
        if(panelNourriture)
        {
            panelNourriture.gameObject.SetActive(afficher);
        }
    }

    public void AfficherInterfaceTuile()
    {
        AfficherNourriture(true);
        //AfficherInterdiction(estRevendiquee);
    }

    public void CacherInterfaceTuile()
    {
        AfficherNourriture(false);
        AfficherInterdiction(false);
    }
    #endregion


    public void EstAPortee()
    {
        aPortee = true;
    }

    public void ColorerTuile(Color couleur)
    {
        spriteBase.color = couleur;
    }

    public void TuileReinit()
    {
        predecesseur = null;
        distance = 0;
        parcouru = false;
        aPortee = false;
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    public void EtreSelectionne()
    {
        ColorerTuile(Color.blue);
        foreach(TuileManager tuile in connections)
        {
            tuile.ColorerTuile(Color.blue);
        }
    }

    public void EtreDeselectionne()
    {

    }

    /* OBSOLET
    public void SetTerrain(Terrains.TypeTerrain nvoTerrain)
    {
        Init();
        sprite.sprite = nvoTerrain.sprite;
        coutFranchissement = nvoTerrain.coutfranchissement;
        estEttendueEau = nvoTerrain.ettendueEau;
        terrain = nvoTerrain;
    }
    */

    

    /* OBSOLET
    public void SetTerrain()
    {
        Init();
        sprite.sprite = terrain.sprite;
        coutFranchissement = terrain.coutfranchissement;
        estEttendueEau = terrain.ettendueEau;
    }
    */
}
