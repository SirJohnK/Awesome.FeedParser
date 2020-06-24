﻿using System.Net.Mail;

namespace Awesome.FeedParser.Models
{
    public class ITunesOwner
    {
        public string? Name { get; internal set; }
        public MailAddress? Email { get; internal set; }
    }
}