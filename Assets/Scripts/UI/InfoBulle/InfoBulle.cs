using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class InfoBulle : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string textInfoBulle = "Texte d'information";
   
    private RectTransform bulleRect;
    public void OnPointerEnter(PointerEventData eventData)
    {
        Rect forme;
        if (GetComponent<RectTransform>())
        {
            forme = new Rect(GetComponent<RectTransform>().rect);
            InterfaceInfoBulle.Actuel.AfficherBulle(textInfoBulle, forme, transform.position);
        }
        else if (GetComponent<Sprite>())
        {
            forme = new Rect(GetComponent<Sprite>().rect);
            InterfaceInfoBulle.Actuel.AfficherBulle(textInfoBulle, forme, transform.position);
        }
        
    }

    private void OnDestroy()
    {
        if (InterfaceInfoBulle.Actuel) InterfaceInfoBulle.Actuel.CacherBulle();
    }

    private void OnDisable()
    {
        if(InterfaceInfoBulle.Actuel)InterfaceInfoBulle.Actuel.CacherBulle();
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
