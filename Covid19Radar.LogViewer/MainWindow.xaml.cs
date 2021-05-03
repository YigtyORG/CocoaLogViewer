using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using Covid19Radar.LogViewer.Models;
using Covid19Radar.LogViewer.Transformers;
using DR   = System.Windows.Forms.DialogResult;
using MBOX = System.Windows.MessageBox;

namespace Covid19Radar.LogViewer
{
	public partial class MainWindow : Window
	{
		private readonly Func<string?, string?> _transformer;

		public MainWindow()
		{
			_transformer = new TransformerPipeline().ConfigureDefaults().Build(m => m);
			this.InitializeComponent();
		}

		public async ValueTask<bool> ShowOpenFileDialogAsync()
		{
			try {
				using (var ofd = new OpenFileDialog() {
					Title                        = "動作情報ファイルを開く",
					Filter                       = "COCOA 動作情報ファイル (cocoa_log_*.csv)|cocoa_log_*.csv|全てのファイル|*",
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
						this.Dispatcher.Invoke(() => this.Title = Path.GetFileName(ofd.FileName));
						lfv.LogFile = await Task.Run(() => new LogFileModel(ofd.OpenFile(), _transformer)).ConfigureAwait(false);
						this.Dispatcher.Invoke(() => openBtn.Visibility = Visibility.Collapsed);
						return true;
					}
				}
			} catch (Exception ex) {
				MBOX.Show(this, ex.Message, "エラーが発生しました。", MessageBoxButton.OK, MessageBoxImage.Error);
			}
			return false;
		}

		private async void openBtn_Click(object sender, RoutedEventArgs e)
		{
			await this.ShowOpenFileDialogAsync();
		}

		public override string ToString()
		{
			return this.Title;
		}
	}
}
