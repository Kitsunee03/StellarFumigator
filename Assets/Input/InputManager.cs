using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager _INPUT_MANAGER;
    private InputMap playerInputs;

    [Header("Generic Buttons")]
    private float m_timeSincePrimaryButtonPressed = 0.3f;
    private float m_timeSinceModeSelectorButtonPressed = 0.3f;

    [Header("Weapon Mode Buttons")]
    private float m_timeSinceJumpButtonPressed = 0.3f;
    private float m_timeSinceDashButtonPressed = 0.3f;

    [Header("Architect Mode Buttons")]
    private float m_timeSincePlaceButtonPressed = 0.3f;
    private float m_timeSinceCancelBuildButtonPressed = 0.3f;
    private float m_timeSinceRotateButtonPressed = 0.3f;

    [Header("Axis")]
    private float m_wheelAxisValue = 0f;
    private Vector2 m_movementAxisValue = Vector2.zero;
    private Vector2 m_mouseAxisValue = Vector2.zero;

    #region Enable/Disable
    private void OnEnable()
    {
        playerInputs.Movement.Enable();
        playerInputs.Camera.Enable();
        playerInputs.Architect.Enable();
    }
    private void OnDisable()
    {
        playerInputs.Movement.Disable();
        playerInputs.Camera.Disable();
        playerInputs.Architect.Disable();
    }
    #endregion

    private void Awake()
    {
        //Creamos Instancia
        if (_INPUT_MANAGER != null && _INPUT_MANAGER != this) { Destroy(_INPUT_MANAGER); }
        else
        {
            playerInputs = new InputMap();

            playerInputs.Movement.Jump.performed += context => m_timeSinceJumpButtonPressed = 0f;
            playerInputs.Movement.Move.performed += context => m_movementAxisValue = context.ReadValue<Vector2>();
            playerInputs.Movement.ModeSelector.performed += context => m_timeSinceModeSelectorButtonPressed = 0f;
            playerInputs.Movement.Dash.performed += context => m_timeSinceDashButtonPressed = 0f;

            playerInputs.Architect.Build.performed += context => m_timeSincePrimaryButtonPressed = 0f;
            playerInputs.Architect.Place.performed += context => m_timeSincePlaceButtonPressed = 0f;
            playerInputs.Architect.CancelBuild.performed += context => m_timeSinceCancelBuildButtonPressed = 0f;
            playerInputs.Architect.Rotate.performed += context => m_timeSinceRotateButtonPressed = 0f;

            playerInputs.Camera.MouseAxis.performed += context => m_mouseAxisValue = context.ReadValue<Vector2>();
            playerInputs.Camera.Zoom.performed += context => m_wheelAxisValue = context.ReadValue<float>();

            _INPUT_MANAGER = this;
        }
    }

    void Update()
    {
        m_timeSinceModeSelectorButtonPressed += Time.deltaTime;
        //Weapon Buttons Update
        m_timeSinceDashButtonPressed += Time.deltaTime;
        m_timeSinceJumpButtonPressed += Time.deltaTime;
        //Architect Buttons Update
        m_timeSincePrimaryButtonPressed += Time.deltaTime;
        m_timeSincePlaceButtonPressed += Time.deltaTime;
        m_timeSinceCancelBuildButtonPressed += Time.deltaTime;
        m_timeSinceRotateButtonPressed += Time.deltaTime;

        InputSystem.Update();
    }

    #region Generic Accessors

    public bool GetModeSelectorButtonPressed()
    {
        return m_timeSinceModeSelectorButtonPressed == 0f;
    }
    public Vector2 GetMovementAxis()
    {
        return m_movementAxisValue;
    }
    #endregion

    #region Camera Accessors
    public Vector2 GetMouseAxis()
    {
        return m_mouseAxisValue;
    }
    public float GetWheelAxis()
    {
        return m_wheelAxisValue;
    }
    #endregion

    #region Weapon Mode Accessors
    public bool GetDashButtonPressed()
    {
        return m_timeSinceDashButtonPressed == 0;
    }
    public bool GetJumpButtonPressed()
    {
        return m_timeSinceJumpButtonPressed == 0f;
    }
    #endregion

    #region Architect Mode Accessors
    public bool GetPrimaryButtonPressed()
    {
        return m_timeSincePrimaryButtonPressed == 0f;
    }
    public bool GetPlaceButtonPressed()
    {
        return m_timeSincePlaceButtonPressed == 0f;
    }
    public bool GetCancelBuildButtonPressed()
    {
        return m_timeSinceCancelBuildButtonPressed == 0f;
    }
    public bool GetRotateButtonPressed()
    {
        return m_timeSinceRotateButtonPressed == 0f;
    }
    #endregion
}