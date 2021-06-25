/****
 * CocoaLogViewer
 * Copyright (C) 2020-2021 Yigty.ORG; all rights reserved.
 * Copyright (C) 2020-2021 Takym.
 *
 * distributed under the MIT License.
****/

using System.Threading;
using Covid19Radar.LogViewer.Views;

namespace Covid19Radar.LogViewer.Globalization
{
	public abstract class LanguageData
	{
		#region 静的

		private const           string       CSV_FILE_FILTER = "cocoa_log_*.csv";
		private static readonly LanguageData _default;
		private static          LanguageData _current;

		public static LanguageData Default => _default;

		public static LanguageData Current
		{
			get => _current;
			set
			{
				value ??= _default;
				var current = _current;
				while (Interlocked.CompareExchange(ref _current, value, current) != current) {
					Thread.Yield();
					current = _current;
				}
			}
		}

		static LanguageData()
		{
			_default = Japanese._inst;
			_current = _default;
		}

		#endregion

		#region VersionInfo

		public abstract string VersionInfo_Unknown { get; }
		public abstract string VersionInfo_Debug   { get; }

		#endregion

		#region MainWindow

		public abstract string MainWindow_Title             { get; }
		public abstract string MainWindow_ButtonOpen        { get; }
		public abstract string MainWindow_OFD_Title         { get; }
		public abstract string MainWindow_OFD_Filter_CSV    { get; }
		public abstract string MainWindow_OFD_Filter_All    { get; }
		public abstract string MainWindow_OFD_Error         { get; }
		public abstract string MainWindow_OFD_Error_Message { get; }

		public virtual string MainWindow_OFD_Filter()
		{
			return $"{this.MainWindow_OFD_Filter_CSV} ({CSV_FILE_FILTER})|{CSV_FILE_FILTER}|{this.MainWindow_OFD_Filter_All}|*";
		}

		#endregion

		#region FormMain

		public abstract string FormMain_FeaturesMenu              { get; }
		public abstract string FormMain_FeaturesMenu_ShowReceiver { get; }
		public abstract string FormMain_FeaturesMenu_ShowSender   { get; }
		public abstract string FormMain_ButtonOpen                { get; }
		public abstract string FormMain_CheckBoxAllowEscape       { get; }
		public abstract string FormMain_FormClosing               { get; }

		#endregion

		#region FormReceiver

		public abstract string FormReceiver_Title       { get; }
		public abstract string FormReceiver_Description { get; }

		#endregion

		#region FormSender

		public abstract string FormSender_Title         { get; }
		public abstract string FormSender_Label_Address { get; }
		public abstract string FormSender_Label_Port    { get; }
		public abstract string FormSender_Label_File    { get; }
		public abstract string FormSender_Button_Cancel { get; }
		public abstract string FormSender_Button_Send   { get; }

		#endregion

		#region ModuleLoader

		public abstract string ModuleLoader_Failed_Title   { get; }
		public abstract string ModuleLoader_Failed_Message { get; }

		#endregion

		#region ControllerView

		public abstract string ControllerView_Refresh         { get; }
		public abstract string ControllerView_Copy            { get; }
		public abstract string ControllerView_CopyAsMarkdown  { get; }
		public abstract string ControllerView_Copy_MessageBox { get; }
		public abstract string ControllerView_Save            { get; }

		public abstract string ControllerView_Refresh_Failed(MainWindow? mwnd);

		#endregion

		#region LogFileView

		public abstract string LogFileView_MessageBox_Title  { get; }
		public abstract string LogFileView_MessageBox_Failed { get; }

		public abstract string LogFileView_MessageBox_Succeeded(MainWindow? mwnd);

		#endregion

		#region LogFileModel

		public abstract string LogFileModel_InvalidLog_Short { get; }
		public abstract string LogFileModel_InvalidLog_Long  { get; }

		#endregion

		#region LogHeaderView

		public abstract string LogHeaderView_Control   { get; }
		public abstract string LogHeaderView_Timestamp { get; }
		public abstract string LogHeaderView_Level     { get; }
		public abstract string LogHeaderView_Location  { get; }
		public abstract string LogHeaderView_Message   { get; }

		#endregion

		#region LogDataView

		public abstract string LogDataView_Details             { get; }
		public abstract string LogDataView_DetailedInformation { get; }
		public abstract string LogDataView_Copy                { get; }
		public abstract string LogDataView_Copy_MessageBox     { get; }

		#endregion

		#region LogDataModel

		public abstract string LogDataModel_DateTime_Format_WithWordWrap { get; }
		public abstract string LogDataModel_DateTime_Format_WithNoWrap   { get; }
		public abstract string LogDataModel_DateTime                     { get; }
		public abstract string LogDataModel_LogLevel                     { get; }
		public abstract string LogDataModel_Location                     { get; }
		public abstract string LogDataModel_Message                      { get; }
		public abstract string LogDataModel_Message_Transformed          { get; }
		public abstract string LogDataModel_Message_Original             { get; }
		public abstract string LogDataModel_Platform                     { get; }
		public abstract string LogDataModel_Device                       { get; }
		public abstract string LogDataModel_Version                      { get; }

		#endregion

		#region LogLevel

		public abstract string LogLevel_Unknown { get; }
		public abstract string LogLevel_Verbose { get; }
		public abstract string LogLevel_Debug   { get; }
		public abstract string LogLevel_Info    { get; }
		public abstract string LogLevel_Warning { get; }
		public abstract string LogLevel_Error   { get; }
		public abstract string LogLevel_Remarks { get; }

		#endregion
	}
}
