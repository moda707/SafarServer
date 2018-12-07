using System;
using System.Collections.Generic;
using System.Text;

namespace SafarObjects.ChatsClasses
{
    public enum ChatMessageType
    {
        Text = 0,
        TextImage = 1,
        Image = 2
    }

    public enum ChatMessageStatus
    {
        Original = 0,
        Edited = 1,
        Deleted = 2
    }

    public enum ChatMessageSeenStatus
    {
        Unseen = 0,
        Seen = 1
    }
}
