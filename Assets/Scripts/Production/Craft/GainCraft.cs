using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GainCraft : MonoBehaviour
{
    
    [SerializeField] private TextMeshProUGUI tMP;
    [SerializeField] private Image image;
    [SerializeField] private InfoBulle infoBulle;

    private Ressource ressource;

    public float Montant
    {
        get
        {
            return float.Parse(tMP.text);
        }
        set
        {
            tMP.text = "" + value;
        }
    }

    public Ressource Ressource
    {
        get
        {
            return ressource;
        }
        set
        {
            ressource = value;
            image.sprite = ListeIcones.Defaut.TrouverIconeRessource(ressource);
            infoBulle.texteInfoBulle = ressource.texteInfobulle;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Montant = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
