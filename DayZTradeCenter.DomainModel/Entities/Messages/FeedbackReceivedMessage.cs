using System;

namespace DayZTradeCenter.DomainModel.Entities.Messages
{
    public class FeedbackReceivedMessage : Message
    {
        #region Ctors

        /// <summary>
        /// Initializes a new instance of the <see cref="FeedbackReceivedMessage"/> class.
        /// </summary>
        /// <param name="feedback">The feedback.</param>
        /// <exception cref="System.ArgumentNullException">feedback</exception>
        public FeedbackReceivedMessage(Feedback feedback)
        {
            if (feedback == null)
            {
                throw new ArgumentNullException("feedback");
            }

            Feedback = feedback;
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="FeedbackReceivedMessage"/> class.
        /// </summary>
        /// <remarks>
        /// Required by EF.
        /// It is 'protected' as otherwise (i.e. 'private') the lazy loading does not work.
        /// </remarks>
        protected FeedbackReceivedMessage()
        {
        }

        #endregion

        #region Public properties

        public override string Subject
        {
            get { return "Feedback received"; }
        }

        public override string Text
        {
            get { return "You've received a feedback score of " + Feedback.Score; }
        }

        public virtual Feedback Feedback { get; private set; }

        #endregion
    }
}
