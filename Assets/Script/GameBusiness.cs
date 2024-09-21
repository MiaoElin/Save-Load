using UnityEngine;
using System.IO;
using System.Text;
using GameFunctions;
using System.Collections.Generic;

public static class GameBusiness {
    public static void NewGame(GameContext ctx) {

        // 生成房间以及里面的角色
        RoomEntity room1 = GameBusiness.Create_Room(ctx);
        RoomEntity room2 = GameBusiness.Create_Room(ctx);
        ctx.rooms.Add(room1.id, room1);
        ctx.rooms.Add(room2.id, room2);
    }

    public static void SaveGame(GameContext ctx) {
        // role pos
        Vector2 pos = ctx.player.transform.position;

        // 存储坐标
        // 方法1：用字符串存储
        // SaveType1(ctx, pos);
        // 方法2：用二进制存储
        // SaveType2(ctx, pos);
        // 方法3：
        SaveType3(ctx);

    }

    public static void LoadGame(GameContext ctx) {
        // 方法1
        // LoadType1(ctx);
        // 方法2
        // LoadType2(ctx);
        // 方法3
        LoadType3(ctx);
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

    #region Type3
    public static void SaveType3(GameContext ctx) {
        byte[] buffer = new byte[1024];
        int index = 4;
        var rooms = ctx.rooms.Values;
        foreach (var room in rooms) {
            RoomSaveMessage roomMessage = new RoomSaveMessage();
            roomMessage.id = room.id;
            roomMessage.pos = room.GetPos();
            var roles = room.roles;
            roomMessage.roleSaves = new List<RoleSaveMessage>(room.roles.Count);
            foreach (var role in roles) {
                RoleSaveMessage roleMessage = new RoleSaveMessage();
                roleMessage.id = role.id;
                roleMessage.pos = role.GetPos();
                roomMessage.roleSaves.Add(roleMessage);
            }
            roomMessage.WriteTo(buffer, ref index);
        }

        int length = index;
        index = 0;
        GFBufferEncoderWriter.WriteUInt32(buffer, (uint)length, ref index);
        using (FileStream fs = new FileStream("Slot1.save", FileMode.Create)) {
            fs.Write(buffer, 0, length);
        }
    }

    public static void LoadType3(GameContext ctx) {
        byte[] buffer = File.ReadAllBytes("Slot1.save");
        int index = 0;
        int length = (int)GFBufferEncoderReader.ReadUInt32(buffer, ref index);
        while (index < length) {
            RoomSaveMessage roomMessage = new RoomSaveMessage();
            roomMessage.FromBytes(buffer, ref index);
            RoomEntity room = new GameObject("Room").AddComponent<RoomEntity>();
            room.id = roomMessage.id;
            room.SetPos(roomMessage.pos);
            var roleSaves = roomMessage.roleSaves;
            foreach (var roleSave in roleSaves) {
                RoleEntity role = GameObject.Instantiate(ctx.rolePrefab);
                role.id = roleSave.id;
                role.SetPos(roleSave.pos);
                room.roles.Add(role);
            }
        }
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