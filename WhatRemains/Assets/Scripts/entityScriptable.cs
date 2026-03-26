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
///   
/// </summary>

[CreateAssetMenu(fileName = "entityScriptable", menuName = "Scriptable Objects/entityScriptable")]
public class entityScriptable : ScriptableObject
{
    ///////////////////////////////////////////////////////////      VARS      ////////////////////////////////////////////////////////////////////////////////
    public int[] roomNumArray = new int[] { 1, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3, 4, 4, 4, 4, 4, 6, 6}; //probability

    public Dictionary<int, Transform> roomNumDict; //FIX almost certainly not gonna be a transform but a container

    ///////////////////////////////////////////////////////////      LOOPSS      ////////////////////////////////////////////////////////////////////////////////
    private void Awake()
    {
    }

    void Start()
    {

    }

    void Update()
    {

    }

    private void FixedUpdate()
    {
    }


    ///////////////////////////////////////////////////////////      FUNCTIONS      ////////////////////////////////////////////////////////////////////////////////
    ///
    private void OnEnable()
    {
        roomNumDict = new Dictionary<int, Transform>()
        {


        };
    }

    public void idleChooseRandomRoom(Transform entityTF)
    {
        int roomNum = Random.Range(0, roomNumArray.Length);
    }
}
