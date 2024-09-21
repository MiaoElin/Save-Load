using UnityEngine;
using GameFunctions;
using System;
using System.Collections.Generic;

[GFBufferEncoderMessage]
public struct GameSaveMessage : IGFBufferEncoderMessage<GameSaveMessage>
{
    public RoleSaveMessage playerSave;
    public List<RoomSaveMessage> roomSaves;
    public void WriteTo(byte[] dst, ref int offset)
    {
        GFBufferEncoderWriter.WriteMessage(dst, playerSave, ref offset);
        GFBufferEncoderWriter.WriteMessageList(dst, roomSaves, ref offset);
    }

    public void FromBytes(byte[] src, ref int offset)
    {
        playerSave = GFBufferEncoderReader.ReadMessage(src, () => new RoleSaveMessage(), ref offset);
        roomSaves = GFBufferEncoderReader.ReadMessageList(src, () => new RoomSaveMessage(), ref offset);
    }
}