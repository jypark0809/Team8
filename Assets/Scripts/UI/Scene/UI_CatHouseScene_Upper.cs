using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UI_CatHouseScene_Upper : UI_Scene
{
    enum Texts
    {
        Current_Room_Text_Step,
        CoinMount,
        DiaMount,
        JellyMount,
    }

    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();
        Bind<TextMeshProUGUI>(typeof(Texts));

        GetText((int)Texts.JellyMount).text = "123 / 123";//123.ToString();
        GetText((int)Texts.DiaMount).text = 99999999.ToString();
        GetText((int)Texts.CoinMount).text  = 99999999.ToString();// 코인,젤리양 데이터 추가



    }

    private void Update()
    {
        //행도력 시스템 갱신추가
    }

    void CoinOpen()
    {
        //추후 이벤트 추가 (과금시스템)
    }
    void JellyOpen()
    {
        //추후 이벤트추가(충전 or 과금)
    }
}
