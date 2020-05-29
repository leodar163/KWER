using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hostile : MonoBehaviour
{
    public Troupeau troupeau;

    private List<Tribu> ciblesAPortee = new List<Tribu>();

    private bool combatEnCours = false;

    public bool PeutAttaquer
    {
        get
        {
            if(troupeau.predateur)
            {
                TrouverCiblesAPortee();
                if (ciblesAPortee.Count > 0) return true;
            }
            return false;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
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

        troupeau.aFaitUneAction = true;
    }

    private void AttaquerCible(Tribu cible)
    {
        print(gameObject.name + " attaque " + cible.gameObject.name);
        combatEnCours = true;
    }

    public void TerminerCombat()
    {
        print("Le combat est terminé");
        combatEnCours = false;
    }

    private void TrouverCiblesAPortee()
    {
        ciblesAPortee.Clear();
        foreach(Revendication cible in troupeau.revendication.TrouverRevendicateursAPortee())
        {
            if(cible.EstTribu && !ciblesAPortee.Contains((Tribu)cible.parent))
            {
                ciblesAPortee.Add((Tribu)cible.parent);
            }
        }
    }
}
