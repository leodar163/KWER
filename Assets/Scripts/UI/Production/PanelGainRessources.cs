using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    public void afficherRessources(ProductionTuile.Production prod)
    {
        ReinitAffichage();

        if(prod.gainNourriture > 0)
        {
            AjouterAffichage(prod.gainNourriture, ListeIcones.ParDefaut.iconeNourriture);
        }
        if(prod.gainPeau > 0)
        {
            AjouterAffichage(prod.gainPeau, ListeIcones.ParDefaut.iconePeau);
        }
        if(prod.gainPierre > 0)
        {
            AjouterAffichage(prod.gainPierre, ListeIcones.ParDefaut.iconePierre);
        }
        if(prod.gainPigment > 0)
        {
            AjouterAffichage(prod.gainPigment, ListeIcones.ParDefaut.iconePigment);
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
