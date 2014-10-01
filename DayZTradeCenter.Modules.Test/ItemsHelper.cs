using System.Collections.Generic;
using System.Linq;
using DayZTradeCenter.DomainModel;

namespace DayZTradeCenter.Modules.Test
{
    /// <summary>
    /// Defines a bunch of test items.
    /// </summary>
    internal static class ItemsHelper
    {
        /// <summary>
        /// Gets the items.
        /// </summary>
        /// <value>
        /// The items.
        /// </value>
        public static IEnumerable<Item> Items
        {
            get
            {
                return _items ?? (_items = new[]
                {
                    new Item {Id = 0, Name = "Mosin"},
                    new Item {Id = 1, Name = "SKS"},
                    new Item {Id = 2, Name = "Tent"},
                    new Item {Id = 3, Name = "Pitchfork"},
                    new Item {Id = 4, Name = "Crowbar"}
                });
            }
        }

        #region Items

        /// <summary>
        /// Gets the mosin.
        /// </summary>
        /// <value>
        /// The mosin.
        /// </value>
        public static Item Mosin
        {
            get
            {
                return GetItemById(0);
            }
        }

        /// <summary>
        /// Gets the SKS.
        /// </summary>
        /// <value>
        /// The SKS.
        /// </value>
        public static Item SKS
        {
            get
            {
                return GetItemById(1);
            }
        }

        /// <summary>
        /// Gets the tent.
        /// </summary>
        /// <value>
        /// The tent.
        /// </value>
        public static Item Tent
        {
            get
            {
                return GetItemById(2);
            }
        }

        /// <summary>
        /// Gets the pitchfork.
        /// </summary>
        /// <value>
        /// The pitchfork.
        /// </value>
        public static Item Pitchfork
        {
            get
            {
                return GetItemById(3);
            }
        }

        /// <summary>
        /// Gets the crowbar.
        /// </summary>
        /// <value>
        /// The crowbar.
        /// </value>
        public static Item Crowbar
        {
            get
            {
                return GetItemById(4);
            }
        }

        #endregion

        /// <summary>
        /// Gets the item with the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The <see cref="Item"/> with the given identifier.</returns>
        private static Item GetItemById(int id)
        {
            return Items.FirstOrDefault(i => i.Id == id);
        }

        private static Item[] _items;
    }
}
