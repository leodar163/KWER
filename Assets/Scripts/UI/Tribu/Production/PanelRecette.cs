using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PanelRecette : MonoBehaviour
{
    public Craft craft;
    public TextMeshProUGUI nomRecette;
    public PanelGainRessources panelRessource;
    private Recette recette;
    [SerializeField] private GameObject slotCraft;
    private List<SlotCraft> listeSlots = new List<SlotCraft>();


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Fait office d'initializateur
    public Recette Recette
    {
        set
        {
            recette = value;
            nomRecette.text = recette.nom;
            GenererSlots();
        }
        get
        {
            return recette;
        }
    }

    private void OnEnable()
    {
        MiseAJourSlots();
    }

    private void MiseAJourSlots()
    {
        if(recette)
        {
            if(recette.slots != listeSlots.Count)
            {
                GenererSlots(recette.slots - listeSlots.Count);
            }
        }
    }

    private void GenererSlots()
    {
        int nbrslot = recette.slots;

        for (int i = 0; i < nbrslot; i++)
        {
            GameObject nvSlot = Instantiate(slotCraft, transform);
           
            SlotCraft nvSlotCraft = nvSlot.GetComponent<SlotCraft>();
            nvSlot.SetActive(true);

            nvSlotCraft.panelRecette = this;
            listeSlots.Add(nvSlotCraft);
        }
        RearangerSlots();
    }

    private void GenererSlots(int nbrSlots)
    {
            if (nbrSlots > 0) //si le nobre de slots à rajouter est positif, on instantie ces slots
            {
                for (int i = 0; i < nbrSlots; i++)
                {
                    GameObject nvSlot = Instantiate(slotCraft, transform);
                    SlotCraft nvSlotCraft = nvSlot.GetComponent<SlotCraft>();
                    listeSlots.Add(nvSlotCraft);
                    nvSlotCraft.panelRecette = this;
                }
            }
            else //sinon on supprime des slots
            {
                for (int i = 0; i < Math.Abs(nbrSlots); i++)
                {
                    Destroy(listeSlots[listeSlots.Count - 1].gameObject);
                    listeSlots.RemoveAt(listeSlots.Count - 1);
                }
            }
        RearangerSlots();
    }

    private void RearangerSlots()
    {
        float ecart = 10;

        RectTransform rectSlotCraft = slotCraft.GetComponent<RectTransform>();
        float tailleSlot = rectSlotCraft.sizeDelta.x;
        int colonneMax = 5;
        Vector3 decalage = new Vector3();

        for (int i = 0; i < listeSlots.Count; i++)
        {
            RectTransform rectT = listeSlots[i].GetComponent<RectTransform>();

            rectT.pivot = rectSlotCraft.pivot;
            rectT.position = rectSlotCraft.position;
            rectT.sizeDelta = rectSlotCraft.sizeDelta;

            if (i == colonneMax - 1)
            {
                decalage.x = 0;
                decalage.y += ecart;
            }
            else if (i != 0)
            {
                decalage.x += ecart + tailleSlot;
            }
            rectT.position += decalage;
        }
    }


    public void AfficherGainRessource()
    {
        panelRessource.GetComponent<PanelGainRessources>().AfficherRessources(GainRessource);
    }

    private Production GainRessource
    {
        get
        {
            int slotsOccupes = 0;
            foreach(SlotCraft slot in listeSlots)
            {
                if(slot.pop != null)
                {
                    slotsOccupes++;
                }
            }
            return recette.production * slotsOccupes;
        }
    }
}
