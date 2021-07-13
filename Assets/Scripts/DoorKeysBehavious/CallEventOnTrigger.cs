using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]

public class CallEventOnTrigger : MonoBehaviour
{
    public UnityEvent<Collider2D> OnCollisionEnter;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnCollisionEnter.Invoke(collision);
    }
}
