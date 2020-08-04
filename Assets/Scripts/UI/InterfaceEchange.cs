using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceEchange : MonoBehaviour
{
    static private InterfaceEchange cela;

    static public InterfaceEchange Actuel
    {
        get
        {
            if (cela == null) cela = FindObjectOfType<InterfaceEchange>();
            return cela;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        cela = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OuvrirEchange(Tribu tribuCile)
    {

    }
}
