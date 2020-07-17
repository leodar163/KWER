using System.Collections;
using System.Collections.Generic;
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
    public float tauxRafraichissementSeconde = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        cela = this;
        PanelInfoRessource.SetActive(false);
        GenererPanelsInfo();

        StartCoroutine(MAJInterfaceRessource());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator MAJInterfaceRessource()
    {
        MiseAJourCapacite(Tribu.tribuQuiJoue.stockRessources.CapaciteDeStockage);
        MiseAjourGain(Tribu.tribuQuiJoue.stockRessources.ProjectionGain);
        MiseAJourStock(Tribu.tribuQuiJoue.stockRessources.RessourcesEnStock);

        yield return new WaitForSeconds(tauxRafraichissementSeconde);

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

    public void MiseAJourCapacite(Production capacite)
    {
        for (int i = 0; i < listePanelsInfoRessource.Count; i++)
        {
            listePanelsInfoRessource[i].Capacite = capacite.gains[i];
        }
    }

    public void MiseAJourStock(Production stock)
    {
        for (int i = 0; i < listePanelsInfoRessource.Count; i++)
        {
            listePanelsInfoRessource[i].Stock = stock.gains[i];
        }
    }

    public void MiseAjourGain(Production gain)
    {
        for (int i = 0; i < listePanelsInfoRessource.Count; i++)
        {
            listePanelsInfoRessource[i].Gain = gain.gains[i];
        }
    }
}
