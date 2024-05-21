using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Touch initialTouch = new Touch();
    private float distance = 0;
    private bool hasSwiped = false;
    public bool jump = false;
    public bool slide = false;
    public Animator Animator;
    public GameObject trigger;
    public float score = 0;
    public bool death = false;
    public Image GameOverImage;
    public TMP_Text scoreText;
    public TMP_Text BestScoreText;
    public CapsuleCollider Collider;
    public float FinaleScore;

    // Start is called before the first frame update
    void Start()
    {
        Animator = GetComponent<Animator>();
        Collider = GetComponent<CapsuleCollider>();
        FinaleScore = PlayerPrefs.GetFloat("MyScore");
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        foreach (Touch t in Input.touches)
        {
            if (t.phase == TouchPhase.Began)
            {
                initialTouch = t;
            }
            else if (t.phase == TouchPhase.Moved && !hasSwiped)
            {
                float deltaX = initialTouch.position.x - t.position.x;
                float deltaY = initialTouch.position.y - t.position.y;
                distance = Mathf.Sqrt((deltaX * deltaX) + (deltaY - deltaY));
                bool swipedSideWay = Mathf.Abs(deltaX) > Mathf.Abs(deltaY);

                if (distance > 100f)
                {
                    if (swipedSideWay && deltaX > 0)
                    {

                    }
                    if (swipedSideWay && deltaX <= 0)
                    {

                    }
                    if (!swipedSideWay && deltaY > 0)
                    {
                        slide = true;
                        StartCoroutine(SlideController());
                    }
                    if (!swipedSideWay && deltaY > 0)
                    {
                        jump = true;
                        StartCoroutine(JumpController());
                    }
                    hasSwiped = true;
                }
            }
            else if (t.phase == TouchPhase.Ended)
            {
                initialTouch = new Touch();
                hasSwiped = false;
            }
        }


        scoreText.text = score.ToString();

        if (score > FinaleScore)
        {
            BestScoreText.text = "Best Score : " + score.ToString();
        }
        else
        {
            BestScoreText.text = "Your Score : " + score.ToString();
        }

        if (death == true)
        {
            GameOverImage.gameObject.SetActive(true);
        }

        if (score > 10000 && death != true)
        {
            transform.Translate(0, 0, 0.2f);
        }
        else if (score >= 20000 && death != true)
        {
            transform.Translate(0, 0, 0.3f);
        }
        else if (death == true)
        {
            transform.Translate(0, 0, 0);
        }
        else
        {
            transform.Translate(0, 0, 1f);
        }
        


        if (Input.GetKey(KeyCode.Space))
        {
            jump = true;
            StartCoroutine(JumpController());
        }
        else
        {
            jump = false;
        }
        if (jump == true)
        {
            Animator.SetBool("isJump", jump);
            transform.Translate(0, 0.6f, 0.2f);
        }
        else if (jump == false)
        {
            Animator.SetBool("isJump", jump);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            slide = true;
            StartCoroutine(SlideController());
        }
        else
        {
            slide = false;
        }
        if (slide == true)
        {
            Animator.SetBool("isSlide", slide);
            transform.Translate(0, 0, 0.1f);
            Collider.height = 1f;
        }
        else if (slide == false)
        {
            Animator.SetBool("isSlide", slide);
            Collider.height = 2.3f;
        }
        trigger = GameObject.FindGameObjectWithTag("Obstacle");
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerTrigger")
        {
            Destroy(trigger.gameObject);
        }
        if (other.gameObject.tag == "Coin")
        {
            Destroy(other.gameObject, 0.5f);
            score += 5f;
        }
        if (other.gameObject.tag == "DeathPoint")
        {
            death = true;
            if (score > FinaleScore)
            {
                PlayerPrefs.SetFloat("MyScore", score);
            }
        }

    }
    IEnumerator JumpController()
    {
        jump = true;
        yield return new WaitForSeconds(0.2f);
        jump = false;
    }
    IEnumerator SlideController()
    {
        slide = true;
        yield return new WaitForSeconds(1f);
        slide = false;
    }
    public void Menu()
    {
        SceneManager.LoadScene(0);
    }
    public void PlayAgain()
    {
        SceneManager.LoadScene(1);
    }
}
