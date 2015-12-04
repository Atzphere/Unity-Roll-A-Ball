using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float speed;
    public Text countText;
    public Text resultText;
    public Text timeRemainingText;
    public float totalTime = 60;

    private Rigidbody rb;
    private int count;
    private float currentTime;
    private float timeRemaining;
    private bool gameDone = false;

	void Start ()
    { 
		rb = GetComponent<Rigidbody> ();
        count = 0;
        SetCountText ();
        resultText.text = "";
        currentTime = 0;      
	}

	void FixedUpdate ()
    {
        if (!gameDone)
        {
            UpdateTime();
            DetermineResult();
            if (!gameDone)
            {
                Moveball();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pickup"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
        }
    }

    private void SetCountText ()
    {
        countText.text = "Count: " + count.ToString();
    }

    private void UpdateTime ()
    {
        int prettyTime = 0;

        currentTime = currentTime + Time.deltaTime;
        timeRemaining = totalTime - currentTime;
        if(timeRemaining > 0)
        {
            prettyTime = (int)System.Math.Ceiling(timeRemaining);
        }
        timeRemainingText.text = "Time Remaining: " + prettyTime.ToString();
    }

    private void DetermineResult ()
    {
        if (hasWon())
        {
            resultText.text = "You Win!";
            gameDone = true;
        }
        if (hasLost())
        {
            resultText.text = "You Ran Out Of Time!";
            gameDone = true;
        }
    }

    private bool hasWon()
    {
        return count >= 29;
    }

    private bool hasLost()
    {
        return timeRemaining <= 0;
    }

    private void Moveball()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * speed);

    }
}