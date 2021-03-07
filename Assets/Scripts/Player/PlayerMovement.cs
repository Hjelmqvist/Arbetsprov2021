using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour, ISavable
{
    [SerializeField] float movementSpeed = 1;

    CharacterController controller = null;
    bool canMove = true;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (canMove && TryGetMoveDirection(out Vector3 moveDir))
        {
            controller.Move(moveDir * movementSpeed * Time.deltaTime);
            transform.rotation = Quaternion.LookRotation(moveDir);
        }
        controller.Move(Physics.gravity * Time.deltaTime);
    }

    /// <summary>
    /// Returns the direction where the player character should be moving
    /// </summary>
    bool TryGetMoveDirection(out Vector3 move)
    {
        float forward = Input.GetAxisRaw("Vertical");
        float right = Input.GetAxisRaw("Horizontal");
        if (Mathf.Approximately(forward, 0) && Mathf.Approximately(right, 0))
        {
            move = Vector3.zero;
            return false;
        }

        Vector3 cameraForward = Camera.main.transform.forward;
        cameraForward.y = 0;
        cameraForward *= forward;
        cameraForward.Normalize();

        Vector3 cameraRight = Camera.main.transform.right;
        cameraRight *= Input.GetAxisRaw("Horizontal");
        cameraRight.Normalize();

        move = (cameraForward + cameraRight).normalized;
        return true;
    }

    private void OnEnable()
    {
        DialogueSystem.OnDialogueStart += OnDialogueStart;
        DialogueSystem.OnDialogueEnd += OnDialogueEnd;
    }

    private void OnDisable()
    {
        DialogueSystem.OnDialogueStart -= OnDialogueStart;
        DialogueSystem.OnDialogueEnd -= OnDialogueEnd;
    }

    private void OnDialogueStart()
    {
        canMove = false;
    }

    private void OnDialogueEnd()
    {
        canMove = true;
    }

    public object CaptureState()
    {
        return new SerializeableVector3(transform.position);
    }

    public void RestoreState(object state)
    {
        SerializeableVector3 pos = (SerializeableVector3)state;
        if (pos != null)
        {
            transform.position = pos.GetVector3();
        }
    }
}