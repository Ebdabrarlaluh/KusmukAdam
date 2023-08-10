using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveBackground : MonoBehaviour
{
    private MoveBackground move_background;
    void Start()
    {
        GameObject moveBackgroundObject = GameObject.Find("Layer_0000_9");

        if (moveBackgroundObject != null)
        {
            move_background = moveBackgroundObject.GetComponent<MoveBackground>();
        }
        else
        {
            Debug.LogError("MoveBackground script not found on MoveBackground object!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (move_background != null)
        {
            float moveAmount = Time.deltaTime * move_background.speed;
            transform.Translate(new Vector3(moveAmount, 0f, 0f));
        }
        if (transform.position.x < -10f || transform.position.x > 10f)
        {
            Destroy(gameObject); // Yaratığı yok et
        }
    }
}
