using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
namespace JLib
{
    public enum JPlatformType
    {
        None = -1,
        OSXEditor = 0,
        OSXPlayer = 1,
        WindowsPlayer = 2,
        OSXWebPlayer = 3,
        OSXDashboardPlayer = 4,
        WindowsWebPlayer = 5,
        WindowsEditor = 7,
        IPhonePlayer = 8,
        PS3 = 9,
        XBOX360 = 10,
        Android = 11,
        NaCl = 12,
        LinuxPlayer = 13,
        FlashPlayer = 15,
        WebGLPlayer = 17,
        MetroPlayerX86 = 18,
        WSAPlayerX86 = 18,
        MetroPlayerX64 = 19,
        WSAPlayerX64 = 19,
        MetroPlayerARM = 20,
        WSAPlayerARM = 20,
        WP8Player = 21,
        BB10Player = 22,
        BlackBerryPlayer = 22,
        TizenPlayer = 23,
        PSP2 = 24,
        PS4 = 25,
        PSM = 26,
        XboxOne = 27,
        SamsungTVPlayer = 28,
        WiiU = 30,
        tvOS = 31
    }

    public enum DefaultEvent
    {
        None = 0,
        LoadScene = 1,
        IngameLoadingComplete,
        CompleteLoadScene,
        AddScene,
        UnloadScene,
        DoTween,
        ShowTooltip,
        HideTooltip,
        SetItemScrollRect,
        ShowEffect,
        AsycResourceLoad,
        EnterSmartObject
    }

    public enum VK_Enum
    {
        None = 0,
        VK_Forward,
        VK_Back,
        VK_Left,
        VK_Right,
        VK_Jump,
        VK_LeftDrag,
        VK_RightDrag,
        VK_UpDrag,
        VK_DownDrag,
        VK_Button1,
        VK_Button2,
        VK_Button3,
        VK_Button4,
    }



    public enum VK_State
    {
        None = 0,
        Down,
        Press,
        Up
    }

    public enum CharacterMethod
    {
        None = 0,
        Forward,
        Back,
        Left,
        Right,
        PitchClock,
        PitchCounterClock,
        YawClock,
        YawCounterClock,
        RollClock,
        RollCounterClock,
        Jump
    }

    public enum TweenMode
    {
        None = 0,
        Normal,
        Pingpong,
        Loop
    }

    /// <summary>
    /// 이곳에 값을 추가하거나 제거하여 ID를 만들 수 있다.
    /// </summary>
    public enum LibUIID
    {
        None = 0,
        ActionButtonScroolRect,
    }

}
