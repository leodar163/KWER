using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "NvlEvenement", menuName = "Evénements/Evément")]
public class Evenement : ScriptableObject
{
    public string titre;
    public string description;
    public Sprite illustration;
    public List<Choix> listeChoix = new List<Choix>();


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
