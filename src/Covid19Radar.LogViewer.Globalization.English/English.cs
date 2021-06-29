/****
 * CocoaLogViewer
 * Copyright (C) 2020-2021 Yigty.ORG; all rights reserved.
 * Copyright (C) 2020-2021 Takym.
 *
 * distributed under the MIT License.
****/

using Covid19Radar.LogViewer.Views;

namespace Covid19Radar.LogViewer.Globalization
{
	public class English : LanguageData
	{
		internal static readonly English _inst = new();

		public override string VersionInfo_Unknown                       => "<Unknown>";
		public override string VersionInfo_Debug                         => "DEBUG";
		public override string MainWindow_Title                          => "The COCOA Log File Viewer";
		public override string MainWindow_ButtonOpen                     => "Open";
		public override string MainWindow_OFD_Title                      => "Open a log file";
		public override string MainWindow_OFD_Filter_CSV                 => "COCOA log files";
		public override string MainWindow_OFD_Filter_All                 => "All files";
		public override string MainWindow_OFD_Error                      => "An error occurred.";
		public override string MainWindow_OFD_Error_Message              => "The specified file is not a log file, or the application cannot access the file.";
		public override string FormMain_FeaturesMenu                     => "&Tools";
		public override string FormMain_FeaturesMenu_ShowReceiver        => "&Receive log files from external...";
		public override string FormMain_FeaturesMenu_ShowSender          => "&Send log files to external...";
		public override string FormMain_ButtonOpen                       => "&Open a log file";
		public override string FormMain_CheckBoxAllowEscape              => "&Convert escape characters.";
		public override string FormMain_FormClosing                      => "Are you sure to close all COCOA log file viewers?";
		public override string FormReceiver_Title                        => "Receive log files from external";
		public override string FormReceiver_Description                  => "Now receiving . . .";
		public override string FormSender_Title                          => "Send log files to external";
		public override string FormSender_Label_Address                  => "The IP address or hostname";
		public override string FormSender_Label_Port                     => "The port number";
		public override string FormSender_Label_File                     => "The path to a log file to send";
		public override string FormSender_Button_Cancel                  => "&Cancel";
		public override string FormSender_Button_Send                    => "&Send";
		public override string ModuleLoader_Failed_Title                 => "Extension loading error";
		public override string ModuleLoader_Failed_Message               => "Failed to load the extension \"{0}\". {1}";
		public override string ControllerView_Refresh                    => "Reload logs";
		public override string ControllerView_Copy                       => "Copy selected logs";
		public override string ControllerView_CopyAsMarkdown             => "Copy as Markdown";
		public override string ControllerView_Copy_MessageBox            => "Copied details of selected logs into the clipboard.";
		public override string ControllerView_Search                     => "Find";
		public override string ControllerView_Save                       => "Save to elsewhere"; // "Save to other location"/"Save to another place"
		public override string LogFileView_MessageBox_Title              => "Open a log file";
		public override string LogFileView_MessageBox_Failed             => "An unexpected error occurred while opening a log file.";
		public override string LogFileModel_InvalidLog_Short             => "INVALID LOG";
		public override string LogFileModel_InvalidLog_Long              => "This log data has been wrongly formatted.";
		public override string LogHeaderView_Control                     => "OP";
		public override string LogHeaderView_Timestamp                   => "Date/time";
		public override string LogHeaderView_Level                       => "Log level";
		public override string LogHeaderView_Location                    => "Location";
		public override string LogHeaderView_Message                     => "Message";
		public override string LogDataView_Details                       => "Details";
		public override string LogDataView_DetailedInformation           => "The detailed information";
		public override string LogDataView_Copy                          => "Copy";
		public override string LogDataView_Copy_MessageBox               => "Copied details of a log data into the clipboard.";
		public override string LogDataModel_DateTime_Format_WithWordWrap => "MMMM dd, yyyy\r\ntthh:mm:ss.fffffff";
		public override string LogDataModel_DateTime_Format_WithNoWrap   => "MMM dd, yyyy tthh:mm:ss.fffffff";
		public override string LogDataModel_DateTime                     => "The date/time: {0}";
		public override string LogDataModel_LogLevel                     => "The log level: {0}";
		public override string LogDataModel_Location                     => "The location: {0}";
		public override string LogDataModel_Message                      => "The message: {0}";
		public override string LogDataModel_Message_Transformed          => "The translated message: {0}";
		public override string LogDataModel_Message_Original             => "The original message: {0}";
		public override string LogDataModel_Platform                     => "The platform: {0}, the version: {1}";
		public override string LogDataModel_Device                       => "The device: {0}, the model: {1}";
		public override string LogDataModel_Version                      => "The app version: {0}, the build number: {1}";
		public override string LogLevel_Unknown                          => "Unknown";
		public override string LogLevel_Verbose                          => "Verbose";
		public override string LogLevel_Debug                            => "Debug";
		public override string LogLevel_Info                             => "Info";
		public override string LogLevel_Warning                          => "Warning";
		public override string LogLevel_Error                            => "Error";
		public override string LogLevel_Remarks                          => "Remarks";

		protected English() { }

		public override string ControllerView_Refresh_Failed(MainWindow? mwnd)
		{
			if (mwnd is null) {
				return "Failed to reload a log file.";
			} else {
				return $"Failed to reload the log file \"{mwnd.Title}\".";
			}
		}

		public override string LogFileView_MessageBox_Succeeded(MainWindow? mwnd)
		{
			if (mwnd is null) {
				return "Completed to load a log file.";
			} else {
				return $"Completed to load the log file \"{mwnd.Title}\".";
			}
		}
	}
}
