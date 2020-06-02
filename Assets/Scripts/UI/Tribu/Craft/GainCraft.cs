using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GainCraft : MonoBehaviour
{
    
    [SerializeField] private TextMeshProUGUI tMP;
    [SerializeField] private Image image;

    private Ressource ressource;

    public float montant
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
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        montant = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MarquerInsuffisant(bool insuffisant)
    {
       if(insuffisant)
        {
            if (ListeCouleurs.Defaut) tMP.color = ListeCouleurs.Defaut.couleurAlerteTexteInterface;
        }
       else
        {
            if (ListeCouleurs.Defaut) tMP.color = ListeCouleurs.Defaut.couleurDefautTexteInterface;
        }
    }

}
