using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TuileManager : MonoBehaviour
{
    public bool tuileHexa = false;

    [Header("Production")]
    public ProductionTuile productionTuile;

    [Header("Graphe")]
    public int nombreConnections;
    public List<TuileManager> connections; //Liste des noeuds directement relié à celui-ci
    public List<float> connectionsDistance; //Poids entre les noeuds / l'index est le même que celui des connections
    [HideInInspector] public float distance = 0;
    [HideInInspector] public bool parcouru = false;
    [HideInInspector] public bool aPortee = false;
    [HideInInspector] public TuileManager predecesseur;
    public bool estOccupee;
    [HideInInspector] public float tailleTuile;
    

    [Header("Sprite")]
    [SerializeField] GameObject garniture;
    SpriteRenderer spriteGarniture;
    SpriteRenderer spriteBase;
    [SerializeField] Sprite[] spritesBase;
    [SerializeField] Sprite[] spritesBaseHiver;
    [SerializeField] int indexGarniture = -1;
    [SerializeField] int indexBase = -1;
    
    
    [Header("Infos")]
    public Color couleurTuileAPortee = Color.white;
    public Color couleurTuileSurChemin = Color.white;

    [SerializeField]public TuileTerrain terrainTuile;

    [Header("Revendication")]
    public RevendicationTuile revendication;

    private void Awake()
    {
        //Init();
        Calendrier.Actuel.EventChangementDeSaison.AddListener(RevetirSpriteSaison);
        spriteBase = GetComponent<SpriteRenderer>();
        spriteGarniture = garniture.GetComponent<SpriteRenderer>();
        TrouverConnections(nombreConnections);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    } 

    public void Init()
    {
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

    public void RevetirSpriteSaison()
    {
        if (Calendrier.Actuel.Hiver)
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


    #region GRAPHE
    private void TrouverConnections(int nbrConnectionAttendue, float decalageDegre)
    {

        connections = new List<TuileManager>();
        connectionsDistance = new List<float>();

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
                connections.Add(checkTuile.collider.GetComponent<TuileManager>());
                connectionsDistance.Add(checkTuile.collider.GetComponent<TuileManager>().terrainTuile.coutFranchissement); 

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

        connections = new List<TuileManager>();
        connectionsDistance = new List<float>();

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
                connections.Add(checkTuile.collider.GetComponent<TuileManager>());
                connectionsDistance.Add(checkTuile.collider.GetComponent<TuileManager>().terrainTuile.coutFranchissement);

                if (checkRiviere)
                {
                    connectionsDistance[i]++;
                }
            }
            index += Mathf.PI * 2 / nbrConnectionAttendue;
        }
    }

    public int RecupIndexConnection(TuileManager connection)
    {
        for (int i = 0; i < connections.Count; i++)
        {
            if (connections[i] == connection) return i;
        }
        return -1;
    }
    #endregion

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

    #endregion


    public void EstAPortee()
    {
        aPortee = true;
    }

    public void ColorerTuile(Color couleur)
    {
        spriteBase.color = couleur;
        spriteGarniture.color = couleur;
    }

    public void TuileReinit()
    {
        predecesseur = null;
        distance = 0;
        parcouru = false;
        aPortee = false;
        ColorerTuile(Color.white);
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
