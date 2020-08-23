using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Events;

public class InterfaceRessource : MonoBehaviour
{
    private static InterfaceRessource cela;
    public static InterfaceRessource Actuel
    {
        get
        {
            if(cela == null)
            {
                cela = FindObjectOfType<InterfaceRessource>();
            }
            return cela;
        }
    }

    [SerializeField] private GameObject PanelInfoRessource;
    private List<PanelInfoRessource> listePanelsInfoRessource = new List<PanelInfoRessource>();

    [HideInInspector] public UnityEvent EventInterfaceMAJ;

    [Space]
    [Header("Consommables")]
    [SerializeField] private GameObject panelConsommables;
    [SerializeReference] private GameObject slotBase;
    private List<SlotConsommable> listeSlotsConsommable = new List<SlotConsommable>();

    // Start is called before the first frame update
    void Start()
    {
        cela = this;
        PanelInfoRessource.SetActive(false);
        slotBase.SetActive(false);
        GenererPanelsInfo();

        StartCoroutine(MAJInterfaceRessource());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator MAJInterfaceRessource()
    {
        if(InfoTribus.TribukiJoue)
        {
            MiseAJourCapacite(InfoTribus.TribukiJoue.stockRessources.CapaciteDeStockage);
            MiseAjourGain(InfoTribus.TribukiJoue.stockRessources.ProjectionGain);
            MiseAJourStock(InfoTribus.TribukiJoue.stockRessources.RessourcesEnStock);
            MiseAJourConsommables();
        }
        

        yield return new WaitForEndOfFrame();

        StartCoroutine(MAJInterfaceRessource());
        EventInterfaceMAJ.Invoke();
    }

    private void GenererPanelsInfo()
    {
        for (int i = 0; i < ListeRessources.Defaut.listeDesRessources.Length; i++)
        {
            GameObject nvInfoRessource = Instantiate(PanelInfoRessource, transform);
            listePanelsInfoRessource.Add(nvInfoRessource.GetComponent<PanelInfoRessource>());

            listePanelsInfoRessource[i].Ressource = ListeRessources.Defaut.listeDesRessources[i];

            nvInfoRessource.SetActive(true);
        }
    }

    private void MiseAJourConsommables()
    {
        GenererConsommable(InfoTribus.TribukiJoue.stockRessources.emplacementConsommable - listeSlotsConsommable.Count);

        //assignation des consommable aux slots
        for (int i = 0; i < listeSlotsConsommable.Count; i++)
        {
            if (InfoTribus.TribukiJoue.stockRessources.consommables.Count > i) 
                listeSlotsConsommable[i].ConsommableAssigne = InfoTribus.TribukiJoue.stockRessources.consommables[i];

            else
            {
                listeSlotsConsommable[i].ConsommableAssigne = null;
            }
            
        }
    }


    private void GenererConsommable(int nombre)
    {
        if (nombre > 0)
        {
            for (int i = 0; i < nombre; i++)
            {
                GameObject nvSlot = Instantiate(slotBase, panelConsommables.transform);
                listeSlotsConsommable.Add(nvSlot.GetComponent<SlotConsommable>());
                nvSlot.SetActive(true);
            }
        }
        else if (nombre < 0)
        {
            for (int i = 0; i < math.abs(nombre); i++)
            {
                Destroy(listeSlotsConsommable[listeSlotsConsommable.Count].gameObject);
            }
        }
    }

    private void MiseAJourCapacite(Production capacite)
    {
        for (int i = 0; i < listePanelsInfoRessource.Count; i++)
        {
            listePanelsInfoRessource[i].Capacite = capacite.gains[i];
        }
    }

    private void MiseAJourStock(Production stock)
    {
        for (int i = 0; i < listePanelsInfoRessource.Count; i++)
        {
            listePanelsInfoRessource[i].Stock = stock.gains[i];
        }
    }

    private void MiseAjourGain(Production gain)
    {
        for (int i = 0; i < listePanelsInfoRessource.Count; i++)
        {
            listePanelsInfoRessource[i].Gain = gain.gains[i];
        }
    }
}
