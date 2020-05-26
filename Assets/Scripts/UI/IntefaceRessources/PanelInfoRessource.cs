using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PanelInfoRessource : MonoBehaviour
{
    public TextMeshProUGUI stockCapacite;
    public TextMeshProUGUI gain;
    [SerializeField]private Image icone;
    private Ressource ressource;

    public Ressource Ressource
    {
        set
        {
            ressource = value;
            icone.sprite = ListeIcones.Defaut.TrouverIconeRessource(ressource.nom);
        }
        get
        {
            return ressource;
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
