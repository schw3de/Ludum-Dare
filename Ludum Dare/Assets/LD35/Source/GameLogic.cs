using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using schw3de.Base.Source;
using UnityEngine;

namespace schw3de.LD35.Source
{
    public class GameLogic : Singleton<GameLogic>
    {
        [SerializeField]
        private int xSize = 11;

        [SerializeField]
        private int ySize = 11;

        private GameState gameState;

        private bool isInProcess;

        private IList<ILevel> levels;

        private ILevel currentLevel;

        private IList<Wall> walls;

        private int lives = 3;

        protected override void Awake()
        {
            base.Awake();
            this.StartGame();
        }

        private void StartGame()
        {
            this.gameState = GameState.MainMenu;
            this.lives = 3;

            this.levels = new List<ILevel>();

            this.levels.Add(new Level1());
            this.currentLevel = this.levels.First();
        }

        private void Start()
        {
            UserInput.Instance.UserInteracted = this.UserInteracted;
            Hud.Instance.HudEventCallback = this.HudEventCallback;
            Hud.Instance.SetState(HudState.Intro);
            CubeMap.Instance.CubeGotHit = this.CubeGotHit;
        }

        private void CubeGotHit()
        {
            Hud.Instance.RemoveLife();
            this.lives -= 1;

            if(this.lives == 0)
            {
                this.GameOver();
            }
        }

        private void GameOver()
        {
            this.gameState = GameState.GameOver;
            Hud.Instance.ShowGameOver();
        }

        private void GameWon()
        {
            this.gameState = GameState.GameOver;
            Hud.Instance.ShowWon();
        }

        private void Update()
        {
            if (this.isInProcess)
                return;

            this.isInProcess = true;

            switch (this.gameState)
            {
                case GameState.StartLevel:
                    break;
                case GameState.GameLoop:
                    break;
                case GameState.Intro:
                    break;
                case GameState.MainMenu:
                    break;
                default:
                    break;
            }
        }

        private void UserInteracted(UserInputType ev)
        {
            switch (this.gameState)
            {
                case GameState.GameLoop:
                    this.HandleGameLoop(ev);
                    break;

                case GameState.Intro:
                    break;
                case GameState.MainMenu:
                    this.HandleMainMenu(ev);
                    break;

                case GameState.GameOver:

                    if(ev == UserInputType.Space)
                    {
                        CubeMap.Instance.Reset();
                        this.StartGame();
                        Hud.Instance.ShowStartMenu(true);
                    }

                    break;

                default:
                    break;
            }
        }

        private void HandleMainMenu(UserInputType ev)
        {
            if (ev != UserInputType.Space)
                return;

            this.gameState = GameState.StartLevel;
            CubeMap.Instance.CreateCubes(this.xSize, this.ySize, this.currentLevel.GetStartup());
            this.walls = this.currentLevel.GetWalls();
            WallCreator.Instance.Create(this.xSize, this.ySize, this.walls.First(), this.WallFinished);
            Hud.Instance.ShowStartMenu(false);
            this.gameState = GameState.GameLoop;
            this.isInProcess = false;
        }

        private void WallFinished()
        {
            if (this.lives <= 0)
                return;

            CubeMap.Instance.ResetHits();
            this.walls.RemoveAt(0);

            if(this.walls.Any())
            {
                WallCreator.Instance.Create(this.xSize, this.ySize, this.walls.First(), this.WallFinished);
            }
            else
            {
                this.GameWon();
            }
        }

        private void HandleGameLoop(UserInputType ev)
        {
            switch (ev)
            {
                case UserInputType.Up:
                    CubeMap.Instance.HandleDirection(CubeDirectionMovement.MoveUp);
                    break;

                case UserInputType.Down:
                    CubeMap.Instance.HandleDirection(CubeDirectionMovement.MoveDown);
                    break;

                case UserInputType.Left:
                    CubeMap.Instance.HandleDirection(CubeDirectionMovement.MoveLeft);
                    break;

                case UserInputType.Right:
                    CubeMap.Instance.HandleDirection(CubeDirectionMovement.MoveRight);
                    break;

                case UserInputType.ToggleCubeMapMode:

                    if (!CubeMap.Instance.IsSelectionValid())
                        break;

                    CubeMap.Instance.ToggleMode();
                    break;
                default:
                    break;
            }
        }

        private void HudEventCallback(HudEvent ev)
        {
            //switch (ev)
            //{
            //    case HudEvent.StartGame:
            //        Hud.Instance.SetState(HudState.GameLoop);
            //        CubeMap.Instance.CreateCubes(this.xSize, this.ySize);
            //        break;
            //    default:
            //        break;
            //}
        }
    }
}
