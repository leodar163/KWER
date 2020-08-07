using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceEchange : MonoBehaviour
{
    private bool interactionActive;

    static private InterfaceEchange cela;

    static public InterfaceEchange Actuel
    {
        get
        {
            if (cela == null) cela = FindObjectOfType<InterfaceEchange>();
            return cela;
        }
    }


    [SerializeField] private Transform platoJoueur;
    [SerializeField] private Transform platoCible;
    [Space]
    [SerializeField] private InventaireEchange inventaire;
    [Space]
    [SerializeField] private Button boutonInventaireJoueur;
    [SerializeField] private Button boutonInventaireCible;

    public List<string> ressourcesEchangees = new List<string>();

    private Echange echange;

    public Echange EchangeAffiche
    {
        get
        {
            return echange;
        }
        set
        {
            echange = value;
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        cela = this;
        InterfaceRessource.Actuel.EventInterfaceMAJ.AddListener(MAJInterface);

        boutonInventaireCible.onClick.AddListener(() => inventaire.AfficherInventaire(Tribu.TribukiJoue));
        boutonInventaireJoueur.onClick.AddListener(() => inventaire.AfficherInventaire(echange.tribuCible));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AjouterSlotEchange(Consommable consommable, Tribu tribu)
    {

    }

    public void AjouterSlotEchange(Ressource ressource, Tribu tribu)
    {

    }

    private void MAJInterface()
    {
        if(inventaire.gameObject.activeSelf && interactionActive)
        {
            ActiverInteraction(false);
        }
        else if (!inventaire.gameObject.activeSelf && !interactionActive)
        {
            ActiverInteraction(true);
        }
    }

    public void ActiverInteraction(bool activer)
    {
        interactionActive = activer;
    }

    public void OuvrirEchange()
    {

    }

    public void FermerEchange()
    {

    }
}
