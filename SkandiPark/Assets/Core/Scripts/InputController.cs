using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour, PlayerControls.IPlayerActions
{
    [SerializeField] private Camera _camera;
    [SerializeField] private LayerMask _hitMask;

    private PlayerControls _controls;

    private void Awake()
    {
        if(_controls == null)
        {
            _controls = new PlayerControls();
        }

        if(_camera == null)
        {
            _camera = Camera.main;
        }
    }

    private void OnEnable()
    {
        _controls.Player.Enable();
        _controls.Player.AddCallbacks(this);

        UIManagerGameOne.OnPaused += HandlePause;
        UIManagerGameOne.OnResumed += HandleResume;
        UIManagerGameOne.OnEnded += HandleEnded;
    }

    private void OnDisable()
    {
        UIManagerGameOne.OnPaused -= HandlePause;
        UIManagerGameOne.OnResumed -= HandleResume;
        UIManagerGameOne.OnEnded -= HandleEnded;

        _controls.Player.Disable();
        _controls.Player.RemoveCallbacks(this);
    }

    private void HandlePause()
    {
        _controls.Player.Disable();
    }

    private void HandleResume()
    {
        _controls.Player.Enable();
    }

    private void HandleEnded()
    {
        _controls.Player.Disable();
    }

    public void OnWhack(InputAction.CallbackContext context)
    {
        if (context.performed && UIManagerGameOne.InputLocked == false)
        {
            Vector2 screenPos = Pointer.current.position.ReadValue();
            Vector2 worldPos = _camera.ScreenToWorldPoint(screenPos);

            var hit = Physics2D.Raycast(worldPos, Vector2.zero, 0f, _hitMask);

            if(hit.collider && hit.collider.TryGetComponent<SealController>(out var seal))
            {
                seal.TryHit();
            }

        }
    }
}
