// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Inputs/MovementControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @MovementControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @MovementControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""MovementControls"",
    ""maps"": [
        {
            ""name"": ""GroundMovement"",
            ""id"": ""eb872a0a-3240-43df-950c-95df46e49a9e"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""PassThrough"",
                    ""id"": ""bff501d8-9925-4c5c-a01f-1daa8e930b50"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MouseX"",
                    ""type"": ""PassThrough"",
                    ""id"": ""ba649b08-51fa-4320-8b0d-df79dbc66b38"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MouseY"",
                    ""type"": ""PassThrough"",
                    ""id"": ""4757167c-f546-4ecb-8201-981fc48406fc"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""LowerStart"",
                    ""type"": ""Button"",
                    ""id"": ""3b8d9a00-a3b3-4e33-8cb2-663fcce736ac"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""RaiseStart"",
                    ""type"": ""Button"",
                    ""id"": ""f731835c-1042-49c4-a589-48bd6c3aaeea"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""SprintStart"",
                    ""type"": ""Button"",
                    ""id"": ""333b5809-9c76-4e6f-b06d-b2faa560ca4e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""LowerFinish"",
                    ""type"": ""Button"",
                    ""id"": ""824aa626-2f6d-4263-a97c-88a799389cd3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=1)""
                },
                {
                    ""name"": ""RaiseFinish"",
                    ""type"": ""Button"",
                    ""id"": ""f7565377-36a0-41ac-9766-4a347b196eee"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=1)""
                },
                {
                    ""name"": ""SprintFinish"",
                    ""type"": ""Button"",
                    ""id"": ""d0406625-7c96-47fc-a21a-4f9442883e67"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=1)""
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""079caaad-73e7-4859-aa3d-8f0accfb4d4b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""23eb1a9b-5198-4c91-9c08-095b82db1bde"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""3815eff1-caf5-422a-a588-581035ede66a"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""2eeb6b26-6e06-42bd-a75c-66893ddeeeae"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""9d46f34c-5ddc-4587-b2d2-3131136f5f71"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""4e779b87-6c1a-4969-a20e-b21ebb247d8a"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""00826ba6-47cc-4e47-8d75-250dd7ba602f"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamePad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6383b6fc-3ec4-4519-adbe-0343271ab7be"",
                    ""path"": ""<Mouse>/delta/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""MouseX"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ffc28b37-96a1-4f94-9f69-74a988947b5b"",
                    ""path"": ""<Gamepad>/rightStick/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamePad"",
                    ""action"": ""MouseX"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c6017505-3325-4108-a337-0dcab72133a2"",
                    ""path"": ""<Mouse>/delta/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""MouseY"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0580f3d9-2f5c-4e54-82b4-18eaee4b00ff"",
                    ""path"": ""<Gamepad>/rightStick/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamePad"",
                    ""action"": ""MouseY"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""795f1d02-1a46-4627-b716-333858e60072"",
                    ""path"": ""<Keyboard>/leftCtrl"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LowerStart"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5d4b34e6-c737-42ba-b224-91dec243b2c5"",
                    ""path"": ""<Gamepad>/rightStickPress"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamePad"",
                    ""action"": ""LowerStart"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c3b1478f-1260-4553-acaf-287663dc7bea"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SprintStart"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""def2895c-1d29-45fd-8354-67bdb7dae414"",
                    ""path"": ""<Gamepad>/leftStickPress"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamePad"",
                    ""action"": ""SprintStart"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c107b99e-6dd9-4b03-ac9e-6081de98c857"",
                    ""path"": ""<Keyboard>/leftCtrl"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LowerFinish"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""25cb374a-1771-40d3-99f4-596e2e7e5803"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SprintFinish"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ba374832-9f89-49cb-8889-9532792e11ab"",
                    ""path"": ""<Gamepad>/leftStickPress"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamePad"",
                    ""action"": ""SprintFinish"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ea326ea3-479e-48aa-84f8-d1e58ad827c9"",
                    ""path"": ""<Gamepad>/rightStickPress"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamePad"",
                    ""action"": ""LowerFinish"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1740b907-cca5-4f94-8265-d85711b7f7ce"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RaiseStart"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9ba5ddc2-f61e-4488-aa1e-2711926a1435"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamePad"",
                    ""action"": ""RaiseStart"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0cca89f6-55b8-4b8f-878b-f4d1bbac5757"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RaiseFinish"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8794912a-4c5e-4ea9-bd6d-ed690c2d219c"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamePad"",
                    ""action"": ""RaiseFinish"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""944a5ff0-d56d-498e-9805-1994156771b5"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cb8362af-d642-4c4a-877b-f8a0c7a6870c"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamePad"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard&Mouse"",
            ""bindingGroup"": ""Keyboard&Mouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""GamePad"",
            ""bindingGroup"": ""GamePad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // GroundMovement
        m_GroundMovement = asset.FindActionMap("GroundMovement", throwIfNotFound: true);
        m_GroundMovement_Move = m_GroundMovement.FindAction("Move", throwIfNotFound: true);
        m_GroundMovement_MouseX = m_GroundMovement.FindAction("MouseX", throwIfNotFound: true);
        m_GroundMovement_MouseY = m_GroundMovement.FindAction("MouseY", throwIfNotFound: true);
        m_GroundMovement_LowerStart = m_GroundMovement.FindAction("LowerStart", throwIfNotFound: true);
        m_GroundMovement_RaiseStart = m_GroundMovement.FindAction("RaiseStart", throwIfNotFound: true);
        m_GroundMovement_SprintStart = m_GroundMovement.FindAction("SprintStart", throwIfNotFound: true);
        m_GroundMovement_LowerFinish = m_GroundMovement.FindAction("LowerFinish", throwIfNotFound: true);
        m_GroundMovement_RaiseFinish = m_GroundMovement.FindAction("RaiseFinish", throwIfNotFound: true);
        m_GroundMovement_SprintFinish = m_GroundMovement.FindAction("SprintFinish", throwIfNotFound: true);
        m_GroundMovement_Jump = m_GroundMovement.FindAction("Jump", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // GroundMovement
    private readonly InputActionMap m_GroundMovement;
    private IGroundMovementActions m_GroundMovementActionsCallbackInterface;
    private readonly InputAction m_GroundMovement_Move;
    private readonly InputAction m_GroundMovement_MouseX;
    private readonly InputAction m_GroundMovement_MouseY;
    private readonly InputAction m_GroundMovement_LowerStart;
    private readonly InputAction m_GroundMovement_RaiseStart;
    private readonly InputAction m_GroundMovement_SprintStart;
    private readonly InputAction m_GroundMovement_LowerFinish;
    private readonly InputAction m_GroundMovement_RaiseFinish;
    private readonly InputAction m_GroundMovement_SprintFinish;
    private readonly InputAction m_GroundMovement_Jump;
    public struct GroundMovementActions
    {
        private @MovementControls m_Wrapper;
        public GroundMovementActions(@MovementControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_GroundMovement_Move;
        public InputAction @MouseX => m_Wrapper.m_GroundMovement_MouseX;
        public InputAction @MouseY => m_Wrapper.m_GroundMovement_MouseY;
        public InputAction @LowerStart => m_Wrapper.m_GroundMovement_LowerStart;
        public InputAction @RaiseStart => m_Wrapper.m_GroundMovement_RaiseStart;
        public InputAction @SprintStart => m_Wrapper.m_GroundMovement_SprintStart;
        public InputAction @LowerFinish => m_Wrapper.m_GroundMovement_LowerFinish;
        public InputAction @RaiseFinish => m_Wrapper.m_GroundMovement_RaiseFinish;
        public InputAction @SprintFinish => m_Wrapper.m_GroundMovement_SprintFinish;
        public InputAction @Jump => m_Wrapper.m_GroundMovement_Jump;
        public InputActionMap Get() { return m_Wrapper.m_GroundMovement; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GroundMovementActions set) { return set.Get(); }
        public void SetCallbacks(IGroundMovementActions instance)
        {
            if (m_Wrapper.m_GroundMovementActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_GroundMovementActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_GroundMovementActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_GroundMovementActionsCallbackInterface.OnMove;
                @MouseX.started -= m_Wrapper.m_GroundMovementActionsCallbackInterface.OnMouseX;
                @MouseX.performed -= m_Wrapper.m_GroundMovementActionsCallbackInterface.OnMouseX;
                @MouseX.canceled -= m_Wrapper.m_GroundMovementActionsCallbackInterface.OnMouseX;
                @MouseY.started -= m_Wrapper.m_GroundMovementActionsCallbackInterface.OnMouseY;
                @MouseY.performed -= m_Wrapper.m_GroundMovementActionsCallbackInterface.OnMouseY;
                @MouseY.canceled -= m_Wrapper.m_GroundMovementActionsCallbackInterface.OnMouseY;
                @LowerStart.started -= m_Wrapper.m_GroundMovementActionsCallbackInterface.OnLowerStart;
                @LowerStart.performed -= m_Wrapper.m_GroundMovementActionsCallbackInterface.OnLowerStart;
                @LowerStart.canceled -= m_Wrapper.m_GroundMovementActionsCallbackInterface.OnLowerStart;
                @RaiseStart.started -= m_Wrapper.m_GroundMovementActionsCallbackInterface.OnRaiseStart;
                @RaiseStart.performed -= m_Wrapper.m_GroundMovementActionsCallbackInterface.OnRaiseStart;
                @RaiseStart.canceled -= m_Wrapper.m_GroundMovementActionsCallbackInterface.OnRaiseStart;
                @SprintStart.started -= m_Wrapper.m_GroundMovementActionsCallbackInterface.OnSprintStart;
                @SprintStart.performed -= m_Wrapper.m_GroundMovementActionsCallbackInterface.OnSprintStart;
                @SprintStart.canceled -= m_Wrapper.m_GroundMovementActionsCallbackInterface.OnSprintStart;
                @LowerFinish.started -= m_Wrapper.m_GroundMovementActionsCallbackInterface.OnLowerFinish;
                @LowerFinish.performed -= m_Wrapper.m_GroundMovementActionsCallbackInterface.OnLowerFinish;
                @LowerFinish.canceled -= m_Wrapper.m_GroundMovementActionsCallbackInterface.OnLowerFinish;
                @RaiseFinish.started -= m_Wrapper.m_GroundMovementActionsCallbackInterface.OnRaiseFinish;
                @RaiseFinish.performed -= m_Wrapper.m_GroundMovementActionsCallbackInterface.OnRaiseFinish;
                @RaiseFinish.canceled -= m_Wrapper.m_GroundMovementActionsCallbackInterface.OnRaiseFinish;
                @SprintFinish.started -= m_Wrapper.m_GroundMovementActionsCallbackInterface.OnSprintFinish;
                @SprintFinish.performed -= m_Wrapper.m_GroundMovementActionsCallbackInterface.OnSprintFinish;
                @SprintFinish.canceled -= m_Wrapper.m_GroundMovementActionsCallbackInterface.OnSprintFinish;
                @Jump.started -= m_Wrapper.m_GroundMovementActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_GroundMovementActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_GroundMovementActionsCallbackInterface.OnJump;
            }
            m_Wrapper.m_GroundMovementActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @MouseX.started += instance.OnMouseX;
                @MouseX.performed += instance.OnMouseX;
                @MouseX.canceled += instance.OnMouseX;
                @MouseY.started += instance.OnMouseY;
                @MouseY.performed += instance.OnMouseY;
                @MouseY.canceled += instance.OnMouseY;
                @LowerStart.started += instance.OnLowerStart;
                @LowerStart.performed += instance.OnLowerStart;
                @LowerStart.canceled += instance.OnLowerStart;
                @RaiseStart.started += instance.OnRaiseStart;
                @RaiseStart.performed += instance.OnRaiseStart;
                @RaiseStart.canceled += instance.OnRaiseStart;
                @SprintStart.started += instance.OnSprintStart;
                @SprintStart.performed += instance.OnSprintStart;
                @SprintStart.canceled += instance.OnSprintStart;
                @LowerFinish.started += instance.OnLowerFinish;
                @LowerFinish.performed += instance.OnLowerFinish;
                @LowerFinish.canceled += instance.OnLowerFinish;
                @RaiseFinish.started += instance.OnRaiseFinish;
                @RaiseFinish.performed += instance.OnRaiseFinish;
                @RaiseFinish.canceled += instance.OnRaiseFinish;
                @SprintFinish.started += instance.OnSprintFinish;
                @SprintFinish.performed += instance.OnSprintFinish;
                @SprintFinish.canceled += instance.OnSprintFinish;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
            }
        }
    }
    public GroundMovementActions @GroundMovement => new GroundMovementActions(this);
    private int m_KeyboardMouseSchemeIndex = -1;
    public InputControlScheme KeyboardMouseScheme
    {
        get
        {
            if (m_KeyboardMouseSchemeIndex == -1) m_KeyboardMouseSchemeIndex = asset.FindControlSchemeIndex("Keyboard&Mouse");
            return asset.controlSchemes[m_KeyboardMouseSchemeIndex];
        }
    }
    private int m_GamePadSchemeIndex = -1;
    public InputControlScheme GamePadScheme
    {
        get
        {
            if (m_GamePadSchemeIndex == -1) m_GamePadSchemeIndex = asset.FindControlSchemeIndex("GamePad");
            return asset.controlSchemes[m_GamePadSchemeIndex];
        }
    }
    public interface IGroundMovementActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnMouseX(InputAction.CallbackContext context);
        void OnMouseY(InputAction.CallbackContext context);
        void OnLowerStart(InputAction.CallbackContext context);
        void OnRaiseStart(InputAction.CallbackContext context);
        void OnSprintStart(InputAction.CallbackContext context);
        void OnLowerFinish(InputAction.CallbackContext context);
        void OnRaiseFinish(InputAction.CallbackContext context);
        void OnSprintFinish(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
    }
}
