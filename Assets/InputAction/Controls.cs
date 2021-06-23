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
                },
                {
                    ""name"": ""UpAIm"",
                    ""type"": ""Value"",
                    ""id"": ""a7fb3a43-3ae0-42ec-b41a-0914108cac84"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""DownAim"",
                    ""type"": ""Value"",
                    ""id"": ""96af4422-13ad-4d23-a83a-18370024f665"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Crouch"",
                    ""type"": ""Value"",
                    ""id"": ""3d20ef92-2b82-46ea-97a2-638f425145f1"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Shoot"",
                    ""type"": ""Button"",
                    ""id"": ""f6c94dc4-4b42-423f-b080-aa70b3c0ae84"",
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
                    ""groups"": ""GamePad"",
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
                    ""groups"": ""GamePad"",
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
                    ""groups"": ""GamePad"",
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
                    ""groups"": ""GamePad"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""343a2749-4369-4489-9073-be6b154d41d3"",
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
                    ""id"": ""102c8e5c-50d8-42c9-a31d-b9bf5aa305b4"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""b253d60d-e92b-45dc-b33a-0958b82fd1da"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""aabe96f8-4a75-42fc-b6cb-418c2fb3116c"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""a0177f74-ca2c-4f3f-b6b1-80aa25e1f901"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
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
                    ""groups"": ""GamePad"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3cf9c7df-0cef-40ea-8c1b-f4ae727a5cdf"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
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
                    ""groups"": ""GamePad"",
                    ""action"": ""Ball Mode"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""306d3e99-5d2f-472d-a16f-4f091957bbc9"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Ball Mode"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e0d628b4-9221-437c-bcec-0431861926f9"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamePad"",
                    ""action"": ""UpAIm"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b63146f4-2962-4160-9fdd-0987add6b3ec"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""UpAIm"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""656bc881-74b4-4940-ae58-7c7098b42412"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamePad"",
                    ""action"": ""DownAim"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e66fd11d-d8e0-4463-80f9-9a47e7ada60c"",
                    ""path"": ""<Keyboard>/t"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""DownAim"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""34b5441a-bc28-4f08-b0a4-85d7eba1ac1f"",
                    ""path"": ""<Gamepad>/leftStickPress"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamePad"",
                    ""action"": ""Crouch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1dc5f344-14e0-4465-8fa1-5e1aac523086"",
                    ""path"": ""<Keyboard>/c"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Crouch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8ce425ca-5dbb-409d-959d-48ed48e2746e"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamePad"",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""463f3a71-5b1c-4ca9-8b75-a91ac0adef68"",
                    ""path"": ""<Keyboard>/ctrl"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""GamePad"",
            ""bindingGroup"": ""GamePad"",
            ""devices"": [
                {
                    ""devicePath"": ""<XInputController>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Keyboard"",
            ""bindingGroup"": ""Keyboard"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Standard
        m_Standard = asset.FindActionMap("Standard", throwIfNotFound: true);
        m_Standard_Movement = m_Standard.FindAction("Movement", throwIfNotFound: true);
        m_Standard_Jump = m_Standard.FindAction("Jump", throwIfNotFound: true);
        m_Standard_BallMode = m_Standard.FindAction("Ball Mode", throwIfNotFound: true);
        m_Standard_UpAIm = m_Standard.FindAction("UpAIm", throwIfNotFound: true);
        m_Standard_DownAim = m_Standard.FindAction("DownAim", throwIfNotFound: true);
        m_Standard_Crouch = m_Standard.FindAction("Crouch", throwIfNotFound: true);
        m_Standard_Shoot = m_Standard.FindAction("Shoot", throwIfNotFound: true);
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
    private readonly InputAction m_Standard_UpAIm;
    private readonly InputAction m_Standard_DownAim;
    private readonly InputAction m_Standard_Crouch;
    private readonly InputAction m_Standard_Shoot;
    public struct StandardActions
    {
        private @Controls m_Wrapper;
        public StandardActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_Standard_Movement;
        public InputAction @Jump => m_Wrapper.m_Standard_Jump;
        public InputAction @BallMode => m_Wrapper.m_Standard_BallMode;
        public InputAction @UpAIm => m_Wrapper.m_Standard_UpAIm;
        public InputAction @DownAim => m_Wrapper.m_Standard_DownAim;
        public InputAction @Crouch => m_Wrapper.m_Standard_Crouch;
        public InputAction @Shoot => m_Wrapper.m_Standard_Shoot;
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
                @UpAIm.started -= m_Wrapper.m_StandardActionsCallbackInterface.OnUpAIm;
                @UpAIm.performed -= m_Wrapper.m_StandardActionsCallbackInterface.OnUpAIm;
                @UpAIm.canceled -= m_Wrapper.m_StandardActionsCallbackInterface.OnUpAIm;
                @DownAim.started -= m_Wrapper.m_StandardActionsCallbackInterface.OnDownAim;
                @DownAim.performed -= m_Wrapper.m_StandardActionsCallbackInterface.OnDownAim;
                @DownAim.canceled -= m_Wrapper.m_StandardActionsCallbackInterface.OnDownAim;
                @Crouch.started -= m_Wrapper.m_StandardActionsCallbackInterface.OnCrouch;
                @Crouch.performed -= m_Wrapper.m_StandardActionsCallbackInterface.OnCrouch;
                @Crouch.canceled -= m_Wrapper.m_StandardActionsCallbackInterface.OnCrouch;
                @Shoot.started -= m_Wrapper.m_StandardActionsCallbackInterface.OnShoot;
                @Shoot.performed -= m_Wrapper.m_StandardActionsCallbackInterface.OnShoot;
                @Shoot.canceled -= m_Wrapper.m_StandardActionsCallbackInterface.OnShoot;
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
                @UpAIm.started += instance.OnUpAIm;
                @UpAIm.performed += instance.OnUpAIm;
                @UpAIm.canceled += instance.OnUpAIm;
                @DownAim.started += instance.OnDownAim;
                @DownAim.performed += instance.OnDownAim;
                @DownAim.canceled += instance.OnDownAim;
                @Crouch.started += instance.OnCrouch;
                @Crouch.performed += instance.OnCrouch;
                @Crouch.canceled += instance.OnCrouch;
                @Shoot.started += instance.OnShoot;
                @Shoot.performed += instance.OnShoot;
                @Shoot.canceled += instance.OnShoot;
            }
        }
    }
    public StandardActions @Standard => new StandardActions(this);
    private int m_GamePadSchemeIndex = -1;
    public InputControlScheme GamePadScheme
    {
        get
        {
            if (m_GamePadSchemeIndex == -1) m_GamePadSchemeIndex = asset.FindControlSchemeIndex("GamePad");
            return asset.controlSchemes[m_GamePadSchemeIndex];
        }
    }
    private int m_KeyboardSchemeIndex = -1;
    public InputControlScheme KeyboardScheme
    {
        get
        {
            if (m_KeyboardSchemeIndex == -1) m_KeyboardSchemeIndex = asset.FindControlSchemeIndex("Keyboard");
            return asset.controlSchemes[m_KeyboardSchemeIndex];
        }
    }
    public interface IStandardActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnBallMode(InputAction.CallbackContext context);
        void OnUpAIm(InputAction.CallbackContext context);
        void OnDownAim(InputAction.CallbackContext context);
        void OnCrouch(InputAction.CallbackContext context);
        void OnShoot(InputAction.CallbackContext context);
    }
}
