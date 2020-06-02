using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PanelRecette : MonoBehaviour
{
    public Craft craft;
    private Recette recette;

    private Production gainRessource;

    [Header("Affichage Recette")]
    public TextMeshProUGUI nomRecette;
    public GameObject listeGainRessource;
    public GameObject listeCoutRessource;
    [SerializeField] private GameObject affichageRessource;
    private List<GainCraft> listeAffichageCout = new List<GainCraft>();
    private List<GainCraft> listeAffichageGain = new List<GainCraft>();


    [Header("Slots")]
    private List<SlotCraft> listeSlots = new List<SlotCraft>();
    [SerializeField] private GameObject slotCraft;
    [SerializeField] private GameObject zoneSlots;


    // Start is called before the first frame update
    void Start()
    {
        TourParTour.Defaut.eventNouveauTour.AddListener(MiseAJourProduction);
        slotCraft.SetActive(false);
        affichageRessource.SetActive(false);
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
            GenererAffichageRecette();
            Invoke("AfficherGainRessource",0.3f);
        }
        get
        {
            return recette;
        }
    }

    private void MiseAJourProduction()
    {
        MiseAJourSlots();
        craft.campement.tribu.stockRessources.AjouterGain(GainRessource);
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

    private void GenererAffichageRecette()
    {
        //Création de l'affichage du coût
        for (int i = 0; i < recette.cout.gains.Length; i++)
        {
            if(recette.cout.gains[i] > 0)
            {
                GameObject nvlAffichage = Instantiate(affichageRessource, listeCoutRessource.transform);
                GainCraft nvGain = nvlAffichage.GetComponent<GainCraft>();

                nvGain.Ressource = ListeRessources.Defaut.listeDesRessources[i];
                nvlAffichage.SetActive(true);

                listeAffichageCout.Add(nvGain);
            }
        }
        //Création de l'affichage du gain
        for (int i = 0; i < recette.production.gains.Length; i++)
        {
            if (recette.production.gains[i] > 0)
            {
                GameObject nvlAffichage = Instantiate(affichageRessource, listeGainRessource.transform);
                GainCraft nvGain = nvlAffichage.GetComponent<GainCraft>();

                nvGain.Ressource = ListeRessources.Defaut.listeDesRessources[i];

                nvlAffichage.SetActive(true);
                listeAffichageGain.Add(nvGain);
            }
        }
    }

    private void MAJAffichageRecette(Production gainProduction)
    {
        foreach (SlotCraft slot in listeSlots)
        {
            foreach (GainCraft affichage in listeAffichageCout)
            {
                foreach (Ressource ressource in slot.RecupRessourcesInsuffisantes())
                {
                    if (affichage.Ressource == ressource)
                    {
                        affichage.MarquerInsuffisant(true);
                        break;
                    }
                    affichage.MarquerInsuffisant(false);
                }
            }
        }

        for (int i = 0; i < gainProduction.gains.Length; i++)
        {
            if(ListeRessources.Defaut)
            {
                //Affichage des gains
                if(gainProduction.gains[i] > 0)
                {
                    foreach(GainCraft affichage in listeAffichageGain)
                    {
                        affichage.montant = 0;
                        if (affichage.Ressource = ListeRessources.Defaut.listeDesRessources[i])
                        {
                            affichage.montant = gainProduction.gains[i];
                        }
                    }
                }

                //Affichage des couts
                if (gainProduction.gains[i] < 0)
                {
                    foreach (GainCraft affichage in listeAffichageCout)
                    {
                        affichage.montant = 0;
                        if (affichage.Ressource = ListeRessources.Defaut.listeDesRessources[i])
                        {
                            affichage.montant = gainProduction.gains[i];
                        }
                    }
                }
            }
        }
    }

    private void GenererSlots()
    {
        int nbrslot = recette.slots;

        for (int i = 0; i < nbrslot; i++)
        {
            GameObject nvSlot = Instantiate(slotCraft, zoneSlots.transform);
           
            SlotCraft nvSlotCraft = nvSlot.GetComponent<SlotCraft>();
            nvSlot.SetActive(true);

            nvSlotCraft.panelRecette = this;
            listeSlots.Add(nvSlotCraft);
        }
    }

    private void GenererSlots(int nbrSlots)
    {
            if (nbrSlots > 0) //si le nobre de slots à rajouter est positif, on instantie ces slots
            {
                for (int i = 0; i < nbrSlots; i++)
                {
                    GameObject nvSlot = Instantiate(slotCraft, zoneSlots.transform);
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
    }

    public void AfficherGainRessource()
    {
        craft.campement.tribu.stockRessources.AjouterGain(GainRessource);
        MAJAffichageRecette(GainRessource);
    }

    private Production GainRessource
    {
        get
        {
            if (gainRessource == null)
            {
                gainRessource = ScriptableObject.CreateInstance<Production>();
                gainRessource.gains = new float[ListeRessources.Defaut.listeDesRessources.Length];
            }
            gainRessource.Clear();
            int slotsActifs = 0;
            foreach(SlotCraft slot in listeSlots)
            {
                if(slot.estActif)
                {
                    slotsActifs++;
                }
            }
            for (int i = 0; i < gainRessource.gains.Length; i++)
            {
                gainRessource.gains[i] += recette.production.gains[i] * slotsActifs;
            }
            return gainRessource;
        }
    }
}
