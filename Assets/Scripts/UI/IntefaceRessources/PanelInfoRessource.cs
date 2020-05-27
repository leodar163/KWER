using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PanelInfoRessource : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI stockCapacite;
    [SerializeField] private TextMeshProUGUI gain;
    [SerializeField] private Image icone;
    private Ressource ressource;
    private float stock;
    private float capacite;
    private float projoGain;

    public float Stock
    {
        set
        {
            stock = value;
            AfficherStock(stock);
        }
        get
        {
            return stock;
        }
    }

    public float Capacite
    {
        set
        {
            capacite = value;
            AfficherCapacite(capacite);
        }
        get
        {
            return capacite;
        }
    }

    public float Gain
    {
        set
        {
            projoGain = value;
            AfficherGain(projoGain);
        }
        get
        {
            return projoGain;
        }
    }

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

    private void AfficherStock(float nvStock)
    {
        if (ListeCouleurs.Defaut)
        {
            stockCapacite.color = ListeCouleurs.Defaut.couleurDefautTexteInterface;
        }
        stockCapacite.text = nvStock + "/" + capacite;
    }

    private void AfficherCapacite(float nvlCapacite)
    {
        if(ListeCouleurs.Defaut)
        {
            stockCapacite.color = ListeCouleurs.Defaut.couleurDefautTexteInterface;
        }
        
        stockCapacite.text = Stock + "/" + nvlCapacite;
    }

    private void AfficherGain(float nvGain)
    {
        if (nvGain < 0)
        {
            if(ListeCouleurs.Defaut)
            {
                gain.color = ListeCouleurs.Defaut.couleurAlerteTexteInterface;
            }
            gain.text = nvGain.ToString();
        }
        else
        {
            if (ListeCouleurs.Defaut)
            {
                gain.color = ListeCouleurs.Defaut.couleurDefautTexteInterface;
            }
            gain.text = "+" + nvGain;
        }
    }
}
