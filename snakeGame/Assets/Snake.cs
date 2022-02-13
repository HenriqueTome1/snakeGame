using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class Snake : MonoBehaviour
{
    public int nInitialSize = 4;
    public AudioSource eatAudioSource;
    public AudioClip eatSound;
    public Animator scoreTextAnimator;
    public Text scoreText;
    public GameMusic gameMusic;
    public GameOverScreen gameOverScreen;
    public SpawnFood spawnFood;
    public GamePaused gamePaused;

    public bool isGameStart = false;

    private int score = 0;
    private bool isGamePaused = false;

    Vector2 dir = Vector2.right;
    // Keep Track of Tail
    List<Transform> tail = new List<Transform>();
    // Tail Prefab
    public GameObject tailPrefab;

    public GameObject[] foodNodes;

    // Start is called before the first frame update
    void Start()
    {
        //ResetState();
        //tail.Add(this.transform);
    }

    // Update is called once per frame
    private void Update()
    {
        if (isGameStart)
        {
            if (Input.GetKey(KeyCode.RightArrow))
                dir = Vector2.right;
            else if (Input.GetKey(KeyCode.DownArrow))
                dir = Vector2.down;    // '-up' means 'down'
            else if (Input.GetKey(KeyCode.LeftArrow))
                dir = Vector2.left; // '-right' means 'left'
            else if (Input.GetKey(KeyCode.UpArrow))
                dir = Vector2.up;


            // Pause the game
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Space))
            {
                isGamePaused = !isGamePaused;
                if (isGamePaused) PauseGame();
                else UnpauseGame();
            }
        }
    }

    private void FixedUpdate()
    {
        if (!isGamePaused && isGameStart) {
            // Move head into new direction
            for (int i = tail.Count - 1; i > 0; i--)
            {
                tail[i].position = tail[i - 1].position;
            }

            transform.position = new Vector3(
                Mathf.Round(transform.position.x) + dir.x,
                Mathf.Round(transform.position.y) + dir.y,
                0.0f);
        }
    }

    private void PauseGame()
    {
        spawnFood.CancelInvoke();
        gameMusic.Pause();
        gamePaused.Paused();
    }

    private void UnpauseGame()
    {
        spawnFood.InvokeRepeating("Spawn", 3, 4);
        gameMusic.Play();
        gamePaused.Unpaused();
    }

    private void Eat()
    {
        GameObject g = (GameObject)Instantiate(tailPrefab);
        g.transform.position = tail.Last().position;
        tail.Add(g.transform);
    }

    public void ResetState()
    {
        score = 0;

        // Remover comida
        foodNodes = GameObject.FindGameObjectsWithTag("Food");
        foreach (GameObject food in foodNodes)
        {
            Destroy(food); 
        }

        tail.Clear();
        tail.Add(this.transform);

        for (int i = 1; i < nInitialSize; i++)
        {
            GameObject g = (GameObject)Instantiate(tailPrefab);
            tail.Add(g.transform);
        }

        this.transform.position = Vector3.zero;
        this.transform.gameObject.SetActive(true);
        gameMusic.StartMusic();
        spawnFood.InvokeRepeating("Spawn", 3, 4);
    }

    private void EatEffect()
    {
        scoreTextAnimator.SetTrigger("effect");
        score += 1;
        scoreText.text = score.ToString();
        float volume = PlayerPrefs.GetFloat("masterVolume", AudioListener.volume);
        eatAudioSource.volume = volume;
        eatAudioSource.PlayOneShot(eatSound);
    }

    private void GameOver()
    {
        spawnFood.CancelInvoke();
        
        scoreText.text = "";
        for (int i = 1; i < tail.Count; i++)
        {
            Destroy(tail[i].gameObject);
        }
        this.transform.gameObject.SetActive(false);
        gameOverScreen.Setup(score);
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Food")
        {
            // Get longer in next Move call
            Eat();
            EatEffect();
            // Remove the Food
            Destroy(coll.gameObject);
        }
        // Collided with Tail or Border
        else if (coll.tag == "Obstacle")
        {
            gameMusic.StopMusic();
            GameOver();
            //ResetState();
        }
    }
}
