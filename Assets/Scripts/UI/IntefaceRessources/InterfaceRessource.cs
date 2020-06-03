using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
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

    public UnityEvent EventInterfaceMAJ;

    // Start is called before the first frame update
    void Start()
    {
        cela = this;
        PanelInfoRessource.SetActive(false);
        GenererPanelsInfo();
    }

    // Update is called once per frame
    void Update()
    {
        
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

    public void MiseAJourCapacite(Production capacite)
    {
        for (int i = 0; i < listePanelsInfoRessource.Count; i++)
        {
            listePanelsInfoRessource[i].Capacite = capacite.gains[i];
        }
        EventInterfaceMAJ.Invoke();
    }

    public void MiseAJourStock(Production stock)
    {
        for (int i = 0; i < listePanelsInfoRessource.Count; i++)
        {
            listePanelsInfoRessource[i].Stock = stock.gains[i];
        }
        EventInterfaceMAJ.Invoke();
    }

    public void MiseAjourGain(Production gain)
    {
        for (int i = 0; i < listePanelsInfoRessource.Count; i++)
        {
            listePanelsInfoRessource[i].Gain = gain.gains[i];
        }
        EventInterfaceMAJ.Invoke();
    }
}
