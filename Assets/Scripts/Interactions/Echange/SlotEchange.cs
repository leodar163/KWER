using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SlotEchange : MonoBehaviour
{

    [Header("Boutons")]
    [SerializeField] private Button boutonPlus;
    [SerializeField] private Button boutonPlusPlus;
    [SerializeField] private Button boutonMoins;
    [SerializeField] private Button boutonMoinsMoins;
    [Space]
    [Header("Affichage Ressource")]
    [SerializeField] private Image icone;
    [SerializeField] private TextMeshProUGUI texteQuantite;
    [Header("Ref Parent")]
    [SerializeField] private PlatoEchange plato;

    private bool slotEstActif;

    private Tribu tribu;
    public Tribu Tribu
    {
        set
        {
            tribu = value;
        }
    }

    [HideInInspector] public int quantite;

    /// <summary>
    /// Fait office d'initialisateur pour un slot d'échange ressource
    /// </summary>
    private Ressource ressource;
    public Ressource Ressource
    {
        set
        {
            ressource = value;
            icone.sprite = ressource.icone;
            EngagerQuantiteRessource(1);
        }
        get { return ressource; }
    }

    /// <summary>
    /// Fait office d'initialisateur pour un slot d'échange consommable
    /// </summary>
    private Consommable consommable;
    public Consommable Consommable
    {
        set
        {
            consommable = value;
            icone.sprite = consommable.icone;
            EngagerQuantiteRessource(1);
        }
        get { return consommable; }
    }


    void Start()
    {
        boutonPlus.onClick.AddListener(() => EngagerQuantiteRessource(1));
        boutonPlusPlus.onClick.AddListener(() => EngagerQuantiteRessource(5));
        boutonMoins.onClick.AddListener(() => EngagerQuantiteRessource(-1));
        boutonMoinsMoins.onClick.AddListener(() => EngagerQuantiteRessource(-5));
    }

    // Update is called once per frame
    void Update()
    {
        VerifierBoutons();
    }

    private void VerifierBoutons()
    {
        if(slotEstActif)
        {
            boutonMoinsMoins.interactable = quantite >= 5;
            boutonMoins.interactable = quantite >= 1;

            if(consommable)
            {
                List<Consommable> consommables = tribu.stockRessources.consommables;
                Dictionary<Consommable, int> inventaireConso = new Dictionary<Consommable, int>();

                for (int i = 0; i < consommables.Count; i++)
                {
                    if (inventaireConso.ContainsKey(consommables[i])) inventaireConso[consommables[i]]++;
                    else inventaireConso.Add(consommables[i], 1);
                }

                if(consommables.Contains(consommable))
                {
                    boutonPlusPlus.interactable = inventaireConso[consommable] >= 5;
                    boutonPlus.interactable = inventaireConso[consommable] >= 1;
                }
                else
                {
                    boutonPlusPlus.interactable = false;
                    boutonPlus.interactable = false;
                }
            }
            else if (ressource)
            {
                float quantiteRessource = 
                    tribu.stockRessources.RessourcesEnStock.RecupuererGainRessource(ressource.nom);

                boutonPlusPlus.interactable = quantiteRessource >= 5;
                boutonPlus.interactable = quantiteRessource >= 1;
            }
        }
        else
        {
            boutonMoinsMoins.interactable = false;
            boutonMoins.interactable = false;
            boutonPlusPlus.interactable = false;
            boutonPlus.interactable = false;
        }
    }

    private void EngagerQuantiteRessource(int montant)
    {
        quantite += montant;
        texteQuantite.text = "" + montant;

        if (consommable)
        {
            if (montant > 0)
            {
                for (int i = 0; i < montant; i++)
                {
                    for (int y = 0; y < tribu.stockRessources.consommables.Count; y++)
                    {
                        if (tribu.stockRessources.consommables[y] == consommable)
                        {
                            tribu.stockRessources.consommables.RemoveAt(y);
                            break;
                        }
                    }
                }
            }
            else if (montant < 0)
            {
                for (int i = 0; i < Mathf.Abs(montant); i++)
                {
                    tribu.stockRessources.consommables.Add(consommable);
                }
            }
        }
        else if (ressource)
        {
            tribu.stockRessources.RessourcesEnStock.AugmenterGain(ressource.nom, -montant);
        }

        if (montant <= 0)
        {
            SupprimerSlot();
        }
    }

    private void SupprimerSlot()
    {
        if (consommable) plato.ressourcesEchangees.Remove(consommable.nom);
        else if (ressource) plato.ressourcesEchangees.Remove(ressource.nom);
        plato.listeSlots.Remove(this);
        Destroy(gameObject);
    }

    public void ActiverSlot(bool activer)
    {
        slotEstActif = activer;
    }

    public void RendreLArgent()
    {
        EngagerQuantiteRessource(-quantite);
    }
}
