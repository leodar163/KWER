using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class PanelRecette : MonoBehaviour
{
    public Craft craft;

    //Consommable
    private TextMeshProUGUI tempsConsommable;
    private Production investissementConsommable;

    private Production gainRessource;

    [Header("Affichage Recette")]
    public TextMeshProUGUI nomRecette;
    public GameObject listeGainRessource;
    public GameObject listeCoutRessource;
    [SerializeField] private GameObject affichageRessource;
    [SerializeField] private GameObject affichageConsommable;
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
        affichageConsommable.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
    }

    private Recette recette;
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
        if(gameObject.activeSelf)
        {
            MiseAJourSlots();
            if (recette.typeOutput == Recette.TypeOutput.Consommable)
            {
                MiseAJourConsommable();
            }
            craft.campement.tribu.stockRessources.AjouterGain(GainRessource);
        }
    }

    private void MiseAJourConsommable()
    {
        for (int i = 0; i < gainRessource.gains.Length; i++)
        {
            if(gainRessource.gains[i] < 0)
            {
                investissementConsommable.gains[i] -= gainRessource.gains[i];
            }
        }
        for (int i = 0; i < recette.cout.gains.Length; i++)
        {
            if(recette.cout.gains[i] > investissementConsommable.gains[i])
            {
                return;
            }
        }
        if(craft.campement.tribu.stockRessources.consommables.Count < craft.campement.tribu.stockRessources.emplacementConsommable)
        {
            for (int i = 0; i < recette.cout.gains.Length; i++)
            {
                if(recette.cout.gains[i] > 0)
                {
                    investissementConsommable.gains[i] -= recette.cout.gains[i];
                }
            }
            craft.campement.tribu.stockRessources.consommables.Add(recette.consommable);
        }
    }

    private void OnEnable()
    {
        MiseAJourSlots();
    }

    //Rajoute ou enlève des slots s'il en maque ou s'il y en a trop
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
        for (int i = 0; i < recette.inputParPop.gains.Length; i++)
        {
            if(recette.inputParPop.gains[i] > 0)
            {
                GameObject nvlAffichage = Instantiate(affichageRessource, listeCoutRessource.transform);
                GainCraft nvGain = nvlAffichage.GetComponent<GainCraft>();

                nvGain.Ressource = ListeRessources.Defaut.listeDesRessources[i];
                nvlAffichage.SetActive(true);

                listeAffichageCout.Add(nvGain);
            }
        }
        //Création de l'affichage du gain
        if(recette.typeOutput == Recette.TypeOutput.Ressources)
        {
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
        else if (recette.typeOutput == Recette.TypeOutput.Consommable)
        {
            GameObject nvlAffichage = Instantiate(affichageConsommable, listeGainRessource.transform);
            nvlAffichage.SetActive(true);

            tempsConsommable = nvlAffichage.GetComponentInChildren<TextMeshProUGUI>();
            tempsConsommable.text = "0 tour";
            nvlAffichage.GetComponentInChildren<Image>().sprite = recette.consommable.icone;
            nvlAffichage.GetComponentInChildren<InfoBulle>().texteInfoBulle = recette.consommable.TexteInfoBulle;

            investissementConsommable = ScriptableObject.CreateInstance<Production>();
            investissementConsommable.gains = new float[ListeRessources.Defaut.listeDesRessources.Length];
        }
    }

    private void MAJAffichageRecette()
    {
        //affiche les couts
        foreach (GainCraft affichage in listeAffichageCout)
        {
            affichage.montant = Mathf.Abs(GainRessource.gains[ListeRessources.Defaut.TrouverIndexRessource(affichage.Ressource)]);
        }
        //affiche les gains
        if (recette.typeOutput == Recette.TypeOutput.Ressources)
            foreach (GainCraft affichage in listeAffichageGain)
            {
           
                affichage.montant = GainRessource.gains[ListeRessources.Defaut.TrouverIndexRessource(affichage.Ressource)]; 
            }
        else if (recette.typeOutput == Recette.TypeOutput.Consommable)
        {
            for (int i = 0; i < recette.cout.gains.Length; i++)
            {
                if(recette.cout.gains[i] > 0)
                {
                    char pluriel = '\0';
                    float temps = math.round((recette.cout.gains[i] - investissementConsommable.gains[i]) / math.abs(GainRessource.gains[i]));

                    if (temps > 1) pluriel = 's';

                    if (temps == float.PositiveInfinity) tempsConsommable.text = "";
                    else tempsConsommable.text = temps + " Tour" + pluriel;
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
        MAJAffichageRecette();
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
            int slotsOccupes = 0;
            foreach(SlotCraft slot in listeSlots)
            {
                if(slot.estOccupe)
                {
                    slotsOccupes++;
                }
            }
            for (int i = 0; i < gainRessource.gains.Length; i++)
            {
               if(recette)
                {
                    if (recette.typeOutput == Recette.TypeOutput.Ressources)
                    {
                        gainRessource.gains[i] += recette.production.gains[i] * slotsOccupes;
                    }
                    gainRessource.gains[i] -= recette.inputParPop.gains[i] * slotsOccupes;
                }
            }

            return gainRessource;
        }
    }
}
