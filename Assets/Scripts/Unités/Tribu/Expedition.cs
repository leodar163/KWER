using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Expedition : MonoBehaviour
{
    [SerializeField] public Tribu tribu;
    [SerializeField] private GameObject exploitation;
    [HideInInspector] public List<Exploitation> listeExploitations = new List<Exploitation>();

    private List<Hostile> hostilesAPortee = new List<Hostile>();
    private List<Combat> combatsMenables = new List<Combat>();


    private void Awake()
    {
        exploitation.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void LancerExpeditions()
    {
        List<TuileManager> zoneExploitation = new List<TuileManager>(tribu.tuileActuelle.connections);
        zoneExploitation.Add(tribu.tuileActuelle);

        for (int i = 0; i < zoneExploitation.Count; i++)
        {
            GameObject exploit = Instantiate(exploitation, transform);
            Exploitation expl = exploit.GetComponent<Exploitation>();

            exploit.SetActive(true);
            expl.expedition = this;
            expl.TuileExploitee = zoneExploitation[i];
            listeExploitations.Add(expl);
        }
        Invoke("GenererCombats",0.1f);
    }

    public void RappelerExpeditions()
    {
        for (int i = 0; i < listeExploitations.Count; i++)
        {
            Destroy(listeExploitations[i].gameObject);
        }
        listeExploitations.Clear();
    }

    public void AfficherExploitations(bool afficher)
    {
        gameObject.SetActive(afficher);

        foreach(Exploitation exploit in listeExploitations)
        {
            exploit.AfficherGainRessource();
        }
        
    }

    private void TrouverHostilsAPortee()
    {
        hostilesAPortee.Clear();
        foreach (Revendication cible in tribu.revendication.TrouverRevendicateursAPortee())
        {
            Hostile hostileObserve = cible.GetComponent<Hostile>();

            if(hostileObserve)
            {
                hostilesAPortee.Add(hostileObserve);
            }
        }
    }

    public void GenererCombats()
    {
        if(tribu.guerrier.jetonAttaque)
        {
            foreach(Combat combat in combatsMenables)
            {
                Destroy(combat.gameObject);
            }
            combatsMenables.Clear();

            TrouverHostilsAPortee();

            foreach(Hostile hostile in hostilesAPortee)
            {
                Combat nvCombat = hostile.InstancierCombat();
                nvCombat.Guerrier = tribu.guerrier;
                combatsMenables.Add(nvCombat);
            }
        }
    }
}
