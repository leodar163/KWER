using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InterfacePopulation : MonoBehaviour
{
    Tribu tribu;
    TextMeshProUGUI stock;

    // Start is called before the first frame update
    void Start()
    {
        stock = GetComponentInChildren<TextMeshProUGUI>();
        tribu = FindObjectOfType<Tribu>();
        MiseAJourPopulation();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MiseAJourPopulation()
    {
    }
}
