// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/PlayerControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""Gameplay"",
            ""id"": ""a675159c-3f61-456c-84e7-48c58ebb3821"",
            ""actions"": [
                {
                    ""name"": ""MoveLeft"",
                    ""type"": ""Button"",
                    ""id"": ""592c05a9-a5f0-4718-a9c8-47ac65d423c8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MoveRight"",
                    ""type"": ""Button"",
                    ""id"": ""8aef99e8-d44b-44c8-a63b-7e36ac6cbcb7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""PressRed"",
                    ""type"": ""Button"",
                    ""id"": ""baa0ae3c-a40c-4db5-807f-f5d34c597ff6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""PressGreen"",
                    ""type"": ""Button"",
                    ""id"": ""33ab8c5c-fe11-4859-9c97-7c68df14fb5d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""PressBlue"",
                    ""type"": ""Button"",
                    ""id"": ""c747b7c2-841a-4140-bd1e-16e4ffc711da"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""PressYellow"",
                    ""type"": ""Button"",
                    ""id"": ""3ef79ffb-3552-495d-86d4-7d5bd8760a44"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""UsePowerUp"",
                    ""type"": ""Button"",
                    ""id"": ""3daaa4dd-056e-442e-8ac1-3cc2adb2fb29"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""fbfaff13-5128-4987-91e1-a1983e5e03eb"",
                    ""path"": ""<Gamepad>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4c86bf1e-a5f7-43be-a523-8e049d591f35"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ca3d11bb-eec2-48a8-9714-bedc1dfe028f"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PressRed"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4be744b8-fd7e-4343-965d-b9a412457238"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PressGreen"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d05d2c5d-39ce-4470-a22c-df694433119b"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PressBlue"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a74dd97c-a26f-499c-a231-0ba7ca8297e5"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PressYellow"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""adc98f02-d3c7-48d0-aba4-fd80751f8916"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""UsePowerUp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Gameplay
        m_Gameplay = asset.FindActionMap("Gameplay", throwIfNotFound: true);
        m_Gameplay_MoveLeft = m_Gameplay.FindAction("MoveLeft", throwIfNotFound: true);
        m_Gameplay_MoveRight = m_Gameplay.FindAction("MoveRight", throwIfNotFound: true);
        m_Gameplay_PressRed = m_Gameplay.FindAction("PressRed", throwIfNotFound: true);
        m_Gameplay_PressGreen = m_Gameplay.FindAction("PressGreen", throwIfNotFound: true);
        m_Gameplay_PressBlue = m_Gameplay.FindAction("PressBlue", throwIfNotFound: true);
        m_Gameplay_PressYellow = m_Gameplay.FindAction("PressYellow", throwIfNotFound: true);
        m_Gameplay_UsePowerUp = m_Gameplay.FindAction("UsePowerUp", throwIfNotFound: true);
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

    // Gameplay
    private readonly InputActionMap m_Gameplay;
    private IGameplayActions m_GameplayActionsCallbackInterface;
    private readonly InputAction m_Gameplay_MoveLeft;
    private readonly InputAction m_Gameplay_MoveRight;
    private readonly InputAction m_Gameplay_PressRed;
    private readonly InputAction m_Gameplay_PressGreen;
    private readonly InputAction m_Gameplay_PressBlue;
    private readonly InputAction m_Gameplay_PressYellow;
    private readonly InputAction m_Gameplay_UsePowerUp;
    public struct GameplayActions
    {
        private @PlayerControls m_Wrapper;
        public GameplayActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @MoveLeft => m_Wrapper.m_Gameplay_MoveLeft;
        public InputAction @MoveRight => m_Wrapper.m_Gameplay_MoveRight;
        public InputAction @PressRed => m_Wrapper.m_Gameplay_PressRed;
        public InputAction @PressGreen => m_Wrapper.m_Gameplay_PressGreen;
        public InputAction @PressBlue => m_Wrapper.m_Gameplay_PressBlue;
        public InputAction @PressYellow => m_Wrapper.m_Gameplay_PressYellow;
        public InputAction @UsePowerUp => m_Wrapper.m_Gameplay_UsePowerUp;
        public InputActionMap Get() { return m_Wrapper.m_Gameplay; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameplayActions set) { return set.Get(); }
        public void SetCallbacks(IGameplayActions instance)
        {
            if (m_Wrapper.m_GameplayActionsCallbackInterface != null)
            {
                @MoveLeft.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMoveLeft;
                @MoveLeft.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMoveLeft;
                @MoveLeft.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMoveLeft;
                @MoveRight.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMoveRight;
                @MoveRight.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMoveRight;
                @MoveRight.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMoveRight;
                @PressRed.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPressRed;
                @PressRed.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPressRed;
                @PressRed.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPressRed;
                @PressGreen.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPressGreen;
                @PressGreen.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPressGreen;
                @PressGreen.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPressGreen;
                @PressBlue.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPressBlue;
                @PressBlue.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPressBlue;
                @PressBlue.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPressBlue;
                @PressYellow.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPressYellow;
                @PressYellow.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPressYellow;
                @PressYellow.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPressYellow;
                @UsePowerUp.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnUsePowerUp;
                @UsePowerUp.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnUsePowerUp;
                @UsePowerUp.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnUsePowerUp;
            }
            m_Wrapper.m_GameplayActionsCallbackInterface = instance;
            if (instance != null)
            {
                @MoveLeft.started += instance.OnMoveLeft;
                @MoveLeft.performed += instance.OnMoveLeft;
                @MoveLeft.canceled += instance.OnMoveLeft;
                @MoveRight.started += instance.OnMoveRight;
                @MoveRight.performed += instance.OnMoveRight;
                @MoveRight.canceled += instance.OnMoveRight;
                @PressRed.started += instance.OnPressRed;
                @PressRed.performed += instance.OnPressRed;
                @PressRed.canceled += instance.OnPressRed;
                @PressGreen.started += instance.OnPressGreen;
                @PressGreen.performed += instance.OnPressGreen;
                @PressGreen.canceled += instance.OnPressGreen;
                @PressBlue.started += instance.OnPressBlue;
                @PressBlue.performed += instance.OnPressBlue;
                @PressBlue.canceled += instance.OnPressBlue;
                @PressYellow.started += instance.OnPressYellow;
                @PressYellow.performed += instance.OnPressYellow;
                @PressYellow.canceled += instance.OnPressYellow;
                @UsePowerUp.started += instance.OnUsePowerUp;
                @UsePowerUp.performed += instance.OnUsePowerUp;
                @UsePowerUp.canceled += instance.OnUsePowerUp;
            }
        }
    }
    public GameplayActions @Gameplay => new GameplayActions(this);
    public interface IGameplayActions
    {
        void OnMoveLeft(InputAction.CallbackContext context);
        void OnMoveRight(InputAction.CallbackContext context);
        void OnPressRed(InputAction.CallbackContext context);
        void OnPressGreen(InputAction.CallbackContext context);
        void OnPressBlue(InputAction.CallbackContext context);
        void OnPressYellow(InputAction.CallbackContext context);
        void OnUsePowerUp(InputAction.CallbackContext context);
    }
}
