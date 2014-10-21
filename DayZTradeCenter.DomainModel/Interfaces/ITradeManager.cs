using System.Collections.Generic;
using DayZTradeCenter.DomainModel.Entities;
using DayZTradeCenter.DomainModel.Services;

namespace DayZTradeCenter.DomainModel.Interfaces
{
    public interface ITradeManager
    {
        /// <summary>
        /// Gets the latest active trades ordered by creation date.
        /// </summary>
        /// <param name="count">How many results.</param>
        /// <returns>
        /// The latest active trades listed in reverse chronological order.
        /// </returns>
        IEnumerable<Trade> GetLatestTrades(int count = 12);

        /// <summary>
        /// Gets the hottest active trades ordered by number of offers.
        /// </summary>
        /// <param name="count">How many results.</param>
        /// <returns>
        /// The active trades listed by number of offers in descending order.
        /// </returns>
        IEnumerable<Trade> GetHottestTrades(int count = 12);

        /// <summary>
        /// Gets the active trades.
        /// </summary>
        IEnumerable<Trade> GetActiveTrades();

        /// <summary>
        /// Gets the active trades.
        /// </summary>
        /// <param name="params">The search parameters.</param>
        /// <returns>
        /// The active trades that satisfy the search parameters.
        /// </returns>
        /// <exception cref="System.NotSupportedException">
        /// The specified search type is not supported yet.
        /// </exception>
        IEnumerable<Trade> GetActiveTrades(SearchParams @params);

        /// <summary>
        /// Gets the trades owned by the user with the specified user id.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        IEnumerable<Trade> GetTradesByUser(string userId);

        /// <summary>
        /// Gets the trades which the user with the specified id has offered to.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        IEnumerable<Trade> GetOffersByUser(string userId);
        
        /// <summary>
        /// Gets the trade by identifier.
        /// </summary>
        /// <param name="tradeId">The trade identifier.</param>
        /// <returns>The trade with the specified id.</returns>
        Trade GetTradeById(int tradeId);

        /// <summary>
        /// Gets all items.
        /// </summary>
        IEnumerable<Item> GetAllItems();

        /// <summary>
        /// Determines whether the user with the specified use id can create a new trade.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        ///   <c>True</c> if the user can create a new trade, <c>false</c> otherwise.
        /// </returns>
        bool CanCreateTrade(string userId);

        /// <summary>
        /// Creates a new trade.
        /// </summary>
        /// <param name="have">The items for the "Have" section.</param>
        /// <param name="want">The items for the "Want" section.</param>
        /// <param name="isHardcore">if set to <c>true</c> the trade is for the hardcore public hive.</param>
        /// <param name="isExperimental">if set to <c>true</c> the trade is for the experimental public hive.</param>
        /// <param name="user">The user.</param>
        /// <returns>
        ///   <c>True</c> if the trade was successfully created, <c>false</c> otherwise.
        /// </returns>
        bool CreateNewTrade(
            IEnumerable<ItemViewModel> have, IEnumerable<ItemViewModel> want, 
            bool isHardcore, bool isExperimental,
            ApplicationUser user);

        /// <summary>
        /// Deletes a trade.
        /// </summary>
        /// <param name="tradeId">The trade identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        ///   <c>True</c> if the trade was successfully deleted, <c>false</c> otherwise.
        /// </returns>
        bool DeleteTrade(int tradeId, string userId);

        /// <summary>
        /// Adds an offer for the specified trade from the given user.
        /// </summary>
        /// <param name="tradeId">The trade identifier.</param>
        /// <param name="user">The user.</param>
        /// <returns>
        ///   <see cref="OfferResult.Success">If the operation is successful.</see>
        ///   <see cref="OfferResult.AlreadOffered">If the user has already offered for the same trade.</see>
        ///   <see cref="OfferResult.OwnerCannotOffer">If the user is the owner of the trade.</see>
        /// </returns>
        OfferResult Offer(int tradeId, ApplicationUser user);

        /// <summary>
        /// Withdraws the given user from specified trade.
        /// </summary>
        /// <param name="tradeId">The trade identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        ///   <c>True</c> if the offer was successfully removed, <c>false</c> otherwise.
        /// </returns>
        bool Withdraw(int tradeId, string userId);

        /// <summary>
        /// Marks the user with the specified user id as the winner for given trade.
        /// </summary>
        /// <param name="tradeId">The trade identifier.</param>
        /// <param name="userId">The user identifier of the winner.</param>
        /// <param name="currentUserId">The user identifier of the user who is invoking the operation.</param>
        /// <returns>
        ///   <c>True</c> if the operation was successful, <c>false</c> otherwise.
        /// </returns>
        bool ChooseWinner(int tradeId, string userId, string currentUserId);

        /// <summary>
        /// Marks the trade with the specified id as completed.
        /// </summary>
        /// <param name="tradeId">The trade identifier.</param>
        /// <param name="user">The user.</param>
        /// <returns>The updated trade object.</returns>
        /// <remarks>A request for feedback is added to the inbox of the specified user.</remarks>
        Trade MarkAsCompleted(int tradeId, ApplicationUser user);

        /// <summary>
        /// The specified user leaves a feedback score for the given trade.
        /// </summary>
        /// <param name="tradeId">The trade identifier.</param>
        /// <param name="score">The score.</param>
        /// <param name="user">The user.</param>
        /// <returns>
        ///   <see cref="LeaveFeedbackResult.Ok">If the operation is successful.</see>
        ///   <see cref="LeaveFeedbackResult.AlreadyLeft">If the user has already left a feedback for the trade.</see>
        ///   <see cref="LeaveFeedbackResult.Unauthorized">If the user is not authorized to leave a feedback for the trade.</see>
        /// </returns>
        LeaveFeedbackResult LeaveFeedback(int tradeId, int score, ApplicationUser user);
    }

    /// <summary>
    /// The search parameters.
    /// </summary>
    public class SearchParams
    {
        public int? ItemId { get; set; }
        public SearchTypes? Type { get; set; }
        public bool HardcoreOnly { get; set; }
    }

    public enum SearchTypes
    {
        Have,
        Want,
        Both
    }
}
