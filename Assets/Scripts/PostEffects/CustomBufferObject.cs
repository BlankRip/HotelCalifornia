using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Rendering
{
    public class CustomBufferObject : MonoBehaviour
    {
        public Renderer renderer;
        public Material material;
        public string bufferName;
        
        

        RenderObject renderObject;


        void Start()
        {
            CreateAndAdd();
            CustomBufferRender.GetCommandBuffer(bufferName).AddObject(renderObject);
        }
        void OnEnable()
        {
            CreateAndAdd();
            CustomBufferRender.GetCommandBuffer(bufferName).AddObject(renderObject);
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
            if (renderObject != null) CustomBufferRender.GetCommandBuffer(bufferName).RemoveObject(renderObject);
        }
        void OnDestroy()
        {
            if (renderObject != null) CustomBufferRender.GetCommandBuffer(bufferName).RemoveObject(renderObject);
        }
    }
}