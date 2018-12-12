using System.Collections;
using UnityEngine;

/// <summary>
/// Uber simple 3rd person character controller
/// </summary>
[RequireComponent(typeof(CharacterController))]
public class CController : MonoBehaviour {
    public float MoveSpeed = 6f;
    public float TurnSpeed = 15f;
    public Transform Model;

    private Transform camTransform;
    private CharacterController controller;
    private Vector3 camForward;

    private void Start()
    {
        camTransform = Camera.main.transform;
        controller = GetComponent<CharacterController>();
        camForward = Vector3.Scale(camTransform.forward, new Vector3(1, 0, 1).normalized); // move this to the rotation method if the cam angle isnt fixed
    }

    private void Update()
    {
        Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        if (moveInput.magnitude > 0)
        {
            DoRotation(moveInput);
            Vector3 moveMotion = transform.forward * MoveSpeed * moveInput.magnitude;
            controller.Move(moveMotion * Time.deltaTime);
        }
    }

    private void DoRotation(Vector3 input)
    {
        Vector3 camMove = input.z * camForward + input.x * camTransform.right;
        Quaternion targetRot = Quaternion.LookRotation(camMove);
        Quaternion modelRot = Model.rotation;
        transform.rotation = targetRot; // character turns instantly and the model follows.
        Model.rotation = Quaternion.Lerp(modelRot, targetRot, TurnSpeed * Time.deltaTime);
    }
}
