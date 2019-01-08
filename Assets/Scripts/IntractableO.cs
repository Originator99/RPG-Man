using UnityEngine;

//Used for all Intractable Objects in the Game

public class IntractableO : MonoBehaviour {

    public float radius = 3f; //Distance between the player and object to intract with it.
    public Transform interactionTransform;

    private bool isFocus = false;
    private bool hasInteracted = false;
    private Transform player;

    public virtual void Interact() {
        //This method is supposed to be overwritten by other objects that are interactable
        //Debug.Log("Interacting with " + transform.name);
    }

    public void OnFocused(Transform playerTransform) {
        isFocus = true;
        hasInteracted = false;
        player = playerTransform;
    }

    public void OnDeFocused() {
        isFocus = false;
        hasInteracted = false;
        player = null;
    }

    private void Update()
    {
        if (isFocus)
        {
            float dist = Vector3.Distance(player.position, interactionTransform.position);
            if (!hasInteracted && dist <= radius)
            {
                hasInteracted = true;
                Interact();
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (interactionTransform == null)
            interactionTransform = transform;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }
}
