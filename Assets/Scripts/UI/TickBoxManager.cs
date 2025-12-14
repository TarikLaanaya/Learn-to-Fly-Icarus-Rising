using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SearchService;

public class TickBoxManager : MonoBehaviour
{
    [SerializeField] private TMP_Text costDisplayUI;
    [SerializeField] private TMP_Text shopCurrencyUI;
    [SerializeField] private GameObject[] tickBoxes;
    [SerializeField] List<GameObject> currentlyHighlighted = new List<GameObject>();
    [SerializeField] private int collectiveCost;
    [SerializeField] private bool fuelUpgrade;

    void Start()
    {
        // --- Highlight already purchased upgrades --- //

        int highestUpgradeIndex;

        if (fuelUpgrade)
        {
            highestUpgradeIndex = SceneManager.instance.gameManager.GetCurrentFuelUpgrade();
        }
        else
        {
            highestUpgradeIndex = SceneManager.instance.gameManager.GetCurrentTowerUpgrade();
        }

        if (highestUpgradeIndex > 0)
        {
            for (int i = 0; i < highestUpgradeIndex; i++)
            {
                tickBoxes[i].GetComponent<TickBox>().Highlight();
                tickBoxes[i].GetComponent<TickBox>().ticked = true;
            }
        }
    }

    public void TickBoxHoveredOver(GameObject hoveredTickBox)
    {
        List<GameObject> previousBoxes = new List<GameObject>();

        // Go through each box and check if it is the hovered over box
        foreach (GameObject box in tickBoxes)
        {
            // If not hovered over add to a list of previous boxes otherwise add the hovered to the list and end the function
            if (box != hoveredTickBox && !box.GetComponent<TickBox>().ticked) // Only add unticked boxes
            {
                previousBoxes.Add(box);
            }
            else if (box != hoveredTickBox) // If ticked but not the hovered skip this iteration
            {
                continue;
            }
            else if (box == hoveredTickBox && !box.GetComponent<TickBox>().ticked) // If the overed box and not ticked
            {
                previousBoxes.Add(box);
                CheckThenHighlight(previousBoxes);
                return;
            }
        }
    }

    void CheckThenHighlight(List<GameObject> previousBoxes)
    {
        collectiveCost = 0;

        // Go through the boxes in the list and highlight the ones that the player can afford (add them to the highlighted list too)
        foreach (GameObject box in previousBoxes)
        {
            int cost = box.GetComponent<TickBox>().cost;
            collectiveCost += cost; 

            // If we have enough money add a half alpha highlight
            if (SceneManager.instance.currencyManager.GetCurrency() >= collectiveCost)
            {
                box.GetComponent<TickBox>().HalfHighlight();
                currentlyHighlighted.Add(box);
            }
            else // When we reach a box that we cant afford we remove the last cost, display the collective cost and end the function
            {
                collectiveCost -= cost;
                if (collectiveCost > 0) DisplayCost();
                return;
            }
        }

        // In case we can afford all the boxes
        DisplayCost();
    }

    public void BoxClicked()
    {
        // Go through all highlighted boxes and set them as ticked
        foreach (GameObject box in currentlyHighlighted)
        {
            box.GetComponent<TickBox>().Highlight();
            box.GetComponent<TickBox>().ticked = true;
        }
        
        // Set the upgrade in the game manager
        int tickedBoxes = 0;

        foreach (GameObject box in tickBoxes)
        {
            if (box.GetComponent<TickBox>().ticked) tickedBoxes++;
        }

        if (fuelUpgrade)
        {
            SceneManager.instance.gameManager.SetCurrentFuelUpgrade(tickedBoxes);
        }
        else
        {
            SceneManager.instance.gameManager.SetTowerUpgrade(tickedBoxes);
        }

        if (SceneManager.instance.currencyManager.SpendCurrency(collectiveCost))
        {
            // Update currency UI
            shopCurrencyUI.text = SceneManager.instance.currencyManager.GetCurrency().ToString();
            collectiveCost = 0;
            currentlyHighlighted.Clear();
            HideCost();
        }
    }

    public void Unhighlight()
    {
        foreach (GameObject box in currentlyHighlighted)
        {
            box.GetComponent<TickBox>().HideHighlight();
        }

        collectiveCost = 0;
        currentlyHighlighted.Clear();
        HideCost();
    }

    private void DisplayCost()
    {
        costDisplayUI.text = collectiveCost.ToString();
    }

    private void HideCost()
    {
        costDisplayUI.text = " ";
    }
}
