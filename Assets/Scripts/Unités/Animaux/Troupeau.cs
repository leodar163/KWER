using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

[RequireComponent(typeof(Migration))]
public class Troupeau : Pion
{
    public Migration migration;
    public Hostile hostile;
    public ProductionTroupeau productionTroupeau;
    public SpriteRenderer spriteRenderer;

    [HideInInspector] public bool domesticable;
    [HideInInspector] public bool megaFaune;
    [HideInInspector] public bool predateur;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }


    // Update is called once per frame
    void Update()
    {

    }

    #region IA
    public override void DebutTour()
    {
        base.DebutTour();
        migration.InitialiserPointsDeplacement();
        StartCoroutine(DeroulerTour());
    }

    
    private IEnumerator DeroulerTour()
    {
        if(predateur)hostile.TrouverCiblesAPortee();

        while(migration.PeutBouger || (hostile != null && hostile.PeutAttaquer))
        {
            if(predateur && (hostile != null && hostile.PeutAttaquer))
            {
                StartCoroutine(hostile.Attaquer());
            }
            else
            {
                if(Calendrier.Actuel.Hiver)
                {
                    StartCoroutine(migration.Migrer());
                }
            }

            yield return new WaitUntil(() => aFaitUneAction);
            aFaitUneAction = false;
        }

        aPasseSonTour = true;
    }
    #endregion


    #region INTERFACE

    #endregion

    public override void TrouverTuileActuelle()
    { 
        if (tuileActuelle)
        {
            revendication.RevendiquerTerritoire(tuileActuelle, false);
            tuileActuelle.productionTuile.ReinitBonusOutil();
            tuileActuelle.estOccupee = false;
            tuileActuelle.estInterdite = false;
        }
        LayerMask layerMaskTuile = LayerMask.GetMask("Tuile");

        Collider2D checkTuile = Physics2D.OverlapBox(transform.position, new Vector2(0.1f, 0.1f), 0, layerMaskTuile);

        if (checkTuile)
        {
            tuileActuelle = checkTuile.gameObject.GetComponent<TuileManager>();
            transform.position = new Vector3(tuileActuelle.transform.position.x, tuileActuelle.transform.position.y, transform.position.z);
        }

        tuileActuelle.estOccupee = true;
        if (predateur || megaFaune) tuileActuelle.estInterdite = false;
        if (predateur) hostile.TrouverCiblesAPortee();
        revendication.RevendiquerTerritoire(tuileActuelle, true);
        productionTroupeau.FertiliserTuile();
    }


}
