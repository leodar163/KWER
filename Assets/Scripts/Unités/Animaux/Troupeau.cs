using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

[RequireComponent(typeof(Migration))]
public class Troupeau : MonoBehaviour
{
    public Migration migration;
    public Hostile hostile;
    public ProductionTroupeau productionTroupeau;
    public SpriteRenderer spriteRenderer;
    public Revendication revendication;

    [HideInInspector] public bool domesticable;
    [HideInInspector] public bool megaFaune;
    [HideInInspector] public bool predateur;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        if(migration == null)
        {
            migration = GetComponent<Migration>();
        }
    }

    
    // Update is called once per frame
    void Update()
    {

    }

    #region iNTELLIGENCE ARTIFICIELLE
    public void DemarrerTour()
    {
        migration.InitialiserPointsDeplacement();
        StartCoroutine(DeroulerTour());
    }

    [HideInInspector] public bool aFaitUneAction = false;
    private IEnumerator DeroulerTour()
    {
        while(migration.PeutBouger || hostile.PeutAttaquer)
        {
            if(predateur)
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

        TourParTour.Defaut.AnimalPasseTour();
    }
    #endregion


    #region INTERFACE
   
    #endregion





}
