using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float speed;
    public Text countText;
    public Text resultText;
    public Text timeRemainingText;
    public float totalTime = 60;

    private enum LostReason
    {
        fall,
        time,
        enemy
    }

    private enum MovementPowerUp
    {
        none,
        noSlide,
        fly
    }

    private Rigidbody rb;
    private int count;
    private float currentTime;
    private float timeRemaining;
    private bool gameDone = false;
    private LostReason endReason;
    private MovementPowerUp powerUpState;


	void Start ()
    { 
		rb = GetComponent<Rigidbody> ();
        count = 0;
        SetCountText ();
        resultText.text = "";
        currentTime = 0;
        powerUpState = MovementPowerUp.none;
        

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
        if (other.gameObject.CompareTag("NoSlidePowerup"))
        {
            powerUpState = MovementPowerUp.noSlide;
        }

    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            endReason = LostReason.enemy;
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
  
            switch (endReason)
            {
                case LostReason.time:
                    {
                        resultText.text = "You Ran Out Of Time!";
                        break;
                    }
                case LostReason.fall:
                    {
                        resultText.text = "You Fell Out!";
                        break;
                    }
                case LostReason.enemy:
                    {
                        resultText.text = "An Enemy Killed You!";
                        break;
                    }
            }

            gameDone = true;
        }
    }

    private bool hasWon()
    {
        return count >= 29;
    }

    private bool hasLost()
    {
        bool hasLost = false;
        if (timeRemaining <= 0)
        {
            endReason = LostReason.time;
            hasLost = true;
        }

        else if (rb.position.y <= 0)
        {
            endReason = LostReason.fall;
            hasLost = true;
        }

        else if (endReason == LostReason.enemy)
        {
            hasLost = true;
        }
        return hasLost;
    }

    private void Moveball()
    {
        float moveVertical = 0;
        float moveHorizontal = 0;

        if (powerUpState == MovementPowerUp.noSlide)
        {
            MovePowered(ref moveVertical, ref moveHorizontal);
        }
        else
        {
            MoveNormal(ref moveVertical, ref moveHorizontal);
        }
        

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        Debug.Log(movement.ToString());
        rb.AddForce(movement * speed);

    }
    private void MoveNormal(ref float moveVert, ref float moveHoriz)
    {
        moveHoriz = Input.GetAxis("Horizontal");
        moveVert = Input.GetAxis("Vertical");
        
    }
    private void MovePowered(ref float moveVert, ref float moveHoriz)
    {
        if (Input.GetKeyDown(""))
    }
}