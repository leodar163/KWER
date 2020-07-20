using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InterfaceEvenement : MonoBehaviour
{
    private static InterfaceEvenement cela;

    public static InterfaceEvenement Defaut
    {
        get
        {
            if(cela == null)
            {
                cela = FindObjectOfType<InterfaceEvenement>();
            }
            return cela;
        }
    }

    [SerializeField] private ContentSizeFitter layoutPrincipalCombat;
    [SerializeField] private ContentSizeFitter layoutPrincipalEvenement;
    [Space]
    [SerializeField] private GameObject fondNoir;
    [SerializeField] private FenetreEvenement fenetreEvenement;
    [SerializeField] private FenetreEvenementCombat fenetreCombat;

    public UnityEvent eventFinEvenement;

    // Start is called before the first frame update
    void Start()
    {
        cela = this;
        FermerFenetreEvenement();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OuvrirRecapCombat(int attaqueGuerrier, int defenseGuerrier, int attaqueHostile, int defenseHostile, int mortsGuerrier, int mortsHostile, bool ennemiFuit, Combat combat)
    {

    }

    public void OuvrirFenetreEvenementCombat(Combat combat)
    {
        EvenementCombat eC = ListeEvenementCombat.Defaut.PiocherEvenement(combat);
        eC.combat = combat;
        fenetreCombat.LancerCombat(combat, eC);
        fondNoir.SetActive(true);
        fenetreCombat.gameObject.SetActive(true);
        StartCoroutine(MAJCanvas());
    }

    public void FermerFenetreEvenement()
    {
        fondNoir.SetActive(false);
        fenetreEvenement.gameObject.SetActive(false);
        fenetreCombat.gameObject.SetActive(false);

        StartCoroutine(VerifierEvenementFini());
    }

    private IEnumerator VerifierEvenementFini()
    {
        yield return new WaitForEndOfFrame();

        if(!fenetreCombat.gameObject.activeSelf && !fenetreCombat.gameObject.activeSelf)
        {
            eventFinEvenement.Invoke();
            if(Interaction.EnCours) Interaction.EnCours.EntrerEnInteraction(false);
            print("Evenement terminé");
        }
    }

    public void OuvrirFenetreEvenement(Evenement evenementALancer)
    {
        if (evenementALancer is EvenementCombat)
        {
            OuvrirFenetreEvenementCombat((EvenementCombat)evenementALancer);
            fenetreCombat.gameObject.SetActive(true);
        }
        else
        {
            fenetreEvenement.EvenementActuel = evenementALancer;
            fenetreEvenement.gameObject.SetActive(true);
        }
        fondNoir.SetActive(true);
        StartCoroutine(MAJCanvas());
    }
        

    public void OuvrirFenetreEvenementCombat(EvenementCombat evenementALancer)
    {
        evenementALancer.combat = fenetreCombat.CombatActuel;
        if (!evenementALancer.illustration)
        {
            evenementALancer.illustration = fenetreCombat.IllustrationActuelle.sprite;
        }
        fenetreCombat.LancerCombat(evenementALancer.combat, evenementALancer);
        StartCoroutine(MAJCanvas());
    }

    private IEnumerator MAJCanvas()
    {
        yield return new WaitForEndOfFrame();
        fenetreCombat.IllustrationActuelle.enabled = false;
        fenetreEvenement.IllustrationActuelle.enabled = false;
        

        yield return new WaitForEndOfFrame();

        fenetreCombat.IllustrationActuelle.enabled = true;
        fenetreEvenement.IllustrationActuelle.enabled = true;
    }


}
