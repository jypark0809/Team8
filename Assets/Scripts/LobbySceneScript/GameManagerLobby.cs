using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerLobby : MonoBehaviour
{
    public GameObject[] Room;
    Camera Camera;
    public int current_room = 0;


    private void Start()
    {
        Camera = GetComponent<Camera>();
    }
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector3 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Debug.DrawRay(mousepos, ray.direction * 100.0f, Color.red, 1.0f);

            if (Physics.Raycast(ray, out hit, LayerMask.GetMask("Cat")))
            {
                Debug.Log("asd");
                Managers.Resource.Instantiate("UI/Popup/Room_UI");
                //hit.collider.gameObject.transform.position = new Vector3(mousepos.x,mousepos.y,0);
            }
        }

    }
}