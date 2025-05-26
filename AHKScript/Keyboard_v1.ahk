; 检测是否有英文输入法键盘，然后自动切换
; AotoHokey v1版脚本

F9:: ; 按 F9键触发切换输入法
    preferred := ["00000409", "00000809"] ; 美国、英国
    for index, layout in preferred {
        if IsLayoutAvailable(layout) {
            SetInputLang(layout)
            MsgBox, 切换输入法到: %layout%
            return
        }
    }
    MsgBox, 没有检测到英文输入法（美国或英国）
return

; 检查输入法是否安装
IsLayoutAvailable(layoutId) {
    hKey := "HKEY_CURRENT_USER\Keyboard Layout\Preload"
    Loop, 20 {
        RegRead, val, %hKey%, %A_Index%
        if (val = layoutId)
            return true
    }
    return false
}

; 切换输入法
SetInputLang(layoutId) {
    hkl := DllCall("LoadKeyboardLayout", "Str", layoutId, "UInt", 1)
    PostMessage, 0x50, 0, %hkl%, , A
}
