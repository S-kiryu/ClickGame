// WindowTrans.cs
using System;
using System.Collections; // これを追加
using System.Runtime.InteropServices;
using UnityEngine;

public class WindowTrans : MonoBehaviour
{
    #region WINDOWS API

    [DllImport("user32.dll")]
    private static extern IntPtr GetActiveWindow();

    [DllImport("user32.dll")]
    private static extern IntPtr GetForegroundWindow();

    [DllImport("user32.dll", SetLastError = true)]
    private static extern uint GetWindowLong(IntPtr hWnd, int nIndex);

    [DllImport("user32.dll", SetLastError = true)]
    private static extern int SetWindowLong(IntPtr hWnd, int nIndex, uint dwNewLong);

    [DllImport("user32.dll", SetLastError = true)]
    private static extern bool SetLayeredWindowAttributes(IntPtr hwnd, uint crKey, byte bAlpha, uint dwFlags);

    [DllImport("user32.dll")]
    private static extern int SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

    #endregion

    private const int GWL_EXSTYLE = -20;
    private const int GWL_STYLE = -16;

    private const uint WS_BORDER = 0x00800000;
    private const uint WS_DLGFRAME = 0x00400000;
    private const uint WS_CAPTION = WS_BORDER | WS_DLGFRAME;

    private const uint WS_EX_LAYERED = 0x80000;

    private const uint LWA_COLORKEY = 0x1;

    private const uint SWP_FRAMECHANGED = 0x0020;
    private const uint SWP_NOMOVE = 0x0002;
    private const uint SWP_NOSIZE = 0x0001;

    private IntPtr window;

    private IEnumerator Start()
    {
        // ウィンドウが確実に作成されるまで待機
        yield return new WaitForSeconds(0.5f);

#if !UNITY_EDITOR && UNITY_STANDALONE_WIN
        SetupTransparentWindow();
#endif
    }

    private void SetupTransparentWindow()
    {
        // ウィンドウハンドルを取得
        window = GetForegroundWindow();

        if (window == IntPtr.Zero)
        {
            Debug.LogError("ウィンドウハンドルの取得に失敗しました");
            return;
        }

        Debug.Log($"Window Handle: {window}");

        // 現在のスタイルを取得
        uint exStyle = GetWindowLong(window, GWL_EXSTYLE);
        uint style = GetWindowLong(window, GWL_STYLE);

        Debug.Log($"Current ExStyle: {exStyle:X}, Style: {style:X}");

        // レイヤードウィンドウスタイルを追加
        SetWindowLong(window, GWL_EXSTYLE, exStyle | WS_EX_LAYERED);

        // タイトルバーとボーダーを削除
        SetWindowLong(window, GWL_STYLE, style & ~WS_CAPTION);

        // 透過色を設定（マゼンタ #FF00FF）
        bool result = SetLayeredWindowAttributes(window, 0x00FF00FF, 0, LWA_COLORKEY);

        Debug.Log($"SetLayeredWindowAttributes result: {result}");

        // ウィンドウの変更を適用
        SetWindowPos(window, IntPtr.Zero, 0, 0, 0, 0,
            SWP_FRAMECHANGED | SWP_NOMOVE | SWP_NOSIZE);
    }
}