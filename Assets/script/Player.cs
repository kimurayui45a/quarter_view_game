using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Player : MonoBehaviour
{
    private readonly float speed = 0.03f;
    private void Update()
    {
        TilemapController.instance.CheckCloseDoor(transform.position);
    }
    public void MoveUp()
    {
        transform.position = new Vector3(transform.position.x + speed, transform.position.y + (speed / 2f), transform.position.z);
    }
    public void MoveDown()
    {
        transform.position = new Vector3(transform.position.x - speed, transform.position.y - (speed / 2f), transform.position.z);
    }
    public void MoveLeft()
    {
        transform.position = new Vector3(transform.position.x - speed, transform.position.y + (speed / 2f), transform.position.z);
    }
    public void MoveRight()
    {
        transform.position = new Vector3(transform.position.x + speed, transform.position.y - (speed / 2f), transform.position.z);
    }


    public void SetZPos(float zPos)
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, zPos);
    }
}