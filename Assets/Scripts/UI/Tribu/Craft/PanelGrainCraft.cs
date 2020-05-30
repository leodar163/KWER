using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PanelGrainCraft : MonoBehaviour
{
    [SerializeField] private PanelRecette panelRecette;
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
}
