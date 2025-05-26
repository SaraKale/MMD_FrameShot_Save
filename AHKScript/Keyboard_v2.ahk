; 检测是否有英文输入法键盘，然后自动切换
#Requires AutoHotkey v2.0

F9:: {
    preferred := ["00000409", "00000809"] ; 美国、英国
    for layout in preferred {
        if IsLayoutAvailable(layout) {
            SetInputLang(layout)
            MsgBox("切换输入法到: " layout)
            return
        }
    }
    MsgBox("没有检测到英文输入法（美国或英国）")
}

IsLayoutAvailable(layoutId) {
    key := "HKEY_CURRENT_USER\Keyboard Layout\Preload"
    loop 20 {
        try {
            val := RegRead(key, A_Index)
            if (val = layoutId)
                return true
        }
    }
    return false
}

SetInputLang(layoutId) {
    hkl := DllCall("LoadKeyboardLayout", "Str", layoutId, "UInt", 1, "Ptr")
    PostMessage(0x50, 0, hkl, , "A") ; WM_INPUTLANGCHANGEREQUEST
}
