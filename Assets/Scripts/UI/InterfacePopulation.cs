using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InterfacePopulation : MonoBehaviour
{
    UniteManager tribu;
    TextMeshProUGUI stock;

    // Start is called before the first frame update
    void Start()
    {
        stock = GetComponentInChildren<TextMeshProUGUI>();
        tribu = FindObjectOfType<UniteManager>();
        MiseAJourPopulation();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MiseAJourPopulation()
    {
        stock.text = tribu.population.ToString();
    }
}
