using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay {
    public class PlayerCamera : MonoBehaviour, IPlayerCamera
    {
        [SerializeField] ScriptablePlayerCamera camInterface;
        [SerializeField] Camera cam;
        [SerializeField] float sensitivity = 3;
        [SerializeField] float maxUpAngle = 80;
        [SerializeField] float maxDownAngle = -80;
        
        private bool ghost;
        private Transform cameraPosition;
        private Transform player;

        private float mouseX;
        private float mouseY;
        private float rotX = 0.0f;
        private float rotY = 0.0f;
        private float rotZ = 0.0f;

        private bool initilized;
        private bool locked;
        
        private void Awake()
        {
            if(cam == null)
                cam = this.GetComponent<Camera>();
            camInterface.cam = this;

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        
        private void Update()
        {
            if(!locked) {
                mouseX = Input.GetAxis("Mouse X") * sensitivity;
                mouseY = Input.GetAxis("Mouse Y") * sensitivity;

                rotX -= mouseY;
                rotX = Mathf.Clamp(rotX, maxDownAngle, maxUpAngle);
                rotY += mouseX;

                if(initilized) {
                    transform.localRotation = Quaternion.Euler(rotX, rotY, rotZ);
                    transform.position = cameraPosition.position;
                    if(ghost)
                        player.rotation = transform.rotation;
                    else
                        player.Rotate(Vector3.up * mouseX);
                }
            }
        }

        public void Initilize(Transform player, Transform camPos, bool ghost) {
            this.player = player;
            this.cameraPosition = camPos;
            this.ghost = ghost;
            initilized = true;

            Invoke("WarpUpInitilize", 1.5f);
        }

        private void WarpUpInitilize() {
            Quaternion targetRotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
            player.rotation = targetRotation;
        }

        public void Lock(bool lockState) {
            locked = lockState;
        }

        public Transform GetTransform() {
            return transform;
        }
    }
}