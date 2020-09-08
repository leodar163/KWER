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
    [SerializeField] private GameObject fondNoir = null;
    [SerializeField] private FenetreEvenement fenetreEvenement = null;
    [SerializeField] private FenetreEvenementCombat fenetreCombat = null;
    [SerializeField] private FenetreRecapCombat recapCombat = null;
    [Space]
    [Header("Combat")]
    [SerializeField] private Sprite illuPillard = null;
    [SerializeField] private Sprite illuPredateur = null;
    [SerializeField] private Sprite illuMegaFaune = null;
    [Header("Saison")]
    [SerializeField] private Evenement evenementHiver;
    [SerializeField] private Evenement evenementEte;
    [Header("Narration")]
    [SerializeField] private Evenement evenementDebut;
    public Evenement evenementGameoverGen;
    public Evenement evenementGameoverTrib;
    public Evenement evenementVictoire;

    private Queue<Evenement> evenementsEnAttente = new Queue<Evenement>();
    private Queue<Evenement> evenementsEnAttenteImmediate = new Queue<Evenement>();

    [HideInInspector] public UnityEvent eventFinEvenement;
    [HideInInspector] public bool evenementEnCours = false;

    private bool changementSaisonEstMontre = true;

    // Start is called before the first frame update
    void Start()
    {
        cela = this;
        FermerFenetreEvenement();
        eventFinEvenement.AddListener(() => evenementEnCours = false);
        Calendrier.Actuel.EventChangementDeSaison.AddListener(() => changementSaisonEstMontre = false);
        TourParTour.Defaut.eventNouveauTour.AddListener(ChargerEvenementNouveauTour);
        evenementDebut.LancerEvenement();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region EVENT TEMPO
    private void ChargerEvenementNouveauTour()
    {
        if(!changementSaisonEstMontre)
        {
            StartCoroutine(OuvrirEvenementChangementSaison());
        }
    }

    private IEnumerator OuvrirEvenementChangementSaison()
    {
        yield return new WaitWhile(() => evenementEnCours);
        if (Calendrier.Actuel.Hiver)
        {
            evenementHiver.LancerEvenement();
        }
        else
        {
            evenementEte.LancerEvenement();
        }

        StartCoroutine(VerifierEvenementFini());
        yield return new WaitWhile(() => evenementEnCours);

        changementSaisonEstMontre = true;

        TirerEvenementTempo();
    }
    private void TirerEvenementTempo()
    {
        float aleaJactata = 100 - Random.Range(0, 100);

        List<EvenementTemporel> evenementsRetenus = new List<EvenementTemporel>();

        foreach (EvenementTemporel evenementTemporel in ListeEvenements.Defaut.listeEvenementsTemporels)
        {
            if (Calendrier.Actuel.Hiver == true)
            {
                if (evenementTemporel.probaHiver >= aleaJactata)
                {
                    evenementsRetenus.Add(evenementTemporel);
                }
            }
            else
            {
                if (evenementTemporel.probaEte >= aleaJactata)
                {
                    evenementsRetenus.Add(evenementTemporel);
                }
            }
        }

        if (evenementsRetenus.Count != 0)
        {
            int aleumJactatum = Random.Range(0, evenementsRetenus.Count - 1);
            evenementsRetenus[aleumJactatum].LancerEvenement();
        }
    }

    #endregion


    public void OuvrirRecapCombat(Combat.RecapCombat recap, Combat combat)
    {
        StartCoroutine(VerifierEvenementFini());
        fondNoir.gameObject.SetActive(true);
        recapCombat.gameObject.SetActive(true);
        Evenement.Choix choix = new Evenement.Choix("","");
        choix.effets.AddListener(FermerFenetreEvenement);
        choix.effets.AddListener(() => combat.Guerrier.tribu.demographie.DesengagerGuerrier(true, recap.mortsGuerrier));
        if (combat.Hostile.nbrCombattant > 0 && combat.Guerrier.nbrGuerrier > 0)
        {
            if(recap.ennemiFuit)
            {
                choix.description = "L'ennemi est démoralisé et fuit !";
                choix.effets.AddListener(combat.EnnemiFuit);
                choix.infobulle = "L'ennemi perd la volonté de se battre";
            }
            else
            {
                choix.description = "Le combat continue";
                if (!OptionsJeu.Defaut.modeCombatsSimplifies)
                    choix.effets.AddListener(ListeEvenementCombat.Defaut.PiocherEvenement(combat).LancerEvenement);
                else
                    choix.effets.AddListener(combat.LancerCombat);
                choix.infobulle = "Pouvons-nous vraiment continuer ce combat ?";
            }
        }
        else
        {
            if(combat.Hostile.nbrCombattant == 0 && combat.Guerrier.nbrGuerrier == 0)
            {
                choix.description = "C'est un carnage";
                choix.infobulle = "Les deux camps ont été anéhantis dans l'affrontement";
            }
            else if(combat.Hostile.nbrCombattant == 0)
            {
                choix.description = "Nous avons gagné !";
                choix.effets.AddListener(combat.looterEnnemi);
                choix.infobulle = "L'unitée ennemie est détruite";
                for (int i = 0; i < combat.Hostile.loot.gains.Length; i++)
                {
                    if(combat.Hostile.loot.gains[i] > 0)
                    {
                        choix.infobulle += "\n<color=#"+ ColorUtility.ToHtmlStringRGBA(ListeCouleurs.Defaut.couleurTexteBonus)+">+"
                            + combat.Hostile.loot.gains[i]+"<color=\"white\"> " + ListeRessources.Defaut.listeDesRessources[i].nom;
                    }
                }
            }
            else if(combat.Guerrier.nbrGuerrier == 0)
            {
                choix.description = "Nous avons perdu !";
                choix.infobulle = "Tous nos guerriers sont morts...\nNous aurions peut-être dû fuir avant";
            }
        }

        if (combat.Hostile.pion.revendication.EstPillard)
        {
            recapCombat.illustration.sprite = illuPillard;
        }
        else if (combat.Hostile.pion.revendication.EstPredateur)
        {
            recapCombat.illustration.sprite = illuPredateur;
        }
        else if (combat.Hostile.pion.revendication.EstMegaFaune)
        {
            recapCombat.illustration.sprite = illuMegaFaune;
        }

        recapCombat.AfficherRecap(recap, combat, choix);
        StartCoroutine(MAJCanvas());
    }

    public void OuvrirFenetreEvenement(Evenement evenementALancer, bool Immediat)
    {
        if(evenementEnCours && !Immediat)
        {
            evenementsEnAttente.Enqueue(evenementALancer);
        }
        else
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
            evenementEnCours = true;
            ControleSouris.Actuel.controleEstActif = false;
            StartCoroutine(MAJCanvas());
        }
    }

    public void OuvrirFenetreEvenementCombat(Combat combat)
    {
        EvenementCombat eC = ListeEvenementCombat.Defaut.PiocherEvenement(combat);
        eC.combat = combat;
        fenetreCombat.LancerCombat(eC);
        fondNoir.SetActive(true);
        fenetreCombat.gameObject.SetActive(true);

        if (combat.Hostile.pion.revendication.EstPillard)
        {
            fenetreCombat.IllustrationActuelle.sprite = illuPillard;
        }
        else if (combat.Hostile.pion.revendication.EstPredateur)
        {
            fenetreCombat.IllustrationActuelle.sprite = illuPredateur;
        }
        else if(combat.Hostile.pion.revendication.EstMegaFaune)
        {
            fenetreCombat.IllustrationActuelle.sprite = illuMegaFaune;
        }

        StartCoroutine(MAJCanvas());
    }

    /// <summary>
    /// Toujours mettre en premier effet d'un choix d'événement
    /// </summary>
    public void FermerFenetreEvenement()
    {
        StopCoroutine(VerifierEvenementFini());
        fondNoir.SetActive(false);
        fenetreEvenement.gameObject.SetActive(false);
        fenetreCombat.gameObject.SetActive(false);
        recapCombat.gameObject.SetActive(false);

         StartCoroutine(VerifierEvenementFini());
    }

    private IEnumerator VerifierEvenementFini()
    {
        for (int i = 0; i < 20; i++)
        {
            yield return new WaitForEndOfFrame();
        }

        if (!fenetreEvenement.gameObject.activeSelf && !fenetreCombat.gameObject.activeSelf && !recapCombat.gameObject.activeSelf)
        {
            eventFinEvenement.Invoke();
            ControleSouris.Actuel.controleEstActif = true;
            if (Interaction.EnCours && (Interaction.EnCours is Combat || Interaction.EnCours is Anomalie)) 
                Interaction.EnCours.EntrerEnInteraction(false);
            print("Evenement terminé");
            if(evenementsEnAttente.Count > 0) OuvrirFenetreEvenement(evenementsEnAttente.Dequeue(), false);
        }
    }

        

    public void OuvrirFenetreEvenementCombat(EvenementCombat evenementALancer)
    {
        if(!OptionsJeu.Defaut.modeCombatsSimplifies)evenementALancer.combat = fenetreCombat.CombatActuel;
        if (!evenementALancer.illustration)
        {
            evenementALancer.illustration = fenetreCombat.IllustrationActuelle.sprite;
        }
        fenetreCombat.LancerCombat(evenementALancer);
        StartCoroutine(MAJCanvas());
    }

    private IEnumerator MAJCanvas()
    {
        yield return new WaitForEndOfFrame();
        fenetreCombat.IllustrationActuelle.enabled = false;
        fenetreEvenement.IllustrationActuelle.enabled = false;
        recapCombat.illustration.enabled = false;
        

        yield return new WaitForEndOfFrame();

        fenetreCombat.IllustrationActuelle.enabled = true;
        fenetreEvenement.IllustrationActuelle.enabled = true;
        recapCombat.illustration.enabled = true;
    }


}
