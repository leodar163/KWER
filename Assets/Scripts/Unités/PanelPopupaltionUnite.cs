using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PanelPopupaltionUnite : MonoBehaviour
{

    TextMeshProUGUI txtMP;
    UniteManager tribu;
    Color couleurDefaut;
    [SerializeField] Color couleurFamine;

    private void Awake()
    {
        tribu = GetComponentInParent<UniteManager>();
        txtMP = GetComponentInChildren<TextMeshProUGUI>();
        couleurDefaut = txtMP.color;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AfficherPopulation()
    {

        gameObject.SetActive(true);



        txtMP.text = tribu.population.ToString();

        if(tribu.gainNourriture < tribu.population)
        {
            txtMP.color = couleurFamine;
        }
        else
        {
            txtMP.color = couleurDefaut;
        }
    }

    public void CacherPopulation()
    {
        gameObject.SetActive(false);
    }
}
