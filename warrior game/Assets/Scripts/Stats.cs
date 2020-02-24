using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Stats : MonoBehaviour
{
    public Image[] hearts;
    public int health = 5;

    int maxhealth = 5;

    public void Damage(int amount)
    {
        hearts[health - 1].enabled = false;
        health -= amount;
    }

    public void Regen(int amount)
    {
        health += amount;

        for (int i = 0; i < health; i++)
        {
            hearts[i].enabled = true;
        }
    }

    private void Update()
    {
        if (health > maxhealth)
        {
            health = maxhealth;
        }

        if (health == 0)
        {
            SceneManager.LoadScene("game over"); 
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Enemy")
        {
            if (health > 0)
            {
                Damage(1);
            }
        }

        if (collision.transform.tag == "Regen")
        {
            if (health < maxhealth)
            {
                Regen(1);
            }
        }
    }
}
