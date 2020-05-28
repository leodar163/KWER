using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

[RequireComponent(typeof(Migration))]
public class Troupeau : MonoBehaviour
{
    public Migration migration;
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

    public void DemarrerTour()
    {
        TourParTour.Defaut.AnimalPasseTour();
    }



    #region INTERFACE
   
    #endregion





}
