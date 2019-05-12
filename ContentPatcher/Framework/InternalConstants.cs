using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using StardewValley.GameData.Crafting;
using StardewValley.GameData.Movies;

namespace ContentPatcher.Framework
{
    /// <summary>Internal constant values.</summary>
    public static class InternalConstants
    {
        /*********
        ** Fields
        *********/
        /// <summary>The character used as an input argument separator.</summary>
        public const string InputArgSeparator = ":";

        /// <summary>The character used as a separator between the mod ID and token name for a mod-provided token.</summary>
        public const string ModTokenSeparator = "/";

        /// <summary>A prefix for player names when specified as an input argument.</summary>
        public const string PlayerNamePrefix = "@";


        /*********
        ** Methods
        *********/
        /// <summary>Get the key for a list asset entry.</summary>
        /// <typeparam name="TValue">The list value type.</typeparam>
        /// <param name="entity">The entity whose ID to fetch.</param>
        public static string GetListAssetKey<TValue>(TValue entity)
        {
            switch (entity)
            {
                case ConcessionTaste taste:
                    return taste.Name;

                case MovieCharacterReaction reaction:
                    return reaction.NPCName;

                case TailorItemRecipe recipe:
                    IList<string> keyParts = new List<string>();
                    if (recipe.FirstItemTags.Any())
                        keyParts.Add($"L:{string.Join(",", recipe.FirstItemTags)}");
                    if (recipe.SecondItemTags.Any())
                        keyParts.Add($"R:{string.Join(",", recipe.SecondItemTags)}");
                    return string.Join("|", keyParts);

                default:
                    PropertyInfo property = entity.GetType().GetProperty("ID");
                    if (property != null)
                        return property.GetValue(entity)?.ToString();

                    throw new NotSupportedException($"No ID implementation for list asset value type {typeof(TValue).FullName}.");
            }
        }
    }
}
