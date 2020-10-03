// GENERATED AUTOMATICALLY FROM 'Assets/ControlsAsset.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @ControlsAsset : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @ControlsAsset()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""ControlsAsset"",
    ""maps"": [
        {
            ""name"": ""Cashier"",
            ""id"": ""a4333bed-fe9a-43e1-ab61-365a0530d338"",
            ""actions"": [
                {
                    ""name"": ""ActivateTreadmill"",
                    ""type"": ""Button"",
                    ""id"": ""a1d807ad-0890-42d5-95ad-1ddc8fa666ce"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Take"",
                    ""type"": ""Button"",
                    ""id"": ""06704e9a-4cd2-4a8f-8a4d-30a3f7fd9dad"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RotateLeft"",
                    ""type"": ""Button"",
                    ""id"": ""e43813a1-3041-407c-b99b-07b1bf0cc26f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RotateRight"",
                    ""type"": ""Button"",
                    ""id"": ""2eee18df-ce67-4d38-9734-c1f86aba2ab6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""bf4062d7-9cd0-499e-9d3d-99cbca65b673"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ActivateTreadmill"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d2df00a3-09f2-4184-9d2f-1beeac3237ab"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Take"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""134adb6e-9997-4e37-8586-9248c8d504a3"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RotateLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9350c77e-d6de-4202-a96c-6fc50056df7a"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RotateRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Cashier
        m_Cashier = asset.FindActionMap("Cashier", throwIfNotFound: true);
        m_Cashier_ActivateTreadmill = m_Cashier.FindAction("ActivateTreadmill", throwIfNotFound: true);
        m_Cashier_Take = m_Cashier.FindAction("Take", throwIfNotFound: true);
        m_Cashier_RotateLeft = m_Cashier.FindAction("RotateLeft", throwIfNotFound: true);
        m_Cashier_RotateRight = m_Cashier.FindAction("RotateRight", throwIfNotFound: true);
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

    // Cashier
    private readonly InputActionMap m_Cashier;
    private ICashierActions m_CashierActionsCallbackInterface;
    private readonly InputAction m_Cashier_ActivateTreadmill;
    private readonly InputAction m_Cashier_Take;
    private readonly InputAction m_Cashier_RotateLeft;
    private readonly InputAction m_Cashier_RotateRight;
    public struct CashierActions
    {
        private @ControlsAsset m_Wrapper;
        public CashierActions(@ControlsAsset wrapper) { m_Wrapper = wrapper; }
        public InputAction @ActivateTreadmill => m_Wrapper.m_Cashier_ActivateTreadmill;
        public InputAction @Take => m_Wrapper.m_Cashier_Take;
        public InputAction @RotateLeft => m_Wrapper.m_Cashier_RotateLeft;
        public InputAction @RotateRight => m_Wrapper.m_Cashier_RotateRight;
        public InputActionMap Get() { return m_Wrapper.m_Cashier; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CashierActions set) { return set.Get(); }
        public void SetCallbacks(ICashierActions instance)
        {
            if (m_Wrapper.m_CashierActionsCallbackInterface != null)
            {
                @ActivateTreadmill.started -= m_Wrapper.m_CashierActionsCallbackInterface.OnActivateTreadmill;
                @ActivateTreadmill.performed -= m_Wrapper.m_CashierActionsCallbackInterface.OnActivateTreadmill;
                @ActivateTreadmill.canceled -= m_Wrapper.m_CashierActionsCallbackInterface.OnActivateTreadmill;
                @Take.started -= m_Wrapper.m_CashierActionsCallbackInterface.OnTake;
                @Take.performed -= m_Wrapper.m_CashierActionsCallbackInterface.OnTake;
                @Take.canceled -= m_Wrapper.m_CashierActionsCallbackInterface.OnTake;
                @RotateLeft.started -= m_Wrapper.m_CashierActionsCallbackInterface.OnRotateLeft;
                @RotateLeft.performed -= m_Wrapper.m_CashierActionsCallbackInterface.OnRotateLeft;
                @RotateLeft.canceled -= m_Wrapper.m_CashierActionsCallbackInterface.OnRotateLeft;
                @RotateRight.started -= m_Wrapper.m_CashierActionsCallbackInterface.OnRotateRight;
                @RotateRight.performed -= m_Wrapper.m_CashierActionsCallbackInterface.OnRotateRight;
                @RotateRight.canceled -= m_Wrapper.m_CashierActionsCallbackInterface.OnRotateRight;
            }
            m_Wrapper.m_CashierActionsCallbackInterface = instance;
            if (instance != null)
            {
                @ActivateTreadmill.started += instance.OnActivateTreadmill;
                @ActivateTreadmill.performed += instance.OnActivateTreadmill;
                @ActivateTreadmill.canceled += instance.OnActivateTreadmill;
                @Take.started += instance.OnTake;
                @Take.performed += instance.OnTake;
                @Take.canceled += instance.OnTake;
                @RotateLeft.started += instance.OnRotateLeft;
                @RotateLeft.performed += instance.OnRotateLeft;
                @RotateLeft.canceled += instance.OnRotateLeft;
                @RotateRight.started += instance.OnRotateRight;
                @RotateRight.performed += instance.OnRotateRight;
                @RotateRight.canceled += instance.OnRotateRight;
            }
        }
    }
    public CashierActions @Cashier => new CashierActions(this);
    public interface ICashierActions
    {
        void OnActivateTreadmill(InputAction.CallbackContext context);
        void OnTake(InputAction.CallbackContext context);
        void OnRotateLeft(InputAction.CallbackContext context);
        void OnRotateRight(InputAction.CallbackContext context);
    }
}
