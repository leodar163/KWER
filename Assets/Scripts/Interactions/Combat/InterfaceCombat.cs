using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class InterfaceCombat : MonoBehaviour
{
    [SerializeField] private StatsCombat statsTribu;
    [SerializeField] private StatsCombat statsEnnemi;
    [HideInInspector] public Hostile ennemi;
    [HideInInspector] public Guerrier guerrier;

    [HideInInspector] public UnityEvent eventMAJInterface;

    [SerializeField] private GameObject slot;
    private List<SlotCombat> listeSlots = new List<SlotCombat>();
    // Start is called before the first frame update
    void Start()
    {
        slot.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        MAJInterface();
    }

    public void MAJInterface()
    {
        if(ennemi && guerrier)
        {
            statsEnnemi.MAJStats(ennemi.nbrCombattant, ennemi.attaque, ennemi.defense);
            statsTribu.MAJStats(guerrier);
            eventMAJInterface.Invoke();
            MAJSlots();
        }
    }

    #region SLOTS
    public void ReinitSlots()
    {
        foreach(Slot slot in listeSlots)
        {
            Destroy(slot.gameObject);
        }
        listeSlots.Clear();
    }
     
    private void MAJSlots()
    {
        if(guerrier)
        {
            if(listeSlots.Count != guerrier.tribu.demographie.taillePopulation -1)
            {
                GenererSlot(guerrier.tribu.demographie.taillePopulation - 1 - listeSlots.Count);
            }
        }
    }

    private void GenererSlot(int nbrSlot)
    {
        if(nbrSlot > 0)
        {
            for (int i = 0; i < nbrSlot; i++)
            {
                GameObject nvSlot = Instantiate(slot, transform);
                nvSlot.SetActive(true);
                SlotCombat nvSlotCombat= nvSlot.GetComponent<SlotCombat>();
                nvSlotCombat.Guerrier = guerrier;
                listeSlots.Add(nvSlotCombat);
            }
        }
        else
        {
            for (int i = 0; i < Math.Abs(nbrSlot); i++)
            {
                Destroy(listeSlots[listeSlots.Count - 1]);
                listeSlots.RemoveAt(listeSlots.Count - 1);
            }
        }
        AjusterRoueSlot();
    }

    private void AjusterRoueSlot()
    {
        float ecart = 0.6f;

        float rayon = 0.30f;
        int rangee = 0;
        float index = 0.4f;

        for (int i = 0; i < listeSlots.Count; i++)
        {
            if (i % 9 == 0 && i != 0)
            {
                index = 0.4f;
                rangee++;
            }
            float x = Mathf.Cos(index) * (rayon + (0.125f * rangee));
            float y = Mathf.Sin(index) * (rayon + (0.125f * rangee));

            Vector3 direction = new Vector3(x, y + 0.5f);

            listeSlots[i].transform.position = transform.position;
            listeSlots[i].transform.position += direction;

            index += ecart;
        }
    }
    #endregion
}
