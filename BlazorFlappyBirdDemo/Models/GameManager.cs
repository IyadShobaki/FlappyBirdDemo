
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorFlappyBirdDemo.Models
{
    public class GameManager
    {
        private readonly int _gravity = 2;
        private int _count = 0;

        public event EventHandler MainLoopCompleted;
        public bool IsRunning { get; private set; } = false;
        public BirdModel Bird { get; private set; }
        public List<PipeModel> Pipes { get; private set; }
        public AnimalTypeEnum AnimalType { get; set; }

        public GameManager()
        {
            ResetGameObjects();
        }

        public async void MainLoop()
        {
            IsRunning = true;
            while (IsRunning)
            {
                MoveGameObjects();
                CheckForCollisions();
                ManagePipes();

                MainLoopCompleted?.Invoke(this, EventArgs.Empty);
                await Task.Delay(20);

            }
        }

        public void Jump()
        {
            if (IsRunning)
            {
                Bird.Jump();
            }
        }

        public void StartGame()
        {
            if (!IsRunning)
            {
                ResetGameObjects();
                MainLoop();
            }
        }
        void CheckForCollisions()
        {
            if (Bird.IsOnGround())
            {
                GameOver();
            }

            var centeredPipe = GetCenteredPipe();
            if (centeredPipe != null)
            {
                if ((centeredPipe is LowerPipe && Bird.DistanceFromGround < centeredPipe.Gap) ||
                    (centeredPipe is UpperPipe && Bird.DistanceFromGround > centeredPipe.Gap - 45))
                {

                    GameOver();
                }
            }

        }

        void ManagePipes()
        {
            _count++;
            if (!Pipes.Any() || _count == 250)//Pipes.Last().DistanceFromLeft <= 250)
            {
                GeneratePipe();
                _count = 0;
            }
            if (Pipes.First().DistanceFromLeft <= 0)
            {
                Pipes.Remove(Pipes.First());
            }
            if (Pipes.Skip(1).First().DistanceFromLeft <= 0)
            {
                Pipes.Remove(Pipes.Skip(1).First());
            }
        }
        public void MoveGameObjects()
        {
            Bird.Fall(_gravity);
            foreach (var pipe in Pipes)
            {
                pipe.Move();

            }
        }
        public void GameOver()
        {
            IsRunning = false;
        }
        private void GeneratePipe()
        {
            Pipes.Add(new LowerPipe());
            Pipes.Add(new UpperPipe());
        }

        private PipeModel GetCenteredPipe()
        {
            return Pipes.FirstOrDefault(p => p.IsCentered());
        }

        private void ResetGameObjects()
        {
            Bird = new BirdModel();
            Pipes = new List<PipeModel>();

        }
    }
}
