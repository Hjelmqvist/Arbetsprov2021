using UnityEngine;

/// <summary>
/// Component to be on the player character that looks for interactable objects
/// </summary>
public class PlayerInteracter : MonoBehaviour
{
    [SerializeField] Vector3 boxSize = Vector3.one;
    [SerializeField] float range = 2f;
    [SerializeField] LayerMask layers = default;
    
    void Update()
    {
        RaycastHit[] hits = Physics.BoxCastAll(transform.position, boxSize / 2, transform.forward, transform.rotation, range, layers);
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].transform.TryGetComponent(out IInteractable interactable))
            {
                Debug.Log(hits[i].transform.name);
                break;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawCube(transform.position + (transform.forward * range / 2), boxSize);
    }
}