using System.Collections.Generic;
using GameFunctions;
using UnityEngine;
using System;

[GFBufferEncoderMessage]
public struct RoomSaveMessage : IGFBufferEncoderMessage<RoomSaveMessage>
{
    public int id;
    public Vector2 pos;
    public List<RoleSaveMessage> roleSaves;
    public void WriteTo(byte[] dst, ref int offset)
    {
        GFBufferEncoderWriter.WriteInt32(dst, id, ref offset);
        GFBufferEncoderWriter.WriteVector2(dst, pos, ref offset);
        GFBufferEncoderWriter.WriteMessageList(dst, roleSaves, ref offset);
    }

    public void FromBytes(byte[] src, ref int offset)
    {
        id = GFBufferEncoderReader.ReadInt32(src, ref offset);
        pos = GFBufferEncoderReader.ReadVector2(src, ref offset);
        roleSaves = GFBufferEncoderReader.ReadMessageList(src, () => new RoleSaveMessage(), ref offset);
    }
}