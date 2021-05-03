using System;
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

		private async void openBtn_Click(object sender, RoutedEventArgs e)
		{
			try {
				using (var ofd = new OpenFileDialog() {
					Title                        = "ログファイルを開く",
					Filter                       = "COCOAログファイル (cocoa_log_*.csv)|cocoa_log_*.csv|全てのファイル|*",
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
						lfv.LogFile = await Task.Run(() => new LogFileModel(ofd.OpenFile(), _transformer));
						openBtn.Visibility = Visibility.Collapsed;
					}
				}
			} catch (Exception ex) {
				MBOX.Show(ex.Message, "エラーが発生しました。", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}
	}
}
