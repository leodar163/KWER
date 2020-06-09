using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

[ExecuteInEditMode]
[RequireComponent(typeof(Image),typeof(Button))]
public abstract class Slot : MonoBehaviour
{
    protected Pop pop;
    protected Demographie demo;
    protected Image image;
    [SerializeField] protected Image iconePop;
    protected Button bouton;

    public bool estOccupe
    {
        get
        {
            if (pop == null)
            {
                return false;
            }
            else return true;
        }
    }


    private void OnEnable()
    {
        ConstruirSlot();
        if (estOccupe) iconePop.gameObject.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        ConstruirSlot();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Construit ou met à jour les éléments nécessaires à un slot
    private void ConstruirSlot()
    {
        if (!image)
        {
            image = GetComponent<Image>();
            image.preserveAspect = true;
        }

        if (ListeIcones.Defaut)
        {
            image.sprite = ListeIcones.Defaut.iconeSlot;
            if(iconePop)
            {
                iconePop.sprite = ListeIcones.Defaut.iconePopulation;
                iconePop.preserveAspect = true;
                iconePop.gameObject.SetActive(false);
            }
        }

        if (gameObject.name != "Slot") gameObject.name = "Slot";

        if (!bouton)
        {
            bouton = GetComponent<Button>();
            bouton.onClick.AddListener(CliquerSurSlot);
            Navigation nav = new Navigation
            {
                mode = Navigation.Mode.None
            };
            bouton.navigation = nav;
        }
    }

    public virtual void CliquerSurSlot()
    {
        if(pop)
        {
            retirerPop();
        }
        else
        {
            if (demo.listePopsCampement.Count > 0)
            {
                AssignerPop();
            }
        }
    }

    protected virtual void AssignerPop()
    {
        pop = demo.RetirerPop(false);
        pop.gameObject.SetActive(false);
        iconePop.gameObject.SetActive(true);
    }

    protected virtual void retirerPop()
    {
        pop.gameObject.SetActive(true);
        demo.AjouterPop(pop);
        pop = null;
        iconePop.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        if (pop)
        {
            demo.AjouterPop(pop);
        }
    }
}
