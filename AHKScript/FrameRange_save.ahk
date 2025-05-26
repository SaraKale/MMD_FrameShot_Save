#Requires AutoHotkey v2.0
#SingleInstance Off
; Help docs: https://www.autohotkey.com/docs/v2/

; This script uses picking coordinate values and inputting frame numbers to save
; 该脚本是运用拾取坐标值输入帧号保存
; 該腳本是運用拾取坐標值輸入幀號保存
; このスクリプトは、座標値を取得し、フレーム番号を付けて保存するものです。

; Read the function of the program here, please do not modify
; 这里读取程序的函数，请不要修改。
; 這裏讀取程序的函數，請不要修改。
; ここのプログラムを読み込む関数は変更しないでください。
x := A_Args[1]
y := A_Args[2]
frame := A_Args[3]
fullpath := A_Args[4]

if (fullpath = "")
{
	; When prompted whether to fill in the export path, a prompt will pop up saying "Please fill in the complete save path"
     ; 提示导出路径是否填写，会弹出“请填写完整的保存路径”
	 ; 提示導出路徑是否填寫，會彈出“請填寫完整的保存路徑”
	 ;エクスポートパスに記入するかどうかを求めるプロンプトが表示され、「完全な保存パスに記入してください」がポップアップ表示されます
	MsgBox("Please fill in the complete save path!")  
	; Exit script instance
	; 退出脚本实例
	; 退出腳本實例
	; 終了スクリプトの例
    ExitApp()
}

; Search for the MikuMikuDance window
; 寻找 MikuMikuDance 窗口
; 尋找 MikuMikuDance 窗口
; MikuMikuDanceウィンドウを探す
if WinExist("MikuMikuDance")
{
	; Specify screen coordinates
	; 指定屏幕坐标
	; 指定屏幕坐標
	; スクリーン座標の指定
	CoordMode("Mouse", "Screen") 
    WinActivate()
	
	;Try switching to English input method (American preferred)
	; 尝试切换到英文输入法（美式优先）
	; 嘗試切換到英文輸入法（美式優先）
	; 英語入力に切り替えてみてください。
	preferred := ["00000409", "00000809"] ; US、UK
	for layout in preferred {
		if IsLayoutAvailable(layout) {
			SetInputLang(layout)
			break
		}
	}

	; Function: Determine whether the input method exists
	; 函数：判断输入法是否存在
	; 函數：判斷輸入法是否存在
	; 関数：入力方式の存在確認
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
	
	; Function: Set input method
	; 函数：设置输入法
	; 函數：設置輸入法
	; 関数：入力方式の設定
	SetInputLang(layoutId) {
		hkl := DllCall("LoadKeyboardLayout", "Str", layoutId, "UInt", 1, "Ptr")
		PostMessage(0x50, 0, hkl, , "A") ; WM_INPUTLANGCHANGEREQUEST
	}	
	
	; Waiting delay time in milliseconds, 1000ms = 1s.
	; 等待延迟时间，以毫秒ms为单位，1000ms=1s
	; 等待延遲時間，以毫秒ms為單位，1000ms=1s
	; 待ち時間（ミリ秒ms単位、1000ms=1秒）
    Sleep(500)
	
	; Mouse over the frame input box and click
    ; 鼠标移动到帧输入框并点击
	; 鼠標移動到幀輸入框併點擊
	; フレーム入力ボックスにマウスオーバーして
    MouseMove(x, y, 10)
    Sleep(500)
    Click
    Sleep(500)

    ; Press Ctrl+A to select all
	; 按下 Ctrl+A 全选
	; 按下 Ctrl+A 全選
	; Ctrl+Aで全選択
    Send("^a")
    Sleep(300)
	; Press Del to delete the content number
	; 按下 Del 键删除内容数字
	; 按下 Del 鍵刪除內容數字
	; Delを押してコンテンツ番号を削除する
    Send("{Del}")
    Sleep(300)

    ; Enter frame number
	; 输入帧号
	; 輸入幀號
	; フレーム番号を入力
    SendText(frame)
    Sleep(500)
	 ; Press Enter to save.
	 ; 按下Enter键保存
	 ; 按下Enter鍵保存
	 ; Enterキーを押して保存してください	
    Send("{Enter}")
    Sleep(500)

	 ; Press Alt+F to open the MMD menu.
	 ; 按下 Alt+F 打开MMD菜单
	 ; 按下 Alt+F 打開MMD菜單
	 ; Alt+FでMMDメニューを開く
    Send("!f")
    Sleep(600)
	 ; 按下 B 保存图片
	 ; 按下 B 保存圖片
	 ; Bボタンを押して画像を保存する
    Send("b")
    Sleep(600)

	 ; Fill in the full path
	 ; 填写完整路径
	 ; 填寫完整路徑
	 ; 完全なパスを入力してください
    Send(fullpath) 
    Sleep(500)

	 ; Press Enter to save.
	 ; 按下Enter键保存
	 ; 按下Enter鍵保存
	 ; Enterキーを押して保存してください
    Send("{Enter}")
    Sleep(2000)
	
	; Determine if the file was saved successfully (wait up to 5 seconds)
	; 判断文件是否成功保存（最多等待 5 秒）
	; 判斷文件是否成功保存（最多等待 5 秒）
	; ファイルの保存成功を判断します（最大5秒待機）
	tryCount := 0
	while (tryCount < 10) {
		if FileExist(fullpath) {
			FileAppend("OK`n", "*")  ; 输出 OK 给 stdout
			ExitApp()
		}
		Sleep(500)
		tryCount++
	}

	; If you wait 5 seconds and the file still doesn't exist
	; 如果等了5秒文件仍不存在
	; 5秒待ってもファイルが存在しない場合
	FileAppend("FAIL`n", "*")  ; 输出 FAIL 给 stdout
	ExitApp()
}
else
{
	; If the window cannot be found, a prompt will pop up: MikuMikuDance window cannot be found
	; 如果找不到窗口会弹出提示：找不到MikuMikuDance窗口
	; 如果找不到窗口會彈出提示：找不到MikuMikuDance窗口
	; MikuMikuDanceのウィンドウが見つからない場合、「MikuMikuDanceのウィンドウが見つかりません」というメッセージが表示されます。
    MsgBox("MikuMikuDance window not found!") 
    ExitApp()
}
