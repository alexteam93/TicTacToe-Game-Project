using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameLogic : MonoBehaviour
{
   public GridBuilder grid;
   private bool isXTurn = true;
   private bool isPlayer1Turn = true;
   private bool isPlayer1Begin = true;
   private bool isWin = false;
   private int TurnsCount = 0;
   public GameObject winnerScreen;
   public Text turnText;
   public Text P1Score;
   public Text P2Score;
   public Text DrawScore;
   [HideInInspector]
   public string Player1 = "Player 1";
   [HideInInspector]
   public string Player2 = "Player 2";

   void Awake()
   {
       grid.BuildGrid(this);
       
   }

   public void SwitchTurn()
   {
       TurnsCount++;

       isWin = grid.CheckWin();

       if(isWin || TurnsCount == 9)
       {
           EndGame(isWin);
           return;
       }
        
        
       isXTurn = !isXTurn;
       isPlayer1Turn = !isPlayer1Turn;////

       if(GameModeManager.AIActive)
       {
            if(isPlayer1Turn == false )
                grid.AIMoveEasyMode();
       }
        
        turnText.text = "Now move " + GetTurnPlayerName();
   }

   public string GetTurnPlayer()
   {
        if(isXTurn)
            return "X";
        else
            return "O";

   }
   public string GetTurnPlayerName()
   {
        if(isPlayer1Turn)
            return Player1;
        else
            return Player2;

   }

   public string GetFirstMovePlayerName()
   {
        if(isPlayer1Begin)
            return Player1;
        else
            return Player2;

   }
    private void EndGame(bool isWin)
    {
        Text winnerText = winnerScreen.GetComponentInChildren<Text>();

        if(isWin)
        {
            winnerText.text = GetTurnPlayerName() + " won!";
        }
        else
            winnerText.text = "Draw!";
        
        winnerScreen.SetActive(true);


        
    }

    public void NewGame()
    {
        grid.ResetGrid();
        
        UpdateScore();///
        DefineFirstMove();///
        isXTurn = true;
        TurnsCount = 0;

        if(isPlayer1Begin == false)
        {            
            grid.AIMoveEasyMode();
        }

        winnerScreen.SetActive(false);
        
        
        
    }

    void DefineFirstMove()
    {
        if(isWin == false || GetFirstMovePlayerName() != GetTurnPlayerName())
        {
            isPlayer1Begin = isPlayer1Turn = !isPlayer1Begin;
           
        }
            
        
        turnText.text = GetFirstMovePlayerName() + " move first";



    }
    void UpdateScore()
    {
        if(isWin == false)
            AddPoint(DrawScore);
        else if(isPlayer1Turn)
            AddPoint(P1Score);
        else 
            AddPoint(P2Score);
    }
    void AddPoint(Text field)
    {
        int score = System.Convert.ToInt32(field.text);
        score ++;
        field.text = score.ToString();
    }
    public void GoToMenu()
    {
        GameModeManager.AIActive = false;
        SceneManager.LoadScene(0);
    }
}


