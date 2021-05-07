using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class RoomGenerator : MonoBehaviour
{
    public enum Diecetion { up, dowm, left, right };
    public Diecetion direction;
    [Header("房间信息")]
    public GameObject roomPrefab;
    public int roomNumber;
    public Color stareColor, endColor;

    public GameObject endPrefab;

    [Header("位置控制")]
    public Transform generatorPoint;
    public float xOffset;
    public float yOffset;

    public LayerMask layer;
    public List<Room> rooms = new List<Room>();
    int maxStep;
    List<GameObject> farRooms = new List<GameObject>();
    List<GameObject> lessFarRooms = new List<GameObject>();
    List<GameObject> oneWayRooms = new List<GameObject>();

    public WallType wallType;
    // Start is called before the first frame update
    void Start()
    {
        ReadFile();
        for (int i = 0; i < roomNumber; i++)
        {
            rooms.Add(Instantiate(roomPrefab, generatorPoint.position, Quaternion.identity).GetComponent<Room>());

            //改变point位置
            ChangerPointPos();
        }
        rooms[0].GetComponent<SpriteRenderer>().color = stareColor;
        endPrefab = rooms[0].gameObject;
        foreach (var room in rooms)
        {
            SetupRoom(room, room.transform.position);

            //if (room.transform.position.sqrMagnitude>endPrefab.transform.position.sqrMagnitude)
            //{
            //    endPrefab = room.gameObject;
            //}
        }
        FindEndRoom();
        endPrefab.GetComponent<SpriteRenderer>().color = endColor;
    }
    //读取txt内容
    void ReadFile()
    {
        //文件夹为与Assets并列对的文件夹，同在一个项目文件夹下。
        string FileName = Application.streamingAssetsPath + @"/rooms.txt";
        

        string[] strs = File.ReadAllLines(FileName);//读取文件的所有行，并将数据读取到定义好的字符数组strs中，一行存一个单元
        for (int i = 0; i < strs.Length; i++)
        {
            roomNumber = int.Parse(strs[i]);
            print(strs[i]);
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
    public void ChangerPointPos()
    {
        do
        {
            direction = (Diecetion)Random.Range(0, 4);
            switch (direction)
            {
                case Diecetion.up:
                    generatorPoint.position += new Vector3(0, yOffset, 0);
                    break;
                case Diecetion.dowm:
                    generatorPoint.position += new Vector3(0, -yOffset, 0);
                    break;
                case Diecetion.left:
                    generatorPoint.position += new Vector3(xOffset, 0, 0);
                    break;
                case Diecetion.right:
                    generatorPoint.position += new Vector3(-xOffset, 0, 0);

                    break;
                default:
                    break;
            }
        }
        while (Physics2D.OverlapCircle(generatorPoint.position, 0.2f, layer));
    }
    public void SetupRoom(Room newRoom, Vector3 roomPosition)
    {
        newRoom.isUpDoor = Physics2D.OverlapCircle(roomPosition + new Vector3(0, yOffset, 0), 0.2f, layer);
        newRoom.isDownDoor = Physics2D.OverlapCircle(roomPosition + new Vector3(0, -yOffset, 0), 0.2f, layer);
        newRoom.isLeftDoor = Physics2D.OverlapCircle(roomPosition + new Vector3(-xOffset, 0, 0), 0.2f, layer);
        newRoom.isRightDoor = Physics2D.OverlapCircle(roomPosition + new Vector3(xOffset, 0, 0), 0.2f, layer);

        newRoom.UpdateRoom(xOffset,yOffset);//参考上面Room脚本的内容
        newRoom.GeneratGoldCOINS();
        switch (newRoom.doorNumber)
        {
            case 1:
                if (newRoom.isUpDoor)
                    Instantiate(wallType.singleUp, roomPosition, Quaternion.identity);
                if (newRoom.isDownDoor)
                    Instantiate(wallType.singleBottom, roomPosition, Quaternion.identity);
                if (newRoom.isLeftDoor)
                    Instantiate(wallType.singleLeft, roomPosition, Quaternion.identity);
                if (newRoom.isRightDoor)
                    Instantiate(wallType.singleRight, roomPosition, Quaternion.identity);
                break;
            case 2:
                if (newRoom.isLeftDoor && newRoom.isUpDoor)
                    Instantiate(wallType.doubleLU, roomPosition, Quaternion.identity);
                if (newRoom.isLeftDoor && newRoom.isRightDoor)
                    Instantiate(wallType.doubleLR, roomPosition, Quaternion.identity);
                if (newRoom.isLeftDoor && newRoom.isDownDoor)
                    Instantiate(wallType.doubleLB, roomPosition, Quaternion.identity);
                if (newRoom.isUpDoor && newRoom.isRightDoor)
                    Instantiate(wallType.doubleUR, roomPosition, Quaternion.identity);
                if (newRoom.isUpDoor && newRoom.isDownDoor)
                    Instantiate(wallType.doubleUB, roomPosition, Quaternion.identity);
                if (newRoom.isRightDoor && newRoom.isDownDoor)
                    Instantiate(wallType.doubleRB, roomPosition, Quaternion.identity);
                break;
            case 3:
                if (newRoom.isLeftDoor && newRoom.isUpDoor && newRoom.isRightDoor)
                    Instantiate(wallType.tripleLUR, roomPosition, Quaternion.identity);
                if (newRoom.isLeftDoor && newRoom.isRightDoor && newRoom.isDownDoor)
                    Instantiate(wallType.tripleLRB, roomPosition, Quaternion.identity);
                if (newRoom.isDownDoor && newRoom.isUpDoor && newRoom.isRightDoor)
                    Instantiate(wallType.tripleURB, roomPosition, Quaternion.identity);
                if (newRoom.isLeftDoor && newRoom.isUpDoor && newRoom.isDownDoor)
                    Instantiate(wallType.tripleLUB, roomPosition, Quaternion.identity);
                break;
            case 4:
                if (newRoom.isLeftDoor && newRoom.isUpDoor && newRoom.isRightDoor && newRoom.isDownDoor)
                    Instantiate(wallType.fourDoors, roomPosition, Quaternion.identity);
                break;
        }
    }

    public void FindEndRoom()
    {
        //最大数值 最远距离数字
        for (int i = 0; i < rooms.Count; i++)
        {
            if (rooms[i].stepToStart > maxStep)
                maxStep = rooms[i].stepToStart;
        }

        //获得最远房间和第二远
        foreach (var room in rooms)
        {
            if (room.stepToStart == maxStep)
                farRooms.Add(room.gameObject);
            if (room.stepToStart == maxStep - 1)
                lessFarRooms.Add(room.gameObject);
        }

        for (int i = 0; i < farRooms.Count; i++)
        {
            if (farRooms[i].GetComponent<Room>().doorNumber == 1)
                oneWayRooms.Add(farRooms[i]);//最远房间里的单侧门加入
        }

        for (int i = 0; i < lessFarRooms.Count; i++)
        {
            if (lessFarRooms[i].GetComponent<Room>().doorNumber == 1)
                oneWayRooms.Add(lessFarRooms[i]);//第二远远房间里的单侧门加入
        }

        if (oneWayRooms.Count != 0)
        {
            endPrefab = oneWayRooms[Random.Range(0, oneWayRooms.Count)];
        }
        else
        {
            endPrefab = farRooms[Random.Range(0, farRooms.Count)];
        }
    }
}
[System.Serializable]
public class WallType
{
    public GameObject singleLeft, singleRight, singleUp, singleBottom,
                      doubleLU, doubleLR, doubleLB, doubleUR, doubleUB, doubleRB,
                      tripleLUR, tripleLUB, tripleURB, tripleLRB,
                      fourDoors;
}