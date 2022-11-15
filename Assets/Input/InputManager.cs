using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager _INPUT_MANAGER;
    private InputMap playerInputs;

    //Vars
    private float m_timeSinceJumpButtonPressed = 0.3f;
    private float m_timeSinceCrouchButtonPressed = 0.3f;
    private float m_wheelAxisValue = 0f;
    private Vector2 m_movementAxisValue = Vector2.zero;
    private Vector2 m_mouseAxisValue = Vector2.zero;

    private void OnEnable()
    {
            playerInputs.Movement.Enable();
            playerInputs.Camera.Enable();        
    }

    private void OnDisable()
    {
        playerInputs.Movement.Disable();
        playerInputs.Camera.Disable();
    }
    private void Awake()
    {
        //Creamos Instancia
        if (_INPUT_MANAGER != null && _INPUT_MANAGER != this) { Destroy(_INPUT_MANAGER); }
        else
        {
            playerInputs = new InputMap();

            playerInputs.Movement.Jump.performed += JumpButtonPressed;
            playerInputs.Movement.Move.performed += MoveAxisUpdate;
            playerInputs.Camera.MouseAxis.performed += MouseAxisUpdate;
            playerInputs.Camera.Zoom.performed += context => m_wheelAxisValue = context.ReadValue<float>(); ;
            //playerInputs.Character.Crouch.performed += CrouchingUpdate;
            //playerInputs.Character.Cappy.performed += CappyUpdate;

            _INPUT_MANAGER = this;
        }
    }

    void Update()
    {
        m_timeSinceJumpButtonPressed += Time.deltaTime;
        m_timeSinceCrouchButtonPressed += Time.deltaTime;

        InputSystem.Update();
    }

    private void JumpButtonPressed(InputAction.CallbackContext context)
    {
        m_timeSinceJumpButtonPressed = 0f;
    }
    private void MoveAxisUpdate(InputAction.CallbackContext context)
    {
        m_movementAxisValue = context.ReadValue<Vector2>();
    }
    private void MouseAxisUpdate(InputAction.CallbackContext context)
    {
        m_mouseAxisValue = context.ReadValue<Vector2>();
    }
    private void CrouchingUpdate(InputAction.CallbackContext context)
    {
        m_timeSinceCrouchButtonPressed = 0f;
    }
    #region Accessors
    public bool GetJumpButtonPressed()
    {
        return m_timeSinceJumpButtonPressed == 0f;
    }
    public float TimeSinceJumpButtonPressed()
    {
        return m_timeSinceJumpButtonPressed;
    }

    public Vector2 GetMovementAxis()
    {
        return m_movementAxisValue;
    }
    public Vector2 GetMouseAxis()
    {
        return m_mouseAxisValue;
    }
    public float GetWheelAxis()
    {
        return m_wheelAxisValue;
    }
    public bool GetCrouchButtonPressed()
    {
        return m_timeSinceCrouchButtonPressed == 0f;
    }
    #endregion
}