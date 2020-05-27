using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RiskOfDeduction.Domain;

namespace RiskOfDeduction.Drawing
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            HideScreens();
            MainMenu.InitializeGame(Game);
            MainMenu.Show();
        }

        private void On_GameStateChanged(GameState state)
        {
            switch (state)
            {
                case GameState.MainMenu:
                    HideScreens();
                    MainMenu.InitializeGame(Game);
                    MainMenu.Show();
                    MainMenu.Focus();
                    break;
                case GameState.Playing:
                    HideScreens();
                    GameWindow.InitializeGame(Game);
                    GameWindow.Show();
                    GameWindow.Focus();
                    break;
                case GameState.ChoosingLevel:
                    HideScreens();
                    ChoosingMenu.InitializeGame(Game);
                    ChoosingMenu.Show();
                    ChoosingMenu.Focus();
                    break;
            }
        }



        private void HideScreens()
        {
            GameWindow.Hide();
            MainMenu.Hide();
            ChoosingMenu.Hide();
        }
    }
}
