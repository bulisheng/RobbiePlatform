using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public GameObject upDoor, downDoor, leftDoor, rightDoor;

    public bool isUpDoor, isDownDoor, isLeftDoor, isRightDoor;

    public int stepToStart;//距离初始点的网格距离

    public int doorNumber;//当前房间的门的数量/入口数量

    public int goldnumber;

    public GameObject gold;
    List<GameObject> games = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        upDoor.SetActive(isUpDoor);
        downDoor.SetActive(isDownDoor);
        leftDoor.SetActive(isLeftDoor);
        rightDoor.SetActive(isRightDoor);

    }
    public void GeneratGoldCOINS()
    {
        int number = Random.Range(0, 10);
        for (int i = 0; i < number; i++)
        {
            GameObject glodobj = Instantiate(gold, this.transform.position, Quaternion.identity, this.transform);
            glodobj.transform.localPosition = new Vector3(Random.Range(-7, 7), Random.Range(-3, 3), 0);
            games.Add(glodobj);
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
    public void SetDoorStart(GameObject game)
    {
        games.Remove(game);
        if (games.Count < 1)
        {
            upDoor.SetActive(false);
            downDoor.SetActive(false);
            leftDoor.SetActive(false);
            rightDoor.SetActive(false);
        }
        Debug.Log(games.Count);

    }
    public void SetDoorClose()
    {
        upDoor.SetActive(false);
        downDoor.SetActive(false);
        leftDoor.SetActive(false);
        rightDoor.SetActive(false);
    }
    public void SetDoorOpen()
    {
        upDoor.SetActive(true);
        downDoor.SetActive(true);
        leftDoor.SetActive(true);
        rightDoor.SetActive(true);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CameraControl.instance.ChangerTarget(transform);
        }
    }
    public void UpdateRoom(float xOffect, float yOffect)
    {
        //计算距离初始点的网格距离
        stepToStart = (int)(Mathf.Abs(transform.position.x / xOffect) + Mathf.Abs(transform.position.y / yOffect));



        if (isUpDoor)
            doorNumber++;
        if (isDownDoor)
            doorNumber++;
        if (isLeftDoor)
            doorNumber++;
        if (isRightDoor)
            doorNumber++;
    }

}
