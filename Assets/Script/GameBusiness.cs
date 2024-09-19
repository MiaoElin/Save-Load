using UnityEngine;
using System.IO;

public static class GameBusiness {
    public static void NewGame(GameContext ctx) {

    }

    public static void SaveGame(GameContext ctx) {
        // role pos
        Vector2 pos = ctx.role.transform.position;

        // 存储坐标
    }

    public static void LoadGame(GameContext ctx) {

    }

}