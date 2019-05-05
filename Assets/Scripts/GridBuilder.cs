using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBuilder : MonoBehaviour
{
    public GameObject cellPref;
    private Cell[] cells = new Cell[9];

    
    public void BuildGrid(GameLogic GL)
    {
        for (int i = 0; i < cells.Length; i++)
        {
            GameObject newCell = Instantiate(cellPref, transform);

            cells[i] = newCell.GetComponent<Cell>();
            cells[i].gameLogic = GL;
        }
    }
    public bool CheckWin()
    {

        int i = 0;

        //check hotizontal

        for(i = 0; i <= 6; i += 3)
        {
            if(!CompareValues(i, i + 1))
                continue;
            if(!CompareValues(i, i + 2))
                continue;

            return true;
        }

        //check vertical

        for(i = 0; i <= 2; i ++)
        {
            if(!CompareValues(i, i + 3))
                continue;
            if(!CompareValues(i, i + 6))
                continue;

            return true;
        }

        //check right diagonal
        if(CompareValues(2, 4) && CompareValues(2, 6))
            return true;

        //check left diagonal
        if(CompareValues(0, 4) && CompareValues(0, 8))
            return true;

        return false;
    }
    private bool CompareValues(int firstIndex, int secondIndex)
    {
        string firstValue = cells[firstIndex].cellText.text;
        string secondValue = cells[secondIndex].cellText.text;

        if(firstValue == "" || secondValue == "")
            return false;
        if(firstValue == secondValue)
            return true;
        else
            return false;
    }

    public void ResetGrid()
    {
        foreach(Cell cell in cells)
        {
            cell.cellText.text = "";
            cell.cellButton.interactable = true;
        }
    }
}
