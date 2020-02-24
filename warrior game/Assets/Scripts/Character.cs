using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Character : MonoBehaviour
{
	public int speed;
	public int jumpspeed;
    public int damage;

    float moveInput;

    public Vector3 offset;

    Vector2 forward;

    RaycastHit2D hit;

	Animator animator;
	Rigidbody2D rb;

	bool canJump = true;
	bool faceRight = true;
    bool canAttack = true;

	private void Start () {
		animator = GetComponent<Animator>();
		rb = GetComponent<Rigidbody2D>();

	}

	private void FixedUpdate () {
		rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

		if (moveInput > 0 || moveInput < 0) {
			animator.SetBool("isRunning", true);
		}
		else {
			animator.SetBool("isRunning", false);
		}

		if (faceRight == true && moveInput < 0) {
			Flip();
		} else if (faceRight == false && moveInput > 0){
			Flip();
		}

		if(Input.GetKeyDown(KeyCode.W)) {
			Jump();
		}

        if (Input.GetKeyDown(KeyCode.F) && canAttack)
        {
            Attack();
        }

        if (rb.position.y < -10f)
        {
            SceneManager.LoadScene("game over");
        }
	}

	private void OnCollisionEnter2D(Collision2D collision) {
		if (collision.transform.tag == "Platform") {
			canJump = true;
		}
	}

	public void Jump() {
		if (canJump) {
			rb.AddForce(Vector2.up * jumpspeed);
			canJump = false;
		}
	}

	private void Flip () {
		faceRight = !faceRight;
		Vector3 scaler = transform.localScale;
		scaler.x *= -1;
		transform.localScale = scaler;
	}

    public void MoveRight()
    {
        moveInput = 1;
    }

    public void MoveLeft()
    {
        moveInput = -1;
    }

    public void MoveStop()
    {
        moveInput = 0;
    }


    public void Attack()
    {
        canAttack = false;

        if(!faceRight)
        {
            forward = transform.TransformDirection(Vector2.right * -2);
        }
        else
        {
            forward = transform.TransformDirection(Vector2.right * 2);
        }

        RaycastHit2D hit = Physics2D.Raycast(transform.position, forward, 1.0f);

        if (hit)
        {
            if (hit.transform.tag == "Enemy")
            {
                hit.transform.GetComponent<EnemyHealth>().GetDamage(damage);
            }
            else
            {
                Debug.Log("This is not enemy");
            }
        }

        animator.SetTrigger("attack");
        StartCoroutine(AttackDelay());
    }

    IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(0.5f);
        canAttack = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "bayrak")
        {
            SceneManager.LoadScene("begin");
        }
    }
}
