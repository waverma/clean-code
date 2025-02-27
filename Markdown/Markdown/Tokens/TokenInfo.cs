﻿namespace Markdown
{
    internal record TokenShellStatus
    {
        public bool IsFrontShellNeed { get; set; }
        public bool IsEndShellNeed { get; set; }
        public Tag ShellTag { get; }

        public TokenShellStatus(Tag shellTag)
        {
            ShellTag = shellTag;
        }
        
        public void Deconstruct(out Tag shellTag, out bool isFrontShellNeed, out bool isEndShellNeed)
        {
            (shellTag, isFrontShellNeed, isEndShellNeed) = (ShellTag, IsFrontShellNeed, IsEndShellNeed);
        }
    }
    
    internal record TokenInfo
    {
        public int Position { get; }
        public string Token { get; }
        public bool CloseValid { get; }
        public bool OpenValid { get; }
        public bool WordPartPlaced { get; }
        public bool Valid { get; set; }

        public TokenShellStatus? ShellStatus { get; set; }

        public Tag Tag => TagFactory.GetTagByChars(Token);

        public TokenInfo(int position, string token, bool closeValid = false, bool openValid = false, bool wordPartPlaced = false, bool valid = false)
        {
            Position = position;
            Token = token;
            CloseValid = closeValid;
            OpenValid = openValid;
            WordPartPlaced = wordPartPlaced;
            Valid = valid;
        }
        
        public void Deconstruct(out int position, out string token, out bool closeValid, out bool openValid, out bool wordPartPlaced, out bool valid)
        {
            (position, token, closeValid, openValid, wordPartPlaced, valid) = (Position, Token, CloseValid, OpenValid, WordPartPlaced, Valid);
        }

        public TokenInfo WithPosition(int position)
        {
            return new TokenInfo(position, Token, CloseValid, OpenValid, WordPartPlaced, Valid);
        }
    }
}