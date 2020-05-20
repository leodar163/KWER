using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

[RequireComponent(typeof(Migration))]
public class Troupeau : MonoBehaviour
{
    [SerializeField] private Migration migration;
    public TuileManager tuileActuelle;

    [SerializeField] private int nbrSlots;
    [SerializeField] private float gainNourriture;
    [SerializeField] private float gainPierre;
    [SerializeField] private float gainPeau;
    [SerializeField] private float gainPigment;

    private ProductionTuile.Production prod;

    public ProductionTuile.Production Prod
    {
        get
        {
            prod = new ProductionTuile.Production(nbrSlots, gainNourriture, gainPierre, gainPeau, gainPigment);
            return prod;
        }
    }

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

    #region INTERFACE
   
    #endregion





}
