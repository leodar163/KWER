using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pion : MonoBehaviour
{

    [HideInInspector] public bool aFaitUneAction = false;
    [HideInInspector] public bool aPasseSonTour = false;
    public Revendication revendication;
    public TuileManager tuileActuelle;

    [Header("Déplacements")]
    public PathFinder pathFinder;
    public float ptsDeplacementDefaut;
    public float ptsDeplacement;
    public float vitesse;
    public bool peutEmbarquer;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        TrouverTuileActuelle();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// Est appelée au début du tour du pion (différent pour chaque type de pion)
    /// </summary>
    public virtual void DebutTour()
    {
        aPasseSonTour = false;
    }
    /// <summary>
    /// Est appelée quand le pion commence son tour (différent pour chaque pion)
    /// </summary>
    public virtual void CommencerTour()
    {
        CameraControle.Actuel.CentrerCamera(transform.position);
    }

    /// <summary>
    /// Est appelée quand le pion a fini de jouer et passe son tour
    /// </summary>
    public virtual void PasserTour()
    {
        aPasseSonTour = true;
    }

    protected virtual void OnDestroy()
    {
        revendication.RevendiquerTerritoire(tuileActuelle, false);
        tuileActuelle.estOccupee = false;
    }

    public virtual void TrouverTuileActuelle()
    {

    }


}
