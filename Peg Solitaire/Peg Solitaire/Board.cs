using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Peg_Solitaire
{
    class Board 
    {
        //variables
       private Texture2D theBoard,lost,won;
       private SpriteBatch spriteBatch;
       private SpriteFont theFont;
       
       private int[,] board;
       private int xpos, ypos,xpos2,ypos2;
      // private const int nothing;
       private int[,] theSelection;
       private int  empty;
      // private bool moves;
        //default constructor
        public Board()
        { 
            
        }

        //Initialize values
        public Board(int[,] board,int[,] theSelection, SpriteBatch spriteBatch, Texture2D theBoard, Texture2D lost, Texture2D won, SpriteFont theFont)
         {
             this.spriteBatch = spriteBatch;
             this.theBoard = theBoard;
             this.theSelection = theSelection;
             this.board = board;
             this.theFont = theFont;
             this.lost = lost;
             this.won = won;
             empty = 0;
             ypos = 0;
             xpos = 0;
             xpos2 = 0;
             ypos2 = 0;
             //moves = false
         }

        //Draws tje board to the screen
        public void DrawBoard()
        {
            Rectangle source = new Rectangle();
            for (int row = 0; row < board.GetLength(0); row++)
            {
                for (int col = 0; col < board.GetLength(1); col++)
                {



                    Color theColor = Color.White;
                    //get length of image
                    int spriteLength = (725 / 7) + 1;

                    //handle normal pegs
                    if (board[row, col] != 0)
                    {

                        source = new Rectangle(board[row, col] * (spriteLength), 0, spriteLength, 95);
                    }
                        //handle blank pegs
                    else if (board[row, col] == 0)
                    {
                        source = new Rectangle(1 * (spriteLength), 0, spriteLength, 95);

                        theColor = Color.Black;
                    }
                    

                    // Calculate the position in the drawn matrix.
                    int x = row * (RMLength()) + col;
                    int rowPrime = x / board.GetLength(0);
                    int colPrime = x % board.GetLength(0);

                    Rectangle dest = new Rectangle(50 + colPrime * 95, 50 + rowPrime * 95, 50, 50);

                    //spriteBatch.Draw(theBoard, dest, source, Color.White);
                    //only draw pegs in array ignore the blanks
                    if (board[row, col] != -1)
                    {
                        spriteBatch.Draw(theBoard, dest, source, theColor);

                    }
                    //render selection
                    bool moves = MovesLeft();
                    if(moves)
                         DrawSelection(row,col);
                   
                }
            }
           
        }

        //changes peg on board to specific color state frame etc
        public void ChangePeg(int row, int col, int value)
        {
            board[row, col] = value;
        }
        //changes slector to specific color state frame etc
        public void ChangeSelector(int row, int col, int value)
        {

            theSelection[row, col] = value;
        }

        //prevents array from going out of bound
        public bool BoundsChecked(int value)
        {
            if ((getRow() + value > RMLength() || getRow() + value < 0))
                return false;
            if ((getCol() + value > CMLength() || getCol() + value < 0))
                return false;
            if ((getRow2() + value > RMLength() || getRow2() + value < 0))
                return false;
            if ((getCol2() + value > CMLength() || getCol2() + value < 0))
                return false;
            return true;
        }
       
        //determines if you won game of peg solitaire
        public bool Won()
        {
            int num = 0;
            for (int row = 0; row < board.GetLength(0); row++)
            {
                for (int col = 0; col < board.GetLength(1); col++)
                {
                    if (board[row, col] > empty)
                        num++;
                    if(num > 1)
                        return false;
                }
            }
            return true;
        }


        public void DrawStats()
        {
            if (Won())
            {
                spriteBatch.Draw(won, new Vector2(85, 105), Color.White);
            
            }
            else if (!Won()) 
            {
                spriteBatch.Draw(lost, new Vector2(85, 105 ), Color.White);
                spriteBatch.DrawString(theFont, "Pegs: " + GetNumberofPegs(), new Vector2(385, 380), Color.Orange);

            }

                
        
        }
        //Returns number of pegs on the board
        public int GetNumberofPegs()
         {   
            int number = 0;
             for (int row = 0; row < board.GetLength(0); row++)
                {
                    for (int col = 0; col < board.GetLength(1); col++)
                    {
                        if(board[row,col] != 0 && board[row,col] !=-1)
                            number++;


                        
                    }
                }
            return number;

        }
        //modifies selection array for Black
        public void setSelectorB(int x, int y)
        {
            xpos = x;
            ypos = y;
        
        }
        //modifies selection array for red
        public void setSelectorR(int x, int y)
        {

            xpos2 = x;
            ypos2 = y;
        }

        public int RMLength()
        { return board.GetLength(0); }
        public int CMLength()
        { return board.GetLength(1); }

        public int getRow()
        { return xpos; }
        public int getCol()
        {return ypos; }

        public int getRow2()
        { return xpos2; }

        public int getCol2()
        { return ypos2; }

        //gets type of board value(FRAME) at specified location
        public int getBoardValue(int row, int col)
        {
            return board[row, col];
        }

        //gets type of selection value at specified location
        public int getSelectorValue(int row, int col)
        {
            return theSelection[row, col];
        }


        public void DrawHud()
        {
            spriteBatch.DrawString(theFont,"Get Row: " + getRow(), new Vector2(0f, 0f), Color.Black);
            spriteBatch.DrawString(theFont, "Get Col: " + getCol(), new Vector2(0f, 20f), Color.Black);
            spriteBatch.DrawString(theFont, "Get Row2: " + getRow2(), new Vector2(0f, 40f), Color.Black);
            spriteBatch.DrawString(theFont, "Get Col2: " + getCol2(), new Vector2(0f, 60f), Color.Black);
            spriteBatch.DrawString(theFont, "Board Val: " + getBoardValue(getRow(),getCol()), new Vector2(0, 80f), Color.Black);
            spriteBatch.DrawString(theFont, "Pegs: " + GetNumberofPegs(), new Vector2(0, 100f), Color.Black);
            spriteBatch.DrawString(theFont, "Moves Left: " + MovesLeft(), new Vector2(0, 120), Color.Black);

            

        }
        
        //checks if a piec on te board meets acertain criteria 
        // and if it does check to see if a valid move is possible
        // if is the function returns true execution leaves function(methond)

        public bool MovesLeft()
        {
            for (int row2 = 0; row2 < RMLength() ; row2++)
            {
                for (int col2 = 0; col2 < CMLength() ; col2++)
                {
                    if ( getBoardValue(row2, col2) > empty 
                                        
                                        && col2 + 2 < CMLength() && getBoardValue(row2, col2 + 1) > empty
                                      
                                    
                         
                                        && getBoardValue(row2, col2 + 2) == empty)
                    {
                        return true;
                    }
                    else if (getBoardValue(row2,col2) > empty
                       
                       && col2 - 2 > -1 &&  getBoardValue(row2, col2 - 1) > empty
                       && getBoardValue(row2, col2 - 2) == empty)
                    {
                        return true;
                    }
                    else if (getBoardValue(row2,col2) > empty
                        && row2 + 2 < RMLength()&& getBoardValue(row2 + 1, col2) > empty
                        && getBoardValue(row2 + 2, col2) == empty)
                    {
                        return true;
                    }
                    else if (getBoardValue(row2,col2) > empty
                       && row2 - 2 > -1 &&getBoardValue(row2 - 1, col2) > empty
                       && getBoardValue(row2 - 2, col2) == empty)
                    {
                        return true;
                    }
                }
            }
            
            return false;
        
        
        }

        public void Cheat()
        {
            for (int row = 0; row < board.GetLength(0); row++)
            {
                for (int col = 0; col < board.GetLength(1); col++)
                {
                    if (board[row, col] > -1)
                        board[row, col] = 0;

                }
            }
        }
        //draws selector
        public void DrawSelection(int row, int col)
        {
            Color theColor;
            Random r = new Random();
                    //gets length of image
                    int spriteLength = (725 / 7) + 1;
                    Rectangle source = new Rectangle();
           // for (int row = 0; row < theSelection.GetLength(0); row++)
           // {
               // for (int col = 0; col < theSelection.GetLength(1); col++)
               // {
                   int j = 120 +  r.Next(160);
                        
                      //grab the very first frame
                    
                        source = new Rectangle(0 * (spriteLength), 0, spriteLength, 95);
                       //compute dersired drawing style
                        int x = row * (7) + col;
                        int rowPrime = x / theSelection.GetLength(0);
                        int colPrime = x % theSelection.GetLength(0);
                        Rectangle dest = new Rectangle(50 + colPrime * 93, 50 + rowPrime * 92, 66, 64);
                       //draw black selector
                       if(theSelection[row,col] == 0){
                                 theColor = Color.Black;
                                spriteBatch.Draw(theBoard, dest, source, theColor);

                                setSelectorB(row, col);
                       }
                           //draw red selector
                       else if (theSelection[row, col] == 1)
                       {
                           theColor = Color.Red;
                           spriteBatch.Draw(theBoard, dest, source, theColor);
                           setSelectorR(row, col);
                       
                       }
              //  }
           // }
        }
        
    }

    


}
