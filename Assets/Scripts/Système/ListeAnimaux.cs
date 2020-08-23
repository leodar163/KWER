using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListeAnimaux : MonoBehaviour
{
    static private ListeAnimaux cela;

    static public ListeAnimaux Defaut
    {
        get
        {
            if(!cela)
            {
                cela = FindObjectOfType<ListeAnimaux>();
            }
            return cela;
        }
    }

    public List<GameObject> domesticables = new List<GameObject>();
    [Space]
    public List<GameObject> MegaFaune = new List<GameObject>();
    [Space]
    public List<GameObject> Predateurs = new List<GameObject>();
    [Space]
    public GameObject pillard;
    
    // Start is called before the first frame update
    void Start()
    {
        cela = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
