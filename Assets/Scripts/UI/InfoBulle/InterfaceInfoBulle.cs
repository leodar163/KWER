using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[RequireComponent(typeof(Image), typeof(ContentSizeFitter))]
public class InterfaceInfoBulle : MonoBehaviour
{
    private TextMeshProUGUI texteMP;
    private RectTransform rectT;

    private static InterfaceInfoBulle cela;

    public static InterfaceInfoBulle Actuel
    {
        get
        {
            if(!cela)
            {
                cela = FindObjectOfType<InterfaceInfoBulle>();
            }
            return cela;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        texteMP = GetComponentInChildren<TextMeshProUGUI>();
        rectT = GetComponent<RectTransform>();
        CacherBulle();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AfficherBulle(string texte, Rect rect, Vector3 position)
    {
        texteMP.text = texte;
        Vector3 nvlPosition = Camera.main.WorldToScreenPoint(position);
        nvlPosition.z = rectT.position.z;
        rectT.position = nvlPosition;

        Resolution resol = Screen.currentResolution;
        Vector2 nvPivot = new Vector2();

        if(rectT.position.x < resol.width/2)
        {
            nvPivot.x = 0;
        }
        else
        {
            nvPivot.x = 1;
        }

        if(rectT.position.y < resol.height/2)
        {
            nvPivot.y = 0;
        }
        else
        {
            nvPivot.y = 1;
        }

        rectT.pivot = nvPivot;
    }

    public void AfficherBulle(string texte, RectTransform rectTransform)
    {
        texteMP.text = texte;
        Vector3 nvlPosition = rectTransform.position;
        nvlPosition.z = rectT.position.z;
        rectT.position = nvlPosition;

        Resolution resol = Screen.currentResolution;
        Vector2 nvPivot = new Vector2();

        if (rectT.position.x < resol.width / 2)
        {
            nvPivot.x = 0;
        }
        else
        {
            nvPivot.x = 1;
        }

        if (rectT.position.y < resol.height / 2)
        {
            nvPivot.y = 0;
        }
        else
        {
            nvPivot.y = 1;
        }

        rectT.pivot = nvPivot;
    }

    public void CacherBulle()
    {
        rectT.position = new Vector3(-1000, -1000, rectT.position.z);
    }
}
