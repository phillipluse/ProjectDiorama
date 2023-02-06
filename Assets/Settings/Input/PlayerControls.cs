//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.4
//     from Assets/Settings/Input/PlayerControls.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @PlayerControls : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""PlayerActions"",
            ""id"": ""687196b5-a7f8-4c7a-a040-1ccb8ec4dd1f"",
            ""actions"": [
                {
                    ""name"": ""Select"",
                    ""type"": ""Button"",
                    ""id"": ""c87c0b2d-7995-447e-9307-6262af5cfce4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""OneButton"",
                    ""type"": ""Button"",
                    ""id"": ""a59dcc06-0b8d-4c42-92cc-61d15b8f8571"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""TwoButton"",
                    ""type"": ""Button"",
                    ""id"": ""41ab75f8-8b58-462e-ab72-8a027198087a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ThreeButton"",
                    ""type"": ""Button"",
                    ""id"": ""f117e22d-761b-4301-86fd-60241cad61e9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""FourButton"",
                    ""type"": ""Button"",
                    ""id"": ""6a6307df-2b23-4943-a8a0-9bf3113c4b03"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""FiveButton"",
                    ""type"": ""Button"",
                    ""id"": ""550380f4-aa15-4b86-8a44-aebf7d81fbc1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Rotate"",
                    ""type"": ""Button"",
                    ""id"": ""a5b8f042-084c-4808-ae38-b6957282804e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Escape"",
                    ""type"": ""Button"",
                    ""id"": ""186d17e5-96d2-485d-9a06-cdfc74e3ac04"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Delete"",
                    ""type"": ""Button"",
                    ""id"": ""ddad8abe-606b-4dcc-b12d-f084f87316eb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""372322c1-44f9-4c1e-b32d-fb0e37ad06d8"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Select"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""60e7d8c6-afb1-4428-a56a-0fcb18b326f6"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""OneButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2203ff57-917f-4a05-a46a-3f397a1a255d"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TwoButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2fb695d1-1d61-4b16-91c5-12908c75d9ae"",
                    ""path"": ""<Keyboard>/3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ThreeButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f016a375-ae77-4ae6-b636-11df46da4b46"",
                    ""path"": ""<Keyboard>/4"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""FourButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5b058b5f-b907-4b56-b5ee-ea5792533822"",
                    ""path"": ""<Keyboard>/5"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""FiveButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8f96919d-3e1f-472b-aefe-197e36114469"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""43fafb42-ab8b-4a1a-94ec-d872e12bbc09"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Escape"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ae5b0923-86cc-4245-8fb5-65b89a2d897b"",
                    ""path"": ""<Keyboard>/delete"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Delete"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""CameraActions"",
            ""id"": ""0584d5ce-8ab9-46f0-b5d8-f16adcae18a1"",
            ""actions"": [
                {
                    ""name"": ""CameraMovement"",
                    ""type"": ""Value"",
                    ""id"": ""135689bc-957e-43ca-ae8c-0c5eefd97ede"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""CameraRotation"",
                    ""type"": ""Value"",
                    ""id"": ""80b6f09b-d0d7-440c-a4ff-877ef7665a43"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Scroll"",
                    ""type"": ""Value"",
                    ""id"": ""b4ea0bc0-c771-4d2d-867c-e72bf1a962a0"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": ""Invert,Clamp(min=-1,max=1)"",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""4dab2ed3-c46d-462e-b3eb-eaf7ad3a3886"",
                    ""path"": ""2DVector(mode=2)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CameraMovement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""8de49ed1-d87d-4fcc-a410-edd609731666"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CameraMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""92019976-2689-4159-b55a-f78524d004bb"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CameraMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""f50358ed-3ad4-4a1f-b738-68ce03548a31"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CameraMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""6a870481-5924-4f3b-81e9-054ebffb4cce"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CameraMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""0be211ae-0195-485d-ab2b-0f5e0b3d966c"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CameraRotation"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""6be74f44-7511-4e4d-9b9d-6d95a1b9473d"",
                    ""path"": ""<Keyboard>/v"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CameraRotation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""2599e1ea-49bc-423a-a71f-b37598dc7ee2"",
                    ""path"": ""<Keyboard>/c"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CameraRotation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""de4703c8-ba0b-4666-9897-0cdb2fdb55e4"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CameraRotation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""ea8979f3-0eef-4fcd-b3bb-c99b12fab7cb"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CameraRotation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""7ba2ecbb-8083-41c5-ace7-fce222b6089a"",
                    ""path"": ""<Mouse>/scroll/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Scroll"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // PlayerActions
        m_PlayerActions = asset.FindActionMap("PlayerActions", throwIfNotFound: true);
        m_PlayerActions_Select = m_PlayerActions.FindAction("Select", throwIfNotFound: true);
        m_PlayerActions_OneButton = m_PlayerActions.FindAction("OneButton", throwIfNotFound: true);
        m_PlayerActions_TwoButton = m_PlayerActions.FindAction("TwoButton", throwIfNotFound: true);
        m_PlayerActions_ThreeButton = m_PlayerActions.FindAction("ThreeButton", throwIfNotFound: true);
        m_PlayerActions_FourButton = m_PlayerActions.FindAction("FourButton", throwIfNotFound: true);
        m_PlayerActions_FiveButton = m_PlayerActions.FindAction("FiveButton", throwIfNotFound: true);
        m_PlayerActions_Rotate = m_PlayerActions.FindAction("Rotate", throwIfNotFound: true);
        m_PlayerActions_Escape = m_PlayerActions.FindAction("Escape", throwIfNotFound: true);
        m_PlayerActions_Delete = m_PlayerActions.FindAction("Delete", throwIfNotFound: true);
        // CameraActions
        m_CameraActions = asset.FindActionMap("CameraActions", throwIfNotFound: true);
        m_CameraActions_CameraMovement = m_CameraActions.FindAction("CameraMovement", throwIfNotFound: true);
        m_CameraActions_CameraRotation = m_CameraActions.FindAction("CameraRotation", throwIfNotFound: true);
        m_CameraActions_Scroll = m_CameraActions.FindAction("Scroll", throwIfNotFound: true);
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
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // PlayerActions
    private readonly InputActionMap m_PlayerActions;
    private IPlayerActionsActions m_PlayerActionsActionsCallbackInterface;
    private readonly InputAction m_PlayerActions_Select;
    private readonly InputAction m_PlayerActions_OneButton;
    private readonly InputAction m_PlayerActions_TwoButton;
    private readonly InputAction m_PlayerActions_ThreeButton;
    private readonly InputAction m_PlayerActions_FourButton;
    private readonly InputAction m_PlayerActions_FiveButton;
    private readonly InputAction m_PlayerActions_Rotate;
    private readonly InputAction m_PlayerActions_Escape;
    private readonly InputAction m_PlayerActions_Delete;
    public struct PlayerActionsActions
    {
        private @PlayerControls m_Wrapper;
        public PlayerActionsActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Select => m_Wrapper.m_PlayerActions_Select;
        public InputAction @OneButton => m_Wrapper.m_PlayerActions_OneButton;
        public InputAction @TwoButton => m_Wrapper.m_PlayerActions_TwoButton;
        public InputAction @ThreeButton => m_Wrapper.m_PlayerActions_ThreeButton;
        public InputAction @FourButton => m_Wrapper.m_PlayerActions_FourButton;
        public InputAction @FiveButton => m_Wrapper.m_PlayerActions_FiveButton;
        public InputAction @Rotate => m_Wrapper.m_PlayerActions_Rotate;
        public InputAction @Escape => m_Wrapper.m_PlayerActions_Escape;
        public InputAction @Delete => m_Wrapper.m_PlayerActions_Delete;
        public InputActionMap Get() { return m_Wrapper.m_PlayerActions; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActionsActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActionsActions instance)
        {
            if (m_Wrapper.m_PlayerActionsActionsCallbackInterface != null)
            {
                @Select.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnSelect;
                @Select.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnSelect;
                @Select.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnSelect;
                @OneButton.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnOneButton;
                @OneButton.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnOneButton;
                @OneButton.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnOneButton;
                @TwoButton.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnTwoButton;
                @TwoButton.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnTwoButton;
                @TwoButton.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnTwoButton;
                @ThreeButton.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnThreeButton;
                @ThreeButton.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnThreeButton;
                @ThreeButton.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnThreeButton;
                @FourButton.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnFourButton;
                @FourButton.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnFourButton;
                @FourButton.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnFourButton;
                @FiveButton.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnFiveButton;
                @FiveButton.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnFiveButton;
                @FiveButton.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnFiveButton;
                @Rotate.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnRotate;
                @Rotate.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnRotate;
                @Rotate.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnRotate;
                @Escape.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnEscape;
                @Escape.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnEscape;
                @Escape.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnEscape;
                @Delete.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnDelete;
                @Delete.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnDelete;
                @Delete.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnDelete;
            }
            m_Wrapper.m_PlayerActionsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Select.started += instance.OnSelect;
                @Select.performed += instance.OnSelect;
                @Select.canceled += instance.OnSelect;
                @OneButton.started += instance.OnOneButton;
                @OneButton.performed += instance.OnOneButton;
                @OneButton.canceled += instance.OnOneButton;
                @TwoButton.started += instance.OnTwoButton;
                @TwoButton.performed += instance.OnTwoButton;
                @TwoButton.canceled += instance.OnTwoButton;
                @ThreeButton.started += instance.OnThreeButton;
                @ThreeButton.performed += instance.OnThreeButton;
                @ThreeButton.canceled += instance.OnThreeButton;
                @FourButton.started += instance.OnFourButton;
                @FourButton.performed += instance.OnFourButton;
                @FourButton.canceled += instance.OnFourButton;
                @FiveButton.started += instance.OnFiveButton;
                @FiveButton.performed += instance.OnFiveButton;
                @FiveButton.canceled += instance.OnFiveButton;
                @Rotate.started += instance.OnRotate;
                @Rotate.performed += instance.OnRotate;
                @Rotate.canceled += instance.OnRotate;
                @Escape.started += instance.OnEscape;
                @Escape.performed += instance.OnEscape;
                @Escape.canceled += instance.OnEscape;
                @Delete.started += instance.OnDelete;
                @Delete.performed += instance.OnDelete;
                @Delete.canceled += instance.OnDelete;
            }
        }
    }
    public PlayerActionsActions @PlayerActions => new PlayerActionsActions(this);

    // CameraActions
    private readonly InputActionMap m_CameraActions;
    private ICameraActionsActions m_CameraActionsActionsCallbackInterface;
    private readonly InputAction m_CameraActions_CameraMovement;
    private readonly InputAction m_CameraActions_CameraRotation;
    private readonly InputAction m_CameraActions_Scroll;
    public struct CameraActionsActions
    {
        private @PlayerControls m_Wrapper;
        public CameraActionsActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @CameraMovement => m_Wrapper.m_CameraActions_CameraMovement;
        public InputAction @CameraRotation => m_Wrapper.m_CameraActions_CameraRotation;
        public InputAction @Scroll => m_Wrapper.m_CameraActions_Scroll;
        public InputActionMap Get() { return m_Wrapper.m_CameraActions; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CameraActionsActions set) { return set.Get(); }
        public void SetCallbacks(ICameraActionsActions instance)
        {
            if (m_Wrapper.m_CameraActionsActionsCallbackInterface != null)
            {
                @CameraMovement.started -= m_Wrapper.m_CameraActionsActionsCallbackInterface.OnCameraMovement;
                @CameraMovement.performed -= m_Wrapper.m_CameraActionsActionsCallbackInterface.OnCameraMovement;
                @CameraMovement.canceled -= m_Wrapper.m_CameraActionsActionsCallbackInterface.OnCameraMovement;
                @CameraRotation.started -= m_Wrapper.m_CameraActionsActionsCallbackInterface.OnCameraRotation;
                @CameraRotation.performed -= m_Wrapper.m_CameraActionsActionsCallbackInterface.OnCameraRotation;
                @CameraRotation.canceled -= m_Wrapper.m_CameraActionsActionsCallbackInterface.OnCameraRotation;
                @Scroll.started -= m_Wrapper.m_CameraActionsActionsCallbackInterface.OnScroll;
                @Scroll.performed -= m_Wrapper.m_CameraActionsActionsCallbackInterface.OnScroll;
                @Scroll.canceled -= m_Wrapper.m_CameraActionsActionsCallbackInterface.OnScroll;
            }
            m_Wrapper.m_CameraActionsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @CameraMovement.started += instance.OnCameraMovement;
                @CameraMovement.performed += instance.OnCameraMovement;
                @CameraMovement.canceled += instance.OnCameraMovement;
                @CameraRotation.started += instance.OnCameraRotation;
                @CameraRotation.performed += instance.OnCameraRotation;
                @CameraRotation.canceled += instance.OnCameraRotation;
                @Scroll.started += instance.OnScroll;
                @Scroll.performed += instance.OnScroll;
                @Scroll.canceled += instance.OnScroll;
            }
        }
    }
    public CameraActionsActions @CameraActions => new CameraActionsActions(this);
    public interface IPlayerActionsActions
    {
        void OnSelect(InputAction.CallbackContext context);
        void OnOneButton(InputAction.CallbackContext context);
        void OnTwoButton(InputAction.CallbackContext context);
        void OnThreeButton(InputAction.CallbackContext context);
        void OnFourButton(InputAction.CallbackContext context);
        void OnFiveButton(InputAction.CallbackContext context);
        void OnRotate(InputAction.CallbackContext context);
        void OnEscape(InputAction.CallbackContext context);
        void OnDelete(InputAction.CallbackContext context);
    }
    public interface ICameraActionsActions
    {
        void OnCameraMovement(InputAction.CallbackContext context);
        void OnCameraRotation(InputAction.CallbackContext context);
        void OnScroll(InputAction.CallbackContext context);
    }
}
