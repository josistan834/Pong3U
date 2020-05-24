using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace Pong3U
{
    public partial class Form1 : Form
    {
        int paddle1X = 30;
        int paddle1Y = 170;
        int player1Score = 0;

        int paddle2X = 560;
        int paddle2Y = 170;
        int player2Score = 0;

        int paddleWidth = 30;
        int paddleHeight = 30;
        int paddleSpeed = 4;

        int ballX = 300;
        int ballY = 185;
        int ballXSpeed = 0;
        int ballYSpeed = 0;
        int ballWidth = 10;
        int ballHeight = 10;

        int wall1X = 0;
        int wall1y = 0;
        int wall2X = 0;
        int wall2y = 230;
        int wall3X = 605;
        int wall3y = 0;
        int wall4X = 605;
        int wall4y = 230;
        int wallWidth = 10;
        int wallHeight = 100;

        bool wDown = false;
        bool sDown = false;
        bool aDown = false;
        bool dDown = false;
        bool upArrowDown = false;
        bool downArrowDown = false;
        bool leftArrowDown = false;
        bool rightArrowDown = false;

        SolidBrush blueBrush = new SolidBrush(Color.DodgerBlue);
        SolidBrush redBrush = new SolidBrush(Color.Red);
        SolidBrush whiteBrush = new SolidBrush(Color.White);
        SolidBrush neonBrush = new SolidBrush(Color.Turquoise);
        Pen redpen = new Pen(Color.Red, 6);
        Font screenFont = new Font("Consolas", 12);

        SoundPlayer wallHit = new SoundPlayer(Properties.Resources.Sample_0000);
        SoundPlayer ballHit = new SoundPlayer(Properties.Resources.Sample_0002);
        SoundPlayer goal = new SoundPlayer(Properties.Resources.Sample_0001);

        

        public Form1()
        {
            InitializeComponent();


        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            // on key down set bool to true
            switch (e.KeyCode)
            {
                case Keys.W:
                    wDown = true;
                    break;
                case Keys.S:
                    sDown = true;
                    break;
                case Keys.A:
                    aDown = true;
                    break;
                case Keys.D:
                    dDown = true;
                    break;
                case Keys.Up:
                    upArrowDown = true;
                    break;
                case Keys.Down:
                    downArrowDown = true;
                    break;
                case Keys.Left:
                    leftArrowDown = true;
                    break;
                case Keys.Right:
                    rightArrowDown = true;
                    break;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            //on key up set the bool to false
            switch (e.KeyCode)
            {
                case Keys.W:
                    wDown = false;
                    break;
                case Keys.S:
                    sDown = false;
                    break;
                case Keys.A:
                    aDown = false;
                    break;
                case Keys.D:
                    dDown = false;
                    break;
                case Keys.Up:
                    upArrowDown = false;
                    break;
                case Keys.Down:
                    downArrowDown = false;
                    break;
                case Keys.Left:
                    leftArrowDown = false;
                    break;
                case Keys.Right:
                    rightArrowDown = false;
                    break;
            }
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            //move ball
            ballX += ballXSpeed;
            ballY += ballYSpeed;

            //move player 1
            if (wDown == true && paddle1Y > 0 && aDown == false && dDown == false)
            {
                paddle1Y -= paddleSpeed;
            }

            if (sDown == true && paddle1Y < this.Height - paddleHeight && aDown == false && dDown == false)
            {
                paddle1Y += paddleSpeed;
            }

            if (aDown == true && sDown == false && wDown == false && paddle1X > 0)
            {
                paddle1X -= paddleSpeed;
            }

            if (dDown == true && sDown == false && wDown == false && paddle1X < 280)
            {
                paddle1X += paddleSpeed;
            }

            if (aDown == true && wDown == true && sDown == false && paddle1X > 0 && paddle1Y > 0)
            {
                paddle1X -= paddleSpeed;
                paddle1Y -= paddleSpeed;
            }

            if (aDown == true && sDown == true && wDown == false && paddle1X > 0 && paddle1Y < this.Height - paddleHeight)
            {
                paddle1X -= paddleSpeed;
                paddle1Y += paddleSpeed;
            }

            if (dDown == true && wDown == true && sDown == false && paddle1X < 280 && paddle1Y > 0)
            {
                paddle1X += paddleSpeed;
                paddle1Y -= paddleSpeed;
            }

            if (dDown == true && sDown == true && wDown == false && paddle1X < 280 && paddle1Y < this.Height - paddleHeight )
            {
                paddle1X += paddleSpeed;
                paddle1Y += paddleSpeed;
            }

            //move player 2
            if (upArrowDown == true && paddle2Y > 0 && leftArrowDown == false && rightArrowDown == false)
            {
                paddle2Y -= paddleSpeed;
            }

            if (downArrowDown == true && paddle2Y < this.Height - paddleHeight && leftArrowDown == false && rightArrowDown == false)
            {
                paddle2Y += paddleSpeed;
            }

            if (leftArrowDown == true && downArrowDown == false && upArrowDown == false && paddle2X > 295)
            {
                paddle2X -= paddleSpeed;
            }

            if (rightArrowDown == true && downArrowDown == false && upArrowDown == false && paddle2X < 580)
            {
                paddle2X += paddleSpeed;
            }

            if (leftArrowDown == true && upArrowDown == true && downArrowDown == false && paddle2X > 295 && paddle2Y > 0)
            {
                paddle2X -= paddleSpeed;
                paddle2Y -= paddleSpeed;
            }

            if (leftArrowDown == true && downArrowDown == true && upArrowDown == false && paddle2X > 295 && paddle2Y < this.Height - paddleHeight)
            {
                paddle2X -= paddleSpeed;
                paddle2Y += paddleSpeed;
            }

            if (rightArrowDown == true && upArrowDown == true && downArrowDown == false && paddle2X < 580 && paddle2Y > 0)
            {
                paddle2X += paddleSpeed;
                paddle2Y -= paddleSpeed;
            }

            if (rightArrowDown == true && downArrowDown == true && upArrowDown == false && paddle2X < 580 && paddle2Y < this.Height - paddleHeight)
            {
                paddle2X += paddleSpeed;
                paddle2Y += paddleSpeed;
            }

            //check if ball hit top or bottom wall and change direction
            if (ballY < 0 || ballY > this.Height - ballHeight)
            {
                
                ballYSpeed *= -1;

                wallHit.Play();
            }

            //create Rectangles of objects for collision detection
            Rectangle player1Rec = new Rectangle(paddle1X, paddle1Y, paddleWidth, paddleHeight);
            Rectangle player2Rec = new Rectangle(paddle2X, paddle2Y, paddleWidth, paddleHeight);
            Rectangle ballRec = new Rectangle(ballX, ballY, ballWidth, ballHeight);

            //create Rectangles for side wall collisions
            Rectangle wall1Rec = new Rectangle(wall1X, wall1y, wallWidth, wallHeight);
            Rectangle wall2Rec = new Rectangle(wall2X, wall2y, wallWidth, wallHeight);
            Rectangle wall3Rec = new Rectangle(wall3X, wall3y, wallWidth, wallHeight);
            Rectangle wall4Rec = new Rectangle(wall4X, wall4y, wallWidth, wallHeight);

            //check if ball hits either paddle. If it does change the direction
            //and place the ball in front of the paddle hit
            if (player1Rec.IntersectsWith(ballRec))
            {
                

                //invert ball speed horizontaly
                ballXSpeed *= -1;

                //check if positive or negative in ball speeds and increase/decrease them acordingly
                if (ballXSpeed < 0)
                {
                    ballXSpeed--;
                }
                else
                {
                    ballXSpeed++;
                }
                if (ballYSpeed < 0)
                {
                    ballYSpeed--;
                }
                else
                {
                    ballYSpeed++;
                }

                ballX = paddle1X + paddleWidth + 1;

                ballHit.Play();
            }
            else if (player2Rec.IntersectsWith(ballRec))
            {
                //invert ball speed horizontaly
                ballXSpeed *= -1;

                //check if positive or negative in ball speeds and increase/decrease them acordingly
                if (ballXSpeed < 0)
                {
                    ballXSpeed--;
                }
                else
                {
                    ballXSpeed++;
                }
                if (ballYSpeed < 0)
                {
                    ballYSpeed--;
                }
                else
                {
                    ballYSpeed++;
                }
                ballX = paddle2X - ballWidth - 1;

                ballHit.Play();

            }

            //check if ball collides with a side wall if true change direction
            if (ballRec.IntersectsWith(wall1Rec) || ballRec.IntersectsWith(wall2Rec) || ballRec.IntersectsWith(wall3Rec) || ballRec.IntersectsWith(wall4Rec))
            {
                
                ballXSpeed *= -1;

                wallHit.Play();
            }

            //check if ball entered into the net on either side
                if (ballX < -10)
            {
                
                player2Score++;
                ballX = 275;
                ballY = 185;
                ballXSpeed = 0;
                ballYSpeed = 0;

                paddle1Y = 170;
                paddle2Y = 170;
                paddle1X = 30;
                paddle2X = 560;
                goal.PlaySync();
            }
            else if (ballX > 610)
            {
                
                player1Score++;

                ballX = 315;
                ballY = 185;
                ballXSpeed = 0;
                ballYSpeed = 0;

                paddle1Y = 170;
                paddle2Y = 170;
                paddle1X = 30;
                paddle2X = 560;
                goal.PlaySync();
            }

            // check score and stop game if either player is at 3
            if (player1Score == 3 || player2Score == 3)
            {
                gameTimer.Enabled = false;
            }

            Refresh();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            //draw field
            e.Graphics.FillRectangle(redBrush, 298, 0, 10, 600);
            e.Graphics.DrawEllipse(redpen, 254, 110, 100, 100);

            //draw nets
            e.Graphics.FillRectangle(neonBrush, wall1X, wall1y, wallWidth, wallHeight);
            e.Graphics.FillRectangle(neonBrush, wall2X, wall2y, wallWidth, wallHeight);
            e.Graphics.FillRectangle(neonBrush, wall3X, wall3y, wallWidth, wallHeight);
            e.Graphics.FillRectangle(neonBrush, wall4X, wall4y, wallWidth, wallHeight);

            //draw paddles
            e.Graphics.FillEllipse(blueBrush, paddle1X, paddle1Y, paddleWidth, paddleHeight);
            e.Graphics.FillEllipse(blueBrush, paddle2X, paddle2Y, paddleWidth, paddleHeight);

            //draw puck
            e.Graphics.FillEllipse(whiteBrush, ballX, ballY, ballWidth, ballHeight);

            //draw score
            e.Graphics.DrawString($"{player1Score}", screenFont, whiteBrush, 280, 10);
            e.Graphics.DrawString($"{player2Score}", screenFont, whiteBrush, 310, 10);
        }
    }
}
