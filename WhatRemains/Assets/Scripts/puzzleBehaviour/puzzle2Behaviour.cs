using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using System.Collections;

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
    public ParticleSystem cauldronParticles;


    [Header("Recipe")]
    public List<int> requiredIngredients = new List<int> { 0, 1, 2, 3 };
    public List<int> currentIngredients;
    bool isSpoiled;

    [Header("Recipe")]
    public AudioSource buttonClickSound;
    public AudioSource correctJarSound;
    public AudioSource incorrectJarSound;

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
    private void OnTriggerEnter(Collider other)
    {
        addIngredient(other.gameObject);
    }
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

            updateParticles();
            checkMixture();
            jar.RespawnJar();

        }
    }

    void checkMixture()
    {
        // play sound effect for correct ingredient
        if (!correctJarSound.isPlaying)
        {
            correctJarSound.Play();
        }
        
        if (currentIngredients.Count == requiredIngredients.Count)
        {
            puzzleManager.completePuzzle(1);
        }
    }

    void spoilMixture()
    {
        // play spoil sound effect
        if (!incorrectJarSound.isPlaying)
        {
            incorrectJarSound.Play();
        }
        
        isSpoiled = true;
        
        //changes the particles
        var main = cauldronParticles.main;
        var emission = cauldronParticles.emission;

        //darkens the colour as they put ingredients in
        Color startColour = Color.black;
        emission.rateOverTime = 0;

        cauldronParticles.Clear();
    }

    //call this when the dump button is pressed
    public void dumpMixture()
    {
        //play button click sound
        if (!buttonClickSound.isPlaying)
        {
            buttonClickSound.Play();
        }

        currentIngredients.Clear();
        isSpoiled = false;

        //changes the particles
        var main = cauldronParticles.main;
        var emission = cauldronParticles.emission;

        //darkens the colour as they put ingredients in
        Color startColour = Color.black;
        emission.rateOverTime = 0;

        cauldronParticles.Clear();

        StartCoroutine(ResetParticlesDelay());
    }

    void updateParticles()
    {
        float progress = (float)currentIngredients.Count / requiredIngredients.Count;

        var main = cauldronParticles.main;
        var emission = cauldronParticles.emission;

        //darkens the colour as they put ingredients in
        Color startColour = Color.Lerp(Color.yellow, Color.red, progress);
        main.startColor = startColour;

        //increases the particles amount over time
        emission.rateOverTime = 10 + (progress * 50);
    }

    IEnumerator ResetParticlesDelay()
    {
        yield return new WaitForSeconds(2f);
        var emission = cauldronParticles.emission;
        emission.rateOverTime = 10;
    }
}
