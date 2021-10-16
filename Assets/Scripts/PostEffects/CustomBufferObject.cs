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
            CreateAndAdd();
            CustomBufferRender.AddObject(renderObject);
        }
        void OnEnable()
        {
            CreateAndAdd();
            CustomBufferRender.AddObject(renderObject);
        }

        void CreateAndAdd()
        {
            if (renderObject == null)
            {
                renderObject = new RenderObject()
                {
                    renderer = renderer,
                    material = material
                };
            }
            else
            {
                renderObject.renderer = renderer;
                renderObject.material = material;
            }
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