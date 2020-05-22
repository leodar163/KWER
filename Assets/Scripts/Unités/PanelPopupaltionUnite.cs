using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PanelPopupaltionUnite : MonoBehaviour
{

    TextMeshProUGUI txtMP;
    Tribu tribu;
    Color couleurDefaut;
    [SerializeField] Color couleurFamine;

    private void Awake()
    {
        tribu = GetComponentInParent<Tribu>();
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

    }

    public void CacherPopulation()
    {
        gameObject.SetActive(false);
    }
}
