using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour
{
    [SerializeField] private float speed = 5f;

    private IMovementInputGetter movementInputGetter = null;

    private void Awake() => movementInputGetter = GetComponent<IMovementInputGetter>();

    private void Update()
    {
        Vector3 movement = new Vector3
        {
            x = movementInputGetter.Horizontal,
            y = 0f,
            z = 0f,
        }.normalized;

        movement *= speed * Time.deltaTime;

        transform.Translate(movement);
    }
}
