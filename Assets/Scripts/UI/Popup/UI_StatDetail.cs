using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
public class UI_StatDetail : UI_Popup
{
    private string[] CatName = { "White", "Black", "Calico", "Tabby", "Gray" };
    private string[] CatFood = { "chew" , "mackerel", "jerky", "tunacan", "salmon" };
    int Index;
    GameObject HaveGo, NotHaveGo;
    TextMeshProUGUI HaveText , NotHaveText, HaveDes, NotHaveDes ,HaveSkil, NotHaveSkill , HaveFoodName, CatPrice;
    Image HaveImage, NotHaveImage , HaveSkillImage, HaveFoodImage, DiaGold;
    Button Buy;

    private int HappyLevel;
    enum GameObjects
    {
        NotHavePanel,
        HavePanel,
        Skill,
        HeartSet,
    }

    enum Buttons
    {
        RightButton,
        LeftButton,
        CloseButton,
        BuyButton

    }
    enum Texts
    {
        NotHaveName,
        HaveName,
        NotHaveCatDesc,
        HaveCatDesc,
        HaveSkillText,
        NotHaveSkillText,
        HaveFoodName,
        Price,
        HappyLevel,
        FeelText,
        NeedExp

    }

    enum Images
    {
        NotHaveCatImage,
        HaveCatImage,
        HaveSkillImage,
        HaveFoodImage,
        DiaOrGold
    }
    void Start()
    {
        Init();
    }
    public override void Init()
    {
        base.Init();
        Bind<Button>(typeof(Buttons));
        Bind<GameObject>(typeof(GameObjects));
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<Image>(typeof(Images));

        SetGet();
        if (Managers.Game.SaveData.CatHave[Index])
        {
            SetHave(Index);
        }
        else
        {
            SetNotHave(Index);
        }

        GetButton((int)Buttons.RightButton).gameObject.BindEvent(RightButtonIndex);
        GetButton((int)Buttons.LeftButton).gameObject.BindEvent(LeftButtonIndex);
        GetButton((int)Buttons.CloseButton).gameObject.BindEvent(OnCloseButton);
        Buy.gameObject.BindEvent(BuyCat);
    }

