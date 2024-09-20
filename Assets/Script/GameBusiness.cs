using UnityEngine;
using System.IO;
using System.Text;
using GameFunctions;
using System.Collections.Generic;

public static class GameBusiness {
    public static void NewGame(GameContext ctx) {

    }

    public static void SaveGame(GameContext ctx) {
        // role pos
        Vector2 pos = ctx.player.transform.position;

        // 存储坐标
        // 方法1：用字符串存储
        // SaveType1(ctx, pos);
        // 方法2：用二进制存储
        SaveType2(ctx, pos);
    }

    public static void LoadGame(GameContext ctx) {
        // 方法1
        // LoadType1(ctx);
        // 方法2
        LoadType2(ctx);
    }

    #region Type1
    public static void SaveType1(GameContext ctx, Vector2 pos) {
        // 存储坐标
        StringBuilder s = new StringBuilder();
        s.Append(pos.x.ToString() + "\r\n");
        s.Append(pos.y.ToString());
        Debug.Log("save");
        File.WriteAllText("Slot1.save", s.ToString());
    }

    public static void LoadType1(GameContext ctx) {
        string[] lines = File.ReadAllLines("Slot1.save");
        var role = ctx.player;
        role.SetPos(new Vector2(float.Parse(lines[0]), float.Parse(lines[1])));
    }

    #endregion

    #region Type2
    public static void SaveType2(GameContext ctx, Vector2 pos) {
        byte[] buffer = new byte[1024];
        int index = 2;
        GFBufferEncoderWriter.WriteSingle(buffer, pos.x, ref index);
        GFBufferEncoderWriter.WriteSingle(buffer, pos.y, ref index);
        int length = index;
        index = 0;
        GFBufferEncoderWriter.WriteUInt16(buffer, (ushort)length, ref index);
        using (FileStream fs = new FileStream("Slot1.save", FileMode.Create)) {
            fs.Write(buffer, 0, length);
        }
    }

    public static void LoadType2(GameContext ctx) {
        byte[] buffer = File.ReadAllBytes("Slot1.save");
        int index = 0;
        ushort length = GFBufferEncoderReader.ReadUInt16(buffer, ref index);
        Vector2 pos = new Vector2();
        pos.x = GFBufferEncoderReader.ReadSingle(buffer, ref index);
        pos.y = GFBufferEncoderReader.ReadSingle(buffer, ref index);
        ctx.player.SetPos(pos);
    }

    #endregion
    #region Create_Room
    static int roomRecord = 0;
    public static RoomEntity Create_Room(GameContext ctx) {
        RoomEntity room = new GameObject("room").AddComponent<RoomEntity>();
        room.id = roomRecord++;
        room.SetPos(UnityEngine.Random.insideUnitCircle * 5);
        room.roles = new List<RoleEntity>();
        for (int i = 0; i < Random.Range(1, 5); i++) {
            RoleEntity role = GameObject.Instantiate(ctx.rolePrefab);
            role.SetPos(room.GetPos() + UnityEngine.Random.insideUnitCircle * 2);
            role.id = i;
            room.roles.Add(role);
        }
        return room;
    }
    #endregion


}