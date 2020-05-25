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

    public MonoBehaviour parent;

    // Start is called before the first frame update
    void Start()
    {
        affichageRessource.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //affiche la liste de toutes les ressources produites ou consommées
    public void AfficherRessources(Production prod)
    {
        ReinitAffichage();
        for (int i = 0; i < prod.gains.Length; i++)
        {
            if (prod.gains[i] != 0)
            {
                AjouterAffichage(prod.gains[i], ListeIcones.Defaut.TrouverIconeRessource(ListeRessources.Defaut.listeDesRessources[i].nom), i);
            }
        }
        ReorganiserAffichages();
    }


    //Ajoute une ressource dans l'affichage général
    private void AjouterAffichage(float gain, Sprite icone, int indexRessource)
    {
        GameObject nvAffichage = Instantiate(affichageRessource, transform);

        TextMeshProUGUI txtMP = nvAffichage.GetComponentInChildren<TextMeshProUGUI>(true);

        if(gain > 0)
        {
            txtMP.color = Color.black;
             txtMP.text = "+" + gain;
        }
        else if(gain < 0)
        {
            if (parent is Exploitation)
            {
                Exploitation exploitation = (Exploitation)parent;

                //si y a moins de ressource en stock qu'il n'en faut pour créer l'objet
                if (exploitation.expedition.tribu.stockRessources.ressourcesEnStock.gains[indexRessource] < gain)
                {
                    txtMP.color = Color.red;
                }
            }
            else if(parent is PanelRecette)
            {
                PanelRecette panelRecette = (PanelRecette)parent;

                //si y a moins de ressource en stock qu'il n'en faut pour créer l'objet
                if (panelRecette.craft.campement.tribu.stockRessources.ressourcesEnStock.gains[indexRessource] < gain)
                {
                    txtMP.color = Color.red;
                }
            }
            else
            {
                Debug.LogError("PanelGainRessource de " + name + " n'a pas le bon type de parent. Il faut un Exploitation ou un PanelRecette");
            }
            txtMP.text = gain.ToString();
        }
       


        nvAffichage.GetComponentInChildren<Image>(true).sprite = icone;
        nvAffichage.GetComponent<RectTransform>().sizeDelta = affichageRessource.GetComponent<RectTransform>().sizeDelta;
        nvAffichage.GetComponent<RectTransform>().pivot = affichageRessource.GetComponent<RectTransform>().pivot;
        nvAffichage.GetComponent<RectTransform>().position = affichageRessource.GetComponent<RectTransform>().position;


        nvAffichage.SetActive(true);

        listeAffichages.Add(nvAffichage);
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
    private void ReorganiserAffichages()
    {
        float hauteurAffichage = affichageRessource.GetComponent<RectTransform>().rect.height;

        for (int i = 0; i < listeAffichages.Count; i++)
        {
            listeAffichages[i].GetComponent<RectTransform>().position += new Vector3(0, hauteurAffichage * i);
        }
    }
}
