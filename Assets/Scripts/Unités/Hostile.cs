using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;
using UnityEngine.UI;

public class Hostile : MonoBehaviour
{
    public Pion pion;

    private List<Tribu> ciblesAPortee = new List<Tribu>();

    private bool combatEnCours = false;

    [SerializeField] private  GameObject combat;

    /// <summary>
    /// quand est dépensé, ne peut plus attaquer jusqu'au tour prochain
    /// </summary>
    private bool jetonAttaque = true;
    public int nbrCombattant = 0;
    public int attaque = 0;
    public int defense = 0;

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
        TrouverCiblesAPortee();
        TourParTour.Defaut.eventNouveauTour.AddListener(() => jetonAttaque = true);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator Attaquer()
    {
        Tribu cible = ciblesAPortee[Random.Range(0, ciblesAPortee.Count - 1)];
        AttaquerCible(cible);
        TerminerCombat();
        yield return new WaitWhile(() => combatEnCours);

        pion.aFaitUneAction = true;
    }

    private void AttaquerCible(Tribu cible)
    {
        print(gameObject.name + " attaque " + cible.gameObject.name);
        ciblesAPortee.Add(cible);
        combatEnCours = true;
    }

    public void TerminerCombat()
    {
        print("Le combat est terminé");
        combatEnCours = false;
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
}


