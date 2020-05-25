using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Craft : MonoBehaviour
{
    public Campement campement;
    public List<Recette> recettesDisponibles;
    [SerializeField] private GameObject panelRecette;
    private List<PanelRecette> listePanelsRecette = new List<PanelRecette>();


    private void OnEnable()
    {
        MisaAJourInterfaceCraft();
    }

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AfficherInterfaceCraft(bool afficher)
    {
        gameObject.SetActive(afficher);
        
    }

    public void GenererPanelsRecette()
    {
        RectTransform rectT = panelRecette.GetComponent<RectTransform>();

        for (int i = 0; i < recettesDisponibles.Count; i++)
        {
            if(!RecetteAPanel(recettesDisponibles[i]))
            {
                GameObject nvlRecette = Instantiate(panelRecette, transform);
                RectTransform nvRectT = nvlRecette.GetComponent<RectTransform>();
                PanelRecette panelRec = nvlRecette.GetComponent<PanelRecette>();

                panelRec.Recette = recettesDisponibles[i];
                listePanelsRecette.Add(panelRec);
            

                nvRectT.sizeDelta = rectT.sizeDelta;
                nvRectT.pivot = rectT.pivot;
                nvRectT.position = rectT.position;

                nvlRecette.gameObject.SetActive(true);
            }
        }
        ReorganiserAffichageRecettes();
    }

    private bool RecetteAPanel(Recette recette)
    {
        foreach(PanelRecette panel in listePanelsRecette)
        {
            if(panel.Recette == recette)
            {
                return true;
            }
        }

        return false;
    }

    private void AjouterPanelRecette(Recette recette)
    {
        if(!RecetteAPanel(recette))
        {
            GameObject nvlRecette = Instantiate(panelRecette, transform);
            RectTransform nvRectT = nvlRecette.GetComponent<RectTransform>();
            RectTransform rectT = panelRecette.GetComponent<RectTransform>();
            PanelRecette panelRec = nvlRecette.GetComponent<PanelRecette>();

            panelRec.Recette = recette;
            listePanelsRecette.Add(panelRec);


            nvRectT.sizeDelta = rectT.sizeDelta;
            nvRectT.pivot = rectT.pivot;
            nvRectT.position = rectT.position;

            nvlRecette.gameObject.SetActive(true);
        }
        ReorganiserAffichageRecettes();
    }

    private void RetirerPanelRecette(Recette recette)
    {
        if(RecetteAPanel(recette))
        {
            Destroy(TrouverPanel(recette).gameObject);
        }

        ReorganiserAffichageRecettes();
    }

    private PanelRecette TrouverPanel(Recette recette)
    {
        foreach(PanelRecette panel in listePanelsRecette)
        {
            if(panel.Recette = recette)
            {
                return panel;
            }
        }
        return null;
    }

    //place les panels de recette correctement sur l'axe Y
    private void ReorganiserAffichageRecettes()
    {
        float hauteurPanel = panelRecette.GetComponent<RectTransform>().rect.height;

        for (int i = 0; i < listePanelsRecette.Count; i++)
        {
            listePanelsRecette[i].GetComponent<RectTransform>().position -= new Vector3(0, hauteurPanel * i);
        }
    }

    private List<Recette> RecupRecettesDesPanel()
    {
        List<Recette> recettesDesPanels = new List<Recette>();

        for (int i = 0; i < listePanelsRecette.Count; i++)
        {
            if(listePanelsRecette[i].Recette)
            {
                recettesDesPanels.Add(listePanelsRecette[i].Recette);
            }
        }

        return recettesDesPanels;
    }

    //detecter s'il y a une différence entre les recettes disponibles et les recettes qui ont déjà un panel.
    //Si oui, ajouter les panels de recette disponible qui n'ont pas encore de panel
    //et/ou retirer les panels de recette qui ne sont pas disponibles.
    private void MisaAJourInterfaceCraft()
    {
        List<Recette> recettesAAjouter = new List<Recette>();
        List<Recette> recettesARetirer = new List<Recette>();
        List<Recette> recettesDesPanels = new List<Recette>(RecupRecettesDesPanel());


        //Vérifie que toutes les recettes disponibles ont un panel
        foreach(Recette recette in recettesDisponibles)
        {
            if(!recettesDesPanels.Contains(recette))
            {
                recettesAAjouter.Add(recette);
            }
        }

        //Vérifie que tous les panels ont une recette qui est disponible
        foreach(Recette recette in recettesDesPanels)
        {
            if(!recettesDisponibles.Contains(recette))
            {
                recettesARetirer.Add(recette);
            }
        }

        //retirer les panels dont la recette n'est pas/plus disponible
        foreach (Recette recette in recettesARetirer)
        { 
            RetirerPanelRecette(recette);
        }

        //ajoute des panels pour les recettes disponibles qui n'en ont pas encore
        foreach (Recette recette in recettesAAjouter)
        {
            AjouterPanelRecette(recette);
        }
        
    }

    public void RendreRecetteDisponible(Recette recette)
    {
        if(!recettesDisponibles.Contains(recette))
        {
            recettesDisponibles.Add(recette);
            AjouterPanelRecette(recette);
        }
    }

    public void RendreRecetteIndisponible(Recette recette)
    {
        if(recettesDisponibles.Contains(recette))
        {
            recettesDisponibles.Remove(recette);
            RetirerPanelRecette(recette);
        }
    }
}
