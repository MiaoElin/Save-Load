using UnityEngine;
using System.Collections.Generic;

public class GameContext {
    public RoleEntity player;
    public RoleEntity rolePrefab;

    public Dictionary<int, RoomEntity> rooms;

    public GameContext() {
        rooms = new Dictionary<int, RoomEntity>();
    }
}