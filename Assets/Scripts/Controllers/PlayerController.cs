using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour {

    [SerializeField] private LayerMask groundMask;
    [SerializeField] private IntractableO focus;

    private PlayerMotor motor;
    private Camera cam;

    // Use this for initialization
    void Start() {
        cam = Camera.main;
        motor = GetComponent<PlayerMotor>();
    }

    // Update is called once per frame
    void Update() {
        if (EventSystem.current.IsPointerOverGameObject())
            return;// will check if the cursor is over any UI or not

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, groundMask))
            {
                motor.MoveToPoint(hit.point);
                //To stop focusing on an Object
                RemoveFocus();
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100f))
            {
                //To interact with Objects on right click
                   SetFocus(hit.collider.GetComponent<IntractableO>());
            }
        }
    }

    private void SetFocus(IntractableO newFocus) {
        if (newFocus != focus && focus !=null)
                focus.OnDeFocused();
            focus = newFocus;

        if (focus != null)
        {
            newFocus.OnFocused(transform);

            motor.FollowTarget(newFocus);
        }
    }

    private void RemoveFocus() {
        if(focus!=null)
            focus.OnDeFocused();
        focus = null;
        motor.StopFollowingTarget(focus);
    }
}
