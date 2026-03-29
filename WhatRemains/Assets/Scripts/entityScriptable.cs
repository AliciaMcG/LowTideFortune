using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;

/// <summary>
/// Holds code for:
/// 
///   0 - symbol room
///   
///   1 to 5 - corresponding puzzle nums
///   6 - dining room
///   
///   7 - hallway
///   
///   8 - safe room near 4
///   9 - safe room near dining
///   10 - safe room near 2
///   
///   
/// </summary>

[CreateAssetMenu(fileName = "entityScriptable", menuName = "Scriptable Objects/entityScriptable")]
public class entityScriptable : ScriptableObject
{
    ///////////////////////////////////////////////////////////      VARS      ////////////////////////////////////////////////////////////////////////////////
    public int[] roomNumArray = new int[] { 1, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3, 4, 4, 4, 4, 4, 6, 6}; //probability

    public Dictionary<int, Transform> roomNumDict; //FIX almost certainly not gonna be a transform but a container

    public room[] entitysMap;

    public class room
    {
        public int roomNum;
        public HashSet<int> neighbouringRooms;

        public room(int roomNum)
        {
            this.roomNum = roomNum;
            neighbouringRooms = new HashSet<int>();

            switch (roomNum)             {
                case 0:
                    neighbouringRooms.Add(1);
                    break;
                case 1:
                    neighbouringRooms.Add(0);
                    neighbouringRooms.Add(7);
                    break;
                case 2:
                    neighbouringRooms.Add(3);
                    neighbouringRooms.Add(7);
                    neighbouringRooms.Add(10);
                    break;
                case 3:
                    neighbouringRooms.Add(2);
                    neighbouringRooms.Add(4);
                    break;
                case 4:
                    neighbouringRooms.Add(3);
                    neighbouringRooms.Add(8);
                    break;
                case 5:
                    neighbouringRooms.Add(8);
                    neighbouringRooms.Add(6);
                    break;
                case 6:
                    neighbouringRooms.Add(5);
                    neighbouringRooms.Add(7);
                    break;
                case 7:
                    neighbouringRooms.Add(1);
                    neighbouringRooms.Add(2);
                    neighbouringRooms.Add(6);
                    neighbouringRooms.Add(9);
                    break;
                case 8:
                    neighbouringRooms.Add(4);
                    neighbouringRooms.Add(5);
                    break;
                case 9:
                    neighbouringRooms.Add(7);
                    break;
                case 10:
                    neighbouringRooms.Add(2);
                    break;


            }
        }

    }


    ///////////////////////////////////////////////////////////      FUNCTIONS      ////////////////////////////////////////////////////////////////////////////////
    ///
    private void OnEnable()
    {
        //roomNumDict = new Dictionary<int, room>()
        //{
        //    {1,  entityBase.entity.entityScriptable.entitysMap[1] }, 


        //};
    }

}
