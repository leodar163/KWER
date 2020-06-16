using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NvlEvenementTempo", menuName = "Evénements/Evément Temporel")]
public class EvenementTemporel : Evenement
{
    [Range(0, 100)]
    public float probaEte;
    [Range(0, 100)]
    public float probaHiver;

    public string infobulle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
