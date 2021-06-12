// GENERATED AUTOMATICALLY FROM 'Assets/InputAction/Controls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @Controls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @Controls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Controls"",
    ""maps"": [
        {
            ""name"": ""Standard"",
            ""id"": ""9ce53f9a-83b1-4568-8847-af8b0683526a"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""PassThrough"",
                    ""id"": ""89310456-9d86-4dbb-9efc-f29cadabf1b5"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""42ee3559-4148-4fda-a906-e9ad92b9e840"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Ball Mode"",
                    ""type"": ""Button"",
                    ""id"": ""d6af86fb-10f8-4f6b-bc32-b46f621a076d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Movement"",
                    ""id"": ""07d6755d-b22a-4762-9276-5ff25db575da"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""c640341a-e9a0-4122-85f7-f63ecd0b72b1"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""80d879e1-867f-4b70-893e-0ecf32defeea"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""60ef0a41-d35d-4c25-9428-3093a1d52ca1"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""989088f7-ec00-412a-b5c7-a69e492e4e06"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""0401fc79-1a03-4022-9a76-1a67249df891"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8b1f747b-2f7e-4b6c-8f97-c94496d591ba"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Ball Mode"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Standard
        m_Standard = asset.FindActionMap("Standard", throwIfNotFound: true);
        m_Standard_Movement = m_Standard.FindAction("Movement", throwIfNotFound: true);
        m_Standard_Jump = m_Standard.FindAction("Jump", throwIfNotFound: true);
        m_Standard_BallMode = m_Standard.FindAction("Ball Mode", throwIfNotFound: true);
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

    // Standard
    private readonly InputActionMap m_Standard;
    private IStandardActions m_StandardActionsCallbackInterface;
    private readonly InputAction m_Standard_Movement;
    private readonly InputAction m_Standard_Jump;
    private readonly InputAction m_Standard_BallMode;
    public struct StandardActions
    {
        private @Controls m_Wrapper;
        public StandardActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_Standard_Movement;
        public InputAction @Jump => m_Wrapper.m_Standard_Jump;
        public InputAction @BallMode => m_Wrapper.m_Standard_BallMode;
        public InputActionMap Get() { return m_Wrapper.m_Standard; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(StandardActions set) { return set.Get(); }
        public void SetCallbacks(IStandardActions instance)
        {
            if (m_Wrapper.m_StandardActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_StandardActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_StandardActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_StandardActionsCallbackInterface.OnMovement;
                @Jump.started -= m_Wrapper.m_StandardActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_StandardActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_StandardActionsCallbackInterface.OnJump;
                @BallMode.started -= m_Wrapper.m_StandardActionsCallbackInterface.OnBallMode;
                @BallMode.performed -= m_Wrapper.m_StandardActionsCallbackInterface.OnBallMode;
                @BallMode.canceled -= m_Wrapper.m_StandardActionsCallbackInterface.OnBallMode;
            }
            m_Wrapper.m_StandardActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @BallMode.started += instance.OnBallMode;
                @BallMode.performed += instance.OnBallMode;
                @BallMode.canceled += instance.OnBallMode;
            }
        }
    }
    public StandardActions @Standard => new StandardActions(this);
    public interface IStandardActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnBallMode(InputAction.CallbackContext context);
    }
}
