using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PanelBouffeUnite : MonoBehaviour
{
    TextMeshProUGUI txtMP;

    private void Awake()
    {
        txtMP = GetComponentInChildren<TextMeshProUGUI>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AfficherGainNourriture(float gainNourriture)
    {
        gameObject.SetActive(true);
        string textGain;

        if(gainNourriture >= 0)
        {
            textGain = "+" + gainNourriture;
        }
        else
        {
            textGain = "-" + gainNourriture;
        }

        txtMP.text = textGain;
    }

    public void CacherGainNourriture()
    {
        gameObject.SetActive(false);
    }
}
