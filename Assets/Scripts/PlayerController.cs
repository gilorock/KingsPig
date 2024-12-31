using System;
using Unity.Mathematics;
using UnityEditor.Tilemaps;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Player Components
    private Rigidbody2D m_rigidbody2D;
    private GatherInput m_gatherInput;
    private Transform m_transform;
    private Animator m_animator;

    [Header("Move and Jump Settings")]
    [SerializeField]  private float speed;
    private int direction = 1;
    [SerializeField] private float jumpForce;
    [SerializeField] private int extraJumps;
    [SerializeField] private int counterExtraJumps;
    private int idSpeed;

    [Header("Ground Settings")]
    [SerializeField] private Transform lFoot;
    [SerializeField] private Transform rFoot;
    [SerializeField] private bool isGrounded;
    [SerializeField] private float rayLegnth;
    [SerializeField] private LayerMask groundLayer;
    private int idIsGrounded;

    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_gatherInput = GetComponent<GatherInput>();
        m_transform = GetComponent<Transform>();
        m_rigidbody2D = GetComponent<Rigidbody2D>();
        m_animator = GetComponent<Animator>();
        idSpeed = Animator.StringToHash("Speed");
        idIsGrounded = Animator.StringToHash("isGrounded");
        lFoot = GameObject.Find("LFoot").GetComponent<Transform>();
        rFoot = GameObject.Find("RFoot").GetComponent<Transform>();
        counterExtraJumps = extraJumps;
    }

    // Update is called once per frame
    private void Update()
    {
        SetAnimatorValues();
       
    }

    private void SetAnimatorValues()
    {
        m_animator.SetFloat(idSpeed, Mathf.Abs(m_rigidbody2D.linearVelocityX));
        m_animator.SetBool(idIsGrounded,isGrounded);
    }

    void FixedUpdate()
    {
        Move();
        Jump();
        CheckGround();
    }

    private void Move()
    {
        Flip();
        m_rigidbody2D.linearVelocity = new Vector2(speed * m_gatherInput.ValueX, m_rigidbody2D.linearVelocityY);
    }

    private void Flip()
    {
        if (m_gatherInput.ValueX * direction < 0)
        {
            m_transform.localScale = new Vector3(-m_transform.localScale.x,1,1);
            direction *= -1;
        }
    }

    private void Jump()
    {
        if(m_gatherInput.IsJumping)
        {
            if(isGrounded)
            m_rigidbody2D.linearVelocity = new Vector2(speed * m_gatherInput.ValueX, jumpForce);
            if(counterExtraJumps > 0)
            {
              m_rigidbody2D.linearVelocity = new Vector2(speed * m_gatherInput.ValueX, jumpForce);
                counterExtraJumps--;
            }
        }
        m_gatherInput.IsJumping = false;
    }

    private void CheckGround()
    {
        RaycastHit2D lFootRay = Physics2D.Raycast(lFoot.position, Vector2.down, rayLegnth, groundLayer);
        RaycastHit2D rFootRay = Physics2D.Raycast(rFoot.position, Vector2.down, rayLegnth, groundLayer);
        if(lFootRay ||  rFootRay)
        {
            isGrounded = true;
            counterExtraJumps = extraJumps;
        }

        else
        {
            isGrounded = false;
        }
    }
}
