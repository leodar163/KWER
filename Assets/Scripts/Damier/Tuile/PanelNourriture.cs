using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PanelNourriture : MonoBehaviour
{
    TextMeshProUGUI txtMesh;
    string nourriture;

    private void OnEnable()
    {
        AfficherNourriture();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void AfficherNourriture()
    {
        //nourriture = GetComponentInParent<TuileManager>().terrainTuile.nourriture.ToString();
        txtMesh = GetComponentInChildren<TextMeshProUGUI>();
        txtMesh.text = nourriture;
    }
}
