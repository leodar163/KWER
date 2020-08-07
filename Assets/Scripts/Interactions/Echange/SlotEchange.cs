using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SlotEchange : MonoBehaviour
{

    [Header("Boutons")]
    [SerializeField] private Button boutonPlus;
    private InfoBulle ibBoutonPlus;
    [SerializeField] private Button boutonPlusPlus;
    private InfoBulle ibBoutonPlusPlus;
    [SerializeField] private Button boutonMoins;
    private InfoBulle ibBoutonMoins;
    [SerializeField] private Button boutonMoinsMoins;
    private InfoBulle ibBoutonMoinsMoins;
    [Space]
    [Header("Affichage Ressource")]
    [SerializeField] private Image icone;
    [SerializeField] private TextMeshProUGUI quantite;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
