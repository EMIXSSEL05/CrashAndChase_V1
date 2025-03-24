using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleType : MonoBehaviour
{
    [Header("General Properties")]
    public float speedLeft = 5;
    [SerializeField] float upNDownSpeed;

    [Header("Left Movement")]
    [SerializeField] bool isLeftMovement;

    [Header("Up Movement")]
    [SerializeField] bool isUpMovement;
    [SerializeField] float minYLimit;
    [SerializeField] float maxYLimit;

    public float limitY;
    private PlayerController playerControllerScript;

    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        limitY = Random.Range(minYLimit, maxYLimit);
    }

    void Update()
    {

        if (isLeftMovement && playerControllerScript.isAlive)
        {
            MoveLeft();
        }
        else if (isUpMovement && playerControllerScript.isAlive)
        {
            MoveLeftUp();
        }

        if (transform.position.x < -10)
        {
            Destroy(gameObject);
        }
    }

    void MoveLeft()
    {
        transform.Translate(Vector2.left * speedLeft * Time.deltaTime);
    }

    void MoveLeftUp()
    {
        MoveLeft();
        if (transform.position.y < limitY)
        {
            transform.Translate(Vector2.up * upNDownSpeed * Time.deltaTime);
        }
    }
}

