using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InterfaceCroissance : MonoBehaviour
{
    TextMeshProUGUI ptsCroissance;

    Tribu tribu;

    // Start is called before the first frame update
    void Start()
    {
        ptsCroissance = GetComponentInChildren<TextMeshProUGUI>();
        tribu = FindObjectOfType<Tribu>();
        MiseAJourCroissance();
    }

    public void MiseAJourCroissance()
    {
        if (tribu.excedentNourriture >= 0)
        {
            ptsCroissance.text = tribu.ptsCroissance.ToString() + " + " + tribu.excedentNourriture.ToString();
        }
        else
        {
            ptsCroissance.text = tribu.ptsCroissance.ToString() + " + 0";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
