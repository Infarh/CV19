﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace CV19.Infrastructure.Extensions
{
    [SuppressMessage("ReSharper", "InconsistentNaming"), SuppressMessage("ReSharper", "IdentifierTypo"), SuppressMessage("ReSharper", "UnusedMember.Global")]
    public enum SC : uint
    {
        SIZE = 0xF000,
        MOVE = 0xF010,
        MINIMIZE = 0xF020,
        MAXIMIZE = 0xF030,
        NEXTWINDOW = 0xF040,
        PREVWINDOW = 0xF050,
        CLOSE = 0xF060,
        VSCROLL = 0xF070,
        HSCROLL = 0xF080,
        MOUSEMENU = 0xF090,
        KEYMENU = 0xF100,
        ARRANGE = 0xF110,
        RESTORE = 0xF120,
        TASKLIST = 0xF130,
        SCREENSAVE = 0xF140,
        HOTKEY = 0xF150,
        #region WINVER >= 0x0400 - Win95
        //#if(WINVER >= 0x0400) //Win95
        DEFAULT = 0xF160,
        MONITORPOWER = 0xF170,
        CONTEXTHELP = 0xF180,
        SEPARATOR = 0xF00F,
        //#endif /* WINVER >= 0x0400 */ 
        #endregion

        #region WINVER >= 0x0600 - Vista
        //#if(WINVER >= 0x0600) //Vista
        ISSECURE = 0x00000001,
        //#endif /* WINVER >= 0x0600 */ 
        #endregion

        [Obsolete]
        ICON = MINIMIZE,
        [Obsolete]
        ZOOM = MAXIMIZE,
    }
}
