using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsJeu : MonoBehaviour
{
    static private OptionsJeu cela;

    static public OptionsJeu Defaut
    {
        get
        {
            if (!cela) cela = FindObjectOfType<OptionsJeu>();
            return cela;
        }
    }

    public bool modeCombatsSimplifies;
    public int nbrTribuGameOver = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
