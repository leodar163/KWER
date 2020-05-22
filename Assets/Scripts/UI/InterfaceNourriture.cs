using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InterfaceNourriture : MonoBehaviour
{
    TextMeshProUGUI gain;


    Tribu tribu;


    void Start()
    {
        gain = GetComponentInChildren<TextMeshProUGUI>();
        tribu = FindObjectOfType<Tribu>();

        MiseAJourTextesNourriture();
    }

   

    public void MiseAJourTextesNourriture()
    {



    }

    void Update()
    {
        
    }
}
