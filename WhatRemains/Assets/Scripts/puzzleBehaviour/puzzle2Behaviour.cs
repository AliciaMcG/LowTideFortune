using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Holds code for:
/// 
///   Puzzle index 1
///   
///   correct ingredients:
///     snake skin, jarID = 0
///     mushrooms,  jarID = 1
///     teeth,      jarID = 2
///     blood,      jarID = 3
///     other,      jarID = 4
/// </summary>

public class puzzle2Behaviour : MonoBehaviour
{
    ///////////////////////////////////////////////////////////      VARS      ////////////////////////////////////////////////////////////////////////////////
    [Header("Objects")]
    gameplayBase puzzleManager;
    public GameObject cauldron;

    [Header("Recipe")]
    public List<int> requiredIngredients = new List<int> { 0, 1, 2, 3 };
    public List<int> currentIngredients;
    bool isSpoiled;

    //[Header("Lists")]

    ///////////////////////////////////////////////////////////      LOOPSS      ////////////////////////////////////////////////////////////////////////////////
    private void Awake()
    {
    }

    void Start()
    {
        isSpoiled = false;
    }

    void Update()
    {
    }

    private void FixedUpdate()
    {
    }


    ///////////////////////////////////////////////////////////      FUNCTIONS      ////////////////////////////////////////////////////////////////////////////////
    ///
    public void addIngredient(GameObject ingredient)
    {
        if (isSpoiled == true)
        {
            return;
        }

        ingredientJar jar = ingredient.GetComponent<ingredientJar>();

        if (jar == null)
        {
            return;
        }

        // compare jar type to required jar types
        int jarID = jar.id;

        if (!requiredIngredients.Contains(jarID))
        {
            spoilMixture();
            return;
        }

        if (!currentIngredients.Contains(jarID))
        {
            currentIngredients.Add(jarID);
            checkMixture();
        }
    }

    void checkMixture()
    {
        // play sound effect for correct ingredient
        
        if (currentIngredients.Count == requiredIngredients.Count)
        {
            puzzleManager.completePuzzle(1);
        }
    }

    void spoilMixture()
    {
        // play spoil sound effect
        isSpoiled = true;
    }

    public void dumpMixture()
    {
        currentIngredients.Clear();
        isSpoiled = false;
    }
}
