using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class UILoaderController : MonoBehaviour
{
    
    [SerializeField] Transform[] points;
    [SerializeField] Material UILoaderMaterial;

    List<Vector4> drawPoints = new List<Vector4>(new Vector4[100]);
    void Update()
    {
        int i;
        for (i = 0; i < points.Length; i++)
        {
            drawPoints[i] = points[i].position;
        }
        drawPoints[i] = points[0].position;


        UILoaderMaterial.SetVectorArray("_Points", drawPoints);
        UILoaderMaterial.SetFloat("_PointCount", points.Length + 1);
    }
}
