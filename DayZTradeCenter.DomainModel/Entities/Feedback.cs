using System;
using rg.Time;

namespace DayZTradeCenter.DomainModel.Entities
{
    public class Feedback
    {
        #region Ctors

        /// <summary>
        /// Initializes a new instance of the <see cref="Feedback"/> class.
        /// </summary>
        /// <param name="score">The score.</param>
        public Feedback(int score)
        {
            Timestamp = TimeProvider.Now;
            Score = score;
        }

        /// <summary>
        /// Required by EF.
        /// </summary>
        private Feedback()
        {
        }

        #endregion
        
        public int Id { get; set; }
        public DateTime Timestamp { get; private set; }
        public int Score { get; private set; }
    }
}