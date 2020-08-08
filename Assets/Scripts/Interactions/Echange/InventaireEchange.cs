using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventaireEchange : MonoBehaviour
{
    [SerializeField] private InterfaceEchange interfaceEchange;
    [HideInInspector] public PlatoEchange platoActuel;
    [SerializeField] private GameObject slotRessourceBase;
    [SerializeField] private Transform fond;
    private RectTransform rectT;
    private List<GameObject> listeSlots = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        rectT = GetComponent<RectTransform>();
        slotRessourceBase.SetActive(false);
        gameObject.SetActive(false);    
    }

    // Update is called once per frame
    void Update()
    {
        CheckerClicEnDehors();
    }

    private void CheckerClicEnDehors()
    {
        if (Input.GetMouseButton(0) && gameObject.activeSelf &&
            !RectTransformUtility.RectangleContainsScreenPoint(
                gameObject.GetComponent<RectTransform>(),
                Input.mousePosition,
                Camera.main))
        {
            CacherInventaire();
        }
    }

    public void AfficherInventaire(Tribu tribu, PlatoEchange platoKiAppelle)
    {
        platoActuel = platoKiAppelle;

        platoActuel.ActiverInteraction(false);

        GenererInventaireSlot(tribu);

        rectT.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, rectT.position.z);
    }

    private void GenererInventaireSlot(Tribu tribu)
    {
        NettoyerInventaire();

        float[] gains = tribu.stockRessources.RessourcesEnStock.gains;

        //Génère slots de ressource
        for (int i = 0; i < gains.Length; i++)
        {
            Ressource ressource = ListeRessources.Defaut.listeDesRessources[i];
            if (gains[i] >= 1 && !platoActuel.ressourcesEchangees.Contains(ressource.nom))
            {
                GameObject nvSlot = Instantiate(slotRessourceBase, fond);
                listeSlots.Add(nvSlot);
                nvSlot.SetActive(true);

                foreach (Image image in nvSlot.GetComponentsInChildren<Image>())
                {
                    if (image.name == "Icone")
                    {
                        image.sprite = ListeIcones.Defaut.TrouverIconeRessource(ressource);
                    }
                }
                nvSlot.GetComponentInChildren<TextMeshProUGUI>().text = "" + gains[i];

                Button nvBouton = nvSlot.GetComponentInChildren<Button>();

                nvBouton.onClick.AddListener(() =>
                platoActuel.AjouterSlotEchange(ressource));
                nvBouton.onClick.AddListener(() => Destroy(nvSlot));
                nvBouton.onClick.AddListener(CacherInventaire);
            }
        }

        List<Consommable> consommables = tribu.stockRessources.consommables;
        Dictionary<Consommable, int> inventaireConso = new Dictionary<Consommable, int>();

        //Génère slots de consommables
        for (int i = 0; i < consommables.Count; i++)
        {
            if (inventaireConso.ContainsKey(consommables[i])) inventaireConso[consommables[i]]++;
            else inventaireConso.Add(consommables[i], 1);
        }

        foreach (Consommable consommable in inventaireConso.Keys)
        {
            if (!platoActuel.ressourcesEchangees.Contains(consommable.nom))
            {
                GameObject nvSlot = Instantiate(slotRessourceBase, fond);
                listeSlots.Add(nvSlot);
                nvSlot.SetActive(true);

                foreach (Image image in nvSlot.GetComponentsInChildren<Image>())
                {
                    if (image.name == "Icone")
                    {
                        image.sprite = consommable.icone;
                    }
                }
                nvSlot.GetComponentInChildren<TextMeshProUGUI>().text = "" + inventaireConso[consommable];

                Button nvBouton = nvSlot.GetComponentInChildren<Button>();

                nvBouton.onClick.AddListener(() =>
                platoActuel.AjouterSlotEchange(consommable));
                nvBouton.onClick.AddListener(() => Destroy(nvSlot));
                nvBouton.onClick.AddListener(CacherInventaire);
            }
        }
    }

    private void CacherInventaire()
    {
        gameObject.SetActive(false);
        platoActuel.ActiverInteraction(true);
    }

    private void NettoyerInventaire()
    {
        foreach(GameObject slot in listeSlots)
        {
            Destroy(slot);
        }

        listeSlots.Clear();
    }
}
