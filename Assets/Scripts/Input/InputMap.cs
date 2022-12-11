//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.3.0
//     from Assets/Scripts/Input/InputMap.inputactions
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

public partial class @InputMap : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputMap()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputMap"",
    ""maps"": [
        {
            ""name"": ""Movement"",
            ""id"": ""e3b8f709-da45-4fc1-ad7a-e3f60e26be4c"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""PassThrough"",
                    ""id"": ""66a70d86-e4be-40d6-b823-f3bc8c84952b"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""01072460-32d1-4040-a229-facb3387d81b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press"",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ModeSelector"",
                    ""type"": ""Button"",
                    ""id"": ""9d2be84e-f090-479b-8d6c-00742ceb3bf4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press"",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Dash"",
                    ""type"": ""Button"",
                    ""id"": ""1beed682-7b70-417f-b4ed-daf9bc93ae7c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press"",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Keyboard"",
                    ""id"": ""e25f063e-9275-494d-9152-0f067e37252f"",
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
                    ""id"": ""55cf41f6-629c-4d91-9e37-a167a964a02e"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""ef868c20-fddd-4baf-8597-0c9237498588"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""25f41110-61ad-4b3a-84eb-df0d360004e9"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""10062aa2-347a-479c-b978-8386bd6cfa3e"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""bcc1c54b-d095-483f-ae17-b0b3a7f3c7ac"",
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
                    ""id"": ""b45a8230-83ed-4b5e-8883-419adf299fd9"",
                    ""path"": ""<Keyboard>/tab"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ModeSelector"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""79180b9c-1ac1-41ee-a502-5b87c25d77ea"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Dash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Camera"",
            ""id"": ""5b1f4ebd-7dd4-421e-a78e-f8d3f8abbb35"",
            ""actions"": [
                {
                    ""name"": ""MouseAxis"",
                    ""type"": ""PassThrough"",
                    ""id"": ""24c792bd-cddf-427d-8826-a46249a0f4b9"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Zoom"",
                    ""type"": ""PassThrough"",
                    ""id"": ""e4979bc6-efd2-4262-b8a6-ce81f57c34d1"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Mouse"",
                    ""id"": ""850b4ff9-e209-493b-bb64-257dc912d292"",
                    ""path"": ""OneModifier"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseAxis"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""modifier"",
                    ""id"": ""c136b237-02d7-4200-9c0e-48fe2daa4985"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseAxis"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""binding"",
                    ""id"": ""e5cadba5-f903-4f2c-8fb9-391c4b6a7364"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseAxis"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""9d0f6632-8748-4ba5-b5a1-bbf0004f4b45"",
                    ""path"": ""<Mouse>/scroll/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Zoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Architect"",
            ""id"": ""818e3507-1c4e-4876-946b-92ce319870c9"",
            ""actions"": [
                {
                    ""name"": ""Build"",
                    ""type"": ""Button"",
                    ""id"": ""c58d2a1c-b78d-4d2f-bd53-74a96cba7ac8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press"",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Place"",
                    ""type"": ""Button"",
                    ""id"": ""f380e486-a09e-4707-bf32-fde8f52931fa"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press"",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""CancelBuild"",
                    ""type"": ""Button"",
                    ""id"": ""7427c5cc-1be1-4e33-bad1-7425e59acc7c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press"",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Rotate"",
                    ""type"": ""Button"",
                    ""id"": ""d07cd7b8-1d4f-4853-8ef9-cf6b9699e3c1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press"",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""NextBuilding"",
                    ""type"": ""Button"",
                    ""id"": ""8be5dc52-d22d-4fb1-a5ba-a821a3e939be"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""PreviousBuilding"",
                    ""type"": ""Button"",
                    ""id"": ""c65412bb-1fef-4a9a-a85a-cff36fae29e2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""8764000f-462f-4065-a138-eb1f83704fd8"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Build"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0fb2b78d-3cec-41e9-88fa-672484457f38"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Place"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7b2ae191-08ed-4125-93a8-24474304354b"",
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
                    ""id"": ""8bf0c4a2-85f6-4a89-bf8e-4afb640d94a2"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""NextBuilding"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d0e58698-0172-4df3-9d20-34c48199bf83"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PreviousBuilding"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f88dcbbf-e129-4407-a9b5-4e16316a456b"",
                    ""path"": ""<Keyboard>/c"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CancelBuild"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Weapon"",
            ""id"": ""7c6e972f-8e92-4ce9-8b1a-bd8a69ce7820"",
            ""actions"": [
                {
                    ""name"": ""Shoot"",
                    ""type"": ""Button"",
                    ""id"": ""157f05de-67aa-49cb-b8ed-ff33eba0b357"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press"",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""71579ba8-d010-4cb5-9898-909542195f1b"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Options"",
            ""id"": ""11f05fb2-463f-46f4-a687-e4ce7b9618d1"",
            ""actions"": [
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""70373e47-5227-49ee-8fa5-e19da4aefa06"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press"",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""781db5eb-a4d2-4f23-a581-33df36cbb7f2"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Movement
        m_Movement = asset.FindActionMap("Movement", throwIfNotFound: true);
        m_Movement_Move = m_Movement.FindAction("Move", throwIfNotFound: true);
        m_Movement_Jump = m_Movement.FindAction("Jump", throwIfNotFound: true);
        m_Movement_ModeSelector = m_Movement.FindAction("ModeSelector", throwIfNotFound: true);
        m_Movement_Dash = m_Movement.FindAction("Dash", throwIfNotFound: true);
        // Camera
        m_Camera = asset.FindActionMap("Camera", throwIfNotFound: true);
        m_Camera_MouseAxis = m_Camera.FindAction("MouseAxis", throwIfNotFound: true);
        m_Camera_Zoom = m_Camera.FindAction("Zoom", throwIfNotFound: true);
        // Architect
        m_Architect = asset.FindActionMap("Architect", throwIfNotFound: true);
        m_Architect_Build = m_Architect.FindAction("Build", throwIfNotFound: true);
        m_Architect_Place = m_Architect.FindAction("Place", throwIfNotFound: true);
        m_Architect_CancelBuild = m_Architect.FindAction("CancelBuild", throwIfNotFound: true);
        m_Architect_Rotate = m_Architect.FindAction("Rotate", throwIfNotFound: true);
        m_Architect_NextBuilding = m_Architect.FindAction("NextBuilding", throwIfNotFound: true);
        m_Architect_PreviousBuilding = m_Architect.FindAction("PreviousBuilding", throwIfNotFound: true);
        // Weapon
        m_Weapon = asset.FindActionMap("Weapon", throwIfNotFound: true);
        m_Weapon_Shoot = m_Weapon.FindAction("Shoot", throwIfNotFound: true);
        // Options
        m_Options = asset.FindActionMap("Options", throwIfNotFound: true);
        m_Options_Pause = m_Options.FindAction("Pause", throwIfNotFound: true);
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

    // Movement
    private readonly InputActionMap m_Movement;
    private IMovementActions m_MovementActionsCallbackInterface;
    private readonly InputAction m_Movement_Move;
    private readonly InputAction m_Movement_Jump;
    private readonly InputAction m_Movement_ModeSelector;
    private readonly InputAction m_Movement_Dash;
    public struct MovementActions
    {
        private @InputMap m_Wrapper;
        public MovementActions(@InputMap wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Movement_Move;
        public InputAction @Jump => m_Wrapper.m_Movement_Jump;
        public InputAction @ModeSelector => m_Wrapper.m_Movement_ModeSelector;
        public InputAction @Dash => m_Wrapper.m_Movement_Dash;
        public InputActionMap Get() { return m_Wrapper.m_Movement; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MovementActions set) { return set.Get(); }
        public void SetCallbacks(IMovementActions instance)
        {
            if (m_Wrapper.m_MovementActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnMove;
                @Jump.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnJump;
                @ModeSelector.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnModeSelector;
                @ModeSelector.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnModeSelector;
                @ModeSelector.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnModeSelector;
                @Dash.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnDash;
                @Dash.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnDash;
                @Dash.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnDash;
            }
            m_Wrapper.m_MovementActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @ModeSelector.started += instance.OnModeSelector;
                @ModeSelector.performed += instance.OnModeSelector;
                @ModeSelector.canceled += instance.OnModeSelector;
                @Dash.started += instance.OnDash;
                @Dash.performed += instance.OnDash;
                @Dash.canceled += instance.OnDash;
            }
        }
    }
    public MovementActions @Movement => new MovementActions(this);

    // Camera
    private readonly InputActionMap m_Camera;
    private ICameraActions m_CameraActionsCallbackInterface;
    private readonly InputAction m_Camera_MouseAxis;
    private readonly InputAction m_Camera_Zoom;
    public struct CameraActions
    {
        private @InputMap m_Wrapper;
        public CameraActions(@InputMap wrapper) { m_Wrapper = wrapper; }
        public InputAction @MouseAxis => m_Wrapper.m_Camera_MouseAxis;
        public InputAction @Zoom => m_Wrapper.m_Camera_Zoom;
        public InputActionMap Get() { return m_Wrapper.m_Camera; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CameraActions set) { return set.Get(); }
        public void SetCallbacks(ICameraActions instance)
        {
            if (m_Wrapper.m_CameraActionsCallbackInterface != null)
            {
                @MouseAxis.started -= m_Wrapper.m_CameraActionsCallbackInterface.OnMouseAxis;
                @MouseAxis.performed -= m_Wrapper.m_CameraActionsCallbackInterface.OnMouseAxis;
                @MouseAxis.canceled -= m_Wrapper.m_CameraActionsCallbackInterface.OnMouseAxis;
                @Zoom.started -= m_Wrapper.m_CameraActionsCallbackInterface.OnZoom;
                @Zoom.performed -= m_Wrapper.m_CameraActionsCallbackInterface.OnZoom;
                @Zoom.canceled -= m_Wrapper.m_CameraActionsCallbackInterface.OnZoom;
            }
            m_Wrapper.m_CameraActionsCallbackInterface = instance;
            if (instance != null)
            {
                @MouseAxis.started += instance.OnMouseAxis;
                @MouseAxis.performed += instance.OnMouseAxis;
                @MouseAxis.canceled += instance.OnMouseAxis;
                @Zoom.started += instance.OnZoom;
                @Zoom.performed += instance.OnZoom;
                @Zoom.canceled += instance.OnZoom;
            }
        }
    }
    public CameraActions @Camera => new CameraActions(this);

    // Architect
    private readonly InputActionMap m_Architect;
    private IArchitectActions m_ArchitectActionsCallbackInterface;
    private readonly InputAction m_Architect_Build;
    private readonly InputAction m_Architect_Place;
    private readonly InputAction m_Architect_CancelBuild;
    private readonly InputAction m_Architect_Rotate;
    private readonly InputAction m_Architect_NextBuilding;
    private readonly InputAction m_Architect_PreviousBuilding;
    public struct ArchitectActions
    {
        private @InputMap m_Wrapper;
        public ArchitectActions(@InputMap wrapper) { m_Wrapper = wrapper; }
        public InputAction @Build => m_Wrapper.m_Architect_Build;
        public InputAction @Place => m_Wrapper.m_Architect_Place;
        public InputAction @CancelBuild => m_Wrapper.m_Architect_CancelBuild;
        public InputAction @Rotate => m_Wrapper.m_Architect_Rotate;
        public InputAction @NextBuilding => m_Wrapper.m_Architect_NextBuilding;
        public InputAction @PreviousBuilding => m_Wrapper.m_Architect_PreviousBuilding;
        public InputActionMap Get() { return m_Wrapper.m_Architect; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ArchitectActions set) { return set.Get(); }
        public void SetCallbacks(IArchitectActions instance)
        {
            if (m_Wrapper.m_ArchitectActionsCallbackInterface != null)
            {
                @Build.started -= m_Wrapper.m_ArchitectActionsCallbackInterface.OnBuild;
                @Build.performed -= m_Wrapper.m_ArchitectActionsCallbackInterface.OnBuild;
                @Build.canceled -= m_Wrapper.m_ArchitectActionsCallbackInterface.OnBuild;
                @Place.started -= m_Wrapper.m_ArchitectActionsCallbackInterface.OnPlace;
                @Place.performed -= m_Wrapper.m_ArchitectActionsCallbackInterface.OnPlace;
                @Place.canceled -= m_Wrapper.m_ArchitectActionsCallbackInterface.OnPlace;
                @CancelBuild.started -= m_Wrapper.m_ArchitectActionsCallbackInterface.OnCancelBuild;
                @CancelBuild.performed -= m_Wrapper.m_ArchitectActionsCallbackInterface.OnCancelBuild;
                @CancelBuild.canceled -= m_Wrapper.m_ArchitectActionsCallbackInterface.OnCancelBuild;
                @Rotate.started -= m_Wrapper.m_ArchitectActionsCallbackInterface.OnRotate;
                @Rotate.performed -= m_Wrapper.m_ArchitectActionsCallbackInterface.OnRotate;
                @Rotate.canceled -= m_Wrapper.m_ArchitectActionsCallbackInterface.OnRotate;
                @NextBuilding.started -= m_Wrapper.m_ArchitectActionsCallbackInterface.OnNextBuilding;
                @NextBuilding.performed -= m_Wrapper.m_ArchitectActionsCallbackInterface.OnNextBuilding;
                @NextBuilding.canceled -= m_Wrapper.m_ArchitectActionsCallbackInterface.OnNextBuilding;
                @PreviousBuilding.started -= m_Wrapper.m_ArchitectActionsCallbackInterface.OnPreviousBuilding;
                @PreviousBuilding.performed -= m_Wrapper.m_ArchitectActionsCallbackInterface.OnPreviousBuilding;
                @PreviousBuilding.canceled -= m_Wrapper.m_ArchitectActionsCallbackInterface.OnPreviousBuilding;
            }
            m_Wrapper.m_ArchitectActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Build.started += instance.OnBuild;
                @Build.performed += instance.OnBuild;
                @Build.canceled += instance.OnBuild;
                @Place.started += instance.OnPlace;
                @Place.performed += instance.OnPlace;
                @Place.canceled += instance.OnPlace;
                @CancelBuild.started += instance.OnCancelBuild;
                @CancelBuild.performed += instance.OnCancelBuild;
                @CancelBuild.canceled += instance.OnCancelBuild;
                @Rotate.started += instance.OnRotate;
                @Rotate.performed += instance.OnRotate;
                @Rotate.canceled += instance.OnRotate;
                @NextBuilding.started += instance.OnNextBuilding;
                @NextBuilding.performed += instance.OnNextBuilding;
                @NextBuilding.canceled += instance.OnNextBuilding;
                @PreviousBuilding.started += instance.OnPreviousBuilding;
                @PreviousBuilding.performed += instance.OnPreviousBuilding;
                @PreviousBuilding.canceled += instance.OnPreviousBuilding;
            }
        }
    }
    public ArchitectActions @Architect => new ArchitectActions(this);

    // Weapon
    private readonly InputActionMap m_Weapon;
    private IWeaponActions m_WeaponActionsCallbackInterface;
    private readonly InputAction m_Weapon_Shoot;
    public struct WeaponActions
    {
        private @InputMap m_Wrapper;
        public WeaponActions(@InputMap wrapper) { m_Wrapper = wrapper; }
        public InputAction @Shoot => m_Wrapper.m_Weapon_Shoot;
        public InputActionMap Get() { return m_Wrapper.m_Weapon; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(WeaponActions set) { return set.Get(); }
        public void SetCallbacks(IWeaponActions instance)
        {
            if (m_Wrapper.m_WeaponActionsCallbackInterface != null)
            {
                @Shoot.started -= m_Wrapper.m_WeaponActionsCallbackInterface.OnShoot;
                @Shoot.performed -= m_Wrapper.m_WeaponActionsCallbackInterface.OnShoot;
                @Shoot.canceled -= m_Wrapper.m_WeaponActionsCallbackInterface.OnShoot;
            }
            m_Wrapper.m_WeaponActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Shoot.started += instance.OnShoot;
                @Shoot.performed += instance.OnShoot;
                @Shoot.canceled += instance.OnShoot;
            }
        }
    }
    public WeaponActions @Weapon => new WeaponActions(this);

    // Options
    private readonly InputActionMap m_Options;
    private IOptionsActions m_OptionsActionsCallbackInterface;
    private readonly InputAction m_Options_Pause;
    public struct OptionsActions
    {
        private @InputMap m_Wrapper;
        public OptionsActions(@InputMap wrapper) { m_Wrapper = wrapper; }
        public InputAction @Pause => m_Wrapper.m_Options_Pause;
        public InputActionMap Get() { return m_Wrapper.m_Options; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(OptionsActions set) { return set.Get(); }
        public void SetCallbacks(IOptionsActions instance)
        {
            if (m_Wrapper.m_OptionsActionsCallbackInterface != null)
            {
                @Pause.started -= m_Wrapper.m_OptionsActionsCallbackInterface.OnPause;
                @Pause.performed -= m_Wrapper.m_OptionsActionsCallbackInterface.OnPause;
                @Pause.canceled -= m_Wrapper.m_OptionsActionsCallbackInterface.OnPause;
            }
            m_Wrapper.m_OptionsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
            }
        }
    }
    public OptionsActions @Options => new OptionsActions(this);
    public interface IMovementActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnModeSelector(InputAction.CallbackContext context);
        void OnDash(InputAction.CallbackContext context);
    }
    public interface ICameraActions
    {
        void OnMouseAxis(InputAction.CallbackContext context);
        void OnZoom(InputAction.CallbackContext context);
    }
    public interface IArchitectActions
    {
        void OnBuild(InputAction.CallbackContext context);
        void OnPlace(InputAction.CallbackContext context);
        void OnCancelBuild(InputAction.CallbackContext context);
        void OnRotate(InputAction.CallbackContext context);
        void OnNextBuilding(InputAction.CallbackContext context);
        void OnPreviousBuilding(InputAction.CallbackContext context);
    }
    public interface IWeaponActions
    {
        void OnShoot(InputAction.CallbackContext context);
    }
    public interface IOptionsActions
    {
        void OnPause(InputAction.CallbackContext context);
    }
}