    void SetGet()
    {
        HaveGo = GetObject((int)GameObjects.HavePanel);
        NotHaveGo = GetObject((int)GameObjects.NotHavePanel);
        HaveText = GetText((int)Texts.HaveName);
        NotHaveText = GetText((int)Texts.NotHaveName);
        HaveImage = GetImage((int)Images.HaveCatImage);
        NotHaveImage = GetImage((int)Images.NotHaveCatImage);
        HaveDes = GetText((int)Texts.HaveCatDesc);
        NotHaveDes = GetText((int)Texts.NotHaveCatDesc);
        HaveSkil = GetText((int)Texts.HaveSkillText);
        NotHaveSkill = GetText((int)Texts.NotHaveSkillText);
        HaveSkillImage = GetImage((int)Images.HaveSkillImage);
        HaveFoodName = GetText((int)Texts.HaveFoodName);
        HaveFoodImage = GetImage((int)Images.HaveFoodImage);
        DiaGold = GetImage((int)Images.DiaOrGold);
        CatPrice = GetText((int)Texts.Price);
        Buy = GetButton((int)Buttons.BuyButton);
    }
    private void SetHave(int _index)
    {
        HaveGo.SetActive(true);
        NotHaveGo.SetActive(false);

        //��������
        HaveText.text = Managers.Data.CatBooks[1401 + _index].Cat_Name;
        HaveImage.sprite = Managers.Resource.Load<Sprite>("Sprites/Nyan/" + CatName[Index] + "/" + CatName[Index] + "_Walk1");
        HaveDes.text = Managers.Data.CatBooks[1401 + _index].Cat_Desc;
        if (Index != 0)
        {
            HaveSkil.text = Managers.Data.CatBooks[1401 + _index].Cat_Skill_Name;
            HaveSkillImage.sprite = Managers.Resource.Load<Sprite>("");
        }
        else
        {
            HaveSkil.text = "";
            HaveSkillImage.sprite = null;
        }
        HaveFoodName.text = Managers.Data.Shops[Managers.Data.CatBooks[1401 + _index].Cat_Favor_Food].Shop_Name;
        HaveFoodImage.sprite = Managers.Resource.Load<Sprite>("Sprites/UI/Bag/" + CatFood[Index]);
        HappyLevel = Managers.Game.SaveData.CatHappinessLevel[_index];
        GetText((int)Texts.NeedExp).text = "���� ���� ���� : " + (Managers.Data.Happinesses[1800 + _index * 5 + HappyLevel + 1].H_Max - Managers.Game.SaveData.CatCurHappinessExp[_index]).ToString();
        GetText((int)Texts.HappyLevel).text = "�ູ�� ���� : " +  HappyLevel.ToString();
        GetText((int)Texts.FeelText).text = " ���� ���� : ";//�����߰�
        SetHappiness();

    }
    private void SetNotHave(int _index)
    {
        HaveGo.SetActive(false);
        NotHaveGo.SetActive(true);

        //��������
        NotHaveText.text = Managers.Data.CatBooks[1401 + _index].Cat_Name;
        NotHaveImage.sprite = Managers.Resource.Load<Sprite>("Sprites/Nyan/" + CatName[Index] + "/" + CatName[Index] + "_Walk1");
        NotHaveDes.text = Managers.Data.CatBooks[1401 + _index].Cat_Desc;
        NotHaveSkill.text = "Ư�� : " + Managers.Data.CatBooks[1401 + _index].Cat_Skill_Name;
        
        if(Managers.Data.CatBooks[1401+Index].Diamond >0)
        {
            DiaGold.sprite = Managers.Resource.Load<Sprite>("Sprites/UI/Diamond");
            CatPrice.text = Managers.Data.CatBooks[1401 + Index].Diamond.ToString();
            if (Managers.Data.CatBooks[1401 + Index].Diamond > Managers.Game.SaveData.Dia)
            {
                Buy.interactable = false;
            }
        }    
        else if(Managers.Data.CatBooks[1401 + Index].Gold > 0)
        {
            DiaGold.sprite = Managers.Resource.Load<Sprite>("Sprites/UI/Gold");
            CatPrice.text = Managers.Data.CatBooks[1401 + Index].Gold.ToString();
            if (Managers.Data.CatBooks[1401 + Index].Gold > Managers.Game.SaveData.Dia)
            {
                Buy.interactable = false;
            }
        }

    }
    public void SetInfo(int _index)
    {
        Index = _index;
    }
    void RightButtonIndex(PointerEventData evt)
    {
        Index++;
        if(Index == Managers.Game.SaveData.CatHave.Length)
        {
            Index = 0;
        }

        if(Managers.Game.SaveData.CatHave[Index])
        {
            SetHave(Index);
        }
        else
        {
            SetNotHave(Index);
        }
    }
    void LeftButtonIndex(PointerEventData evt)
    {
        Index--;
        if (Index == -1)
        {
            Index = Managers.Game.SaveData.CatHave.Length-1;
        }

        if (Managers.Game.SaveData.CatHave[Index])
        {
            SetHave(Index);
        }
        else
        {
            SetNotHave(Index);
        }
    }

    void BuyCat(PointerEventData evt)
    {
        //������ �����߰�
        Managers.Game.SaveData.CatHave[Index] = true;
        Managers.UI.CloseAllPopupUI();
    }

    void SetHappiness()
    {
        GameObject gridPanel = Get<GameObject>((int)GameObjects.HeartSet);

        foreach (Transform child in gridPanel.transform)
            Managers.Resource.Destroy(child.gameObject);

        for (int i = 0; i < HappyLevel; i++)
        {
            GameObject Item = Managers.Resource.Instantiate("UI/UI_HeartBook");
            Item.transform.SetParent(gridPanel.transform);
            UI_HeartSet HerartSet = Util.GetOrAddComponent<UI_HeartSet>(Item);
            HerartSet.SetInfo(1, 1);
        }
        for (int i = HappyLevel; i < HappyLevel + 1; i++)
        {
            GameObject Item = Managers.Resource.Instantiate("UI/UI_HeartBook");
            Item.transform.SetParent(gridPanel.transform);
            UI_HeartSet HerartSet = Util.GetOrAddComponent<UI_HeartSet>(Item);
            HerartSet.SetInfo(Managers.Game.SaveData.CatCurHappinessExp[Index], Managers.Data.Happinesses[1800 + Index * 5 + HappyLevel + 1].H_Max);
        }

        for (int i = HappyLevel + 1; i < 5; i++)
        {
            GameObject Item = Managers.Resource.Instantiate("UI/UI_HeartBook");
            Item.transform.SetParent(gridPanel.transform);
            UI_HeartSet HerartSet = Util.GetOrAddComponent<UI_HeartSet>(Item);
            HerartSet.SetInfo(0, 1);
        }
    }
    void OnCloseButton(PointerEventData evt)
    {
        Managers.UI.ClosePopupUI();
    }
}