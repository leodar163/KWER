using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Craft : MonoBehaviour
{
    public Campement campement;
    public List<Recette> recettesDisponibles;
    [SerializeField] private GameObject panelRecette;
    private List<PanelRecette> listePanelsRecette = new List<PanelRecette>();


    private void OnEnable()
    {
        
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
        MisaAJourCraft();
    }

    public void GenererPanelsRecette()
    {
        for (int i = 0; i < recettesDisponibles.Count; i++)
        {
            GameObject nvlRecette = Instantiate(panelRecette, transform);
            RectTransform nvRectT = nvlRecette.GetComponent<RectTransform>();
            RectTransform rectT = panelRecette.GetComponent<RectTransform>();
            PanelRecette panelRec = nvlRecette.GetComponent<PanelRecette>();

            panelRec.Recette = recettesDisponibles[i];
            listePanelsRecette.Add(panelRec);

            nvRectT.sizeDelta = rectT.sizeDelta;
            nvRectT.pivot = rectT.pivot;
            nvRectT.position = rectT.position;

            nvlRecette.gameObject.SetActive(true);
        }
        ReorganiserAffichageRecettes();
    }

    private void AjouterPanelRecette(Recette recette)
    {

        ReorganiserAffichageRecettes();
    }

    private void RetirerPanelRecette(Recette recette)
    {
        ReorganiserAffichageRecettes();
    }

    //place les panels de recette correctement sur l'axe Y
    private void ReorganiserAffichageRecettes()
    {
        float hauteurPanel = panelRecette.GetComponent<RectTransform>().rect.height;

        for (int i = 0; i < listePanelsRecette.Count; i++)
        {
            listePanelsRecette[i].GetComponent<RectTransform>().position -= new Vector3(0, hauteurPanel * i);
            listePanelsRecette[i].RecentrerSLots();
        }
    }

    //detecter s'il y a une différence entre les recettes disponibles et les recettes qui ont déjà un panel.
    //Si oui, ajouter les panels de recette disponible qui n'ont pas encore de panel
    //et/ou retirer les panels de recette qui ne sont pas dispponibles.
    private void MisaAJourCraft()
    {
        
    }
}
