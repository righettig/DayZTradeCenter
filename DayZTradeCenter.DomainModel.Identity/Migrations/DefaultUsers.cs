using System.Collections.Generic;

namespace DayZTradeCenter.DomainModel.Identity.Migrations
{
    public static class DefaultUsers
    {
        static DefaultUsers()
        {
            Users = new List<User>
            {
                new User("Administrator", "00000000000000001", "798efd31-5288-4331-97a9-0cb59fca44f9", true)
                {
                    Description = "The administrator"
                },
                new User("TestUser1", "00000000000000002", "447b34b7-1f8d-41e1-b391-4b4fd7eef503")
                {
                    Description =
                        "A test user with a trade already created. This trade has received 2 offers from test user #2, #3"
                },
                new User("TestUser2", "00000000000000003", "d618f1ca-8aad-4b22-af9a-fa2cb13237b1")
                {
                    Description = "A test user who has offered to the trade created by test user #1"
                },
                new User("TestUser3", "00000000000000004", "b4d90414-04e3-4039-a72b-d7c80489fb8e")
                {
                    Description = "A test user who has offered to the trade created by test user #2"
                },
                new User("TestUser4", "00000000000000005", "32760e14-dc0d-4c29-b0b9-9dd7139e38ea")
                {
                    Description = "A user who has not yet done anything."
                },
            };
        }

        public static IEnumerable<User> All
        {
            get { return Users; }
        }

        #region Test users: Admin, TestUser1, TestUser2, TestUser3, TestUser4

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
                return Users[0];
            }
        }

        /// <summary>
        /// Gets the test user #1.
        /// </summary>
        /// <value>
        /// The test user #1.
        /// </value>
        public static User TestUser1
        {
            get
            {
                return Users[1];
            }
        }

        /// <summary>
        /// Gets the test user #2.
        /// </summary>
        /// <value>
        /// The test user #2.
        /// </value>
        public static User TestUser2
        {
            get
            {
                return Users[2];
            }
        }

        /// <summary>
        /// Gets the test user #3.
        /// </summary>
        /// <value>
        /// The test user #3.
        /// </value>
        public static User TestUser3
        {
            get
            {
                return Users[3];
            }
        }

        /// <summary>
        /// Gets the test user #4.
        /// </summary>
        /// <value>
        /// The test user #4.
        /// </value>
        public static User TestUser4
        {
            get
            {
                return Users[4];
            }
        }

        #endregion

        private static readonly List<User> Users;
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

        public string UserId
        {
            get { return _userId; }
        }

        public bool IsAdmin
        {
            get { return _isAdmin; }
        }

        public string Description { get; set; }

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="userId">The local id.</param>
        /// <param name="steamId">The steam identifier.</param>
        /// <param name="isAdmin">if set to <c>true</c> [is admin].</param>
        public User(string userName, string steamId, string userId, bool isAdmin = false)
        {
            _userName = userName;
            _steamId = steamId;
            _userId = userId;
            _isAdmin = isAdmin;
        }
        
        #region Private fields

        private readonly string _userName;
        private readonly string _steamId;
        private readonly string _userId;
        private readonly bool _isAdmin;

        #endregion
    }
}