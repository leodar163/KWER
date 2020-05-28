using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoutonTourSuivant : MonoBehaviour
{
    #region SINGLETON
    private static BoutonTourSuivant cela;

    public static BoutonTourSuivant Actuel
    {
        get
        {
            if(cela == null)
            {
                cela = FindObjectOfType<BoutonTourSuivant>();
            }
            return cela;
        }
    }
    #endregion

    private Button bouton;
    private Image image;

    private void Awake()
    {
        cela = this;

        bouton = GetComponent<Button>();
        image = GetComponent<Image>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Activer(bool activer)
    {
        bouton.interactable = activer;
        if (activer) image.color = Color.white;
        else image.color = Color.gray;
    }
}
