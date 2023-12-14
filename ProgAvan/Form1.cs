using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProgAvan
{
    public partial class Form1 : Form
    {

        List<int> numbers = new List<int> { 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8, 8 };
        string firstChoice;
        string secondChoice;
        int tries;
        List<PictureBox> pictures = new List<PictureBox>();
        PictureBox picA;
        PictureBox picB;
        int totaltime = 60;
        int countDownTime;
        bool gameOver = false;
        private int score = 0;
        private bool match;
    

        public Form1()
        {
            InitializeComponent();
            LoadPictures();

        }

        private void TimerEvent(object sender, EventArgs e)
        {
            countDownTime--;

            lblTimeleft.Text = "Tiempo restante : " + countDownTime;

            if (countDownTime < 1)
            {
                GameOver("Tiempo terminado! .  Perdiste  ");

                foreach (PictureBox x in pictures)
                {
                    if (x.Tag != null)
                    {
                        x.Image = Image.FromFile("pics/" + (string)x.Tag + ".png");
                    }
                }

            }

        }

        private void RestartGameEvent(object sender, EventArgs e)
        {
            RestartGame();
        }

        private void LoadPictures()
        {
            int leftPos = 80;
            int topPos = 80;
            int rows = 0;

            for (int i = 0; i < 16; i++)
            {
                PictureBox newPic = new PictureBox();
                newPic.Height = 100;
                newPic.Width = 100;
                newPic.BackColor = Color.LightGray;
                newPic.SizeMode = PictureBoxSizeMode.StretchImage;
                newPic.Click += NewPic_Click;
                pictures.Add(newPic);

                if (rows < 4)
                {
                    rows++;
                    newPic.Left = leftPos;
                    newPic.Top = topPos;
                    this.Controls.Add(newPic);
                    leftPos = leftPos + 140;
                }

                if (rows == 4)
                {
                    leftPos = 80;
                    topPos += 140;
                    rows = 0;

                }

                RestartGame();


            }

        }
        private void NewPic_Click(object sender, EventArgs e)
        {
            if (gameOver)
            {
                // No permitir el click si se termina el juego
                return;
            }
            if (firstChoice == null)
            {
                picA = sender as PictureBox;
                if (picA.Tag != null && picA.Image == null)
                {
                    picA.Image = Image.FromFile("pics/" + (string)picA.Tag + ".png");
                    firstChoice = (string)picA.Tag;
                }
            }
            else if (secondChoice == null)
            {
                picB = sender as PictureBox;
                if (picB.Tag != null && picB.Image == null)
                {
                    picB.Image = Image.FromFile("pics/" + (string)picB.Tag + ".png");
                    secondChoice = (string)picB.Tag;
                }
                // Para comprobar si coinciden
                match = firstChoice == secondChoice;
                if (match)
                {
                    score += 20;
                }
                else
                {
                    score -= 2;
                }

                // Actualizar la interfaz de usuario para mostrar el nuevo puntaje
                scoreLabel.Text = "Puntaje: " + score;
            }
            else
            {
                CheckPictures(picA, picB);
            }

        }





        private void RestartGame()
        {
            var randomList = numbers.OrderBy(X => Guid.NewGuid()).ToList();

            numbers = randomList;

            for (int i = 0; i < pictures.Count; i++)
            {
                pictures[i].Image = null;
                pictures[i].Tag = numbers[i].ToString();
            }
            tries = 0;
            lblStatus.Text = "Erradas : " + tries + " veces .";
            lblTimeleft.Text = "Tiempo restante : " + totaltime;
            gameOver = false;
            GameTimer.Start();
            countDownTime = totaltime;


        }

        private void CheckPictures(PictureBox A, PictureBox B)
        {

            if (firstChoice == secondChoice)
            {
                A.Tag = null;
                B.Tag = null;
                
              
            }
            else
            {
                tries++;
                lblStatus.Text = "Fallaste  " + tries + "   veces.";
            }
            firstChoice = null;
            secondChoice = null;

            foreach (PictureBox pics in pictures.ToList())
            {
                if (pics.Tag != null)
                {
                    pics.Image = null;
                }
            }

            if (pictures.All(o => o.Tag == pictures[0].Tag))
            {
                GameTimer.Stop();
                GameOver(" Buen trabajo. Has terminado ");
                MessageBox.Show("  Presiona para jugar de nuevo  ");

            }
        }
        
            private void GameOver(string msg)
        {
            GameTimer.Stop();
            gameOver = true;
            MessageBox.Show(msg + "  Presiona para jugar de nuevo  ");
            
        }

        private void lblTimeleft_Click(object sender, EventArgs e)
        {

        }

        private void lblStatus_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void StartGame(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
   
}
