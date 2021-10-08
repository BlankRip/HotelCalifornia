using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Rendering
{
    [ExecuteAlways]
    public class CustomBufferObject : MonoBehaviour
    {
        public Renderer renderer;
        public Material material;

        RenderObject renderObject;

        void Start()
        {
            if (renderObject == null)
            {
                renderObject = new RenderObject()
                {
                    renderer = renderer,
                    material = material
                };
            }
            CustomBufferRender.AddObject(renderObject);
        }
        void OnEnable()
        {
            if (renderObject == null)
            {
                renderObject = new RenderObject()
                {
                    renderer = renderer,
                    material = material
                };
            }
            CustomBufferRender.AddObject(renderObject);
        }
        void OnDisable()
        {
            if (renderObject != null) CustomBufferRender.RemoveObject(renderObject);
        }
        void OnDestroy()
        {
            if (renderObject != null) CustomBufferRender.RemoveObject(renderObject);
        }
    }
}