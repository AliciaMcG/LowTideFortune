using UnityEngine;

/// <summary>
/// Holds code for:
/// 
///   ///
///   
/// </summary>

[CreateAssetMenu(fileName = "playerScriptable", menuName = "Scriptable Objects/playerScriptable")]
public class playerScriptable : ScriptableObject
{
    ///////////////////////////////////////////////////////////      VARS      ////////////////////////////////////////////////////////////////////////////////
    [Header("Player Attributes")]
    public float interactRange = 5;
    public float attachedDist = 3; 

    public float playerSpeed = 10;
    public float jumpForce = 7f;
    public float gravity = -20f;


    ///////////////////////////////////////////////////////////      FUNCTIONS      ////////////////////////////////////////////////////////////////////////////////
    ///
}