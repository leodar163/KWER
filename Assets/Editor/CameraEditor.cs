using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Camera))]
public class CameraEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Camera camera = (Camera)target;
        RectTransform rectTrans = camera.gameObject.GetComponent<RectTransform>();
        
        if (DrawDefaultInspector())
        {
            rectTrans.sizeDelta = new Vector2(3.5f * camera.orthographicSize, 2 * camera.orthographicSize);
        }
        
    }
    
}
