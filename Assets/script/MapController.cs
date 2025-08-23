using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MapController : MonoBehaviour
{
    public static MapController instance;
    [SerializeField] List<GameObject> floorList;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    private void Start()
    {
        SetCurrentFloor(0);
    }
    public void SetCurrentFloor(int floor)
    {
        for (int i = 0; i < floorList.Count; i++)
        {
            floorList[i].SetActive(i == floor);
        }
    }
}