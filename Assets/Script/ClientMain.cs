using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClientMain : MonoBehaviour {
    [SerializeField] Panel_Login panel_Login;
    [SerializeField] RoleEntity player;
    [SerializeField] RoleEntity rolePrefab;
    [SerializeField] Button btn_Save;
    GameContext ctx;
    void Start() {
        ctx = new GameContext();
        player.isPlayer = true;
        ctx.player = player;
        ctx.rolePrefab = rolePrefab;
        // Panel_Login
        panel_Login.onclickNewGameHandle = () => {
            GameBusiness.NewGame(ctx);
            panel_Login.Hide();
        };

        panel_Login.onClickLoadHandle = () => {
            GameBusiness.LoadGame(ctx);
            panel_Login.Hide();
        };
        panel_Login.Ctor();

        // btn_Save
        btn_Save.gameObject.SetActive(false);
        btn_Save.onClick.AddListener(() => {
            GameBusiness.SaveGame(ctx);
            btn_Save.gameObject.SetActive(false);
        });

        // 生成房间以及里面的角色
        RoomEntity room1 = GameBusiness.Create_Room(ctx);
        RoomEntity room2 = GameBusiness.Create_Room(ctx);
        ctx.rooms.Add(room1.id, room1);
        ctx.rooms.Add(room2.id, room2);
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.P)) {
            btn_Save.gameObject.SetActive(true);
        }
    }
}
