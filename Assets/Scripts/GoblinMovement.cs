using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class GoblinMovement : MonoBehaviour
{
    public GoblinSCO goblin;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(-goblin.SPD, rb.velocity.y);
    }
}
