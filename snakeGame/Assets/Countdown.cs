using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{

    public int countdownTime = 3;
    public Text countdownText;
    public Snake snake;
    public AudioSource audioSource;
    public AudioClip countdownSound;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(startCountdown());
    }

    IEnumerator startCountdown()
    {
        float volume = PlayerPrefs.GetFloat("masterVolume", AudioListener.volume);
        audioSource.volume = (volume / 2);
        audioSource.pitch = .8f;
        snake.isGameStart = false;
        while (countdownTime > 0)
        {
            countdownText.text = countdownTime.ToString();
            audioSource.PlayOneShot(countdownSound);
            yield return new WaitForSeconds(1f);
            countdownTime--;
        }
        snake.isGameStart = true;
        snake.ResetState();
        countdownText.gameObject.SetActive(false);
    }
}
