using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    public int speed;
    public Rigidbody enemy;
    public int invocationInterval = 1;

    private System.Random numberMaker;
    private int enemyWait = 3;

    void Start ()
    {
        numberMaker = new System.Random();
        enemy = GetComponent<Rigidbody>();
        InvokeRepeating("MoveEnemy", enemyWait, invocationInterval);
    }

    // Update is called once per frame
    private void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.CompareTag("EnemyCollides"))
        {
            transform.Rotate(new Vector3(90, 0, 0));
        }
    }

    void LateUpdate()
    {

    }

    private void MoveEnemy()
    {
        int moveHorizontal = numberMaker.Next(21);
        int moveVertical = numberMaker.Next(21);
        int horizontalSign = numberMaker.Next(2);
        if (horizontalSign == 0)
        {
            horizontalSign = -1;
        }

        int verticalSign = numberMaker.Next(2);
        if (verticalSign == 0)
        {
            verticalSign = -1;
        }

        Vector3 movement = new Vector3(moveHorizontal * horizontalSign, 0, moveVertical * verticalSign);

        enemy.AddForce(movement * speed);
    }

}
