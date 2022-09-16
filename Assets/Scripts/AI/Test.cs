using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    Vector2 currentPosition;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            MoveLeft();
        }
        else if (Input.GetKeyDown(KeyCode.Y))
        {
            MoveRight();
        }

        //FaceDirection();
    }

    private void FaceDirection()
    {
        currentPosition = rb.transform.localPosition;
        Debug.Log("X: " + currentPosition.x + "; Y: " + currentPosition.y);
        if (currentPosition.x > 0f && Mathf.Abs(currentPosition.x) > Mathf.Abs(currentPosition.y))
        {
            Debug.Log("Left");
        }
        else
        {
            Debug.Log("Right");
        }
    }

    private void MoveLeft()
    {
        rb.velocity = new Vector2(-1f * 4f, rb.velocity.y);
        Debug.Log("Left");
        rb.transform.localScale = new Vector3(1f, 1f, 1f);
        //rb.transform.Translate(Vector3.left * 4f * Time.deltaTime);
    }
    private void MoveRight()
    {
        rb.velocity = new Vector2(1f * 4f, rb.velocity.y);
        Debug.Log("Left");
        rb.transform.localScale = new Vector3(-1f, 1f, 1f);
        //rb.transform.Translate(Vector3.right * 4f * Time.deltaTime);
    }
}
