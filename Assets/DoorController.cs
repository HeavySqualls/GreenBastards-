using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    float yDifference = 0.8f;
    float oneWayDuration = 0.75f;

    public void OpenDoor()
    {
        print("Door Opened.");
        StartCoroutine(Mover());
    }

    IEnumerator Mover()
    {
        Vector2 startPos = transform.position;
        Vector2 desirePos = new Vector3(startPos.x, startPos.y + yDifference);
        float timeElapsed = 0f;

        while (timeElapsed < oneWayDuration)
        {
            timeElapsed += Time.deltaTime;
            transform.position = Vector2.Lerp(startPos, desirePos, timeElapsed / oneWayDuration);
            yield return new WaitForEndOfFrame();
        }
    }
}
