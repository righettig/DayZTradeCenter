namespace DayZTradeCenter.DomainModel.Identity.Migrations
{
    public static class DefaultUsers
    {
        /// <summary>
        /// Gets the admin.
        /// </summary>
        /// <value>
        /// The admin.
        /// </value>
        public static User Admin
        {
            get
            {
                return new User("Administrator", "00000000000000001", true);
            }
        }

        /// <summary>
        /// Gets the test user.
        /// </summary>
        /// <value>
        /// The test user.
        /// </value>
        public static User TestUser
        {
            get
            {
                return new User("TestUser", "00000000000000002");
            }
        }
    }

    public class User
    {
        #region Public properties

        public string UserName
        {
            get { return _userName; }
        }

        public string SteamId
        {
            get { return _steamId; }
        }

        public bool IsAdmin
        {
            get { return _isAdmin; }
        }

        #endregion
        
        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="steamId">The steam identifier.</param>
        /// <param name="isAdmin">if set to <c>true</c> [is admin].</param>
        public User(string userName, string steamId, bool isAdmin = false)
        {
            _userName = userName;
            _steamId = steamId;
            _isAdmin = isAdmin;
        }
        
        #region Private fields

        private readonly string _userName;
        private readonly string _steamId;
        private readonly bool _isAdmin;

        #endregion
    }
}