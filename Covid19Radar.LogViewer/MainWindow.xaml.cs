/****
 * CocoaLogViewer
 * Copyright (C) 2020-2021 Yigty.ORG; all rights reserved.
 * Copyright (C) 2020-2021 Takym.
 *
 * distributed under the MIT License.
****/

using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using Covid19Radar.LogViewer.Globalization;
using Covid19Radar.LogViewer.Models;
using Covid19Radar.LogViewer.Transformers;
using DR   = System.Windows.Forms.DialogResult;
using MBOX = System.Windows.MessageBox;

namespace Covid19Radar.LogViewer
{
	public partial class MainWindow : Window
	{
		private readonly Func<string?, string?> _transformer;
		private          bool                   _file_loaded;

		public MainWindow() : this(new TransformerPipeline().ConfigureDefaults()) { }

		public MainWindow(TransformerPipeline transformerPipeline)
		{
			_transformer = transformerPipeline.Build(m => m);
			this.InitializeComponent();
			this.Title             = LanguageData.Current.MainWindow_Title;
			btnOpen   .Content     = LanguageData.Current.MainWindow_ButtonOpen;
			lblVersion.Content     = $"{VersionInfo.GetCaption()}\t{VersionInfo.GetCopyright()}";
			controller.LogFileView = lfv;
		}

		public ValueTask<bool> ShowOpenFileDialogAsync(bool allowEscape)
		{
			return this.OpenFile(() => {
				using (var ofd = new OpenFileDialog() {
					Title                        = LanguageData.Current.MainWindow_OFD_Title,
					Filter                       = LanguageData.Current.MainWindow_OFD_Filter(),
					RestoreDirectory             = true,
					DereferenceLinks             = true,
					AddExtension                 = false,
					SupportMultiDottedExtensions = true,
					Multiselect                  = false,
					CheckPathExists              = true,
					CheckFileExists              = true,
					ValidateNames                = true,
					AutoUpgradeEnabled           = true,
				}) {
					if (ofd.ShowDialog() == DR.OK) {
						return (ofd.FileName, ofd.OpenFile);
					} else {
						return null;
					}
				}
			}, allowEscape);
		}

		public ValueTask<bool> OpenFile(string filename, bool allowEscape)
		{
			return this.OpenFile(() => (filename, () => new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read)), allowEscape);
		}

		private async ValueTask<bool> OpenFile(Func<(string path, Func<Stream> open)?> file, bool allowEscape)
		{
			if (_file_loaded) {
				return false;
			}

			try {
				var f = file();
				if (f.HasValue) {
					await this.Dispatcher.InvokeAsync(() => this.Title = Path.GetFileName(f.Value.path));
					lfv.LogFile = await Task.Run(() => new LogFileModel(f.Value.open(), _transformer, allowEscape)).ConfigureAwait(false);
					await this.Dispatcher.InvokeAsync(() => {
						btnOpen.Visibility      = Visibility.Collapsed;
						lblException.Visibility = Visibility.Collapsed;
					});
					_file_loaded = true;
					return true;
				}
			} catch (Exception e) {
				await this.Dispatcher.InvokeAsync(() => {
					MBOX.Show(
						this,
						LanguageData.Current.MainWindow_OFD_Error_Message,
						LanguageData.Current.MainWindow_OFD_Error,
						MessageBoxButton.OK,
						MessageBoxImage.Error
					);
					this.PrintException(e);
				});
				Debug.Fail(e.ToString());
			}
			return false;
		}

		public void PrintException(Exception e)
		{
			lblException.Content    = e;
			lblException.Visibility = Visibility.Visible;
		}

		private async void btnOpen_Click(object sender, RoutedEventArgs e)
		{
			await this.ShowOpenFileDialogAsync(false);
		}

		public override string ToString()
		{
			return this.Title;
		}
	}
}
