﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Markdown
{
    public class Md
    {
        private readonly ITokenParser parser;

        public Md()
        {
            var token = Token.GetPairToken("_");

            parser = TokenParserConfigurator
                .CreateTokenParser()
                .SetShieldingSymbol('\\')
                .AddToken(Token.GetSymmetricToken("_")).That
                    .CanBeNestedIn(Token.GetSymmetricToken("__")).And
                    .CantIntersect()
                .AddToken(Token.GetSymmetricToken("__")).That
                    .CantBeNestedIn(Token.GetSymmetricToken("_")).And
                    .CantIntersect()
                .AddToken(Token.GetSingleToken("#")).That
                    .CanIntersectWithAnyTokens().And
                    .CanBeNestedInAnyTokens()
                .Configure();
        }

        public string Render(string input)
        {
            var paragraphs = input.Split('\n');
            var parsedText = new StringBuilder();
            
            foreach (var paragraph in paragraphs)
            {
                parser.ParseParagraph(paragraph);

                var tokenSegments = parser
                    .FindAllTokens()
                    .Validate()
                    .GroupToDictionariesBy(x => x.Value.Token.ToString())
                    .Select(g => parser.GetTokensSegments(g))
                    .ForEachPairs(parser.ValidatePairSets)
                    .Aggregate((f, s) => f.Union(s));

                parsedText.Append(parser.ReplaceTokens(tokenSegments, null));
            }
    
            return parsedText.ToString();
        }
    }

    internal static class DictionaryExc
    {
        internal static Dictionary<int, TokenInfo> Validate(this Dictionary<int, TokenInfo> source)
        {
            return source
                .Where(x => x.Value.Valid)
                .ToDictionary(x => x.Key, x => x.Value);
        }

        internal static IEnumerable<Dictionary<TKey, TValue>> GroupToDictionariesBy<TKey, TValue, TGroup>(
            this Dictionary<TKey, TValue> source, 
            Func<KeyValuePair<TKey, TValue>, TGroup> groupFunc)
        {
            throw new NotImplementedException();
        }

        internal static IEnumerable<TSource> ForEachPairs<TSource>(
            this IEnumerable<TSource> sources,
            Func<(TSource, TSource), (TSource, TSource)> action)
        {
            throw new NotImplementedException();
        }
    }
}