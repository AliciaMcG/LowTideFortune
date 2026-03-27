using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class bookController : MonoBehaviour
{
    public GameObject[] pages; //6 pages total

    public GameObject leftpage;
    public GameObject rightPage;

    public GameObject nextButton;
    public GameObject prevButton;

    private int currentSpread = 0;

    private void Start()
    {
        UpdatePages();
    }

    public void NextPage()
    {
        if (currentSpread < (pages.Length/2) - 1)
        {
            currentSpread++;
            UpdatePages();
        }
    }

    public void PrevPage()
    {
        if(currentSpread > 0)
        {
            currentSpread--;
            UpdatePages();
        }
    }

    void UpdatePages()
    {
        int leftIdx = currentSpread * 2;
        int rightIdx = leftIdx + 1;

        //turns off all pages
        foreach (GameObject page in pages)
        {
            page.SetActive(false);
        }

        //turns on current page
        pages[leftIdx].SetActive(true);
        pages[rightIdx].SetActive(true);

        //hide previous button if one first page
        if(prevButton != null)
        {
            prevButton.SetActive(currentSpread > 0);
        }

        //hide next button if on last page
        if (nextButton != null)
        {
            nextButton.SetActive(currentSpread < (pages.Length / 2) - 1);
        }
    }
}
