using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    Camera camera;
    // Start is called before the first frame update
    void Start()
    {
        camera = FindObjectOfType<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        MaJTailleUI();   
    }

    public void MaJTailleUI()
    {
        float zoom = camera.orthographicSize;
        RectTransform[] tailleEnfants = GetComponentsInChildren<RectTransform>();

        foreach(RectTransform rectEnfant in tailleEnfants)
        {
            if(rectEnfant.gameObject != gameObject)
            {
                rectEnfant.localScale = new Vector2(0.25f * zoom, 0.25f * zoom);
            }
            
        }

        
    }
}
