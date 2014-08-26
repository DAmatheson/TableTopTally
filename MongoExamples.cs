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
    return
        gamesCollection.Find(
            Query.And(
                Query.EQ("_id", gameId), 
                Query.ElemMatch("Variant", Query.EQ("_id", variantId)))).
            SetFields(
                Fields.Include("_id", "Url").
                ElemMatch("Variants", Query.EQ("_id", variantId))).
            Single();
}

// Limit return results to a single document from an array
public Game GetVariantByUrl(string gameUrl, string variantUrl)
{
    return
        gamesCollection.Find(Query.EQ("Url", gameUrl)).
            SetFields(
                Fields.Include("_id", "Url").
                    ElemMatch("Variants", Query.EQ("Url", variantUrl))).
            Single();
}


// Update an document within an array
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