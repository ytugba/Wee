using UnityEngine;

public class Groups : MonoBehaviour
{
    // Time since last gravity tick
    float lastFall = 0;
    int fallSpeed;
    public static bool isOver;

    public Transform player; // Drag your player here
    private Vector2 fp; // first finger position
    private Vector2 lp; // last finger position

    private float angle;
    private float swipeDistanceX;
    private float swipeDistanceY;

    bool isValidGridPos()
    {
        foreach (Transform child in transform)
        {
            Vector2 v = SmashPlayfield.roundVec2(child.position);

            // Not inside Border?
            if (!SmashPlayfield.insideBorder(v))
                return false;

            // Block in grid cell (and not part of same group)?
            if (SmashPlayfield.grid[(int)v.x, (int)v.y] != null &&
                SmashPlayfield.grid[(int)v.x, (int)v.y].parent != transform)
                return false;
        }
        return true;
    }

    void UpdateGrid()
    {
        // Remove old children from grid
        for (int y = 0; y < SmashPlayfield.h; ++y)
            for (int x = 0; x < SmashPlayfield.w; ++x)
                if (SmashPlayfield.grid[x, y] != null)
                    if (SmashPlayfield.grid[x, y].parent == transform)
                        SmashPlayfield.grid[x, y] = null;

        // Add new children to grid
        foreach (Transform child in transform)
        {
            Vector2 v = SmashPlayfield.roundVec2(child.position);
            SmashPlayfield.grid[(int)v.x, (int)v.y] = child;
        }
    }

    void Update()
    {
        if (Time.time - lastFall >= 1)
        {
            lastFall = Time.time;
            // Modify position
            transform.position += new Vector3(0, -fallSpeed, 0);

            // See if valid
            if (isValidGridPos())
            {
                // It's valid. Update grid.
                UpdateGrid();
            }
            else
            {
                // It's not valid. revert.
                transform.position += new Vector3(0, fallSpeed, 0);

                // Clear filled horizontal lines
                SmashPlayfield.deleteFullRows();

                // Spawn next Group
                FindObjectOfType<Spawner>().spawnNext();

                // Disable script
                enabled = false;
            }

            lastFall = Time.time;
        }
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                fp = Input.GetTouch(0).position;
                lp = Input.GetTouch(0).position;
            }

            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                lp = Input.GetTouch(0).position;
                swipeDistanceX = Mathf.Abs((lp.x - fp.x));
                swipeDistanceY = Mathf.Abs((lp.y - fp.y));
            }
            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                angle = Mathf.Atan2((lp.x - fp.x), (lp.y - fp.y)) * 57.2957795f;
                if (lp == fp || (swipeDistanceX < 10 && swipeDistanceY < 10))
                {
                    transform.Rotate(0, 0, -90);

                    // See if valid
                    if (isValidGridPos())
                        // It's valid. Update grid.
                        UpdateGrid();
                    else
                    {
                        // It's not valid. revert.
                        transform.Rotate(0, 0, 90);
                    }
                }

                if (angle > 60 && angle < 120 && swipeDistanceX > 40)
                {
                    //Debug.Log("right swipe...");
                    // Modify position
                    transform.position += new Vector3(1, 0, 0);
                    // See if valid
                    if (isValidGridPos())
                        // It's valid. Update grid.
                        UpdateGrid();
                    else
                        // It's not valid. revert.
                        transform.position += new Vector3(-1, 0, 0);
                }
                if ((angle > 150 || angle < -150 && swipeDistanceY > 40))
                {

                    for(int i = 0; i < 5; i++)
                    {
                        //Debug.Log("down  swipe...");
                        // Modify position
                        transform.position += new Vector3(0, -fallSpeed, 0);

                        // See if valid
                        if (isValidGridPos())
                        {
                            // It's valid. Update grid.
                            UpdateGrid();
                        }
                        else
                        {
                            // It's not valid. revert.
                            transform.position += new Vector3(0, fallSpeed, 0);
                        }
                    }
                    if (isValidGridPos())
                    {
                        // It's valid. Update grid.
                        UpdateGrid();
                    }
                    else
                    {
                        // Clear filled horizontal lines
                        SmashPlayfield.deleteFullRows();

                        // Spawn next Group
                        FindObjectOfType<Spawner>().spawnNext();

                        // Disable script
                        enabled = false;
                    }
                }
                if (angle < -60 && angle > -120 && swipeDistanceX > 40)
                {
                    //Debug.Log("left  swipe...");
                    transform.position += new Vector3(-1, 0, 0);
                    // See if valid
                    if (isValidGridPos())
                        // It's valid. Update grid.
                        UpdateGrid();
                    else
                        // It's not valid. revert.
                        transform.position += new Vector3(1, 0, 0);
                }
            }
        }    
    }

    void Start()
    {
        fallSpeed = 1;
        // Default position not valid? Then it's game over
        if (!isValidGridPos())
        {
            isOver = true;
            Destroy(gameObject);
        }
    }
}