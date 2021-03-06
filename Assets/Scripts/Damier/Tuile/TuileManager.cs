﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TuileManager : MonoBehaviour
{
    public bool tuileHexa = false;

    [Header("Production")]
    public ProductionTuile productionTuile;
    public AmenagementTuile tuileAmenagement;

    [Header("Graphe")]
    public int nombreConnections;
    public List<TuileManager> connections; //Liste des noeuds directement relié à celui-ci
    public List<float> connectionsDistance; //Poids entre les noeuds / l'index est le même que celui des connections
    [HideInInspector] public float distance = 0;
    [HideInInspector] public bool parcouru = false;
    [HideInInspector] public bool aPortee = false;
    [HideInInspector] public TuileManager predecesseur;
    public bool estOccupee;
    public bool estInterdite;
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

        tuileAmenagement.RevetirSpriteSaison();
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

            Init();
    }

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
    
    public void MontrerPredecesseur()
    {
        if (predecesseur)
        {
            Debug.DrawLine(garniture.transform.position + Vector3.up, predecesseur.garniture.transform.position + Vector3.up, Color.cyan, float.PositiveInfinity);
        }
        else ColorerTuile(Color.blue);
    }
}
