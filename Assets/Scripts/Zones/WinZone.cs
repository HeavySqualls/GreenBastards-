using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinZone : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerController>())
        {
            print("Player Wins!");
            // Send players score to the game manager
            other.gameObject.GetComponent<PlayerController>().DeliverScore();
            other.gameObject.GetComponent<PlayerController>().isFrozen = true;

            // Stop timer 
            Toolbox.GetInstance().GetTimeManager().StopTimeTracker();

            // Change to next level 
            Toolbox.GetInstance().GetGameManager().NextOnClick();
        }
    }
}
