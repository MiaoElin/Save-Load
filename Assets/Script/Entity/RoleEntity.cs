using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleEntity : MonoBehaviour {
    Vector2 moveAxis;
    [SerializeField] Rigidbody2D rb;
    bool isOnGround;
    public int id;
    public bool isPlayer;
    public void SetPos(Vector2 pos) {
        transform.position = pos;
    }

    void Update() {
        if (isPlayer) {
            Move();
            Falling();
            Jump();
            Check_Ground();
        }
    }

    void Move() {
        moveAxis = Vector2.zero;
        if (Input.GetKey(KeyCode.A)) {
            moveAxis.x += -1;
        } else if (Input.GetKey(KeyCode.D)) {
            moveAxis.x += 1;
        }

        var velocity = rb.velocity;
        velocity = moveAxis * 5;
        velocity.y = rb.velocity.y;
        rb.velocity = velocity;
    }

    void Check_Ground() {
        if (rb.velocity.y <= 0) {
            LayerMask ground = 1 << 3;
            RaycastHit2D[] has = Physics2D.RaycastAll(transform.position, Vector2.down, 1.1f, ground);
            if (has.Length > 0) {
                isOnGround = true;
            } else {
                isOnGround = false;
            }
        } else {
            isOnGround = false;
        }
    }

    void Jump() {
        if (isOnGround && Input.GetKeyDown(KeyCode.Space)) {
            var velocity = rb.velocity;
            velocity.y = 8;
            rb.velocity = velocity;
        }
    }

    void Falling() {
        var velocity = rb.velocity;
        velocity.y += -9.8f * Time.deltaTime;
        rb.velocity = velocity;
    }
}
