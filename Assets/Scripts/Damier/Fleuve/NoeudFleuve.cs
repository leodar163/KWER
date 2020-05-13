using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoeudFleuve : MonoBehaviour
{
    [SerializeField] Sprite sprDefaut;
    [SerializeField] Sprite sprSeletion;
    [SerializeField] bool estSelectionne;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EstSelectionne(bool selectionne)
    {
        if(selectionne)
        {
            GetComponent<SpriteRenderer>().sprite = sprSeletion;
            estSelectionne = true;
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = sprDefaut;
            estSelectionne = false;
        }
    }
}
