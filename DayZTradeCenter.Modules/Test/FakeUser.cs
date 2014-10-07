using DayZTradeCenter.DomainModel.Entities;

namespace DayZTradeCenter.Modules.Test
{
    public class FakeUser : ApplicationUser
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FakeUser"/> class.
        /// </summary>
        /// <param name="reputation">The reputation.</param>
        public FakeUser(float reputation)
        {
            _reputation = reputation;
        }

        public override float GetReputation()
        {
            return _reputation;
        }

        private readonly float _reputation;
    }
}