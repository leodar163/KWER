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
    public override void DemarrerTour()
    {
        base.DemarrerTour();
        migration.InitialiserPointsDeplacement();
        StartCoroutine(DeroulerTour());
    }

    
    private IEnumerator DeroulerTour()
    {
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





}
