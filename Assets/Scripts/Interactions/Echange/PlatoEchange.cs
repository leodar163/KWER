using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlatoEchange : MonoBehaviour
{
    private bool interactionActive;

    private List<SlotEchange> listeSlots = new List<SlotEchange>();
    public List<string> ressourcesEchangees = new List<string>();

    [SerializeField] private InventaireEchange inventaire;

    [SerializeField] private Button boutonInventaire;
    [SerializeField] private GameObject slotEchangeBase;
    [HideInInspector] public Tribu tribu;

    private StockRessource stockEngage;

    public StockRessource StockEngage
    {
        get
        {
            if (stockEngage) Destroy(stockEngage);
            AssignerStockEngage();
            return stockEngage;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        boutonInventaire.onClick.AddListener(() => inventaire.AfficherInventaire(tribu,this));
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AjouterSlotEchange(Consommable consommable)
    {
        if (!ressourcesEchangees.Contains(consommable.nom))
        {
            ressourcesEchangees.Add(consommable.nom);
            GameObject nvSlot = Instantiate(slotEchangeBase, transform);
            SlotEchange nvSlotEchange = nvSlot.GetComponent<SlotEchange>();

            nvSlotEchange.Tribu = tribu;
            nvSlotEchange.Consommable = consommable;

            listeSlots.Add(nvSlotEchange);
        }
    }

    public void AjouterSlotEchange(Ressource ressource)
    {
        if (!ressourcesEchangees.Contains(ressource.nom))
        {
            ressourcesEchangees.Add(ressource.nom);
            GameObject nvSlot = Instantiate(slotEchangeBase, transform);
            SlotEchange nvSlotEchange = nvSlot.GetComponent<SlotEchange>();

            listeSlots.Add(nvSlotEchange);
        }
    }

    public void MAJPlato()
    {
        if (inventaire.gameObject.activeSelf && interactionActive)
        {
            ActiverInteraction(false);
        }
        else if (!inventaire.gameObject.activeSelf && !interactionActive)
        {
            ActiverInteraction(true);
        }
    }

    public void ActiverInteraction(bool activer)
    {
        interactionActive = activer;

        foreach (SlotEchange slot in listeSlots)
        {
            slot.ActiverSlot(activer);
        }
    }

    public void RendreToutLArgent()
    {
        foreach(SlotEchange slot in listeSlots)
        {
            slot.RendreLArgent();
        }
    }

    private void AssignerStockEngage()
    {
        stockEngage = gameObject.AddComponent<StockRessource>();

        Production prod = ScriptableObject.CreateInstance<Production>();
        prod.Initialiser();

        foreach (SlotEchange slot in listeSlots)
        {
            if (slot.Ressource) prod.AugmenterGain(slot.Ressource.nom, slot.quantite);
            else if (slot.Consommable) stockEngage.consommables.Add(slot.Consommable);
        }

        stockEngage.RessourcesEnStock = prod;
    }

    public void NettoyerPlato()
    {
        foreach (SlotEchange slot in listeSlots)
        {
            Destroy(slot.gameObject);
        }
        listeSlots.Clear();
    }
}
