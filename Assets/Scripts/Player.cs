using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Apple.ReplayKit;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public static Player Instance;
    private Rigidbody2D rb;
    private Animator animator;
    private bool death = false;
    //C#里的委托和事件
    public delegate void DeathNotify();
    public event DeathNotify OnDeath;
    //Unity自带的委托和事件(无需定义委托)
    public UnityAction<int> OnScore;
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        Idle();
    }

    // Update is called once per frame
    void Update()
    {
        if (death)return;
        if(Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(new Vector2(0,5),ForceMode2D.Impulse);
        }
    }

    public void Idle()
    {
        rb.simulated = false;
        animator.SetTrigger("Idle");
        
    }
    public void Fly()
    {
        animator.SetTrigger("Fly");
        rb.simulated = true;
        this.gameObject.transform.position = new Vector3(0,3,0);
        

    }
    public void Die()
    {
        death = true;
        OnDeath?.Invoke();
    }

    public void ReStart()
    {
        death = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.transform.tag == "Ground")
        {
            Die();
            Idle();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.transform.tag == "Piple")
        {
            Die();
            Idle();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.transform.tag == "Score" && GameManager.Instance.status == GameManager.GAME_STATUS.InGame)
        {
            OnScore?.Invoke(1);
        }
    }
}
