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

        public RenderTextureFormat texFormat;
        public string textureName;

        [SerializeField]Texture textureRT;


        private void Awake()
        {
            textureRT = new RenderTexture(Screen.width, Screen.height, 0, texFormat, RenderTextureReadWrite.Default);
            textureRT.name = textureName;
            if(commandBufferName.Contains(commandBufferName))
            {
                cBufferDist[commandBufferName] = this;
            }
            else cBufferDist.Add(commandBufferName, this);
            Reset = ClearUp;
        }

        public static CustomBufferRender GetCommandBuffer(string cBufferName)
        {
            if(cBufferDist.ContainsKey(cBufferName)){
                return cBufferDist[cBufferName];
            }

            return null;
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
            cBufferDist.Remove(commandBufferName);
        }


        private void OnDestroy() {
            ClearUp();
            cBufferDist.Remove(commandBufferName);
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


            RenderTargetIdentifier rtID = new RenderTargetIdentifier(textureRT);

            commandBuffer.SetRenderTarget(rtID);
            commandBuffer.ClearRenderTarget(true, true, new Color(0,0,0,0));
            

            foreach (RenderObject item in renderObjects)
            {
                commandBuffer.DrawRenderer(item.renderer, item.material);
            }
            
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
