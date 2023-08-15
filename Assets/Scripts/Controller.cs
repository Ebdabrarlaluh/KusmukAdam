using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] float m_speed = 4.0f;
    [SerializeField] float m_jumpForce = 7.5f;
    [SerializeField] float fallThreshold = 3.5f;

    public LayerMask enemyLayers;
    public Transform attackPoint;
    public float attackRange = 1f;
    public int attackDamage;
    public float attackRate = 2f;
    float nextAttackTime = 0f;

    private Animator m_animator;
    private Rigidbody2D m_body2d;
    private Sensor_Bandit m_groundSensor;

    private bool m_grounded = false;
    private bool m_isDead = false;
    private bool isFalling = false;

    void Start()
    {
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        m_groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_Bandit>();
    }

    void Update()
    {

        //Ground Sensor

        if (!m_grounded && m_groundSensor.State())
        {
            m_grounded = true;
            isFalling = false;
            m_animator.SetBool("Grounded", m_grounded);
            m_animator.SetBool("isJumping", m_grounded);
        }

        if (m_grounded && !m_groundSensor.State())
        {
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
        }
        
        if (!m_grounded && m_body2d.velocity.y < fallThreshold)
        {
            isFalling = true;
            m_animator.SetBool("isFalling", isFalling);
        }
        else if (!m_grounded && m_body2d.velocity.y > fallThreshold)
        {
            isFalling = false;
            m_animator.SetBool("isFalling", isFalling);
        }


        //Movement

        float inputX = Input.GetAxis("Horizontal");

        if (inputX > 0)
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        else if (inputX < 0)
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);

        m_body2d.velocity = new Vector2(inputX * m_speed, m_body2d.velocity.y);
        m_animator.SetFloat("AirSpeed", m_body2d.velocity.y);

        //Death

        if (Input.GetKeyDown("e"))
        {
            if (!m_isDead)
                m_animator.SetTrigger("Death");
            m_isDead = !m_isDead;
        }

        //Hurt

        else if (Input.GetKeyDown("q"))
            m_animator.SetTrigger("Hurt");

        //Attack

        else if (Time.time >= nextAttackTime && Input.GetMouseButtonDown(0))
        {
            nextAttackTime = Time.time + 1f / attackRate;
            m_animator.SetTrigger("Attack");

            Invoke("Attack", 0.15f);

            Invoke("Attack", 0.15f);
        }
            
        //Jump

        else if (Input.GetKeyDown("space") && m_grounded)
        {
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
            m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce);
            m_animator.SetBool("isJumping",true);
            m_groundSensor.Disable(0.2f);
        }

        //Idle
        else if (Mathf.Abs(inputX) > Mathf.Epsilon)
            m_animator.SetInteger("AnimState", 1);
        else
            m_animator.SetInteger("AnimState", 0);
    }

    void Attack()
    {
        //Select all enemies in range
        Collider2D[] hitEnemies= Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        
        if (attackPoint == null)
            return;
        //Range for visualize in unity
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);

    }
}
