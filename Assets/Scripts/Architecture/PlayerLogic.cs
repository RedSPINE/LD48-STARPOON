using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

[RequireComponent(typeof(PlayerInput))]
public class PlayerLogic : MonoBehaviour
{
    private Target target;
    private Vector2 aimPos;
    private Vector2 targetWorldPos;
    private Harpoon harpoon;
    public bool hasHarpoon;
    public float range;
    public AudioEvent HarpoonFire;
    public Anchor currentAnchor;

    private void Awake() {
        Cursor.visible = false; 
        target = FindObjectOfType<Target>();
        harpoon = FindObjectOfType<Harpoon>();
        hasHarpoon = true;
    }

    public void OnAim(InputValue value)
    {
        aimPos = value.Get<Vector2>();
    }

    private void Update() {
        
        if (InputSettings.Instance.Scheme == InputSettings.ControlScheme.Gamepad)
        {
            targetWorldPos = aimPos + new Vector2(transform.position.x, transform.position.y);
        }
        else
        {
            var mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            targetWorldPos = new Vector2(mousePos.x, mousePos.y);
        }
        target.transform.position = targetWorldPos;
    }

    public void OnFire()
    {
        target.PunchScale(0.5f);
        if (!hasHarpoon) return;
        FireHarpoon();
    }

    private void FireHarpoon()
    {
        HarpoonFire.Play(GetComponent<AudioSource>());
        var direction = (targetWorldPos - new Vector2(transform.position.x, transform.position.y)).normalized;
        var targetPos = direction * range + new Vector2(transform.position.x, transform.position.y);
        harpoon.SetTarget(targetPos);
        hasHarpoon = false;
    }

    public void SnapToAnchor(Transform anchor)
    {
        if (currentAnchor != null) currentAnchor.hasPlayer = false;
        currentAnchor = anchor.GetComponent<Anchor>();
        currentAnchor.hasPlayer = true;
        transform.DOMove(anchor.position, 1/harpoon.GetComponent<Harpoon>().speed);
    }
}
