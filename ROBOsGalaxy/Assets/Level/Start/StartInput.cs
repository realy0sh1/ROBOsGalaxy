//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.3.0
//     from Assets/Level/Start/StartInput.inputactions
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

public partial class @StartInput : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @StartInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""StartInput"",
    ""maps"": [
        {
            ""name"": ""Start"",
            ""id"": ""6e46a0cc-2af8-4342-bcdb-b2b6ad26af63"",
            ""actions"": [
                {
                    ""name"": ""Game"",
                    ""type"": ""Button"",
                    ""id"": ""60d27133-4c5f-41f2-be77-4bb5cd706bce"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""375b0062-255b-43e9-b77b-53eaf55baebd"",
                    ""path"": ""<Keyboard>/anyKey"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Game"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7dd08bf8-b129-4ee3-be15-91a5286e6dda"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Game"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""49207f47-38c3-4484-b0dd-198180de7657"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Game"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ad37e3a3-59ce-4da6-b42a-8687723088b1"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Game"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2cfd27f0-f728-4064-9b44-6a517cf9f2f8"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Game"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Start
        m_Start = asset.FindActionMap("Start", throwIfNotFound: true);
        m_Start_Game = m_Start.FindAction("Game", throwIfNotFound: true);
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

    // Start
    private readonly InputActionMap m_Start;
    private IStartActions m_StartActionsCallbackInterface;
    private readonly InputAction m_Start_Game;
    public struct StartActions
    {
        private @StartInput m_Wrapper;
        public StartActions(@StartInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Game => m_Wrapper.m_Start_Game;
        public InputActionMap Get() { return m_Wrapper.m_Start; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(StartActions set) { return set.Get(); }
        public void SetCallbacks(IStartActions instance)
        {
            if (m_Wrapper.m_StartActionsCallbackInterface != null)
            {
                @Game.started -= m_Wrapper.m_StartActionsCallbackInterface.OnGame;
                @Game.performed -= m_Wrapper.m_StartActionsCallbackInterface.OnGame;
                @Game.canceled -= m_Wrapper.m_StartActionsCallbackInterface.OnGame;
            }
            m_Wrapper.m_StartActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Game.started += instance.OnGame;
                @Game.performed += instance.OnGame;
                @Game.canceled += instance.OnGame;
            }
        }
    }
    public StartActions @Start => new StartActions(this);
    public interface IStartActions
    {
        void OnGame(InputAction.CallbackContext context);
    }
}
