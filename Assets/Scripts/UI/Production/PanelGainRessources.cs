using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using TMPro.EditorUtilities;
using System.Security.Policy;

public class PanelGainRessources : MonoBehaviour
{

    [SerializeField] private GameObject affichageRessource;

    private List<GameObject> listeAffichages = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        affichageRessource.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AfficherRessources(Production prod)
    {
        ReinitAffichage();
        for (int i = 0; i < prod.gains.Length; i++)
        {
            if (prod.gains[i] != 0)
            {
                AjouterAffichage(prod.gains[i], ListeIcones.Defaut.TrouverIcone(ListeRessources.Defaut.listeDesRessources[i].nom));
            }
        }
        ReorganiserAffichages();
    }

    private void ReinitAffichage()
    {
        if(listeAffichages.Count > 0)
        {
            foreach(GameObject affichage in listeAffichages)
            {
                Destroy(affichage);
            }
            listeAffichages.Clear();
        }
    }

    private void AjouterAffichage(float gain, Sprite icone)
    {
        GameObject nvAffichage = Instantiate(affichageRessource, transform);

        nvAffichage.GetComponentInChildren<TextMeshProUGUI>(true).text = "+" + gain;
        nvAffichage.GetComponentInChildren<Image>(true).sprite = icone;
        nvAffichage.GetComponent<RectTransform>().sizeDelta = affichageRessource.GetComponent<RectTransform>().sizeDelta;
        nvAffichage.GetComponent<RectTransform>().pivot = affichageRessource.GetComponent<RectTransform>().pivot;
        nvAffichage.GetComponent<RectTransform>().position = affichageRessource.GetComponent<RectTransform>().position;


        nvAffichage.SetActive(true);

        listeAffichages.Add(nvAffichage);
    }

    private void ReorganiserAffichages()
    {
        float hauteurAffichage = affichageRessource.GetComponent<RectTransform>().rect.height;

        for (int i = 0; i < listeAffichages.Count; i++)
        {
            listeAffichages[i].GetComponent<RectTransform>().position += new Vector3(0, hauteurAffichage * i);
        }
    }
}
