using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using UnityEditorInternal;

[ExecuteInEditMode]
[RequireComponent(typeof(Image),typeof(Button),typeof(InfoBulle))]
public abstract class Slot : MonoBehaviour
{
    protected string texteInfobulleDefaut = "Cliquer pour assigner une population";
    protected Pop pop;
    protected Demographie demo;
    protected Image image;
    [SerializeField] protected Image iconePop;
    public InfoBulle infobulle;
    protected Button bouton;
    private bool textInfoBulleInit = false;

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

    private void Awake()
    {
        
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

        if (infobulle && !textInfoBulleInit)
        {
            infobulle.texteInfoBulle = texteInfobulleDefaut;
            textInfoBulleInit = true;
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
        demo.RetournerPop(pop);
        pop.gameObject.SetActive(true);
        pop = null;
        iconePop.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        if (pop)
        {
            demo.RetournerPop(pop);
        }
    }

    public void InterdireSlot(string message)
    {
        if (!bouton || !image) ConstruirSlot();
        bouton.interactable = false;
        image.color = Color.red;

        textInfoBulleInit = true;
        infobulle.texteInfoBulle = message;

        if (estOccupe) CliquerSurSlot();
    }

    public void AutoriserSlot()
    {
        if (!bouton || !image) ConstruirSlot();
        bouton.interactable = true;
        image.color = Color.white;

        textInfoBulleInit = true;
        infobulle.texteInfoBulle = texteInfobulleDefaut;
    }

    public void AutoriserSlot(string message)
    {
        if (!bouton || !image) ConstruirSlot();
        bouton.interactable = true;
        image.color = Color.white;

        textInfoBulleInit = true;
        infobulle.texteInfoBulle = message;
    }
}
