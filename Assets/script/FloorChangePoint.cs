using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FloorChangePoint : MonoBehaviour
{
    [SerializeField] int floorNum;
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            MapController.instance.SetCurrentFloor(floorNum);
            switch (floorNum)
            {
                case 0:
                    collision.transform.GetComponent<Player>().SetZPos(2f);
                    break;
                case 1:
                case 2:
                    collision.transform.GetComponent<Player>().SetZPos(8f);
                    break;
            }
        }
    }
}
