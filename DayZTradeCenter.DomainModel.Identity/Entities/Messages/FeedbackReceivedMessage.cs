namespace DayZTradeCenter.DomainModel.Identity.Entities.Messages
{
    // bug: the text is null when read, 
    // the problem is that when read there's no data saved (score) to recreate the message from.
    public class FeedbackReceivedMessage : Message
    {
        public FeedbackReceivedMessage()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FeedbackReceivedMessage"/> class.
        /// </summary>
        /// <param name="score">The score.</param>
        public FeedbackReceivedMessage(int score)
            : base("You've received a feedback score of " + score)
        {
        }

        public override string Subject
        {
            get { return "Feedback received"; }
        }
    }
}
