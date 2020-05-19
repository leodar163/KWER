using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Expedition : MonoBehaviour
{
    [SerializeField] public Tribu tribu;
    [SerializeField] private GameObject exploitation;
    private List<Exploitation> listeExploitations = new List<Exploitation>();

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

            expl.expedition = this;
            expl.TuileExploitee = zoneExploitation[i];
            listeExploitations.Add(expl);
        }
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
}
