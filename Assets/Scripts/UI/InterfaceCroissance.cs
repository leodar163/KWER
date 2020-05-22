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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
