using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class SlotConsommable : MonoBehaviour
{
    [SerializeField] private GameObject iconeConsommable;

    private Consommable consommable;

    /// <summary>
    /// Assigne la variable consommable et initialise le slot
    /// </summary>
    public Consommable ConsommableAssigne
    {
        get
        {
            return consommable;
        }
        set
        {
            if(value == null)
            {
                DesactiverConsommable();
            }
            else
            {
                consommable = value;
                ActiverConsommable();
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ActiverConsommable()
    {
        iconeConsommable.SetActive(true);

        iconeConsommable.GetComponent<Image>().sprite = consommable.icone;
        iconeConsommable.GetComponent<InfoBulle>().texteInfoBulle = consommable.TexteInfoBulle;

        Button boutonConso = iconeConsommable.GetComponent<Button>();
        boutonConso.onClick.RemoveAllListeners();
        if (consommable.type == Consommable.typeConsommable.amenagement)
        {
            boutonConso.onClick.AddListener(consommable.amenagement.ActiverAmenagement);
        }
        else if(consommable.type == Consommable.typeConsommable.buff)
        {
            boutonConso.onClick.AddListener(consommable.buff.activerBuff);
        }

        boutonConso.onClick.AddListener(ConsommerConsommable);
    }

    private void ConsommerConsommable()
    {
        Tribu.TribukiJoue.stockRessources.consommables.Remove(consommable);
    }

    private void DesactiverConsommable()
    {
        iconeConsommable.SetActive(false);
    }

}
