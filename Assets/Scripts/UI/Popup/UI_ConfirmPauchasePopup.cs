using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static Define;

public class UI_ConfirmPauchasePopup : UI_Popup
{
    enum PurchaseType
    {
        Furniture,
        Item
    }

    FurnitureData _fData;
    ShopItemData _iData;
    PurchaseType _purchaseType;

    enum Texts
    {
        ItemNameText,
        PriceText,
    }

    enum Buttons
    {
        PurchaseButton,
        CancleButton
    }

    void Awake()
    {
        Bind<Button>(typeof(Buttons));
        Bind<TextMeshProUGUI>(typeof(Texts));
    }

    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();
        //id = PlayerPrefs.GetInt("ItemId");
        //Managers.Data.Furnitures.TryGetValue(id, out fData);

        //GetText((int)Texts.ItemNameText).text = fData.F_Name;
        //GetText((int)Texts.PriceText).text = fData.F_Gold.ToString();

        GetButton((int)Buttons.PurchaseButton).gameObject.BindEvent(OnPurchaseButtonClicked);
        GetButton((int)Buttons.CancleButton).gameObject.BindEvent(CancleButtonClicked);
    }

    public void SetItemInfo(ShopItemData iData)
    {
        _iData = iData;
        _purchaseType = PurchaseType.Item;
        RefreshUI();
    }

    public void SetFurnitureInfo(FurnitureData fData)
    {
        _fData = fData;
        _purchaseType = PurchaseType.Furniture;
        RefreshUI();
    }

    void RefreshUI()
    {
        switch ((int)_purchaseType)
        {
            case (int)PurchaseType.Furniture:
                GetText((int)Texts.ItemNameText).text = _fData.F_Name;
                GetText((int)Texts.PriceText).text = _fData.F_Gold.ToString();
                break;
            case (int)PurchaseType.Item:
                GetText((int)Texts.ItemNameText).text = _iData.Shop_Name;
                GetText((int)Texts.PriceText).text = _iData.Value.ToString();
                break;
        }
    }

    void OnPurchaseButtonClicked(PointerEventData evt)
    {
        switch ((int)_purchaseType)
        {
            case (int)PurchaseType.Furniture:
                ArrangeFurniture();
                break;
            case (int)PurchaseType.Item:
                GetReward();
                break;
        }
    }

    void CancleButtonClicked(PointerEventData evt)
    {
        ClosePopupUI();
    }

    void ArrangeFurniture()
    {
        // Happiness Point

        // Gold
        // Managers.Game.SaveData.Gold -= fData.F_Gold;

        // Add furniture to fList of save data
        Managers.Game.SaveData.FList.Add(_fData);

        // arrange Furniture
        GameObject go = Managers.Resource.Instantiate($"Furniture/{_fData.F_Space_Num}/{_fData.F_Int_Name}");
        go.transform.position = new Vector2(go.transform.localPosition.x + 0.5f, go.transform.localPosition.y + 0.5f);

        // Save Data
        Managers.Game.SaveGame();

        // Close All Popup UI;
        Managers.UI.CloseAllPopupUI();
    }

    // ���� [Ĺ�ٻ���, ��, ������, ����, ��ġĵ, ����,]
    void GetReward()
    {
        switch(_iData.Shop_Id)
        {
            case 1601:
                Managers.Game.SaveData.Food[(int)Define.SnackType.Churu]++;
                break;
            case 1602:
                Managers.Game.SaveData.Food[(int)Define.SnackType.Mackerel]++;
                break;
            case 1603:
                Managers.Game.SaveData.Food[(int)Define.SnackType.Jerky]++;
                break;
            case 1604:
                Managers.Game.SaveData.Food[(int)Define.SnackType.Tuna]++;
                break;
            case 1605:
                Managers.Game.SaveData.Food[(int)Define.SnackType.Salmon]++;
                break;
            case 1606:
                Managers.Game.SaveData.Food[(int)Define.SnackType.CatnipCandy]++;
                break;
        }

        Managers.UI.ClosePopupUI();
    }
}