using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pion : MonoBehaviour
{

    [HideInInspector] public bool aFaitUneAction = false;
    [HideInInspector] public bool aPasseSonTour = false;
    public Revendication revendication;

    // Start is called before the first frame update
    protected virtual void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Est appelée au début du tour du pion (différent pour chaque type de pion)
    /// </summary>
    public virtual void DemarrerTour()
    {
        aPasseSonTour = false;
    }
}
