/* Gerald Blake
 * Video Game Design CS 4173
 * Individual 1 PEG SOLITAIRE
 * Due Tuesday 
 * 1/29/13
 * 
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace Peg_Solitaire
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        
        Random r = new Random();
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D theBoard, lost, won;
        SpriteFont theFont;
        const int nothing = -1;
        const int empty = 0;
        const int select = 0;
        const int red = 1;
        static int selectionProcess = 0;
        KeyboardState oldState;
        Point position = new Point(0, 0);

        //Create Parallel Arrays that Map Board
        //and selector the 
        // -1 = blank
        // 0 = hole
        // 1 = yellow
        // 3 = Red
        // 4 = blue
        // 5 = orange
        // 6 = Green
    int[,] board =       { {-1,-1,1,1,1,-1,-1},
                            {-1,-1,1,1,1,-1,-1},
                            {3,3,5,1,5,4,4},
                            {3,3,3,0,4,4,4},
                            {3,3,5,6,5,4,4},
                            {-1,-1,6,6,6,-1,-1},
                            {-1,-1,6,6,6,-1,-1}
                       };
        int[,] theSelection =       { {-1,-1,-1,-1,-1,-1,-1},
                                      {-1,-1,-1,-1,-1,-1,-1},
                                      {-1,-1,-1,0,-1,-1,-1},
                                      {-1,-1,-1,-1,-1,-1,-1},
                                      {-1,-1,-1,-1,-1,-1,-1},
                                      {-1,-1,-1,-1,-1,-1,-1},
                                      {-1,-1,-1,-1,-1,-1,-1}
                       };
        //declare board object
       Board b;
       
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Window.Title = "Peg Solitaire Gerald Blake";
            //set to proper widths
            graphics.PreferredBackBufferWidth = 750;
            graphics.PreferredBackBufferHeight = 750;

        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            oldState = Keyboard.GetState();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            theBoard = Content.Load<Texture2D>("Images/Marbles");
            theFont = Content.Load<SpriteFont>(@"Arial");
            lost = Content.Load<Texture2D>("Images/Lost");
            won = Content.Load<Texture2D>("Images/Won Image");

            //initialize board object
            b = new Board(board,theSelection, spriteBatch, theBoard,lost, won, theFont);
            
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            KeyboardState keyState = Keyboard.GetState();
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here

            //Switches selector from red to black
            //handles keyboard input an selection process
            //moves selector
            switch(selectionProcess)
            {
                case 0:
                    if (keyState.IsKeyDown(Keys.Right) && oldState.IsKeyUp(Keys.Right))
                    {
                        if ((b.getCol() + 1 ) < b.CMLength()
                            && b.getRow() < b.CMLength()
                            && b.getBoardValue(b.getRow(),b.getCol() +1) != nothing)
                          
                        {
                            b.ChangeSelector(b.getRow(), b.getCol(), nothing);
                            b.ChangeSelector(b.getRow(), b.getCol()+1, select);
                        }
                    
                
                    }
                    else if (keyState.IsKeyDown(Keys.Up) && oldState.IsKeyUp(Keys.Up))
                    {
                        if (b.getCol() > nothing
                            && (b.getRow() - 1) > -1
                            && b.getBoardValue(b.getRow() - 1, b.getCol()) != nothing)
                        {
                            b.ChangeSelector(b.getRow(), b.getCol(), nothing);
                            b.ChangeSelector(b.getRow() - 1, b.getCol(), select);
                        }

                    }
                    else if (keyState.IsKeyDown(Keys.Left) && oldState.IsKeyUp(Keys.Left))
                    {

                        if ((b.getRow()) > -1 
                            && b.getCol() - 1 > -1 
                            && b.getBoardValue(b.getRow(), b.getCol() - 1) != nothing)
                        {
                            b.ChangeSelector(b.getRow(), b.getCol(), nothing);
                            b.ChangeSelector(b.getRow(), b.getCol() - 1, select);
                        }
                    }
                    else if (keyState.IsKeyDown(Keys.Down) && oldState.IsKeyUp(Keys.Down))
                    {

                        if (b.getCol() < b.CMLength() 
                            && (b.getRow()) + 1 < b.RMLength()
                            && b.getBoardValue(b.getRow()+1, b.getCol()) != nothing)
                        {
                            b.ChangeSelector(b.getRow(), b.getCol(), nothing);
                            b.ChangeSelector(b.getRow()+1, b.getCol(), select);
                        }
                    }
                    else if(keyState.IsKeyDown(Keys.Enter) && oldState.IsKeyUp(Keys.Enter))
                    {

                      //  b.ChangeSelector(b.getRow(), b.getCol(), 1);
                        if(b.getBoardValue(b.getRow(), b.getCol()) != empty)
                        {

                       
                            //change selector to red
                            b.ChangeSelector(b.getRow(), b.getCol(), red);
                            //send a new black selector to the middle
                            if (!(b.getRow() == 3 && b.getCol() == 3))
                            {
                                b.ChangeSelector(3, 3, select);
                                selectionProcess = 1;
                            }
                            else
                            {
                                bool done = false;
                                while (!done)
                                {
                                    int num1 = r.Next(b.RMLength() / 2);
                                    int num2 = r.Next(b.CMLength() / 2);
                                    if ((b.getCol() != num2 || b.getRow() != num1)
                                        && b.getBoardValue(num1,num2)!=nothing)
                                    {
                                        b.ChangeSelector(num1, num2, select);

                                        selectionProcess = 1;
                                        done = true;
                                    }
                                }
                            }
                        }

                    }

                        //Tired of playing and game got u stomped press this button to 
                        /// see what happens
                    else if (keyState.IsKeyDown(Keys.C) && oldState.IsKeyUp(Keys.C))
                    {
                        b.Cheat();
                    }


                    break;
                case 1:
                    if (keyState.IsKeyDown(Keys.Right) && oldState.IsKeyUp(Keys.Right))
                    {
                        if (b.getCol() + 1 < b.CMLength() 
                            && b.getRow() < b.RMLength() 
                            && b.getBoardValue(b.getRow(), b.getCol() + 1) != nothing
                            &&b.getSelectorValue(b.getRow(), b.getCol() +1) != red)
                        {
                            b.ChangeSelector(b.getRow(), b.getCol(), nothing);
                            b.ChangeSelector(b.getRow(), b.getCol() + 1, select);
                        }


                    }
                    else if (keyState.IsKeyDown(Keys.Up) && oldState.IsKeyUp(Keys.Up))
                    {
                        if (b.getCol() > -1 && (b.getRow() - 1) > -1 
                            && b.getBoardValue(b.getRow() - 1, b.getCol()) != nothing
                            && b.getSelectorValue(b.getRow() - 1, b.getCol()) != red)
                        {
                            b.ChangeSelector(b.getRow(), b.getCol(), nothing);
                            b.ChangeSelector(b.getRow() - 1, b.getCol(), select);
                        }

                    }
                    else if (keyState.IsKeyDown(Keys.Left) && oldState.IsKeyUp(Keys.Left))
                    {

                        if ((b.getRow()) > -1
                            && b.getCol() - 1 > -1 
                            && b.getBoardValue(b.getRow(), b.getCol() - 1) != nothing
                            && b.getSelectorValue(b.getRow(), b.getCol() - 1) != red)
                        {
                            b.ChangeSelector(b.getRow(), b.getCol(), nothing);
                            b.ChangeSelector(b.getRow(), b.getCol() - 1, select);
                        }
                    }
                    else if (keyState.IsKeyDown(Keys.Down) && oldState.IsKeyUp(Keys.Down))
                    {

                            if (b.getCol() < b.CMLength() 
                                && ((b.getRow()) + 1 )< b.RMLength() 
                                && (b.getBoardValue(b.getRow() + 1, b.getCol()) != nothing
                                && b.getSelectorValue(b.getRow()+1, b.getCol()) != red))
                            { 
                                b.ChangeSelector(b.getRow(), b.getCol(), nothing);
                                b.ChangeSelector(b.getRow() + 1, b.getCol(), select);
                            }
                        }
                        else if (keyState.IsKeyDown(Keys.Enter) && oldState.IsKeyUp(Keys.Enter)) 
                        {
                        
                            if((b.getRow() == b.getRow2())
                                && (b.getCol()  == b.getCol2()+2
                                && b.getBoardValue(b.getRow2(),b.getCol2()+1) != empty
                                && b.getBoardValue(b.getRow2(),b.getCol2()+2)== empty))
                            
                            {
                                b.ChangePeg(b.getRow(), b.getCol(), b.getBoardValue(b.getRow2(), b.getCol2()));
                                b.ChangePeg(b.getRow2(), b.getCol2(), empty);
                                b.ChangePeg(b.getRow2(),b.getCol2() + 1,empty);
                                b.ChangeSelector(b.getRow2(), b.getCol2(), nothing);
                                selectionProcess = 0;
                            }
                            else if ((b.getRow() == b.getRow2()
                               && (b.getCol2()-2 == b.getCol()
                               && b.getBoardValue(b.getRow2(), b.getCol2() -1 ) != empty
                               && b.getBoardValue(b.getRow2(), b.getCol2() - 2) == empty)))
                            {
                                b.ChangePeg(b.getRow(), b.getCol(), b.getBoardValue(b.getRow2(), b.getCol2()));
                                b.ChangePeg(b.getRow2(), b.getCol2(), empty);
                                b.ChangePeg(b.getRow2(),b.getCol2() - 1,empty);
                                b.ChangeSelector(b.getRow2(), b.getCol2(), nothing);
                                selectionProcess = 0;
                            }
                            else if ((b.getCol() == b.getCol2()
                                && (b.getRow() == b.getRow2()+2
                                && b.getBoardValue(b.getRow2() + 1, b.getCol2()) != empty
                                && b.getBoardValue(b.getRow2()+ 2, b.getCol2()) == empty)))
                            {
                                b.ChangePeg(b.getRow(), b.getCol(), b.getBoardValue(b.getRow2(), b.getCol2()));
                                b.ChangePeg(b.getRow2(), b.getCol2(), empty);
                                b.ChangePeg(b.getRow2()+1, b.getCol2(), empty);
                                b.ChangeSelector(b.getRow2(), b.getCol2(), nothing);
                                selectionProcess = 0;

                            }
                            else if ((b.getCol() == b.getCol2()
                               && (b.getRow() == b.getRow2()-2
                               && b.getBoardValue(b.getRow2() - 1, b.getCol2()) != empty
                               && b.getBoardValue(b.getRow2() - 2, b.getCol2()) == empty)))
                            {
                                b.ChangePeg(b.getRow(), b.getCol(), b.getBoardValue(b.getRow2(), b.getCol2()));
                                b.ChangePeg(b.getRow2(), b.getCol2(), empty);
                                b.ChangePeg(b.getRow2()-1, b.getCol2(), empty);
                                b.ChangeSelector(b.getRow2(), b.getCol2(), nothing);
                                selectionProcess = 0;
                            }
                            else
                            {
                                //If the space is an empty space
                            
                               // b.ChangeSelector(b.getRow(), b.getCol(), nothing);
                                b.ChangeSelector(b.getRow2(), b.getCol2(), nothing);
                                selectionProcess = 0;
                            }

                    
                    }
                    break;
                // Yes; move the position of the sprite 100 units to the right.
               // position.X += 100;
        }
            oldState = keyState;

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.NavajoWhite);
            spriteBatch.Begin();
            // TODO: Add your drawing code here

            //Draw the board to the screen
           
                b.DrawBoard();

              //DEBUGING

              //  b.DrawHud();
           
            if(!b.MovesLeft())
            {
                b.DrawStats();
            }
            spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
