using Riptide;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private CharacterController controller;
    [SerializeField] private Transform cam;
    [SerializeField] private float grav;
    [SerializeField] private float speed;
    [SerializeField] private float jumpHeight;

    private float gravAccel;
    private float moveSpeed;
    private float jumpSpeed;
    private bool[] inputs;
    private float yVelocity;

    public CharacterController Controller => controller;

    private void OnValidate()
    {
        if (controller == null) {
            controller = GetComponent<CharacterController>();
        }

        if (player == null)
        {
            player = GetComponent<Player>();
        }

        Initialize();
    }

    private void Start()
    {
        Initialize();
        inputs = new bool[6];
    }

    private void FixedUpdate()
    {
        Vector2 inputDirection = Vector2.zero;
        if (inputs[0])
            inputDirection.y += 1;

        if (inputs[1])
            inputDirection.y -= 1;

        if (inputs[2])
            inputDirection.x -= 1;

        if (inputs[3])
            inputDirection.x += 1;

        Move(inputDirection, inputs[4], inputs[5]);
    }


    private void Initialize()
    {
        gravAccel = grav * Time.fixedDeltaTime * Time.fixedDeltaTime;
        moveSpeed = speed * Time.fixedDeltaTime;
        jumpSpeed = Mathf.Sqrt(jumpHeight * -3f * gravAccel);
    }

    private void Move(Vector2 inputDirection, bool jump, bool sprint)
    {
        Vector3 moveDirection = Vector3.Normalize(cam.right * inputDirection.x + Vector3.Normalize(FlattenVector3(cam.forward)) * inputDirection.y);
        moveDirection *= moveSpeed;

        if (sprint)
            moveDirection *= 2f;

        if (controller.isGrounded)
        {
            yVelocity = 0f;
            if (jump)
                yVelocity = jumpSpeed;
        }
        yVelocity += gravAccel;

        moveDirection.y = yVelocity;
        controller.Move(moveDirection);

        SendMovement();
    }

    private Vector3 FlattenVector3(Vector3 vector)
    {
        vector.y = 0;
        return vector;
    }

    public void SetInput(bool[] inputs, Vector3 forward)
    {
        this.inputs = inputs;
        cam.forward = forward;
    }

    private void SendMovement()
    {
        Message message = Message.Create(MessageSendMode.Unreliable, ServerToClientId.playerMovement);
        message.AddUShort(player.Id);
        message.AddVector3(transform.position);
        message.AddVector3(cam.forward);
        NetworkManager.Singleton.Server.SendToAll(message);
    }

}
