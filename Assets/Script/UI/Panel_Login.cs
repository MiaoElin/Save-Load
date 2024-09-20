using UnityEngine;
using UnityEngine.UI;
using System;

public class Panel_Login : MonoBehaviour {
    [SerializeField] Button btn_NewGame;
    [SerializeField] Button btn_LoadGame;

    public Action onclickNewGameHandle;
    public Action onClickLoadHandle;

    public void Ctor() {
        btn_NewGame.onClick.AddListener(() => {
            onclickNewGameHandle.Invoke();
        });

        btn_LoadGame.onClick.AddListener(() => {
            onClickLoadHandle.Invoke();
        });
    }

    internal void Hide() {
        gameObject.SetActive(false);
    }
}