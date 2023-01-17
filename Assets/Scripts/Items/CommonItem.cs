using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CommonItem : Item
{
    [SerializeField]
    ItemData itemData;

    ItemGenerator ig;

    public bool isPull;
    Transform _target;

    private void Start()
    {
        _target = Managers.Object.Player.GetComponent<Transform>();
        ig = transform.parent.GetComponent<ItemGenerator>();
    }

    void Update()
    {
        if(isPull)
        {
            transform.position = Vector2.MoveTowards(transform.position, _target.position, Time.deltaTime * 2f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            ig.isActive = true;
            Managers.Object.Player.Stat.Gold += itemData.Gold * Managers.Object.Player.Stat.Stage;
            Managers.Object.Player.Stat.Exp += itemData.Exp + (Managers.Object.Player.Stat.Stage - 1);

            (Managers.UI.SceneUI as UI_GameScene).UpdateGoldText();
            gameObject.SetActive(false);
        }
    }
}
