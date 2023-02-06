using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
public class UI_Condition : UI_Popup
{
    enum Texts
    {
        CottonCount,
        WoodCount,
        StoneCount,

        GoldUpText,
        DiaUpText
    }

    enum Buttons
    {
        CloseButton,
        GoldUp,
        DiaUp,
    }

    enum GameObjects
    {
        ConSetCotton,
        ConSetWood,
        ConSetStone,
        CottonPanel,
        WoodPanel,
        StonePanel,

        ConditionSet
    }
    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(Buttons));
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<GameObject>(typeof(GameObjects));

        // 재화 체크
        CheckGoods();
        SetFur();


        GetText((int)Texts.GoldUpText).text = Managers.Data.Sooms[1300 + Managers.Game.SaveData.SoomLevel + 1].Gold.ToString();
        GetText((int)Texts.DiaUpText).text = Managers.Data.Sooms[1300 + Managers.Game.SaveData.SoomLevel + 1].Diamond.ToString();
        GetButton((int)Buttons.CloseButton).gameObject.BindEvent(OnCloseButton);


        if (Managers.Game.SaveData.IsSoomUp && Managers.Game.SaveData.Dia >= Managers.Data.Sooms[1300 + Managers.Game.SaveData.SoomLevel + 1].Diamond)
        {
            GetButton((int)Buttons.DiaUp).gameObject.BindEvent(OnDiaUpgrade);
            GetButton((int)Buttons.DiaUp).image.color = Color.yellow;
        }
        else
        {
            GetButton((int)Buttons.DiaUp).interactable = false;
        }
        if (Managers.Game.SaveData.IsSoomUp)
        {
            GetButton((int)Buttons.GoldUp).gameObject.BindEvent(OnGoldUpgrdae);
            GetButton((int)Buttons.GoldUp).image.color = Color.yellow;
        }
        else
        {
            GetButton((int)Buttons.GoldUp).interactable = false;
        }
    }




    void SetFur()
    {
        GameObject gridPanel = Get<GameObject>((int)GameObjects.ConditionSet);

        int SpaceLevel = Managers.Game.SaveData.SpaceLevel;
        int SoomLevel = Managers.Game.SaveData.SoomLevel;

        int MaxCount = Managers.Data.Spaces[1200 + Managers.Data.Sooms[1300 + SoomLevel].Space_Num].Space_Furniture_Count;
        int StartSoomFur = 0;

        bool CurHave = false;
        bool CurRoom = false;

        //방조건
        foreach (Transform child in gridPanel.transform)
            Managers.Resource.Destroy(child.gameObject);

        GameObject Item1 = Managers.Resource.Instantiate("UI/UI_FurnitureCheckPanel");
        Item1.transform.SetParent(gridPanel.transform);
        UI_FurnitureCheckPanel RoomSet = Util.GetOrAddComponent<UI_FurnitureCheckPanel>(Item1);


        // 이따 수정할부분
        if (Managers.Game.SaveData.SpaceLevel == Managers.Data.Sooms[1300 + SoomLevel].Space_Num)

            if (SpaceLevel == Managers.Data.Sooms[1300 + SoomLevel].Space_Num)

                CurRoom = true;
            else
                CurRoom = false;
        RoomSet.SetInfo(Managers.Data.Spaces[1200 + Managers.Data.Sooms[1300 + SoomLevel].Space_Num].Space_Name, CurRoom);

        //가구조건
        for (int i = 1; i < Managers.Data.Sooms[1300 + SoomLevel].Space_Num; i++)
        {
            StartSoomFur += Managers.Data.Spaces[1200 + i].Space_Furniture_Count;
        }

        for (int i = 0; i < MaxCount; i++)
        {
            /*
            GameObject Item = Managers.UI.MakeSubItem<UI_FurnitureCheckPanel>().gameObject;
            Item.transform.SetParent(gridPanel.transform);
            UI_FurnitureCheckPanel FurSet = Util.GetOrAddComponent<UI_FurnitureCheckPanel>(Item);
            */
            GameObject Item = Managers.Resource.Instantiate("UI/UI_FurnitureCheckPanel");
            Item.transform.SetParent(gridPanel.transform);
            UI_FurnitureCheckPanel FurSet = Util.GetOrAddComponent<UI_FurnitureCheckPanel>(Item);
            for (int j = StartSoomFur; j < Managers.Game.SaveData.FList.Count; j++)
            {
                if (Managers.Game.SaveData.FList[j].F_Name == Managers.Data.Furnitures[1101 + i + StartSoomFur].F_Name)
                {
                    CurHave = true;
                    break;
                }
                else
                {
                    CurHave = false;
                }
            }
            FurSet.SetInfo(Managers.Data.Furnitures[1101 + i + StartSoomFur].F_Name, CurHave);

        }
    }


    void OnCloseButton(PointerEventData evt)
    {
        Managers.UI.CloseAllPopupUI();
    }
    void OnDiaUpgrade(PointerEventData evt)
    {
        Managers.UI.ShowPopupUI<UI_DiaUp>();
    }
    void OnGoldUpgrdae(PointerEventData evt)
    {
        Managers.Object.SoomOpen.SomUpgrade();
        Managers.UI.CloseAllPopupUI();
    }
    void CheckGoods()
    {
        if (Managers.Data.Sooms[1300 + Managers.Game.SaveData.SoomLevel + 1].Cotton == 0)
            Managers.Resource.Destroy(GetObject((int)GameObjects.ConSetCotton));
        else
        {
            if (Managers.Data.Sooms[1300 + Managers.Game.SaveData.SoomLevel + 1].Cotton > Managers.Game.SaveData.Cotton)
                GetText((int)Texts.CottonCount).text = "<color=red>" + Managers.Game.SaveData.Cotton + "</color> / " + Managers.Data.Sooms[1300 + Managers.Game.SaveData.SoomLevel + 1].Cotton.ToString();
            else
            {
                GetObject((int)GameObjects.CottonPanel).GetComponent<Image>().color = Color.cyan;
                GetText((int)Texts.CottonCount).text = Managers.Game.SaveData.Cotton + " / " + Managers.Data.Sooms[1300 + Managers.Game.SaveData.SoomLevel + 1].Cotton.ToString();
            }
        }
        if (Managers.Data.Sooms[1300 + Managers.Game.SaveData.SoomLevel + 1].Wood == 0)
            Managers.Resource.Destroy(GetObject((int)GameObjects.ConSetWood));
        else
        {
            if (Managers.Data.Sooms[1300 + Managers.Game.SaveData.SoomLevel + 1].Wood > Managers.Game.SaveData.Wood)
                GetText((int)Texts.WoodCount).text = "<color=red>" + Managers.Game.SaveData.Wood + "</color> / " + Managers.Data.Sooms[1300 + Managers.Game.SaveData.SoomLevel + 1].Wood.ToString();
            else
            {
                GetObject((int)GameObjects.WoodPanel).GetComponent<Image>().color = Color.cyan;
                GetText((int)Texts.WoodCount).text = Managers.Game.SaveData.Wood + " / " + Managers.Data.Sooms[1300 + Managers.Game.SaveData.SoomLevel + 1].Wood.ToString();
            }
        }
        if (Managers.Data.Sooms[1300 + Managers.Game.SaveData.SoomLevel + 1].Stone == 0)
            Managers.Resource.Destroy(GetObject((int)GameObjects.ConSetStone));
        else
        {
            if (Managers.Data.Sooms[1300 + Managers.Game.SaveData.SoomLevel + 1].Stone > Managers.Game.SaveData.Cotton)
                GetText((int)Texts.StoneCount).text = "<color=red>" + Managers.Game.SaveData.Stone + "</color> / " + Managers.Data.Sooms[1300 + Managers.Game.SaveData.SoomLevel + 1].Stone.ToString();
            else
            {
                GetObject((int)GameObjects.StonePanel).GetComponent<Image>().color = Color.cyan;
                GetText((int)Texts.StoneCount).text = Managers.Game.SaveData.Stone + " / " + Managers.Data.Sooms[1300 + Managers.Game.SaveData.SoomLevel + 1].Stone.ToString();
            }
        }
    }
}
