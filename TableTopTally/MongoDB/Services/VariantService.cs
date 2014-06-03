/* VariantService.cs
* 
* Purpose: A class with methods for CRUDing a Game's variant documents in MongoDB
* 
* Revision History:
*      Drew Matheson, 2014.05.29: Created
*/

using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using TableTopTally.Models;

namespace TableTopTally.MongoDB.Services
{
    /// <summary>
    ///     Provides methods for CRUDing game variants in a mongoDB database
    /// </summary>
    public class VariantService : IVariantService
    {
        private readonly MongoCollection<Game> gamesCollection;

        /// <summary>
        ///     Initializes a new instance of the VariantService class
        /// </summary>
        public VariantService()
        {
            gamesCollection = MongoHelper.GetTableTopCollection<Game>();
        }

        /// <summary>
        ///     Creates a variant that belongs to the specified game
        /// </summary>
        /// <param name="gameId">ObjectId of the game to add to</param>
        /// <param name="variant">Variant to be added</param>
        /// <returns>Returns a bool representing if the creation completed successfully</returns>
        public bool Create(ObjectId gameId, Variant variant)
        {
            return !gamesCollection.Update(
                Query.EQ("_id", gameId),
                Update.PushWrapped("Variants", variant)).
                HasLastErrorMessage;
        }

        /// <summary>
        ///     Updates the variant that belongs to the specified game
        /// </summary>
        /// <param name="gameId">ObjectId of the game to update</param>
        /// <param name="variant">Variant representing the variant to update</param>
        /// <returns>Returns a bool representing if the edit completed successfully</returns>
        public bool Edit(ObjectId gameId, Variant variant)
        {
            // Bug: I think currently this would keep adding ScoreItems to the Variant
            return !gamesCollection.Update(
                Query.And(
                    Query.EQ("_id", gameId),
                    Query.ElemMatch("Variants", Query.EQ("_id", variant.Id))),
                Update.
                    Set("Variants.$.Name", variant.Name).
                    PushEachWrapped("Variants.$.ScoreItems", variant.ScoreItems)).
                HasLastErrorMessage;
        }

        /// <summary>
        ///     Removes the specified variant from the specified game
        /// </summary>
        /// <param name="gameId">ObjectId value of the game to find</param>
        /// <param name="variantId">ObjectId value of the variant to remove</param>
        /// <returns>Returns a bool representing if the deletion completed successfully</returns>
        public bool Delete(ObjectId gameId, ObjectId variantId)
        {
            return !gamesCollection.Update(
                Query.EQ("_id", gameId),
                Update.Pull("Variants", Query.EQ("_id", variantId))).
                HasLastErrorMessage;
        }

        /// <summary>
        ///     Gets the specified variant which belongs to the specified game
        /// </summary>
        /// <param name="gameUrl">Url value of the game to find</param>
        /// <param name="variantUrl">Url value of the variant to find</param>
        /// <returns>A Game object with the matching Variant</returns>
        public Game GetVariantByUrl(string gameUrl, string variantUrl)
        {
            return
                gamesCollection.Find(Query.EQ("Url", gameUrl)).
                    SetFields(
                        Fields.Include("_id", "Url").
                            ElemMatch("Variants", Query.EQ("Url", variantUrl))).
                    Single();
        }

        /// <summary>
        ///     Gets the specified variant which belongs to the specified game
        /// </summary>
        /// <param name="gameId">ObjectId of the Game to find</param>
        /// <param name="variantId">ObjectId of the variant to find</param>
        /// <returns>A Game object with the matching Variant</returns>
        public Game GetVariantById(ObjectId gameId, ObjectId variantId)
        {
            // ElemMatch returns only the matching element from the array instead of all elements
            return
                gamesCollection.Find(Query.EQ("_id", gameId)).
                    SetFields(
                        Fields.Include("_id", "Url").
                            ElemMatch("Variants", Query.EQ("_id", variantId))).
                    Single();

            // Unsure: Other option, but it seems redundant to me
            //return
            //    gamesCollection.Find(
            //        Query.And(
            //            Query.EQ("_id", gameId), 
            //            Query.ElemMatch("Variant", Query.EQ("_id", variantId)))).
            //        SetFields(
            //            Fields.Include("_id", "Url").
            //            ElemMatch("Variants", Query.EQ("_id", variantId))).
            //        Single();
        }

        /// <summary>
        ///     Gets all of the variants of the specified game
        /// </summary>
        /// <param name="gameUrl">Url value of the game to find</param>
        /// <returns>An IOrderedEnumerable of the variants ordered by Name</returns>
        public IEnumerable<Variant> GetVariants(string gameUrl)
        {
            Game game = gamesCollection.Find(Query.EQ("Url", gameUrl)).
                SetFields(Fields.Include("Variants")).
                Single();

            return game.Variants.OrderBy(g => g.Name);
        }
    }
}