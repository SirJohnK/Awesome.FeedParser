using System.Collections.Generic;

namespace Awesome.FeedParser.Models.Media
{
    /// <summary>
    /// Media RSS Parser Feed Item Result Class.
    /// </summary>
    public class MediaItem
    {
        /// <summary>
        /// Internal list of media content for parser access
        /// </summary>
        internal List<MediaContent>? content;

        /// <summary>
        /// List of specific feed item media content information.
        /// </summary>
        public IReadOnlyList<MediaContent>? Content => content;

        /// <summary>
        /// Internal list of media groups for parser access
        /// </summary>
        internal List<MediaGroup>? groups;

        /// <summary>
        /// List of feed item media groups.
        /// </summary>
        public IReadOnlyList<MediaGroup>? Groups => groups;

        /// <summary>
        /// Supplemental feed item media information.
        /// </summary>
        public MediaInformation? Information { get; internal set; }

        #region internal

        /// <summary>
        /// Internal property for the current media group being parsed
        /// </summary>
        private MediaGroup? CurrentGroup { get; set; }

        /// <summary>
        /// Internal property for the current media content being parsed
        /// </summary>
        private MediaContent? CurrentContent { get; set; }

        /// <summary>
        /// Internal property for the current media information group being parsed
        /// </summary>
        internal MediaInformation CurrentInformation
        {
            get
            {
                if (CurrentContent != null)
                    return CurrentContent.Information ??= new MediaInformation();
                else if (CurrentGroup != null)
                    return CurrentGroup.Information ??= new MediaInformation();
                else
                    return Information ??= new MediaInformation();
            }
        }

        /// <summary>
        /// Internal method adding new media group to the current feed item being parsed.
        /// </summary>
        /// <remarks>
        /// CurrentGroup will be set to the new media group being added.
        /// </remarks>
        /// <returns>The new media group being added.</returns>
        internal MediaGroup AddGroup()
        {
            //Create, Save, Set as Current and Return New Media Group
            CurrentGroup = new MediaGroup();
            groups ??= new List<MediaGroup>();
            groups.Add(CurrentGroup);
            return CurrentGroup;
        }

        /// <summary>
        /// Internal method closing the current media group.
        /// </summary>
        internal void CloseGroup() => CurrentGroup = null;

        /// <summary>
        /// Internal method adding new media content to the current feed item being parsed.
        /// </summary>
        /// <param name="close">Flag if content will be closed directly. (CurrentContent will be cleared!)</param>
        /// <remarks>
        /// CurrentContent will be set to the new media content being added.
        /// </remarks>
        /// <returns>The new media group being added.</returns>
        internal MediaContent AddContent(bool close = false)
        {
            //Create, Save, Set as Current and Return New Media Content
            var newContent = new MediaContent();
            CurrentContent = close ? null : newContent;
            if (CurrentGroup != null)
            {
                CurrentGroup.content ??= new List<MediaContent>();
                CurrentGroup.content.Add(newContent);
            }
            else
            {
                content ??= new List<MediaContent>();
                content.Add(newContent);
            }
            return newContent;
        }

        /// <summary>
        /// Internal method closing the current media content.
        /// </summary>
        internal void CloseContent() => CurrentContent = null;

        #endregion internal
    }
}