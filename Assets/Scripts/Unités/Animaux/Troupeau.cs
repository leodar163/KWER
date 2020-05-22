using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

[RequireComponent(typeof(Migration))]
public class Troupeau : MonoBehaviour
{
    [SerializeField] private Migration migration;
    public TuileManager tuileActuelle;

    public int nbrSlot;
    public Production gainProduction;

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
