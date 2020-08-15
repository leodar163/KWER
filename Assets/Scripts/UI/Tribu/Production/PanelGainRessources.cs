using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PanelGainRessources : MonoBehaviour
{

    [SerializeField] private GameObject affichageRessource;
    private List<GameObject> listeAffichages = new List<GameObject>();

    public MonoBehaviour parent;

    // Start is called before the first frame update
    void Start()
    {
        InterfaceRessource.Actuel.EventInterfaceMAJ.AddListener(MAJCouleurs);
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
                AjouterAffichage(prod.gains[i], ListeRessources.Defaut.listeDesRessources[i]);
            }
        }
    }


    //Ajoute une ressource dans l'affichage général
    private void AjouterAffichage(float gain, Ressource ressource)
    {
        GameObject nvAffichage = Instantiate(affichageRessource, transform);

        TextMeshProUGUI txtMP = nvAffichage.GetComponentInChildren<TextMeshProUGUI>(true);

        if (gain > 0)
        {
            if (parent is Exploitation) txtMP.text = "+" + gain;
            else if (parent is PanelRecette) txtMP.text = "" + gain;
        }
        else if(gain < 0)
        {
            txtMP.text = "" + gain;
        }
       
        nvAffichage.GetComponentInChildren<Image>(true).sprite = ressource.icone;
        nvAffichage.SetActive(true);
        nvAffichage.GetComponentInChildren<InfoBulle>().texteInfoBulle = ressource.texteInfobulle;

        listeAffichages.Add(nvAffichage);

        MAJCouleurs();
    }

    private void MAJCouleurs()
    {
        foreach(GameObject affichage in listeAffichages)
        {
            TextMeshProUGUI txtMP = affichage.GetComponentInChildren<TextMeshProUGUI>(true);
            float gain = float.Parse(txtMP.text);

            if (gain > 0)
            {
                if (ListeCouleurs.Defaut) txtMP.color = ListeCouleurs.Defaut.couleurDefautTexteInterface;
                else if (parent is PanelRecette) txtMP.text = "" + gain;
            }
            else if (gain < 0)
            {
                if (parent is Exploitation)
                {
                    if (ListeCouleurs.Defaut) txtMP.color = ListeCouleurs.Defaut.couleurAlerteTexteInterface;   
                }
                else
                {
                    Debug.LogError("PanelGainRessource de " + name + " n'a pas le bon type de parent. Il faut un Exploitation ou un PanelRecette");
                }
            }
        }
    }

    public void MarquerRessourceInsuffisante(Ressource ressource)
    {
        if(ListeIcones.Defaut)
        {
            foreach (GameObject affichage in listeAffichages)
            {
                if (affichage.GetComponentInChildren<Image>().sprite == ListeIcones.Defaut.TrouverIconeRessource(ressource))
                {
                    if (ListeCouleurs.Defaut) affichage.GetComponent<TextMeshProUGUI>().color = 
                            ListeCouleurs.Defaut.couleurAlerteTexteInterface;
                }
            }
        }
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
}
