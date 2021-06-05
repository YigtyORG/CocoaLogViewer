/****
 * CocoaLogViewer
 * Copyright (C) 2020-2021 Yigty.ORG; all rights reserved.
 * Copyright (C) 2020-2021 Takym.
 *
 * distributed under the MIT License.
****/

namespace Covid19Radar.LogViewer.Globalization
{
	internal sealed class Japanese : LanguageData
	{
		internal static readonly Japanese _inst = new();

		public override string VersionInfo_Unknown                       => "<不明>";
		public override string VersionInfo_Debug                         => "デバッグ版";
		public override string MainWindow_Title                          => "接触確認アプリ(COCOA)の動作情報確認ツール";
		public override string MainWindow_ButtonOpen                     => "開く";
		public override string MainWindow_OFD_Title                      => "動作情報ファイルを開く";
		public override string MainWindow_OFD_Filter_CSV                 => "COCOA 動作情報ファイル";
		public override string MainWindow_OFD_Filter_All                 => "全てのファイル";
		public override string MainWindow_OFD_Error                      => "エラーが発生しました。";
		public override string MainWindow_OFD_Error_Message              => "指定されたファイルにアクセスできないか、正しい動作情報ファイルではありません。";
		public override string FormMain_ButtonOpen                       => "動作情報ファイルを開く(&O)";
		public override string FormMain_CheckBoxAllowEscape              => "エスケープ文字を変換する。";
		public override string FormMain_FormClosing                      => "全ての COCOA 動作情報ファイルウィンドウを閉じます。宜しいですか？";
		public override string ModuleLoader_Failed_Title                 => "拡張機能読み込みエラー";
		public override string ModuleLoader_Failed_Message               => "拡張機能「{0}」の読み込みに失敗しました。{1}";
		public override string ControllerView_Refresh                    => "再読み込み";
		public override string ControllerView_Copy                       => "選択範囲を一括コピー";
		public override string ControllerView_CopyAsMarkdown             => "Markdownとしてコピー";
		public override string ControllerView_Copy_MessageBox            => "クリップボードに選択されたログの詳細情報をコピーしました。";
		public override string LogFileView_MessageBox_Title              => "動作情報ファイルを開く";
		public override string LogFileView_MessageBox_Failed             => "動作情報ファイルの読み込み中に予期せぬエラーが発生しました。";
		public override string LogFileModel_InvalidLog_Short             => "無効なログ";
		public override string LogFileModel_InvalidLog_Long              => "書式が誤っているため読み込めませんでした。";
		public override string LogHeaderView_Control                     => "操作";
		public override string LogHeaderView_Timestamp                   => "日時";
		public override string LogHeaderView_Level                       => "ログレベル";
		public override string LogHeaderView_Location                    => "場所";
		public override string LogHeaderView_Message                     => "内容";
		public override string LogDataView_Details                       => "詳細";
		public override string LogDataView_DetailedInformation           => "詳細情報";
		public override string LogDataView_Copy                          => "コピー";
		public override string LogDataView_Copy_MessageBox               => "クリップボードにログの詳細情報をコピーしました。";
		public override string LogDataModel_DateTime_Format_WithWordWrap => "yyyy\'年\'MM\'月\'dd\'日\'\r\nHH\'時\'mm\'分\'ss.fffffff\'秒\'";
		public override string LogDataModel_DateTime_Format_WithNoWrap   => "yyyy\'年\'MM\'月\'dd\'日\' HH\'時\'mm\'分\'ss.fffffff\'秒\'";
		public override string LogDataModel_DateTime                     => "日時：{0}";
		public override string LogDataModel_LogLevel                     => "ログレベル：{0}";
		public override string LogDataModel_Location                     => "場所：{0}";
		public override string LogDataModel_Message                      => "内容：{0}";
		public override string LogDataModel_Message_Transformed          => "翻訳された内容：{0}";
		public override string LogDataModel_Message_Original             => "元の内容：{0}";
		public override string LogDataModel_Platform                     => "プラットフォーム: {0} (バージョン: {1})";
		public override string LogDataModel_Device                       => "機器: {0} (種類: {1})";
		public override string LogDataModel_Version                      => "アプリのバージョン: {0} (ビルド番号: {1})";
		public override string LogLevel_Unknown                          => "不明";
		public override string LogLevel_Verbose                          => "詳細";
		public override string LogLevel_Debug                            => "検査";
		public override string LogLevel_Info                             => "情報";
		public override string LogLevel_Warning                          => "警告";
		public override string LogLevel_Error                            => "失敗";
		public override string LogLevel_Remarks                          => "注釈";

		private Japanese() { }

		public override string ControllerView_Refresh_Failed(MainWindow? mwnd)
		{
			if (mwnd is null) {
				return "動作情報ファイルの再読み込みに失敗しました。";
			} else {
				return $"動作情報ファイル「{mwnd.Title}」の再読み込みに失敗しました。";
			}
		}

		public override string LogFileView_MessageBox_Succeeded(MainWindow? mwnd)
		{
			if (mwnd is null) {
				return "動作情報ファイルの読み込みが完了しました。";
			} else {
				return $"動作情報ファイル「{mwnd.Title}」の読み込みが完了しました。";
			}
		}
	}
}
