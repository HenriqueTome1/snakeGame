using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePaused : MonoBehaviour
{
    public void Paused()
    {
        gameObject.SetActive(true);
    }

    public void Unpaused()
    {
        gameObject.SetActive(false);
    }
}
