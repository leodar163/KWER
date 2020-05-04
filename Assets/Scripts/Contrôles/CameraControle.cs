using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControle : MonoBehaviour
{
    float vitesse = 10f;
    Vector3 vecteurMove;
    Camera camera;
    RectTransform rectTrans;

    [SerializeField] float maxZoom = 10f;
    [SerializeField] float minZoom = 4f;
    [SerializeField] float vitesseZoom = 0.2f;
    [HideInInspector]public bool sourisAccrochee = false;

    ControleSouris controleSouris;

    // Start is called before the first frame update
    void Start()
    {
        controleSouris = FindObjectOfType<ControleSouris>();
        rectTrans = GetComponent<RectTransform>();
        camera = GetComponent<Camera>();
        vecteurMove = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        MoveCamera();
        ZoomCamera();

        BougerAvecSouris();
    }

    private void MaJTailleUI()
    {
        float zoom = camera.orthographicSize;

        rectTrans.sizeDelta = new Vector2(3.5f * zoom, 2 * zoom);
    }

    private void MoveCamera()
    {
        vecteurMove = transform.position;

        if(Input.GetKey("q"))
        {
            vecteurMove.x -= 1 * vitesse * Time.deltaTime;  
        }

        if (Input.GetKey("d"))
        {
            vecteurMove.x += 1 * vitesse * Time.deltaTime;
        }

        if (Input.GetKey("z"))
        {
            vecteurMove.y += 1 * vitesse * Time.deltaTime;
        }

        if (Input.GetKey("s"))
        {
            vecteurMove.y -= 1 * vitesse * Time.deltaTime;
        }

        transform.position = vecteurMove;


       

    }

    private void ZoomCamera()
    {
        float molette = Input.GetAxis("Mouse ScrollWheel");
        
        

        if (molette < 0 && camera.orthographicSize < maxZoom )
        {
            

            camera.orthographicSize += vitesseZoom;
            
        }

        if (molette > 0 && camera.orthographicSize > minZoom)
        {
            
            camera.orthographicSize -= vitesseZoom;
        }
    }


    public void BougerAvecSouris()
    {
        
        if(sourisAccrochee)
        {
        Vector3 decalage =  Camera.main.ScreenToWorldPoint(Input.mousePosition) - controleSouris.pointAccrocheSouris;
        transform.position -= decalage;
        }
    }
}
