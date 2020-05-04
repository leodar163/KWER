using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InterfaceNourriture : MonoBehaviour
{
    TextMeshProUGUI gain;


    UniteManager tribu;


    void Start()
    {
        gain = GetComponentInChildren<TextMeshProUGUI>();
        tribu = FindObjectOfType<UniteManager>();

        MiseAJourTextesNourriture();
    }

   

    public void MiseAJourTextesNourriture()
    {

        gain.text = "+" + tribu.gainNourriture.ToString();

    }

    void Update()
    {
        
    }
}
