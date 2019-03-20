using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class SimpleCharacterControl : MonoBehaviour {
    public static int book;
    public static int bookdoor;
    public static int piano;
    public static int pianodoor;
    public static int chest;
    public static int chestdoor;
    public static int tank;
    public static int tankdoor;
    public static int Coin1;
    public static int Coin2;
    public static int Coin3;

    public static int crystal;
    public static bool displayText = false;
    public float textX_axis;
    public float textY_axis;
    public float textWidth;
    public float textHeight;
    public GUIStyle style;

    public string[] messageList;

    void Start()
    {
        book = 0;
        bookdoor = 0;
        piano = 0;
        pianodoor = 0;
        chest = 0;
        chestdoor = 0;
        tank = 0;
        tankdoor = 0;

        Coin1 = 0;
        Coin2 = 0;
        Coin3 = 0;

        crystal = 0;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Crystal"))
        {
            other.gameObject.SetActive(false);
            crystal = crystal + 1;
        }
    }

    private void OnGUI()
    {
        if (book >= 1 && displayText == true)
            GUI.Box(new Rect(textX_axis, textY_axis, textWidth, textHeight), messageList[0], style);
        if (piano >= 1 && displayText == true)
            GUI.Box(new Rect(textX_axis, textY_axis, textWidth, textHeight), messageList[1], style);
        if (chest >= 1 && displayText == true)
            GUI.Box(new Rect(textX_axis, textY_axis, textWidth, textHeight), messageList[2], style);
        if (tank >= 1 && displayText == true)
            GUI.Box(new Rect(textX_axis, textY_axis, textWidth, textHeight), messageList[3], style);
        if (Coin1 >= 1 && displayText == true)
            GUI.Box(new Rect(textX_axis, textY_axis, textWidth, textHeight), messageList[4], style);
        if (Coin2 >= 1 && displayText == true)
            GUI.Box(new Rect(textX_axis, textY_axis, textWidth, textHeight), messageList[5], style);
        if (Coin3 >= 1 && displayText == true)
            GUI.Box(new Rect(textX_axis, textY_axis, textWidth, textHeight), messageList[6], style);
    }

    private enum ControlMode
    {
        Tank,
        Direct
    }

    [SerializeField] private float m_moveSpeed = 2;
    [SerializeField] private float m_turnSpeed = 200;
    [SerializeField] private float m_jumpForce = 4;
    [SerializeField] private Animator m_animator;
    [SerializeField] private Rigidbody m_rigidBody;

    [SerializeField] private ControlMode m_controlMode = ControlMode.Direct;

    private float m_currentV = 0;
    private float m_currentH = 0;

    private readonly float m_interpolation = 10;
    private readonly float m_walkScale = 0.33f;
    private readonly float m_backwardsWalkScale = 0.16f;
    private readonly float m_backwardRunScale = 0.66f;

    private bool m_wasGrounded;
    private Vector3 m_currentDirection = Vector3.zero;

    private float m_jumpTimeStamp = 0;
    private float m_minJumpInterval = 0.25f;

    private bool m_isGrounded;
    private List<Collider> m_collisions = new List<Collider>();

    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint[] contactPoints = collision.contacts;
        for(int i = 0; i < contactPoints.Length; i++)
        {
            if (Vector3.Dot(contactPoints[i].normal, Vector3.up) > 0.5f)
            {
                if (!m_collisions.Contains(collision.collider)) {
                    m_collisions.Add(collision.collider);
                }
                m_isGrounded = true;
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        ContactPoint[] contactPoints = collision.contacts;
        bool validSurfaceNormal = false;
        for (int i = 0; i < contactPoints.Length; i++)
        {
            if (Vector3.Dot(contactPoints[i].normal, Vector3.up) > 0.5f)
            {
                validSurfaceNormal = true; break;
            }
        }

        if(validSurfaceNormal)
        {
            m_isGrounded = true;
            if (!m_collisions.Contains(collision.collider))
            {
                m_collisions.Add(collision.collider);
            }
        } else
        {
            if (m_collisions.Contains(collision.collider))
            {
                m_collisions.Remove(collision.collider);
            }
            if (m_collisions.Count == 0) { m_isGrounded = false; }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if(m_collisions.Contains(collision.collider))
        {
            m_collisions.Remove(collision.collider);
        }
        if (m_collisions.Count == 0) { m_isGrounded = false; }
    }

	void Update () {
        m_animator.SetBool("Grounded", m_isGrounded);

        switch(m_controlMode)
        {
            case ControlMode.Direct:
                DirectUpdate();
                break;

            case ControlMode.Tank:
                TankUpdate();
                break;

            default:
                Debug.LogError("Unsupported state");
                break;
        }

        m_wasGrounded = m_isGrounded;
    }

    private void TankUpdate()
    {
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");

        bool walk = Input.GetKey(KeyCode.LeftShift);

        if (v < 0) {
            if (walk) { v *= m_backwardsWalkScale; }
            else { v *= m_backwardRunScale; }
        } else if(walk)
        {
            v *= m_walkScale;
        }

        m_currentV = Mathf.Lerp(m_currentV, v, Time.deltaTime * m_interpolation);
        m_currentH = Mathf.Lerp(m_currentH, h, Time.deltaTime * m_interpolation);

        transform.position += transform.forward * m_currentV * m_moveSpeed * Time.deltaTime;
        transform.Rotate(0, m_currentH * m_turnSpeed * Time.deltaTime, 0);

        m_animator.SetFloat("MoveSpeed", m_currentV);

        JumpingAndLanding();
    }

    private void DirectUpdate()
    {
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");


        if (Input.GetKey(KeyCode.LeftShift))
        {
            v *= m_walkScale;
            h *= m_walkScale;
        }

        m_currentV = Mathf.Lerp(m_currentV, v, Time.deltaTime * m_interpolation);
        m_currentH = Mathf.Lerp(m_currentH, h, Time.deltaTime * m_interpolation);


        JumpingAndLanding();
    }

    private void JumpingAndLanding()
    {
        bool jumpCooldownOver = (Time.time - m_jumpTimeStamp) >= m_minJumpInterval;

        if (jumpCooldownOver && m_isGrounded && Input.GetKey(KeyCode.Space))
        {
            m_jumpTimeStamp = Time.time;
            m_rigidBody.AddForce(Vector3.up * m_jumpForce, ForceMode.Impulse);
        }

        if (!m_wasGrounded && m_isGrounded)
        {
            m_animator.SetTrigger("Land");
        }

        if (!m_isGrounded && m_wasGrounded)
        {
            m_animator.SetTrigger("Jump");
        }
    }
}
