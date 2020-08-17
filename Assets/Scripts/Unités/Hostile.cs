using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hostile : MonoBehaviour
{
    public Pion pion;

    private List<Tribu> ciblesAPortee = new List<Tribu>();

    public bool combatEstEnCours = false;

    [SerializeField] private  GameObject combat;

    /// <summary>
    /// quand est dépensé, ne peut plus attaquer jusqu'au tour prochain
    /// </summary>
    private bool jetonAttaque = true;
    [HideInInspector]public int nbrCombattant = 0;
    public int attaque = 0;
    public int defense = 0;

    [Tooltip("Soustrait les dégats moraux subit par le joueur.\n(100 signifie que l'ennemi ne fuira jamais)")]
    [Range(0, 100)]
    public int resistanceMorale;

    [Header("Generation")]
    [Tooltip("-1 si pas de valeur min")]
    public int attaqueMin = -1;
    [Tooltip("-1 si pas de valeur max")]
    public int attaqueMax = -1;
    [Space]
    [Tooltip("-1 si pas de valeur min")]
    public int defenseMin = -1;
    [Tooltip("-1 si pas de valeur max")]
    public int defenseMax = -1;
    [Space]
    [Tooltip("-1 si pas de valeur min")]
    public int nbrCombattantMin = -1;
    [Tooltip("-1 si pas de valeur max")]
    public int nbrCombattantMax = -1;
    [Space]
    public Production loot;
    public bool PeutAttaquer
    {
        get
        {
            if(jetonAttaque)
            {
                if(pion is Troupeau)
                {
                    Troupeau troupeau = (Troupeau)pion;
                    if (troupeau.predateur)
                    {
                        if (ciblesAPortee.Count > 0) return true;
                    }

                }
                else if (pion is Pillard)
                {
                    if (ciblesAPortee.Count > 0) return true;
                }
            }
            return false;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        TourParTour.Defaut.eventNouveauTour.AddListener(() => jetonAttaque = true);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator Attaquer()
    {
        Tribu cible = ciblesAPortee[Random.Range(0, ciblesAPortee.Count - 1)];
        cible.guerrier.EngagementGeneral();
        Combat combatEnCours = AttaquerCible(cible);

        yield return new WaitWhile(() => combatEstEnCours);

        Destroy(combatEnCours.gameObject);
        cible.guerrier.DesengagementGeneral();
        pion.aFaitUneAction = true;
    }

    private Combat AttaquerCible(Tribu cible)
    {
        jetonAttaque = false;

        Combat nvCombat = InstancierCombat();

        nvCombat.Guerrier = cible.guerrier;

        InterfaceEvenement.Defaut.OuvrirFenetreEvenementCombat(nvCombat);

        combatEstEnCours = true;

        return nvCombat;
    }

    public void TrouverCiblesAPortee()
    {
        ciblesAPortee.Clear();
        foreach(Revendication cible in pion.revendication.TrouverRevendicateursAPortee())
        {
            if(cible.EstTribu && !ciblesAPortee.Contains((Tribu)cible.parent))
            {
                ciblesAPortee.Add((Tribu)cible.parent);
            }
        }
    }

    public Combat InstancierCombat()
    {
        GameObject nvCombat = Instantiate(combat, gameObject.transform);
        Combat compCombat = nvCombat.GetComponent<Combat>();
        compCombat.Hostile = this;

        return compCombat;
    }

    public void Fuire(TuileManager tuileOpposee)
    {
        jetonAttaque = false;

        Vector2 direction = transform.position - (tuileOpposee.transform.position - transform.position);

        LayerMask maskTuile = LayerMask.GetMask("Tuile");
        RaycastHit2D ray = Physics2D.Raycast(transform.position, direction);

        if(ray)
        {
            TuileManager tuileCible = ray.collider.GetComponent<TuileManager>();
            if (!tuileCible.estOccupee || !tuileCible.terrainTuile.ettendueEau)
            {
                StartCoroutine(Fuire(direction));
            }

        }
    }

    private IEnumerator Fuire(Vector2 direction)
    {
        while ((Vector2)transform.position != direction)
        {
            transform.position = Vector2.MoveTowards(transform.position, direction, pion.vitesse * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        pion.TrouverTuileActuelle();
    }
}


