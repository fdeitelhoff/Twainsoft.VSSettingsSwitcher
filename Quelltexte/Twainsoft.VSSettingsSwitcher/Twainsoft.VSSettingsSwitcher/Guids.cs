// Guids.cs
// MUST match guids.h
using System;

namespace Twainsoft.Twainsoft_VSSettingsSwitcher
{
    static class GuidList
    {
        public const string guidTwainsoft_VSSettingsSwitcherPkgString = "3b3448bb-4c9d-4588-ac9d-edb469bacf5e";
        public const string guidTwainsoft_VSSettingsSwitcherCmdSetString = "f1b02c3d-e0d2-4f0d-b1bf-ea4af8d58313";
        public const string guidToolWindowPersistanceString = "3ef26d8c-7d7d-4e79-835c-2ada89334dbe";

        public static readonly Guid guidTwainsoft_VSSettingsSwitcherCmdSet = new Guid(guidTwainsoft_VSSettingsSwitcherCmdSetString);
    };
}