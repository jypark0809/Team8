using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : Item
{
    [SerializeField]
    int value = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Managers.Sound.Play(Define.Sound.Effect, "Effects/GetItem", volume: 0.4f);

            Managers.Object.Player.Stat.Hp += value;
            gameObject.SetActive(false);
        }
    }
}
