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
    public float Range;
    public AudioEvent HarpoonFireSFX;
    public AudioEvent LoseSFX;
    public Anchor currentAnchor;
    public float speedModifier = 1;
    public float maxRange = 4.5f;
    [SerializeField] private Animator starAnimator;

    private void Awake() {
        Cursor.visible = false; 
        target = FindObjectOfType<Target>();
        harpoon = FindObjectOfType<Harpoon>();
        hasHarpoon = true;
        speedModifier = 1;
        UpdateAnimatorSpeed();
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

    public void UpdateAnimatorSpeed()
    {
        starAnimator.speed = Mathf.Pow(Range/maxRange, 1.5f);
    }

    private void FireHarpoon()
    {
        HarpoonFireSFX.Play(GetComponent<AudioSource>());
        var direction = (targetWorldPos - new Vector2(transform.position.x, transform.position.y)).normalized;
        var targetPos = direction * Range + new Vector2(transform.position.x, transform.position.y);
        harpoon.SetTarget(targetPos);
        hasHarpoon = false;
    }

    public void SnapToAnchor(Transform anchor)
    {
        if (currentAnchor != null) currentAnchor.HasPlayer = false;
        currentAnchor = anchor.GetComponent<Anchor>();
        transform.DOMove(anchor.position, 1/harpoon.GetComponent<Harpoon>().speed * speedModifier);
    }

    public void Damage()
    {
        HeartCollectionUI heartsC = FindObjectOfType<HeartCollectionUI>();
        int leftHeartsCount = heartsC.PopHeart();        
        StartCoroutine(FindObjectOfType<CameraShake>()._ProcessShake());
        if (leftHeartsCount <= 0)
            Die();
    }

    private void Die() {
        Debug.Log("STOP PLAYER");
        speedModifier = 10000;
        hasHarpoon = false;
        transform.DOKill();
        LoseSFX.Play(GetComponent<AudioSource>());
        FindObjectOfType<SceneChanger>().ResetScene(1f);
    }
}
