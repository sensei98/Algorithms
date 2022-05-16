using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace ChutesAndLadders_WPF
{
    public class ChuteLadder
    {
        // ladder: end position will be higher than startposition
        // chute: end position will be lower than startposition
        public int StartPosition;
        public int EndPosition;

        public ChuteLadder(int start, int end)
        {
            StartPosition = start;
            EndPosition = end;
        }
    }

    public class ChutesAndLadders
    {
        private const int NumberOfPositions = 100;

        private List<ChuteLadder> chutesAndladders = new List<ChuteLadder>();
        private double[,] transitionMatrix = new double[NumberOfPositions + 1, NumberOfPositions + 1];
        public double[] Position = new double[NumberOfPositions + 1];
        private int nrOfMoves;

        public int NrOfMoves { get { return nrOfMoves; } }

        private BackgroundWorker myBackgroundWorker;
        private int workerSleep;

        // events
        public event EventHandler<EventArgs> AnimationStarted;
        public event EventHandler<EventArgs> AnimationChanged;
        public event EventHandler<EventArgs> AnimationCompleted;

        public int AnimationSpeed
        {
            // input must be 0 (= slow) .. 100 (= full speed)
            set { this.workerSleep = 1000 - 10 * value; }
        }

        public ChutesAndLadders()
        {
            InitChutesAndLadders();

            // initialize the transition matrix
            InitTransitionMatrix();

            // start at position 0
            Position[0] = 1;    // chance at 'off board position' is 100%
            nrOfMoves = 0;
        }

        public void Reset()
        {
            for (int i = 0; i < Position.Length; i++)
                Position[i] = 0;
            Position[0] = 1;    // chance at 'off board position' is 100%
            nrOfMoves = 0;
        }

        /// <summary>Create chutes and ladders.</summary>
        private void InitChutesAndLadders()
        {
            // ladders (all ending at higher position)
            chutesAndladders.Add(new ChuteLadder(1, 38));
            chutesAndladders.Add(new ChuteLadder(4, 14));
            chutesAndladders.Add(new ChuteLadder(9, 31));
            chutesAndladders.Add(new ChuteLadder(21, 42));
            chutesAndladders.Add(new ChuteLadder(28, 84));
            chutesAndladders.Add(new ChuteLadder(36, 44));
            chutesAndladders.Add(new ChuteLadder(51, 67));
            chutesAndladders.Add(new ChuteLadder(71, 91));
            chutesAndladders.Add(new ChuteLadder(80, 100));

            // chutes (all ending at lower position)
            chutesAndladders.Add(new ChuteLadder(98, 78));
            chutesAndladders.Add(new ChuteLadder(95, 75));
            chutesAndladders.Add(new ChuteLadder(93, 73));
            chutesAndladders.Add(new ChuteLadder(87, 24));
            chutesAndladders.Add(new ChuteLadder(64, 60));
            chutesAndladders.Add(new ChuteLadder(62, 19));
            chutesAndladders.Add(new ChuteLadder(56, 53));
            chutesAndladders.Add(new ChuteLadder(49, 11));
            chutesAndladders.Add(new ChuteLadder(48, 26));
            chutesAndladders.Add(new ChuteLadder(16, 6));
        }

        /// <summary>Initializes the transition matrix.</summary>
        private void InitTransitionMatrix()
        {
             // todo...
        }

        /// <summary>Calculates position chances after one (next) move.</summary>
        public void NextMove()
        {
            // todo...

            nrOfMoves++;
        }

        public void StartAnimation(int nrOfSteps)
        {
            // use a background worker
            myBackgroundWorker = new BackgroundWorker();
            myBackgroundWorker.WorkerReportsProgress = true;
            myBackgroundWorker.DoWork += worker_DoWork;
            myBackgroundWorker.ProgressChanged += worker_ProgressChanged;
            myBackgroundWorker.RunWorkerCompleted += worker_RunWorkerCompleted;
            myBackgroundWorker.RunWorkerAsync(nrOfSteps);
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            int nrOfSteps = (int)e.Argument;

            for (int i = 0; i < nrOfSteps; i++)
            {
                this.NextMove();

                // report 'cell is part of solution' to worker thread
                myBackgroundWorker.ReportProgress(0, null);
                System.Threading.Thread.Sleep(this.workerSleep);
            }
        }

        void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            OnAnimationChanged(e);
        }

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            OnAnimationCompleted(e);
        }

        protected virtual void OnAnimationStarted(EventArgs e)
        {
            EventHandler<EventArgs> handler = AnimationStarted;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnAnimationChanged(EventArgs e)
        {
            EventHandler<EventArgs> handler = AnimationChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnAnimationCompleted(EventArgs e)
        {
            EventHandler<EventArgs> handler = AnimationCompleted;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
}