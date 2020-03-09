using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Catcontroller : MonoBehaviour

{
    public AudioClip musicClipOne;

    public AudioClip musicClipTwo;

    public AudioSource musicSource;

    private Rigidbody2D rd2d;

    public float speed;

    public Text countText;
    public Text livesText;
    public Text winText;
    private int lives;
    private int count;

    Animator anim;

    void Start()
    {
        musicSource.clip = musicClipOne;
        musicSource.Play();
        anim = GetComponent<Animator>();
        rd2d = GetComponent<Rigidbody2D>();
        count = 0;
        winText.text = "";
        lives = 3;
        SetCountText();
        SetLivesText();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement));

       
        if (Input.GetKeyDown(KeyCode.D))
        {
            anim.SetInteger("State", 1);
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            anim.SetInteger("State", 0);
        }

      
        if (Input.GetKeyDown(KeyCode.A))
        {
            anim.SetInteger("State", 1);
          
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            anim.SetInteger("State", 0);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            anim.SetInteger("State", 2);
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            anim.SetInteger("State", 0);
        }

  

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Coin")
        {
            count += 1;
            SetCountText();
            Destroy(collision.collider.gameObject);
        }

        else if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.SetActive(false);
            lives -= 1;
            SetLivesText();
        }

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 10), ForceMode2D.Impulse);
            }
        }
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count == 4)
        {

            transform.position = new Vector2(350.0f, 0.0f);
            lives = 3;
            SetLivesText();
        }

        else if (count >= 8)
        {
            Destroy(GetComponent<Rigidbody2D>());
            winText.text = "You Win! Game created by Amelia Stephens!";
            musicSource.clip = musicClipTwo;
            musicSource.Play();
        }
    }
    void SetLivesText()
    {
        if (lives <= 0)
        {
            Destroy(this.gameObject);
            winText.text = "You Lose! Game created by Amelia Stephens!";
        }
        livesText.text = "Lives: " + lives.ToString();
    }
}


