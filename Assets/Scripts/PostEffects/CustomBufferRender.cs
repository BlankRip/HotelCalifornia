using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace Knotgames.Rendering
{
    public class CustomBufferRender : MonoBehaviour
    {
        [SerializeField]
        CameraEvent bufferLocation;

        List<RenderObject> renderObjects = new List<RenderObject>();

        static Dictionary<string, CustomBufferRender>
            cBufferDist = new Dictionary<string, CustomBufferRender>();

        Dictionary<Camera, CommandBuffer>
            camAndBuffer = new Dictionary<Camera, CommandBuffer>();

        Camera cam;

        event System.Action Reset;

        public bool makeTexture;

        public string commandBufferName;

        public string textureName;

        Texture textureRT;


        private void Awake()
        {
            textureRT = new RenderTexture(Screen.width, Screen.height, 0, RenderTextureFormat.R8, RenderTextureReadWrite.Default);
            textureRT.name = textureName;
            cBufferDist.Add(commandBufferName, this);
            Reset = ClearUp;
        }

        public static CustomBufferRender GetCommandBuffer(string cBufferName)
        {
            return cBufferDist[cBufferName];
        }

        public void AddObject(RenderObject rObject)
        {
            renderObjects.Remove (rObject);
            renderObjects.Add (rObject);
            Reset?.Invoke();
        }

        public void RemoveObject(RenderObject rObject)
        {
            renderObjects.Remove (rObject);
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

            CommandBuffer commandBuffer = new CommandBuffer();
            commandBuffer.name = commandBufferName;
            camAndBuffer[cam] = commandBuffer;

           // int texID = Shader.PropertyToID(textureName);

            RenderTargetIdentifier rtID = new RenderTargetIdentifier(textureRT);

            

/*
            commandBuffer
                .GetTemporaryRT(texID,
                -1,
                -1,
                24,
                FilterMode.Bilinear,
                RenderTextureFormat.R16);
      
*/


            commandBuffer.SetRenderTarget(rtID);
            commandBuffer.ClearRenderTarget(true, true, Color.black);
            

            foreach (RenderObject item in renderObjects)
            {
                commandBuffer.DrawRenderer(item.renderer, item.material);
            }
            //commandBuffer.SetGlobalTexture(textureName, texID);
            //textureRT = Shader.GetGlobalTexture(texID);

            Shader.SetGlobalTexture(textureName, textureRT);

            cam.AddCommandBuffer (bufferLocation, commandBuffer);
        }
    }

    public class RenderObject
    {
        public Material material;

        public Renderer renderer;
    }
}
