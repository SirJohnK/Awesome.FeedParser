using Awesome.FeedParser.Interfaces;
using Awesome.FeedParser.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml;

namespace Awesome.FeedParser.Parsers
{
    internal sealed class AtomParser : BaseParser
    {
        //Atom Namespace URI
        public static string Namespace { get; } = @"http://www.w3.org/2005/Atom";

        public static Lazy<IParser> Instance { get; } = new Lazy<IParser>(() => new AtomParser());

        private AtomParser()
        {
        }

        public override Task<bool> Parse(Stack<NodeInformation> parent, XmlReader reader, Feed feed, bool root = true)
        {
            return Task.FromResult(false);
        }
    }
}