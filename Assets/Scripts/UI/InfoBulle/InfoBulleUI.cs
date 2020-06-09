using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InfoBulleUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string textInfoBulle = "Texte d'information";

    public void OnPointerEnter(PointerEventData eventData)
    {
        InterfaceInfoBulle.Actuel.AfficherBulle(textInfoBulle, GetComponent<RectTransform>());
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        InterfaceInfoBulle.Actuel.CacherBulle();

    }

    private void OnDestroy()
    {
        if (InterfaceInfoBulle.Actuel) InterfaceInfoBulle.Actuel.CacherBulle();
    }

    private void OnDisable()
    {
        if (InterfaceInfoBulle.Actuel) InterfaceInfoBulle.Actuel.CacherBulle();
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
