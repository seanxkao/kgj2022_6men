using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    private Game game;
    private Rigidbody rb;
    private Vector3 xVector;
    private Vector3 yVector;

    [SerializeField]
    private float speed;
    [SerializeField]
    private float jumpHeight;
    [SerializeField]
    private float gravity;

    private Vector3 jumpHorizontalVelocity;

    [Header("Player Grounded")]
    [SerializeField]
    private float groundedOffset = -0.2f;
    [SerializeField]
    private float groundedRadius = 0.305f;
    [SerializeField]
    private float moveCheckOffset = 0.505f;
    [SerializeField]
    private float moveCheckRadius = 0.495f;
    [SerializeField]
    private float moveCheckDepth = 0.505f;
    [SerializeField]
    private LayerMask groundLayers;
    [SerializeField]
    private bool grounded = true;

    public GameEvent<Player> Interact { get; private set; } = new GameEvent<Player>();

    private void Awake() 
    {
        rb = GetComponent<Rigidbody>();    
    }

    private void Start() 
    {
        game = Game.instance;
        if(game != null)
        {
            SetMoveAxis(game.projectionAxis);
            game.AxisChange.AddListener(SetMoveAxis);
        }
    }

    private void SetMoveAxis(ProjectionAxis axis)
    {
        transform.position = new Vector3
        (
            Mathf.RoundToInt(transform.position.x),
            Mathf.RoundToInt(transform.position.y),
            Mathf.RoundToInt(transform.position.z)
        );
        
        switch(axis)
        {
            case ProjectionAxis.X:
                rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX;
                xVector = Vector3.forward;
                yVector = Vector3.up;
                break;
            case ProjectionAxis.Y:
                rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
                xVector = Vector3.left;
                yVector = Vector3.back;
                break;
            case ProjectionAxis.Z:
                rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;
                xVector = Vector3.left;
                yVector = Vector3.up;
                break;
        }
    }

    private void Update() 
    {
        if(Input.GetKeyDown(KeyCode.Space) && grounded)
            Interact.Invoke(this);
        if(game.projectionAxis != ProjectionAxis.Y && grounded && Input.GetKeyDown(KeyCode.W))
            jumpHorizontalVelocity = Vector3.up * Mathf.Sqrt(2 * gravity * jumpHeight); // v = (2gh)^0.5
    }

    private void FixedUpdate() 
    {
        Vector3 velocity;
        if(game.projectionAxis == ProjectionAxis.Y)
            velocity = MoveWithoutJump();
        else
            velocity = MoveWithJump();
        
        velocity += jumpHorizontalVelocity;
        jumpHorizontalVelocity += Vector3.down * gravity * Time.fixedDeltaTime;

        Vector3 expectedNextFramePosition = transform.position + velocity * Time.fixedDeltaTime;
        Vector3 constrainedPosition = expectedNextFramePosition; 
        if(game.InvalidPosition(expectedNextFramePosition, out constrainedPosition))
        {
            velocity = Vector3.zero;
            transform.position = constrainedPosition;
        }
        rb.velocity = velocity;

        GroundCheck();
    }

    private void GroundCheck()
    {
        Vector3 spherePosition;
        
        // Ground check
        spherePosition = transform.position + Vector3.up * groundedOffset;
        grounded = Physics.CheckSphere(spherePosition, groundedRadius, groundLayers, QueryTriggerInteraction.Ignore);

        if(grounded && jumpHorizontalVelocity.y < 0)
            jumpHorizontalVelocity = Vector3.zero;
        
        if(game.projectionAxis == ProjectionAxis.Y)
        {
            if(grounded)
                rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
            else
                rb.constraints = RigidbodyConstraints.FreezeRotation;
        }
    }

    Vector3 MoveWithJump()
    {
        Vector3 direction = Vector3.zero;
        if(Input.GetKey(KeyCode.A))
            direction -= xVector;
        if(Input.GetKey(KeyCode.D))
            direction += xVector;

        /*var canMove = true;
        
        if(direction.magnitude > 0)
            canMove = PredictMoveRaycast(transform.position, direction);
        
        if(!canMove)
            direction = Vector3.zero;*/

        if(direction.sqrMagnitude > 0)
            direction = direction.normalized;
        
        return direction * speed;
    }

    Vector3 MoveWithoutJump()
    {
        Vector3 direction = Vector3.zero;
        if(Input.GetKey(KeyCode.W))
            direction += yVector;
        if(Input.GetKey(KeyCode.A))
            direction -= xVector;
        if(Input.GetKey(KeyCode.S))
            direction -= yVector;
        if(Input.GetKey(KeyCode.D))
            direction += xVector;
        
        /*var canMove = true;
        
        if(direction.magnitude > 0)
            canMove = PredictMoveRaycast(transform.position, direction);
        
        if(!canMove)
            direction = Vector3.zero;*/

        if(direction.sqrMagnitude > 0)
            direction = direction.normalized;
        
        return direction * speed;
    }

    private bool PredictMoveRaycast(Vector3 position, Vector3 moveDirection)
    {
        Vector3 checkPosition = transform.position + moveDirection * moveCheckOffset;
        bool a = Physics.Raycast(checkPosition + xVector * moveCheckRadius, Vector3.down, moveCheckDepth, groundLayers, QueryTriggerInteraction.Ignore);
        bool b = Physics.Raycast(checkPosition - xVector * moveCheckRadius, Vector3.down, moveCheckDepth, groundLayers, QueryTriggerInteraction.Ignore);
        bool c = Physics.Raycast(checkPosition + yVector * moveCheckRadius, Vector3.down, moveCheckDepth, groundLayers, QueryTriggerInteraction.Ignore);
        bool d = Physics.Raycast(checkPosition - yVector * moveCheckRadius, Vector3.down, moveCheckDepth, groundLayers, QueryTriggerInteraction.Ignore);
        bool e = Physics.Raycast(checkPosition, Vector3.down, moveCheckDepth, groundLayers, QueryTriggerInteraction.Ignore);

        if(Vector3.Dot(moveDirection, yVector) != 0 && ((!a && b) || (a && !b)))
        {
            return false;
        }
        if(Vector3.Dot(moveDirection, xVector) != 0 && ((!c && d) || (c && !d)))
        {
            return false;        
        }
        if(!a && !b && !c && !d && !e)
            return false;
        
        return true;
    }
}
