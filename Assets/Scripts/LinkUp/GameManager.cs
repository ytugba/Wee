using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private Tile[,] AllTiles = new Tile[4,4];
    private List<Tile[]> columns = new List<Tile[]> ();
    private List<Tile[]> rows = new List<Tile[]> ();
    private List<Tile> EmptyTiles = new List<Tile> ();

    public GameObject game;
    public static bool isStarted;
    public static bool isOver;

    void Start()
    {
    
    }


    bool MakeOneMoveDownIndex(Tile[] LineOfTiles)
    {
        for (int i = 0; i < LineOfTiles.Length-1 ; i++)
        {
            //MOVE BLOCK
            if(LineOfTiles[i].Number == 0 && LineOfTiles[i+1].Number != 0)
            {
                LineOfTiles[i].Number = LineOfTiles[i+1].Number;
                LineOfTiles[i+1].Number = 0;
                return true;
            }

            // MERGE BLOCK
            if(LineOfTiles[i].Number != 0 && LineOfTiles[i].Number == LineOfTiles[i+1].Number &&
                LineOfTiles[i].mergedThisTurn == false && LineOfTiles[i+1].mergedThisTurn == false
            )
            {
                LineOfTiles[i].Number *= 2;
                LineOfTiles[i+1].Number = 0;
                LineOfTiles[i].mergedThisTurn = true;
                ScoreTracker.instance.Score += LineOfTiles[i].Number;
                return true;
            }
        }

        return false;
    }

    bool MakeOneMoveUpIndex(Tile[] LineOfTiles)
    {
        for (int i = LineOfTiles.Length-1 ; i > 0 ; i--)
        {
            //MOVE BLOCK
            if(LineOfTiles[i].Number == 0 && LineOfTiles[i-1].Number != 0)
            {
                LineOfTiles[i].Number = LineOfTiles[i-1].Number;
                LineOfTiles[i-1].Number = 0;

                return true;
            }

            // MERGE BLOCK
            if(LineOfTiles[i].Number != 0 && LineOfTiles[i].Number == LineOfTiles[i-1].Number &&
                LineOfTiles[i].mergedThisTurn == false && LineOfTiles[i-1].mergedThisTurn == false
            )
            {
                LineOfTiles[i].Number *= 2;
                LineOfTiles[i-1].Number = 0;
                LineOfTiles[i].mergedThisTurn = true;
                ScoreTracker.instance.Score += LineOfTiles[i].Number;
                return true;
            }
        }

        return false;
    }

    void Generate()
    {
        if(EmptyTiles.Count > 0){
            int indexForNewNumber = Random.Range(0,EmptyTiles.Count);
            int randomNum = Random.Range(0,10);
            if(randomNum == 0){
                EmptyTiles[indexForNewNumber].Number = 4;
            }
            else
                EmptyTiles[indexForNewNumber].Number = 2;

            EmptyTiles.RemoveAt(indexForNewNumber);
        }
    }

    
    void Update()
    {

        if(isStarted){
            Tile[] AllTilesOneDim = GameObject.FindObjectsOfType<Tile> ();
        foreach (Tile t in AllTilesOneDim)
        {
            t.Number = 0;
            AllTiles[t.indRow,t.indCol] = t;
            EmptyTiles.Add(t);
        }

        columns.Add(new Tile[] {AllTiles[0,0], AllTiles[1,0], AllTiles[2,0],AllTiles[3,0]});
        columns.Add(new Tile[] {AllTiles[0,1], AllTiles[1,1], AllTiles[2,1],AllTiles[3,1]});
        columns.Add(new Tile[] {AllTiles[0,2], AllTiles[1,2], AllTiles[2,2],AllTiles[3,2]});
        columns.Add(new Tile[] {AllTiles[0,3], AllTiles[1,3], AllTiles[2,3],AllTiles[3,3]});

        rows.Add(new Tile[] {AllTiles[0,0], AllTiles[0,1], AllTiles[0,2],AllTiles[0,3]});
        rows.Add(new Tile[] {AllTiles[1,0], AllTiles[1,1], AllTiles[1,2],AllTiles[1,3]});
        rows.Add(new Tile[] {AllTiles[2,0], AllTiles[2,1], AllTiles[2,2],AllTiles[2,3]});
        rows.Add(new Tile[] {AllTiles[3,0], AllTiles[3,1], AllTiles[3,2],AllTiles[3,3]});

        Generate();
        Generate();
        isStarted = false;
        }
    }
    

    public bool canMove(){
        if(EmptyTiles.Count > 0){
            return true;
        }
        else{
            //check columns
            for (int i = 0; i < columns.Count; i++)
            {
                for (int j = 0; j < rows.Count-1; j++)
                {
                    if(AllTiles[j,i].Number == AllTiles[j+1,i].Number){
                        return true;
                    }
                }
            }

            //check rows
            for (int i = 0; i < rows.Count; i++)
            {
                for (int j = 0; j < columns.Count-1; j++)
                {
                    if(AllTiles[i,j].Number == AllTiles[i,j+1].Number){
                        return true;
                    }
                }                
            }

            return false;
        }
    }

    private void ResetMergedFlags()
    {
        foreach (Tile t in AllTiles)
        {
            t.mergedThisTurn = false;
        }
    }

    private void UpdateEmptyTiles(){
        
        EmptyTiles.Clear();
        foreach(Tile t in AllTiles)
        {
            if(t.Number == 0 ){
                EmptyTiles.Add(t);
            }
        }
    }

    public void Move(MoveDirection md)
    {
        //Debug.Log(md.ToString() +" move." );
        bool moveMade = false;
        for (int i = 0; i < rows.Count; i++)
        {
            ResetMergedFlags();
            if(game.activeSelf == true)
            {
                 switch (md)
                {
                    case MoveDirection.Down:
                        while(MakeOneMoveUpIndex(columns[i])){
                            moveMade = true;
                        }
                        break;

                    case MoveDirection.Left:
                        while(MakeOneMoveDownIndex(rows[i])){
                            moveMade = true;
                        }
                        break;

                    case MoveDirection.Right:
                        while(MakeOneMoveUpIndex(rows[i])){
                            moveMade = true;
                        }
                        break;

                    case MoveDirection.Up:
                        while(MakeOneMoveDownIndex(columns[i])){
                            moveMade = true;
                        }
                        break;
                }
            }
           
        }
        if(moveMade){
            UpdateEmptyTiles();
            Generate();

            if(!canMove()){
                isOver = true;
            }
        }
    }
}
