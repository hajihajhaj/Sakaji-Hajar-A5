using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Rigidbody2D ball;
    public Text scoreLeftText;
    public Text scoreRightText;
    public GameObject countdownWrapper;
    public Text countdownText;

    private int _scoreLeft;
    private int _scoreRight;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
            instance = this;

        // countdown to the start of the game
        _scoreLeft = 0;
        _scoreRight = 0;

        InitializeBall();
        StartCoroutine(_Countingdown());
    }

   public void  InitializeBall()
   {
        // get a random angle for the ball direction
       float angle = Random.Range(0f, 30f);
       float r = Random.Range(0f, 1f);
       if (r < 0.25f)
            angle = 180f - angle;
       else if (r < 0.5f)
            angle += 180f;
       else if (r < 0.75f)
            angle = 360f - angle;
       angle *= Mathf.Deg2Rad;

       Vector2 dir = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
       ball.velocity = dir.normalized * 10f;

       ball.transform.position = Vector3.zero; // reset the ball
   }

    public void IncreaseScore(bool left)
    {
        if (left)
        {
            // check if left player scored/won
            _scoreLeft++;
            scoreLeftText.text = _scoreLeft.ToString();
            if (_scoreLeft >= 11)
            {
                GameOver();
            }
        }
        else
        {
            // check if right player scored/won
            _scoreRight++;
            scoreRightText.text = _scoreRight.ToString();
            if (_scoreRight >= 11)
            {
                GameOver();
            }
        }
    }

    public void GameOver()
    {
        Time.timeScale = 0; // freeze the game when its over
    }

    private IEnumerator _Countingdown()
    {
        //freeze everything
        Time.timeScale = 0;

        // count down
        for (int c = 3; c > 0; c--)
        {
            countdownText.text = c.ToString();
            yield return new WaitForSecondsRealtime(1);
        }

        countdownWrapper.SetActive(false);

        // reset the time scale
        Time.timeScale = 1;
    }
}
