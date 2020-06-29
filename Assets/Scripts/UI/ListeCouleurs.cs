using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListeCouleurs : MonoBehaviour
{
    #region SINGLETON
    private static ListeCouleurs cela;

    public static ListeCouleurs Defaut
    {
        get
        {
            if(cela == null)
            {
                cela = FindObjectOfType<ListeCouleurs>();
            }
            return cela;
        }
    }
    #endregion

    public Color couleurDefautTexteInterface;
    public Color couleurAlerteTexteInterface;
    public Color couleurTexteSansFond;
    public Color couleurTexteBonus;
    public Color couleurAlerteMoyenne;


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
