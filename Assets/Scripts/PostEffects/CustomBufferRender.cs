using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;


namespace Knotgames.Rendering
{
    public class CustomBufferRender : MonoBehaviour
    {
        [SerializeField] CameraEvent bufferLocation;
        private CommandBuffer commandBuffer;
        static List<RenderObject> renderObjects = new List<RenderObject>();
        Dictionary<Camera, CommandBuffer> camAndBuffer = new Dictionary<Camera, CommandBuffer>();
        Camera cam;
        static event System.Action Reset;

        public static void AddObject(RenderObject rObject)
        {
            renderObjects.Remove(rObject);
            renderObjects.Add(rObject);
            Reset?.Invoke();
        }

        public static void RemoveObject(RenderObject rObject)
        {
            renderObjects.Remove(rObject);
            Reset?.Invoke();
        }

        public void OnDisable()
        {
            ClearUp();
        }

        public void OnEnable()
        {
            ClearUp();
        }

        void ClearUp()
        {
            foreach (var cam in camAndBuffer)
            {
                if (cam.Key)
                    cam.Key.RemoveCommandBuffer(bufferLocation, cam.Value);
            }
            camAndBuffer.Clear();
        }

        void Awake()
        {
            Reset = ClearUp;
        }

        void Update()
        {
            bool render = this.gameObject.activeSelf;
            cam = Camera.current;

            if (!render)
            {
                ClearUp();
                return;
            }

            if (!cam || camAndBuffer.ContainsKey(cam)) return;

            commandBuffer = new CommandBuffer();
            commandBuffer.name = "PossesionEffect";
            camAndBuffer[cam] = commandBuffer;

            int texID = Shader.PropertyToID("TempTex");
            commandBuffer.GetTemporaryRT(texID, -1, -1, 24, FilterMode.Bilinear, RenderTextureFormat.R16);
            commandBuffer.SetRenderTarget(texID);
            commandBuffer.ClearRenderTarget(true, true, Color.black);

            foreach (RenderObject item in renderObjects)
            {
                commandBuffer.DrawRenderer(item.renderer, item.material);
            }

            commandBuffer.SetGlobalTexture("_PossesedTex", texID);

            cam.AddCommandBuffer(bufferLocation, commandBuffer);
        }
    }

    public class RenderObject
    {
        public Material material;
        public Renderer renderer;
    }

}