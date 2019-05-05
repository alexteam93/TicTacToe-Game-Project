using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    
    public Text cellText;
    public Button cellButton;
    public GameLogic gameLogic;

    void Awake()
    {
        cellText = GetComponentInChildren<Text>();
        cellButton = GetComponent<Button>();
    }

    public void FillCell()
    {
        cellButton.interactable = false;
        cellText.text = gameLogic.GetTurnPlayer();
        gameLogic.SwitchTurn();
    }
}
