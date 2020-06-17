using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceEvenement : MonoBehaviour
{
    private static InterfaceEvenement cela;

    public static InterfaceEvenement Defaut
    {
        get
        {
            if(cela == null)
            {
                cela = FindObjectOfType<InterfaceEvenement>();
            }
            return cela;
        }
    }

    [SerializeField] private GameObject fondNoir;
    [SerializeField] private FenetreEvenement fenetreEvenement;


    

    // Start is called before the first frame update
    void Start()
    {
        cela = this;
        FermerFenetreEvenement();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FermerFenetreEvenement()
    {
        fondNoir.SetActive(false);
        fenetreEvenement.gameObject.SetActive(false);
    }

    public void OuvrirFenetreEvenement(Evenement evenementALancer)
    {
        fenetreEvenement.EvenementActuel = evenementALancer;
        fondNoir.SetActive(true);
        fenetreEvenement.gameObject.SetActive(true);
    }
}
