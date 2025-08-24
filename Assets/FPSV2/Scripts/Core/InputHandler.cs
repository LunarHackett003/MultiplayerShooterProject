using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    public static void InitialiseOnLoad()
    {
        DontDestroyOnLoad(new GameObject("singleton-input-handler", typeof(InputHandler)));
    }

    public static InputHandler Instance { get; private set; }

    public FPSInputActions actions;
    public static Vector2 MoveInput, LookInput, WeaponSelect;
    public static bool JumpInput, CrouchInput, Action1Input, Action2Input, SprintInput, InteractInput, MeleeInput, AttackInput, WeaponActionInput, ReloadInput, InspectInput, ScoreboardInput;

    public static bool MenuOpen;
    public delegate void MenuToggled(bool toggled);
    public static MenuToggled OnMenuToggle;


    public static bool ToggleSprint;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        InputInit();
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void OnDestroy()
    {
        if(Instance == this)
            InputTerminate();
    }

    void InputInit()
    {
        actions ??= new();
        if (!actions.asset.enabled)
        {
            actions.Enable();
            SubscribeInputs();
        }
    }
    void InputTerminate()
    {
        if (actions.asset.enabled)
        {
            actions.Disable();
            UnsubscribeInputs();
            actions.Dispose();
        }
    }
    private void Paused(InputAction.CallbackContext obj)
    {
        MenuOpen = !MenuOpen;
        OnMenuToggle?.Invoke(MenuOpen);
        Cursor.lockState = MenuOpen ? CursorLockMode.None : CursorLockMode.Locked;
    }
    void SubscribeInputs()
    {
        actions.Player.Crouch.Link(GetCrouch);
        actions.Player.Move.Link(GetMove);
        actions.Player.Look.Link(GetLook);
        actions.Player.Jump.Link(GetJump);
        actions.Player.Sprint.Link(GetSprint);
        actions.Player.WeaponSelect.Link(GetWeaponSelect);
        actions.Player.Attack.Link(GetFire);
        actions.Player.Action1.Link(GetAction1);
        actions.Player.Action2.Link(GetAction2);
        actions.Player.Interact.Link(GetInteract);
        actions.Player.WeaponAction.Link(GetWeaponAction);
        actions.Player.Reload.Link(GetReload);
        actions.Player.Inspect.Link(GetInspect);
        actions.Player.ShowScoreboard.Link(GetScoreboard);
        actions.Player.Melee.Link(GetMelee);
        actions.Player.Pause.performed += Paused;
    }

    void UnsubscribeInputs()
    {
        actions.Player.Crouch.Unlink(GetCrouch);
        actions.Player.Move.Unlink(GetMove);
        actions.Player.Look.Unlink(GetLook);
        actions.Player.Jump.Unlink(GetJump);
        actions.Player.Sprint.Unlink(GetSprint);
        actions.Player.WeaponSelect.Unlink(GetWeaponSelect);
        actions.Player.Attack.Unlink(GetFire);
        actions.Player.Action1.Unlink(GetAction1);
        actions.Player.Action2.Unlink(GetAction2);
        actions.Player.Interact.Unlink(GetInteract);
        actions.Player.Melee.Unlink(GetInteract);
        actions.Player.WeaponAction.Unlink(GetWeaponAction);
        actions.Player.Reload.Unlink(GetReload);
        actions.Player.Inspect.Unlink(GetInspect);
        actions.Player.ShowScoreboard.Unlink(GetScoreboard);
        actions.Player.Melee.Unlink(GetMelee);
        actions.Player.Pause.performed -= Paused;
    }

    //Input Setter
    public void ButtonInput(InputAction.CallbackContext ctx, ref bool value) => value = !MenuOpen && ctx.ReadValueAsButton();
    public void Vector2Input(InputAction.CallbackContext ctx, ref Vector2 value) => value = MenuOpen ? Vector2.zero : ctx.ReadValue<Vector2>();

    //Input Redirectors
    //---Buttons
    void GetCrouch(InputAction.CallbackContext ctx) => ButtonInput(ctx, ref CrouchInput);
    void GetAction1(InputAction.CallbackContext ctx) => ButtonInput(ctx, ref Action1Input);
    void GetAction2(InputAction.CallbackContext ctx) => ButtonInput(ctx, ref Action2Input);
    void GetWeaponAction(InputAction.CallbackContext ctx) => ButtonInput(ctx, ref WeaponActionInput);
    void GetFire(InputAction.CallbackContext ctx) => ButtonInput(ctx, ref AttackInput);
    void GetInspect(InputAction.CallbackContext ctx) => ButtonInput(ctx, ref InspectInput);
    void GetReload(InputAction.CallbackContext ctx) => ButtonInput(ctx, ref ReloadInput);
    void GetScoreboard(InputAction.CallbackContext ctx) => ButtonInput(ctx, ref ScoreboardInput);
    void GetSprint(InputAction.CallbackContext ctx) => ButtonInput(ctx, ref SprintInput);
    void GetJump(InputAction.CallbackContext ctx) => ButtonInput(ctx, ref JumpInput);
    void GetMelee(InputAction.CallbackContext ctx) => ButtonInput(ctx, ref MeleeInput);
    void GetInteract(InputAction.CallbackContext ctx) => ButtonInput(ctx, ref InteractInput);
    //---Vectors
    void GetMove(InputAction.CallbackContext ctx) => Vector2Input(ctx, ref MoveInput);
    void GetLook(InputAction.CallbackContext ctx) => Vector2Input(ctx, ref LookInput);
    void GetWeaponSelect(InputAction.CallbackContext ctx) => Vector2Input(ctx, ref WeaponSelect);
}
