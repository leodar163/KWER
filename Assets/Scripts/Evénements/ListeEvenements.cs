using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ListeEvenements : MonoBehaviour
{
    private static ListeEvenements cela;

    public static ListeEvenements Defaut
    {
        get
        {
            if(cela == null)
            {
                cela = FindObjectOfType<ListeEvenements>();
            }

            return cela;
        }
    }

    public List<EvenementTemporel> listeEvenementsTemporels = new List<EvenementTemporel>();


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
