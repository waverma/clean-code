﻿using System.Collections.Generic;

namespace Markdown
{
    internal interface ITokenParser
    {
        public TagRules GetRules();

        string ReplaceTokens(string text, IEnumerable<TokenSegment> segments, ITagTranslator translator);

        TokenInfoCollection FindAllTokens(string paragraph);
    }
}