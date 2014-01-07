Imports System.Runtime.InteropServices
Friend Class WinAPI

    Public Class WinMacro

        Public Shared Function MAKEWORD(ByVal a As Byte, ByVal b As Byte) As UInt16
            Return (a And &HFF) Or ((b And &HFF) << 8)
        End Function
        Public Shared Function MAKELONG(ByVal a As UInt16, ByVal b As UInt16) As UInt32
            Return (a And &HFFFF) Or ((b And &HFFFF) << 16)
        End Function
        Public Shared Function LOWORD(ByVal l As UInt32) As UInt16
            Return l And &HFFFF
        End Function
        Public Shared Function HIWORD(ByVal l As UInt32) As UInt16
            Return (l >> 16) And &HFFFF
        End Function
        Public Shared Function LOBYTE(ByVal w As UInt16) As Byte
            Return w And &HFF
        End Function
        Public Shared Function HIBYTE(ByVal w As UInt16) As Byte
            Return (w >> 8) And &HFF
        End Function

        Public Shared Function MAKEPOINT(ByVal lParam As IntPtr) As Drawing.Point

            MAKEPOINT.X = LOWORD(lParam)
            MAKEPOINT.Y = HIWORD(lParam)

        End Function

    End Class

    Public Const PM_REMOVE = &H1
    Public Enum WindowsMessages

        WM_ACTIVATE = &H6
        WM_ACTIVATEAPP = &H1C
        WM_AFXFIRST = &H360
        WM_AFXLAST = &H37F
        WM_APP = &H8000
        WM_ASKCBFORMATNAME = &H30C
        WM_CANCELJOURNAL = &H4B
        WM_CANCELMODE = &H1F
        WM_CAPTURECHANGED = &H215
        WM_CHANGECBCHAIN = &H30D
        WM_CHANGEUISTATE = &H127
        WM_CHAR = &H102
        WM_CHARTOITEM = &H2F
        WM_CHILDACTIVATE = &H22
        WM_CLEAR = &H303
        WM_CLOSE = &H10
        WM_COMMAND = &H111
        WM_COMPACTING = &H41
        WM_COMPAREITEM = &H39
        WM_CONTEXTMENU = &H7B
        WM_COPY = &H301
        WM_COPYDATA = &H4A
        WM_CREATE = &H1
        WM_CTLCOLORBTN = &H135
        WM_CTLCOLORDLG = &H136
        WM_CTLCOLOREDIT = &H133
        WM_CTLCOLORLISTBOX = &H134
        WM_CTLCOLORMSGBOX = &H132
        WM_CTLCOLORSCROLLBAR = &H137
        WM_CTLCOLORSTATIC = &H138
        WM_CUT = &H300
        WM_DEADCHAR = &H103
        WM_DELETEITEM = &H2D
        WM_DESTROY = &H2
        WM_DESTROYCLIPBOARD = &H307
        WM_DEVICECHANGE = &H219
        WM_DEVMODECHANGE = &H1B
        WM_DISPLAYCHANGE = &H7E
        WM_DRAWCLIPBOARD = &H308
        WM_DRAWITEM = &H2B
        WM_DROPFILES = &H233
        WM_ENABLE = &HA
        WM_ENDSESSION = &H16
        WM_ENTERIDLE = &H121
        WM_ENTERMENULOOP = &H211
        WM_ENTERSIZEMOVE = &H231
        WM_ERASEBKGND = &H14
        WM_EXITMENULOOP = &H212
        WM_EXITSIZEMOVE = &H232
        WM_FONTCHANGE = &H1D
        WM_GETDLGCODE = &H87
        WM_GETFONT = &H31
        WM_GETHOTKEY = &H33
        WM_GETICON = &H7F
        WM_GETMINMAXINFO = &H24
        WM_GETOBJECT = &H3D
        WM_GETTEXT = &HD
        WM_GETTEXTLENGTH = &HE
        WM_HANDHELDFIRST = &H358
        WM_HANDHELDLAST = &H35F
        WM_HELP = &H53
        WM_HOTKEY = &H312
        WM_HSCROLL = &H114
        WM_HSCROLLCLIPBOARD = &H30E
        WM_ICONERASEBKGND = &H27
        WM_IME_CHAR = &H286
        WM_IME_COMPOSITION = &H10F
        WM_IME_COMPOSITIONFULL = &H284
        WM_IME_CONTROL = &H283
        WM_IME_ENDCOMPOSITION = &H10E
        WM_IME_KEYDOWN = &H290
        WM_IME_KEYLAST = &H10F
        WM_IME_KEYUP = &H291
        WM_IME_NOTIFY = &H282
        WM_IME_REQUEST = &H288
        WM_IME_SELECT = &H285
        WM_IME_SETCONTEXT = &H281
        WM_IME_STARTCOMPOSITION = &H10D
        WM_INITDIALOG = &H110
        WM_INITMENU = &H116
        WM_INITMENUPOPUP = &H117
        WM_INPUTLANGCHANGE = &H51
        WM_INPUTLANGCHANGEREQUEST = &H50
        WM_KEYDOWN = &H100
        WM_KEYFIRST = &H100
        WM_KEYLAST = &H108
        WM_KEYUP = &H101
        WM_KILLFOCUS = &H8
        WM_LBUTTONDBLCLK = &H203
        WM_LBUTTONDOWN = &H201
        WM_LBUTTONUP = &H202
        WM_MBUTTONDBLCLK = &H209
        WM_MBUTTONDOWN = &H207
        WM_MBUTTONUP = &H208
        WM_MDIACTIVATE = &H222
        WM_MDICASCADE = &H227
        WM_MDICREATE = &H220
        WM_MDIDESTROY = &H221
        WM_MDIGETACTIVE = &H229
        WM_MDIICONARRANGE = &H228
        WM_MDIMAXIMIZE = &H225
        WM_MDINEXT = &H224
        WM_MDIREFRESHMENU = &H234
        WM_MDIRESTORE = &H223
        WM_MDISETMENU = &H230
        WM_MDITILE = &H226
        WM_MEASUREITEM = &H2C
        WM_MENUCHAR = &H120
        WM_MENUCOMMAND = &H126
        WM_MENUDRAG = &H123
        WM_MENUGETOBJECT = &H124
        WM_MENURBUTTONUP = &H122
        WM_MENUSELECT = &H11F
        WM_MOUSEACTIVATE = &H21
        WM_MOUSEFIRST = &H200
        WM_MOUSEHOVER = &H2A1
        WM_MOUSELAST = &H20D
        WM_MOUSELEAVE = &H2A3
        WM_MOUSEMOVE = &H200
        WM_MOUSEWHEEL = &H20A
        WM_MOUSEHWHEEL = &H20E
        WM_MOVE = &H3
        WM_MOVING = &H216
        WM_NCACTIVATE = &H86
        WM_NCCALCSIZE = &H83
        WM_NCCREATE = &H81
        WM_NCDESTROY = &H82
        WM_NCHITTEST = &H84
        WM_NCLBUTTONDBLCLK = &HA3
        WM_NCLBUTTONDOWN = &HA1
        WM_NCLBUTTONUP = &HA2
        WM_NCMBUTTONDBLCLK = &HA9
        WM_NCMBUTTONDOWN = &HA7
        WM_NCMBUTTONUP = &HA8
        WM_NCMOUSELEAVE = &H2A2
        WM_NCMOUSEMOVE = &HA0
        WM_NCPAINT = &H85
        WM_NCRBUTTONDBLCLK = &HA6
        WM_NCRBUTTONDOWN = &HA4
        WM_NCRBUTTONUP = &HA5
        WM_NEXTDLGCTL = &H28
        WM_NEXTMENU = &H213
        WM_NOTIFY = &H4E
        WM_NOTIFYFORMAT = &H55
        WM_NULL = &H0
        WM_PAINT = &HF
        WM_PAINTCLIPBOARD = &H309
        WM_PAINTICON = &H26
        WM_PALETTECHANGED = &H311
        WM_PALETTEISCHANGING = &H310
        WM_PARENTNOTIFY = &H210
        WM_PASTE = &H302
        WM_PENWINFIRST = &H380
        WM_PENWINLAST = &H38F
        WM_POWER = &H48
        WM_POWERBROADCAST = &H218
        WM_PRINT = &H317
        WM_PRINTCLIENT = &H318
        WM_QUERYDRAGICON = &H37
        WM_QUERYENDSESSION = &H11
        WM_QUERYNEWPALETTE = &H30F
        WM_QUERYOPEN = &H13
        WM_QUEUESYNC = &H23
        WM_QUIT = &H12
        WM_RBUTTONDBLCLK = &H206
        WM_RBUTTONDOWN = &H204
        WM_RBUTTONUP = &H205
        WM_RENDERALLFORMATS = &H306
        WM_RENDERFORMAT = &H305
        WM_SETCURSOR = &H20
        WM_SETFOCUS = &H7
        WM_SETFONT = &H30
        WM_SETHOTKEY = &H32
        WM_SETICON = &H80
        WM_SETREDRAW = &HB
        WM_SETTEXT = &HC
        WM_SETTINGCHANGE = &H1A
        WM_SHOWWINDOW = &H18
        WM_SIZE = &H5
        WM_SIZECLIPBOARD = &H30B
        WM_SIZING = &H214
        WM_SPOOLERSTATUS = &H2A
        WM_STYLECHANGED = &H7D
        WM_STYLECHANGING = &H7C
        WM_SYNCPAINT = &H88
        WM_SYSCHAR = &H106
        WM_SYSCOLORCHANGE = &H15
        WM_SYSCOMMAND = &H112
        WM_SYSDEADCHAR = &H107
        WM_SYSKEYDOWN = &H104
        WM_SYSKEYUP = &H105
        WM_TCARD = &H52
        WM_TIMECHANGE = &H1E
        WM_TIMER = &H113
        WM_UNDO = &H304
        WM_UNINITMENUPOPUP = &H125
        WM_USER = &H400
        WM_USERCHANGED = &H54
        WM_VKEYTOITEM = &H2E
        WM_VSCROLL = &H115
        WM_VSCROLLCLIPBOARD = &H30A
        WM_WINDOWPOSCHANGED = &H47
        WM_WINDOWPOSCHANGING = &H46
        WM_WININICHANGE = &H1A
        WM_XBUTTONDBLCLK = &H20D
        WM_XBUTTONDOWN = &H20B
        WM_XBUTTONUP = &H20C

    End Enum
    Public Enum WindowStylesEx As UInteger

        WS_EX_ACCEPTFILES = &H10
        WS_EX_APPWINDOW = &H40000
        WS_EX_CLIENTEDGE = &H200
        WS_EX_COMPOSITED = &H2000000
        WS_EX_CONTEXTHELP = &H400
        WS_EX_CONTROLPARENT = &H10000
        WS_EX_DLGMODALFRAME = &H1
        WS_EX_LAYERED = &H80000
        WS_EX_LAYOUTRTL = &H400000
        WS_EX_LEFT = &H0
        WS_EX_LEFTSCROLLBAR = &H4000
        WS_EX_LTRREADING = &H0
        WS_EX_MDICHILD = &H40
        WS_EX_NOACTIVATE = &H8000000
        WS_EX_NOINHERITLAYOUT = &H100000
        WS_EX_NOPARENTNOTIFY = &H4
        WS_EX_OVERLAPPEDWINDOW = WS_EX_WINDOWEDGE Or WS_EX_CLIENTEDGE
        WS_EX_PALETTEWINDOW = WS_EX_WINDOWEDGE Or WS_EX_TOOLWINDOW Or WS_EX_TOPMOST
        WS_EX_RIGHT = &H1000
        WS_EX_RIGHTSCROLLBAR = &H0
        WS_EX_RTLREADING = &H2000
        WS_EX_STATICEDGE = &H20000
        WS_EX_TOOLWINDOW = &H80
        WS_EX_TOPMOST = &H8
        WS_EX_TRANSPARENT = &H20
        WS_EX_WINDOWEDGE = &H100

    End Enum
    Public Enum WindowStyles As UInteger

        WS_BORDER = &H800000
        WS_CAPTION = &HC00000
        WS_CHILD = &H40000000
        WS_CLIPCHILDREN = &H2000000
        WS_CLIPSIBLINGS = &H4000000
        WS_DISABLED = &H8000000
        WS_DLGFRAME = &H400000
        WS_GROUP = &H20000
        WS_HSCROLL = &H100000
        WS_MAXIMIZE = &H1000000
        WS_MAXIMIZEBOX = &H10000
        WS_MINIMIZE = &H20000000
        WS_MINIMIZEBOX = &H20000
        WS_OVERLAPPED = &H0
        WS_OVERLAPPEDWINDOW = WS_OVERLAPPED Or WS_CAPTION Or WS_SYSMENU Or WS_SIZEFRAME Or WS_MINIMIZEBOX Or WS_MAXIMIZEBOX
        WS_POPUP = &H80000000UI
        WS_POPUPWINDOW = WS_POPUP Or WS_BORDER Or WS_SYSMENU
        WS_SIZEFRAME = &H40000
        WS_SYSMENU = &H80000
        WS_TABSTOP = &H10000
        WS_VISIBLE = &H10000000
        WS_VSCROLL = &H200000

    End Enum
    Public Enum ClassStyles As UInteger

        ByteAlignClient = &H1000
        ByteAlignWindow = &H2000
        ClassDC = &H40
        DoubleClicks = &H8
        DropShadow = &H20000
        GlobalClass = &H4000
        HorizontalRedraw = &H2
        NoClose = &H200
        OwnDC = &H20
        ParentDC = &H80
        SaveBits = &H800
        VerticalRedraw = &H1

    End Enum
    Public Enum ShowWindowCommands

        Hide = 0
        Normal = 1
        ShowMinimized = 2
        Maximize = 3
        ShowMaximized = 3
        ShowNoActivate = 4
        Show = 5
        Minimize = 6
        ShowMinNoActive = 7
        ShowNA = 8
        Restore = 9
        ShowDefault = 10
        ForceMinimize = 11

    End Enum
    Public Enum ImeFlags

        GCS_COMPREADSTR = &H1
        GCS_COMPREADATTR = &H2
        GCS_COMPREADCLAUSE = &H4
        GCS_COMPSTR = &H8
        GCS_COMPATTR = &H10
        GCS_COMPCLAUSE = &H20
        GCS_CURSORPOS = &H80
        GCS_DELTASTART = &H100
        GCS_RESULTREADSTR = &H200
        GCS_RESULTREADCLAUSE = &H400
        GCS_RESULTSTR = &H800
        GCS_RESULTCLAUSE = &H1000

    End Enum
    Public Enum VirtualKeys

        ' // UNASSIGNED // = &HFFFF0000    ' // [Modifiers] = -65536
        ' // UNASSIGNED // = &H0    ' // [None] = 000

        VK_LBUTTON = &H1        ' // [LButton] = 001
        VK_RBUTTON = &H2        ' // [RButton] = 002
        VK_CANCEL = &H3         ' // [Cancel] = 003
        VK_MBUTTON = &H4        ' // [MButton] = 004    ' NOT contiguous with L & RBUTTON
        VK_XBUTTON1 = &H5       ' // [XButton1] = 005   ' NOT contiguous with L & RBUTTON
        VK_XBUTTON2 = &H6       ' // [XButton2] = 006   ' NOT contiguous with L & RBUTTON
        ' // UNASSIGNED // = &H7    ' // ''UNASSIGNED = 007

        VK_BACK = &H8           ' // [Back] = 008
        VK_TAB = &H9        ' // [Tab] = 009
        ' // RESERVED // = &HA        ' // [LineFeed] = 010
        ' // RESERVED // = &HB        ' // ''UNASSIGNED = 011
        VK_CLEAR = &HC          ' // [Clear] = 012
        VK_RETURN = &HD         ' // [Return] = 013
        ' // UNDEFINED //       ' // [Enter] = 013
        VK_SHIFT = &H10         ' // [ShiftKey] = 016
        VK_CONTROL = &H11       ' // [ControlKey] = 017
        VK_MENU = &H12          ' // [Menu] = 018
        VK_PAUSE = &H13         ' // [Pause] = 019
        VK_CAPITAL = &H14       ' // [Capital] = 020
        ' // UNDEFINED //       ' // [CapsLock] = 020

        VK_HANGUL = &H15        ' // [HangulMode] = 021
        VK_HANGEUL = &H15       ' // [HanguelMode] = 021 ' old name (compatibility)
        VK_KANA = &H15          ' // [KanaMode] = 021
        VK_JUNJA = &H17         ' // [JunjaMode] = 023
        VK_FINAL = &H18         ' // [FinalMode] = 024
        VK_KANJI = &H19         ' // [KanjiMode] = 025
        VK_HANJA = &H19         ' // [HanjaMode] = 025

        VK_ESCAPE = &H1B        ' // [Escape] = 027

        VK_CONVERT = &H1C       ' // [IMEConvert] = 028
        VK_NONCONVERT = &H1D    ' // [IMENonconvert] = 029
        VK_ACCEPT = &H1E        ' // [IMEAccept] = 030
        VK_MODECHANGE = &H1F    ' // [IMEModeChange] = 031

        VK_SPACE = &H20         ' // [Space] = 032
        VK_PRIOR = &H21         ' // [Prior] = 033
        ' // UNDEFINED //       ' // [PageUp] = 033
        VK_NEXT = &H22          ' // [Next] = 034
        ' // UNDEFINED //       ' // [PageDown] = 034
        VK_END = &H23           ' // [End] = 035
        VK_HOME = &H24          ' // [Home] = 036

        VK_LEFT = &H25          ' // [Left] = 037
        VK_UP = &H26        ' // [Up] = 038
        VK_RIGHT = &H27         ' // [Right] = 039
        VK_DOWN = &H28          ' // [Down] = 040

        VK_SELECT = &H29        ' // [Select] = 041
        VK_PRINT = &H2A         ' // [Print] = 042
        VK_EXECUTE = &H2B       ' // [Execute] = 043
        VK_SNAPSHOT = &H2C      ' // [Snapshot] = 044
        ' // UNDEFINED //       ' // [PrintScreen] = 044
        VK_INSERT = &H2D        ' // [Insert] = 045
        VK_DELETE = &H2E        ' // [Delete] = 046
        VK_HELP = &H2F          ' // [Help] = 047

        VK_0 = &H30         ' // [D0] = 048
        VK_1 = &H31         ' // [D1] = 049
        VK_2 = &H32         ' // [D2] = 050
        VK_3 = &H33         ' // [D3] = 051
        VK_4 = &H34         ' // [D4] = 052
        VK_5 = &H35         ' // [D5] = 053
        VK_6 = &H36         ' // [D6] = 054
        VK_7 = &H37         ' // [D7] = 055
        VK_8 = &H38         ' // [D8] = 056
        VK_9 = &H39         ' // [D9] = 057

        ' // UNASSIGNED // = &H40 to &H4F (058 to 064)

        VK_A = &H41         ' // [A] = 065
        VK_B = &H42         ' // [B] = 066
        VK_C = &H43         ' // [C] = 067
        VK_D = &H44         ' // [D] = 068
        VK_E = &H45         ' // [E] = 069
        VK_F = &H46         ' // [F] = 070
        VK_G = &H47         ' // [G] = 071
        VK_H = &H48         ' // [H] = 072
        VK_I = &H49         ' // [I] = 073
        VK_J = &H4A         ' // [J] = 074
        VK_K = &H4B         ' // [K] = 075
        VK_L = &H4C         ' // [L] = 076
        VK_M = &H4D         ' // [M] = 077
        VK_N = &H4E         ' // [N] = 078
        VK_O = &H4F         ' // [O] = 079
        VK_P = &H50         ' // [P] = 080
        VK_Q = &H51         ' // [Q] = 081
        VK_R = &H52         ' // [R] = 082
        VK_S = &H53         ' // [S] = 083
        VK_T = &H54         ' // [T] = 084
        VK_U = &H55         ' // [U] = 085
        VK_V = &H56         ' // [V] = 086
        VK_W = &H57         ' // [W] = 087
        VK_X = &H58         ' // [X] = 088
        VK_Y = &H59         ' // [Y] = 089
        VK_Z = &H5A         ' // [Z] = 090

        VK_LWIN = &H5B          ' // [LWin] = 091
        VK_RWIN = &H5C          ' // [RWin] = 092
        VK_APPS = &H5D          ' // [Apps] = 093
        ' // RESERVED // = &H5E        ' // ''UNASSIGNED = 094
        VK_SLEEP = &H5F         ' // [Sleep] = 095

        VK_NUMPAD0 = &H60       ' // [NumPad0] = 096
        VK_NUMPAD1 = &H61       ' // [NumPad1] = 097
        VK_NUMPAD2 = &H62       ' // [NumPad2] = 098
        VK_NUMPAD3 = &H63       ' // [NumPad3] = 099
        VK_NUMPAD4 = &H64       ' // [NumPad4] = 100
        VK_NUMPAD5 = &H65       ' // [NumPad5] = 101
        VK_NUMPAD6 = &H66       ' // [NumPad6] = 102
        VK_NUMPAD7 = &H67       ' // [NumPad7] = 103
        VK_NUMPAD8 = &H68       ' // [NumPad8] = 104
        VK_NUMPAD9 = &H69       ' // [NumPad9] = 105

        VK_MULTIPLY = &H6A      ' // [Multiply] = 106
        VK_ADD = &H6B           ' // [Add] = 107
        VK_SEPARATOR = &H6C     ' // [Separator] = 108
        VK_SUBTRACT = &H6D      ' // [Subtract] = 109
        VK_DECIMAL = &H6E       ' // [Decimal] = 110
        VK_DIVIDE = &H6F        ' // [Divide] = 111

        VK_F1 = &H70        ' // [F1] = 112
        VK_F2 = &H71        ' // [F2] = 113
        VK_F3 = &H72        ' // [F3] = 114
        VK_F4 = &H73        ' // [F4] = 115
        VK_F5 = &H74        ' // [F5] = 116
        VK_F6 = &H75        ' // [F6] = 117
        VK_F7 = &H76        ' // [F7] = 118
        VK_F8 = &H77        ' // [F8] = 119
        VK_F9 = &H78        ' // [F9] = 120
        VK_F10 = &H79           ' // [F10] = 121
        VK_F11 = &H7A           ' // [F11] = 122
        VK_F12 = &H7B           ' // [F12] = 123

        VK_F13 = &H7C           ' // [F13] = 124
        VK_F14 = &H7D           ' // [F14] = 125
        VK_F15 = &H7E           ' // [F15] = 126
        VK_F16 = &H7F           ' // [F16] = 127
        VK_F17 = &H80           ' // [F17] = 128
        VK_F18 = &H81           ' // [F18] = 129
        VK_F19 = &H82           ' // [F19] = 130
        VK_F20 = &H83           ' // [F20] = 131
        VK_F21 = &H84           ' // [F21] = 132
        VK_F22 = &H85           ' // [F22] = 133
        VK_F23 = &H86           ' // [F23] = 134
        VK_F24 = &H87           ' // [F24] = 135

        ' // UNASSIGNED // = &H88 to &H8F (136 to 143)

        VK_NUMLOCK = &H90       ' // [NumLock] = 144
        VK_SCROLL = &H91        ' // [Scroll] = 145

        VK_OEM_NEC_EQUAL = &H92     ' // [NEC_Equal] = 146    ' NEC PC-9800 kbd definitions "=" key on numpad
        VK_OEM_FJ_JISHO = &H92      ' // [Fujitsu_Masshou] = 146    ' Fujitsu/OASYS kbd definitions "Dictionary" key
        VK_OEM_FJ_MASSHOU = &H93    ' // [Fujitsu_Masshou] = 147    ' Fujitsu/OASYS kbd definitions "Unregister word" key 
        VK_OEM_FJ_TOUROKU = &H94    ' // [Fujitsu_Touroku] = 148    ' Fujitsu/OASYS kbd definitions "Register word" key  
        VK_OEM_FJ_LOYA = &H95       ' // [Fujitsu_Loya] = 149    ' Fujitsu/OASYS kbd definitions "Left OYAYUBI" key 
        VK_OEM_FJ_ROYA = &H96       ' // [Fujitsu_Roya] = 150    ' Fujitsu/OASYS kbd definitions "Right OYAYUBI" key

        ' // UNASSIGNED // = &H97 to &H9F (151 to 159)

        ' NOTE :: &HA0 to &HA5 (160 to 165) = left and right Alt, Ctrl and Shift virtual keys.
        ' NOTE :: Used only as parameters to GetAsyncKeyState() and GetKeyState().
        ' NOTE :: No other API or message will distinguish left and right keys in this way.
        VK_LSHIFT = &HA0        ' // [LShiftKey] = 160
        VK_RSHIFT = &HA1        ' // [RShiftKey] = 161
        VK_LCONTROL = &HA2      ' // [LControlKey] = 162
        VK_RCONTROL = &HA3      ' // [RControlKey] = 163
        VK_LMENU = &HA4         ' // [LMenu] = 164
        VK_RMENU = &HA5         ' // [RMenu] = 165

        VK_BROWSER_BACK = &HA6      ' // [BrowserBack] = 166
        VK_BROWSER_FORWARD = &HA7   ' // [BrowserForward] = 167
        VK_BROWSER_REFRESH = &HA8   ' // [BrowserRefresh] = 168
        VK_BROWSER_STOP = &HA9      ' // [BrowserStop] = 169
        VK_BROWSER_SEARCH = &HAA    ' // [BrowserSearch] = 170
        VK_BROWSER_FAVORITES = &HAB ' // [BrowserFavorites] = 171
        VK_BROWSER_HOME = &HAC      ' // [BrowserHome] = 172

        VK_VOLUME_MUTE = &HAD       ' // [VolumeMute] = 173
        VK_VOLUME_DOWN = &HAE       ' // [VolumeDown] = 174
        VK_VOLUME_UP = &HAF     ' // [VolumeUp] = 175

        VK_MEDIA_NEXT_TRACK = &HB0  ' // [MediaNextTrack] = 176
        VK_MEDIA_PREV_TRACK = &HB1  ' // [MediaPreviousTrack] = 177
        VK_MEDIA_STOP = &HB2    ' // [MediaStop] = 178
        VK_MEDIA_PLAY_PAUSE = &HB3  ' // [MediaPlayPause] = 179

        VK_LAUNCH_MAIL = &HB4       ' // [LaunchMail] = 180
        VK_LAUNCH_MEDIA_SELECT = &HB5 ' // [SelectMedia] = 181
        VK_LAUNCH_APP1 = &HB6       ' // [LaunchApplication1] = 182
        VK_LAUNCH_APP2 = &HB7       ' // [LaunchApplication2] = 183
        ' // UNASSIGNED // = &HB8   ' // ''UNASSIGNED = 184
        ' // UNASSIGNED // = &HB9   ' // ''UNASSIGNED = 185

        VK_OEM_1 = &HBA         ' // [Oem1] = 186           ' ";:" for USA
        ' // UNDEFINED //       ' // [OemSemicolon] = 186       ' ";:" for USA
        VK_OEM_PLUS = &HBB      ' // [Oemplus] = 187        ' "+" any country
        VK_OEM_COMMA = &HBC     ' // [Oemcomma] = 188       ' "," any country
        VK_OEM_MINUS = &HBD     ' // [OemMinus] = 189       ' "-" any country
        VK_OEM_PERIOD = &HBE    ' // [OemPeriod] = 190      ' "." any country
        VK_OEM_2 = &HBF         ' // [Oem2] = 191           ' "/?" for USA
        ' // UNDEFINED //       ' // [OemQuestion] = 191    ' "/?" for USA
        ' // UNDEFINED //       ' // [Oemtilde] = 192       ' "'~" for USA
        VK_OEM_3 = &HC0         ' // [Oem3] = 192           ' "'~" for USA

        ' // RESERVED // = &HC1 to &HD7 (193 to 215)
        ' // UNASSIGNED // = &HD8 to &HDA (216 to 218)

        VK_OEM_4 = &HDB         ' // [Oem4] = 219           ' "[{" for USA
        ' // UNDEFINED //       ' // [OemOpenBrackets] = 219    ' "[{" for USA
        ' // UNDEFINED //       ' // [OemPipe] = 220        ' "\|" for USA
        VK_OEM_5 = &HDC         ' // [Oem5] = 220           ' "\|" for USA
        VK_OEM_6 = &HDD         ' // [Oem6] = 221           ' "]}" for USA
        ' // UNDEFINED //       ' // [OemCloseBrackets] = 221   ' "]}" for USA
        ' // UNDEFINED //       ' // [OemQuotes] = 222      ' "'"" for USA
        VK_OEM_7 = &HDE         ' // [Oem7] = 222           ' "'"" for USA
        VK_OEM_8 = &HDF         ' // [Oem8] = 223

        ' // RESERVED // = &HE0        ' // ''UNASSIGNED = 224
        VK_OEM_AX = &HE1        ' // [OEMAX] = 225          ' "AX" key on Japanese AX kbd
        ' // UNDEFINED //       ' // [OemBackslash] = 226       ' "<>" or "\|" on RT 102-key kbd
        VK_OEM_102 = &HE2       ' // [Oem102] = 226         ' "<>" or "\|" on RT 102-key kbd
        VK_ICO_HELP = &HE3      ' // [ICOHelp] = 227        ' Help key on ICO
        VK_ICO_00 = &HE4        ' // [ICO00] = 228          ' 00 key on ICO

        VK_PROCESSKEY = &HE5    ' // [ProcessKey] = 229
        VK_ICO_CLEAR = &HE6     ' // [ICOClear] = 230
        VK_PACKET = &HE7        ' // [Packet] = 231
        ' // UNASSIGNED // = &HE8   ' // ''UNASSIGNED = 232

        ' NOTE :: Nokia/Ericsson definitions
        VK_OEM_RESET = &HE9     ' // [OEMReset] = 233
        VK_OEM_JUMP = &HEA      ' // [OEMJump] = 234
        VK_OEM_PA1 = &HEB       ' // [OEMPA1] = 235
        VK_OEM_PA2 = &HEC       ' // [OEMPA2] = 236
        VK_OEM_PA3 = &HED       ' // [OEMPA3] = 237
        VK_OEM_WSCTRL = &HEE    ' // [OEMWSCtrl] = 238
        VK_OEM_CUSEL = &HEF     ' // [OEMCUSel] = 239
        VK_OEM_ATTN = &HF0      ' // [OEMATTN] = 240
        VK_OEM_FINISH = &HF1    ' // [OEMFinish] = 241
        VK_OEM_COPY = &HF2      ' // [OEMCopy] = 242
        VK_OEM_AUTO = &HF3      ' // [OEMAuto] = 243
        VK_OEM_ENLW = &HF4      ' // [OEMENLW] = 244
        VK_OEM_BACKTAB = &HF5       ' // [OEMBackTab] = 245

        VK_ATTN = &HF6          ' // [Attn] = 246
        VK_CRSEL = &HF7         ' // [Crsel] = 247
        VK_EXSEL = &HF8         ' // [Exsel] = 248
        VK_EREOF = &HF9         ' // [EraseEof] = 249
        VK_PLAY = &HFA          ' // [Play] = 250
        VK_ZOOM = &HFB          ' // [Zoom] = 251
        VK_NONAME = &HFC        ' // [NoName] = 252
        VK_PA1 = &HFD           ' // [Pa1] = 253
        VK_OEM_CLEAR = &HFE     ' // [OemClear] = 254

        ' // UNASSIGNED // = &HFFFF    ' // [KeyCode] = 65535
        ' // UNASSIGNED // = &H10000    ' // [Shift] = 65536
        ' // UNASSIGNED // = &H20000    ' // [Control] = 131072
        ' // UNASSIGNED // = &H40000    ' // [Alt] = 262144

    End Enum


    Public Structure MSG

        Public hWnd As IntPtr
        Public Msg As UInt32
        Public WParam As IntPtr
        Public LParam As IntPtr
        Public Time As UInt32
        Public PT As Drawing.Point

    End Structure
    <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Auto)> _
    Public Structure WNDCLASSEX

        Public cbSize As UInteger
        Public style As ClassStyles
        '<MarshalAs(UnmanagedType.FunctionPtr)>
        Public lpfnWndProc As IntPtr
        Public cbClsExtra As Integer
        Public cbWndExtra As Integer
        Public hInstance As IntPtr
        Public hIcon As IntPtr
        Public hCursor As IntPtr
        Public hbrBackground As IntPtr
        Public lpszMenuName As IntPtr
        Public lpszClassName As IntPtr
        Public hIconSm As IntPtr

        ' Use this function to make a new one with cbSize already filled in.
        ' For example:
        'Dim WndClss As WNDCLASSEX = WNDCLASSEX.GetNew()
        Shared Function MakeNew()
            Dim nw As New WNDCLASSEX
            nw.cbSize = Marshal.SizeOf(GetType(WNDCLASSEX))
            Return nw
        End Function

    End Structure



    <DllImport("kernel32", CharSet:=CharSet.Auto)>
    Public Shared Function IsDBCSLeadByte(ByVal TestChar As Byte) As Int32
    End Function
    <DllImport("user32.dll", CharSet:=CharSet.Auto)> _
    Public Shared Function DefWindowProc(ByVal hWnd As IntPtr, ByVal uMsg As Int32, ByVal wParam As IntPtr, ByVal lParam As IntPtr) As IntPtr
    End Function
    <DllImport("user32.dll", CharSet:=CharSet.Auto)> _
    Public Shared Function CallWindowProc(ByVal Prev As IntPtr, ByVal hWnd As IntPtr, ByVal uMsg As Int32, ByVal wParam As IntPtr, ByVal lParam As IntPtr) As IntPtr
    End Function
    <DllImport("user32.dll", SetLastError:=True, CharSet:=CharSet.Auto)> _
    Public Shared Function ShowWindow(ByVal hwnd As IntPtr, ByVal nCmdShow As ShowWindowCommands) As Boolean
    End Function
    <DllImport("user32.dll", SetLastError:=True, CharSet:=CharSet.Auto)> _
    Public Shared Function RegisterClassEx(ByRef lpwcx As WNDCLASSEX) As Short
    End Function
    <DllImport("user32.dll", SetLastError:=True, CharSet:=CharSet.Auto)>
    Public Shared Function CreateWindowEx(ByVal dwExStyle As WindowStylesEx, ByVal lpClassName As String, ByVal lpWindowName As String, ByVal dwStyle As WindowStyles, ByVal x As Integer, ByVal y As Integer, ByVal nWidth As Integer, ByVal nHeight As Integer, ByVal hWndParent As IntPtr, ByVal hMenu As IntPtr, ByVal hInstance As IntPtr, ByVal lpParam As IntPtr) As IntPtr
    End Function
    <DllImport("user32.dll", CharSet:=CharSet.Auto, SetLastError:=True)> _
    Public Shared Function GetMessage(ByRef lpMsg As MSG, ByVal hWnd As IntPtr, ByVal wMsgFilterMin As UInteger, ByVal wMsgFilterMax As UInteger) As Int32
    End Function
    <DllImport("user32.dll", CharSet:=CharSet.Auto, SetLastError:=True)> _
    Public Shared Function TranslateMessage(ByRef lpMsg As MSG) As Int32
    End Function
    <DllImport("user32.dll", CharSet:=CharSet.Auto, SetLastError:=True)> _
    Public Shared Function DispatchMessage(ByRef lpmsg As MSG) As Int32
    End Function
    <DllImport("user32", CharSet:=CharSet.Auto)>
    Public Shared Function SetWindowLong(ByVal Handle As IntPtr, ByVal Index As Int32, ByVal NewLong As [Delegate]) As Int32
    End Function
    <DllImport("user32.dll", CharSet:=CharSet.Auto, SetLastError:=True)> _
    Public Shared Function PeekMessage(ByRef lpMsg As MSG, ByVal hWnd As IntPtr, ByVal wMsgFilterMin As UInteger, ByVal wMsgFilterMax As UInteger, ByVal msgRemove As UInteger) As Int32
    End Function
    <DllImport("imm32", CharSet:=CharSet.Auto)>
    Public Shared Function ImmGetContext(ByVal Handle As IntPtr) As IntPtr
    End Function
    <DllImport("imm32", CharSet:=CharSet.Auto)>
    Public Shared Function ImmReleaseContext(ByVal Handle As IntPtr, ByVal ImcHandle As IntPtr) As Int32
    End Function
    <DllImport("imm32", CharSet:=CharSet.Auto)>
    Public Shared Function ImmGetCompositionString(ByVal ImcHandle As IntPtr, ByVal Flag As Int32, ByVal Buffer As Byte(), ByVal BufferLen As Int32) As Int32
    End Function

End Class