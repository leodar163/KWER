﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class InfoBulle : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [TextArea]
    public string texteInfoBulle = "Texte d'information";
   
    private RectTransform bulleRect;

    private void OnDestroy()
    {
        if (InterfaceInfoBulle.Actuel) InterfaceInfoBulle.Actuel.CacherBulle();
    }

    private void OnDisable()
    {
        if(InterfaceInfoBulle.Actuel)InterfaceInfoBulle.Actuel.CacherBulle();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        
        InterfaceInfoBulle.Actuel.AfficherBulle(texteInfoBulle);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        InterfaceInfoBulle.Actuel.CacherBulle();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
