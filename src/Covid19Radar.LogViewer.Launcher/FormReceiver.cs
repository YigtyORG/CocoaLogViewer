/****
 * CocoaLogViewer
 * Copyright (C) 2020-2021 Yigty.ORG; all rights reserved.
 * Copyright (C) 2020-2021 Takym.
 *
 * distributed under the MIT License.
****/

using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Covid19Radar.LogViewer.Globalization;

namespace Covid19Radar.LogViewer.Launcher
{
	public partial class FormReceiver : Form
	{
		private readonly FormMain                _owner;
		private readonly CancellationTokenSource _cts;

		public FormReceiver(FormMain owner)
		{
			_owner = owner;
			_cts   = new();
			this.InitializeComponent();
		}

		public void ShowReceiver()
		{
			this.Show(_owner);
		}

		private async void FormReceiver_Load(object sender, EventArgs e)
		{
			this            .Text = LanguageData.Current.FormReceiver_Title;
			labelDescription.Text = LanguageData.Current.FormReceiver_Description;

			var listener = new TcpListener(await GetLocalIPAddress(), 0);
			listener.Start();

			if (listener.LocalEndpoint is IPEndPoint ipep) {
				textBox.Text = $"IP  : {ipep.Address}\r\nPort: {ipep.Port}";
			} else {
				textBox.Text = listener.LocalEndpoint?.ToString();
			}

			try {
				bool allowEscape;

				while (!_cts.IsCancellationRequested) {
					string temp = Path.GetTempFileName();
					byte[] buf  = new byte[1024];

					using (var client = await Task.Run(listener.AcceptTcpClientAsync, _cts.Token)) {
						var ns = client.GetStream();
						await using (ns.ConfigureAwait(false)) {
							using (var br = new BinaryReader(ns)) {
								while (!ns.DataAvailable) {
									await Task.Yield();
								}
								allowEscape = br.ReadBoolean();
								long len    = br.ReadInt64();
								var  fs     = new FileStream(temp, FileMode.Create, FileAccess.Write, FileShare.None);
								await using (fs.ConfigureAwait(false)) {
									while (fs.Length < len) {
										while (!ns.DataAvailable) {
											await Task.Yield();
										}
										int bytes = await ns.ReadAsync(buf.AsMemory(), _cts.Token);
										if (bytes > 0) {
											await fs.WriteAsync(buf.AsMemory(0..bytes), _cts.Token);
										}
									}
								}
							}
						}
					}

					await _owner.OpenFileAsync(temp, allowEscape);
				}
			} finally {
				listener.Stop();
			}
		}

		private void FormReceiver_FormClosing(object sender, FormClosingEventArgs e)
		{
			_cts.Cancel();
		}

		private static async ValueTask<IPAddress> GetLocalIPAddress()
		{
			var addrs = await Dns.GetHostAddressesAsync(Dns.GetHostName());
			return addrs.Length > 0 ? addrs[0] : IPAddress.Loopback;
		}
	}
}
