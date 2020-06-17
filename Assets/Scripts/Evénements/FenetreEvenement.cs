using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEditor.Events;
using UnityEngine.Analytics;

public class FenetreEvenement : MonoBehaviour
{
    [SerializeField] private Image illustration;
    [SerializeField] private TextMeshProUGUI titre;
    [SerializeField] private TextMeshProUGUI description;

    [Header("Gestion des Choix")]
    [SerializeField] private GameObject lesChoix;
    [SerializeField] private GameObject choixBase;
    private List<GameObject> listeChoix = new List<GameObject>();

    private Evenement evenement;
    public Evenement EvenementActuel
    {
        set
        {
            evenement = value;
            DessinerEvenement();
        }
        get
        {
            return evenement;
        }
    }

    private void DessinerEvenement()
    {

        illustration.sprite = evenement.illustration;
        titre.text = evenement.titre;
        description.text = evenement.description;
        GenererChoix();


        if(evenement is EvenementCombat)
        {

        }
    }

    private void GenererChoix()
    {
        ClearChoix();

        for (int i = 0; i < evenement.listeChoix.Count; i++)
        {
            GameObject nvChoix = Instantiate(choixBase, lesChoix.transform);
            nvChoix.SetActive(true);
            nvChoix.GetComponent<InfoBulle>().textInfoBulle = evenement.listeChoix[i].infobulle;
            nvChoix.GetComponent<TextMeshProUGUI>().text = evenement.listeChoix[i].description;
            nvChoix.GetComponent<Button>().onClick.AddListener(evenement.listeChoix[i].effets.Invoke);

            listeChoix.Add(nvChoix);
        }
    }

    private void ClearChoix()
    {
        foreach(GameObject choix in listeChoix)
        {
            Destroy(choix);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        choixBase.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
