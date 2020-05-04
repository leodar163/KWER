using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Troupeau : MonoBehaviour
{
    
    PanelBouffeUnite panelNourriture;
    public float nourriture;


    // Start is called before the first frame update
    void Start()
    {
        panelNourriture = GetComponentInChildren<PanelBouffeUnite>();
        
        CacherNourriture();
    }

    
    // Update is called once per frame
    void Update()
    {

    }

    #region INTERFACE
    public void AfficherNourriture()
    {
        panelNourriture.AfficherGainNourriture(nourriture);
    }
    public void CacherNourriture()
    {
        panelNourriture.CacherGainNourriture();
    }
    #endregion





}
