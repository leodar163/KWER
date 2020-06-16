using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FenetreEvenement : MonoBehaviour
{
    [SerializeField] private Image illustration;
    [SerializeField] private TextMeshProUGUI titre;
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private GameObject choixBase;
    private List<Choix> listeChoix = new List<Choix>();
    private Evenement evenement;
    public Evenement EvementActuel
    {
        set
        {
            evenement = value;
        }
        get
        {
            return evenement;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
