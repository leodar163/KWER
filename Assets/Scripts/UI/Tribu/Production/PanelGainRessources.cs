using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using TMPro.EditorUtilities;
using System.Security.Policy;
using System.Linq;

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

        if (gain > 0)
        {
            txtMP.color = ListeCouleurs.Defaut.couleurDefautTexteInterface;
            
             txtMP.text = "+" + gain;
        }
        else if(gain < 0)
        {
            if (parent is Exploitation)
            {
                txtMP.color = ListeCouleurs.Defaut.couleurAlerteTexteInterface;
            }
            else if(parent is PanelRecette)
            {
                PanelRecette panelRecette = (PanelRecette)parent;

                //si y a moins de ressource en stock qu'il n'en faut pour créer l'objet
                if (panelRecette.craft.campement.tribu.stockRessources.RessourcesEnStock.gains[indexRessource] < Mathf.Abs(gain) && panelRecette.craft.campement.tribu.stockRessources.ProjectionGain.gains[indexRessource] < Mathf.Abs(gain))
                {
                    txtMP.color = ListeCouleurs.Defaut.couleurAlerteTexteInterface;
                }
                else txtMP.color = ListeCouleurs.Defaut.couleurDefautTexteInterface;
            }
            else
            {
                Debug.LogError("PanelGainRessource de " + name + " n'a pas le bon type de parent. Il faut un Exploitation ou un PanelRecette");
            }
            txtMP.text = "" + gain;
        }
       
        nvAffichage.GetComponentInChildren<Image>(true).sprite = icone;
        nvAffichage.SetActive(true);

        listeAffichages.Add(nvAffichage);
    }

    private void MAJCouleurs()
    {
        foreach(GameObject affichage in listeAffichages)
        {
            TextMeshProUGUI txtMP = affichage.GetComponentInChildren<TextMeshProUGUI>(true);
            float gain = float.Parse(txtMP.text);
            int indexRessource = -1;
            if (ListeRessources.Defaut) indexRessource = ListeRessources.Defaut.TrouverIndexRessource(affichage.GetComponentInChildren<Image>(true).sprite);

            if (gain > 0)
            {
                    if (ListeCouleurs.Defaut) txtMP.color = ListeCouleurs.Defaut.couleurDefautTexteInterface;
            }
            else if (gain < 0)
            {
                if (parent is Exploitation)
                {
                    if (ListeCouleurs.Defaut) txtMP.color = ListeCouleurs.Defaut.couleurAlerteTexteInterface;   
                }
                else if (parent is PanelRecette)
                {
                    PanelRecette panelRecette = (PanelRecette)parent;

                    //si y a moins de ressource en stock qu'il n'en faut pour créer l'objet 
                    //et que la production au prochain tour ne permet pas de compenser
                    if (indexRessource > -1 && panelRecette.craft.campement.tribu.stockRessources.RessourcesEnStock.gains[indexRessource] < Mathf.Abs(gain) 
                        && panelRecette.craft.campement.tribu.stockRessources.ProjectionGain.gains[indexRessource] < 0)
                    {
                        if (ListeCouleurs.Defaut) txtMP.color = ListeCouleurs.Defaut.couleurAlerteTexteInterface;
                    }
                    else if (ListeCouleurs.Defaut) txtMP.color = ListeCouleurs.Defaut.couleurDefautTexteInterface;
                }
                else
                {
                    Debug.LogError("PanelGainRessource de " + name + " n'a pas le bon type de parent. Il faut un Exploitation ou un PanelRecette");
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
    private void ReorganiserAffichages()
    {
        float hauteurAffichage = affichageRessource.GetComponent<RectTransform>().rect.height;

        for (int i = 0; i < listeAffichages.Count; i++)
        {
            listeAffichages[i].GetComponent<RectTransform>().position += new Vector3(0, hauteurAffichage * i);
        }
    }
}
