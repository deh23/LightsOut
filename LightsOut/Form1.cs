using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LightsOut
{
    public partial class Form1 : Form
    {
        // Array of lights
        public bool[,] Lights = new bool[5, 5];
        // Array of buttons
        public Button[,] ButtonLights = new Button[5, 5];

        public Form1()
        {
            InitializeComponent();

            //Start the game.
            GameStart();
        }

        public void GameStart()
        {
            // Generate all the buttons.
            //Default to light off.
            for (int i = 0; i < ButtonLights.GetLength(1); i++)
            {
                for (int j = 0; j < ButtonLights.GetLength(0); j++)
                {
                    ButtonLights[i, j] = new Button();
                    ButtonLights[i, j].Size = new Size(25, 25);
                    ButtonLights[i, j].Click += light_Click;

                    //So we know where this button is inside the two arrays.
                    ButtonLights[i, j].Name = i.ToString() + j.ToString();

                    ButtonLights[i, j].Location = new Point(10 + (j * 25), (i * 20));
                    ButtonLights[i, j].BackColor = Color.DarkGreen;

                    Lights[i, j] = false;
                    Controls.Add(ButtonLights[i, j]);
                }
            }

            Random rnd = new Random();

            // Random number of lights to turn on between 1 and 10.
            for (int i = 0; i < rnd.Next(1, 10); i++)
            {
                int x = rnd.Next(0, ButtonLights.GetLength(1));
                int y = rnd.Next(0, ButtonLights.GetLength(0));

                //Change button status to light on.
                ChangeButtonStatus(ButtonLights[x, y], x, y);
            }
        }

        public void light_Click(object sender, EventArgs e)
        {
            Button b = sender as Button;

            //Get the position of the button.
            int i = (int)char.GetNumericValue(b.Name[0]);
            int j = (int)char.GetNumericValue(b.Name[1]);

            //Invert button clicked.
            HandleLightChanges(ButtonLights[i, j], i, j);

            //Has the player won yet.
            //Check if all lights are off
            if (GameStatus() == true)
            {
                //Let the user know they have won.
                MessageBox.Show(this, "You win!", "Winner!");
                Application.Exit();
            }
        }

        public void HandleLightChanges(object sender, int i, int j)
        {
            //Invert button clicked
            ChangeButtonStatus(ButtonLights[i, j], i, j);

            //Check button above status
            if (i > 0)
                ChangeButtonStatus(ButtonLights[i - 1, j], i - 1, j);

            //Check button below status
            if (i < (ButtonLights.GetLength(1) - 1))            
                ChangeButtonStatus(ButtonLights[i + 1, j], i + 1, j); 
            
            //Check button to the left status
            if (j > 0)
                ChangeButtonStatus(ButtonLights[i, j - 1], i, j - 1);
           
            //Check button to the right status
            if (j < (ButtonLights.GetLength(1) - 1))
                ChangeButtonStatus(ButtonLights[i, j + 1], i, j + 1);
        }

        public void ChangeButtonStatus(object sender, int i, int j)
        {
            //Object is a button.
            Button button = sender as Button;

            //Invert the bool to change light status.
            Lights[i, j] = !Lights[i, j];

            // If light is turned on
            if (Lights[i, j] == true)
                // Set colour to light green because the light is turned on.
                button.BackColor = Color.LightGreen;
            else
                // Set colour to dark green because the light is turned off.
                button.BackColor = Color.DarkGreen;

        }

        public bool GameStatus()
        {
            // Loop through all bools in bool array
            for (int i = 0; i < Lights.GetLength(1); i++)
            {
                for (int j = 0; j < Lights.GetLength(0); j++)
                {
                    // Check if light is on
                    if (Lights[i, j] == true)
                        //Game contiunes.
                        return false;
                }
            }

            //All lights are turned off.
            return true;
        }
    }
}
