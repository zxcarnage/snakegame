using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    private Vector2 direction =  Vector2.right;
    private List<Transform> segments;
    public Transform SegmentPrefab;
    private int tale;

    private void Start()
    {
        segments = new List<Transform>();
        segments.Add(this.transform);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W)){

            if (direction == Vector2.down)
            {
                direction = Vector2.down;
            }
            else
            {
                direction = Vector2.up;
                this.transform.eulerAngles = new Vector3 (0, 0, 0);
            }
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            if(direction == Vector2.right)
            {
                direction = Vector2.right;
            }
            else
            {
                direction = Vector2.left;
                this.transform.eulerAngles = new Vector3 (0, 0, 90);
            }
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            if (direction == Vector2.up)
            {
                direction = Vector2.up;
            }
            else
            {
                direction = Vector2.down;
                this.transform.eulerAngles = new Vector3(0, 0, 180);
            }
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            if (direction == Vector2.left)
            {
                direction = Vector2.left;
            }
            else
            {
                direction = Vector2.right;
                this.transform.eulerAngles = new Vector3(0, 0, 270);
            }
        }
    }

    private void FixedUpdate()
    {
        tale = segments.Count;
        if(segments.Count != 1) { 
            segments[tale - 1].transform.localScale = new Vector3(0.85f, 0.4f, 0.0f);
            segments[0].transform.localScale = new Vector3(1, 1, 1);
            segments[tale-2].transform.localScale = new Vector3(0.75f, 0.75f, 0.0f);
        }
       
        for(int i = segments.Count - 1; i > 0; i--)
        {
            segments[i].position = segments[i - 1].position;
        }

        this.transform.position = new Vector3(
            Mathf.Round(this.transform.position.x) + direction.x,
            Mathf.Round(this.transform.position.y) + direction.y,
            0.0f
            );
    }

    private void Grow()
    {
        Transform segment = Instantiate(this.SegmentPrefab);
        segment.position = segments[segments.Count - 1].position;

        segments.Add(segment);


    }

    private void Death()
    {
        for(int i = 1; i<segments.Count; i ++)
        {
            Destroy(segments[i].gameObject);
        }
        segments.Clear();
        segments.Add(this.transform);

        this.transform.position = new Vector3(0,0,0);
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Food")
        {
            Grow();
        }

        if (collision.tag == "Wall")
        {
            Death();
        }
        if (collision.tag == "SnakeChain")
        {
            Death();
        }
    }
}
