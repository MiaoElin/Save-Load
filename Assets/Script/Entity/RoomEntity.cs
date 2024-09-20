using UnityEngine;
using System.Collections.Generic;

public class RoomEntity : MonoBehaviour {

    public int id;
    public List<RoleEntity> roles;

    public void SetPos(Vector2 pos) {
        transform.position = pos;
    }

    public Vector2 GetPos() {
        return transform.position;
    }
}