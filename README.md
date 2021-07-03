# CocoaLogViewer
Copyright (C) 2020-2021 Yigty.ORG; all rights reserved.
Copyright (C) 2020-2021 Takym.

[![Developing version](https://img.shields.io/badge/Developing%20version-v0.1.0.1-important)](https://github.com/YigtyORG/CocoaLogViewer/projects/1)
[![Latest version](https://img.shields.io/badge/Latest%20version-v0.1.0.0-informational)](https://github.com/YigtyORG/CocoaLogViewer/releases)
[![License](https://img.shields.io/github/license/YigtyORG/CocoaLogViewer)](https://github.com/YigtyORG/CocoaLogViewer/blob/master/LICENSE.md)
[![Build](https://github.com/YigtyORG/CocoaLogViewer/workflows/Build/badge.svg)](https://github.com/YigtyORG/CocoaLogViewer/actions/workflows/Build.yml)

[![GitHub Watchers](https://img.shields.io/github/watchers/YigtyORG/CocoaLogViewer?style=social)](https://github.com/YigtyORG/CocoaLogViewer/watchers)
[![GitHub Stars](https://img.shields.io/github/stars/YigtyORG/CocoaLogViewer?style=social)](https://github.com/YigtyORG/CocoaLogViewer/stargazers)
[![GitHub Forks](https://img.shields.io/github/forks/YigtyORG/CocoaLogViewer?style=social)](https://github.com/YigtyORG/CocoaLogViewer/network/members)

## 概要
このソフトウェアは、接触確認アプリ [COCOA](https://github.com/cocoa-mhlw/cocoa) の動作情報ファイル(ログファイル)を人間が読み易い形で表示します。
今後はログ情報を解析する機能を開発する予定です。
現在実装されている主な機能は下記になります。
* ログレベルの色分け
* 個人情報を含むメッセージの強調表示
* 一部メッセージの翻訳
* 詳細情報のコピー/Markdown へ変換
* ログ情報の検索 (試験的機能)
* ログファイルを外部と送受信

## 注意事項
このソフトウェアは厚生労働省とは関係ありません。

## 推奨環境
* OS: **Windows 10 20H2**以降
* ランタイム: **.NET 5.0**以降
* 言語: **C# 9.0**以降

## 使い方
1. COCOA からログファイルを抽出します。
	* 「**お問い合わせ**」→「**動作情報を送信**」→「**動作情報を確認する**」から抽出できます。
2. ソフトウェアを入手する方法は二つあります。
	* **[リリースビルド](https://github.com/YigtyORG/CocoaLogViewer/releases)をダウンロードする場合** (一般人向け)
		1. バージョンを選択します。
			* 特に理由が無い場合は最新版を選んでください。
		2. `Assets` からダウンロードするファイルを選びます。
			* 本体(ランチャー含む)は **`Covid19Radar.LogViewer.*.zip`** です。
		3. ダウンロードしたZIPファイルを適当な場所に解凍してください。
		4. これでインストールは完了です。このソフトウェアはレジストリを使いません。
			* 正常に実行できない場合は最新の .NET ランタイムをインストールしてみてください。
				* .NET ランタイムは <https://dot.net> からダウンロードできます。
				* または <https://dotnet.microsoft.com/download/dotnet/5.0/runtime> から .NET 5 をダウンロードできます。
	* **ソースコードをダウンロードする場合** (開発者向け)
		1. リポジトリをクローンします。
			1. コマンドプロンプトを開きます。
			2. `git clone https://github.com/YigtyORG/CocoaLogViewer.git` と入力し、コマンドを実行します。
		1. ソリューションを開いて、プロジェクトをビルドします。
			* ビルドの詳細は後述します。
3. 起動方法は三つあります。
	* **ランチャーを使う場合** (推奨)
		1. `Covid19Radar.LogViewer.Launcher.exe` を起動します。
		2. 画面左上の「**動作情報ファイルを開く(O)**」ボタンから COCOA のログファイルを開きます。
	* **ランチャーを使わない場合** (非推奨)
		1. `Covid19Radar.LogViewer.exe` を起動します。
		2. 画面上部の「**開く**」ボタンから COCOA のログファイルを開きます。
	* **コマンドプロンプトから起動する場合**
		1. `Covid19Radar.LogViewer.Launcher.exe` を格納しているディレクトリでコマンドプロンプトを開きます。
		2. `c19r.lv <ファイル名>` と入力し、コマンドを実行します。
			* `<ファイル名>` に開くログファイルのパスを入れます。
			* エスケープ文字を変換する場合は `<ファイル名>` の後ろに `--allow-escape` と入力します。
4. ログ情報が全て読み込まれるまで待機します。
	* 通常のログファイルはかなり大きいので時間が掛かります。
	* ログファイルの読み込みが完了するまで、ウィンドウを閉じないでください。
		* 完了するとメッセージボックスが表示されます。

### 拡張機能
* 拡張機能はランチャーから起動した場合にのみ読み込まれます。
* ランチャーに `--disallow-extensions` を指定して起動すると拡張機能の読み込みを拒否できます。
* **拡張機能一覧**
	* [英語版](./src/Covid19Radar.LogViewer.Globalization.English/)
	* [追加検索フィルタ](./src/Covid19Radar.LogViewer.SearchFilters/)
	* [翻訳処理構成ツール](./src/Covid19Radar.LogViewer.Transformers.Configuration/)

### コマンド行引数について
* `c19r.lv` に下記の引数を指定して起動方法を制御できます。
* `-e` `--allow-escape` `/AllowEscape`
	* エスケープ文字の変換を許可します。
* `--disallow-extensions` `/DisallowExtensions`
	* 拡張機能の読み込みを拒否します。
* **ログファイルへのパス**
	* 起動時に指定したログファイルを表示します。

### その他の機能
* **個人情報を含むメッセージの強調表示**
	* ログメッセージに個人情報が含まれている可能性がある場合、そのメッセージの背景は薄い桃色になります。
	* 個人情報が含まれていなくても強調表示される場合もあります。
* **ログファイルを外部と送受信**
	* ランチャーから起動した場合、ログファイルを外部と送受信する事ができます。
	* ログファイルを受信する場合は「**機能(T)**」メニュー内の「**外部から動作情報ファイルを受信する(R)...**」をクリックします。
		* IPアドレス(LAN内専用)とポート番号が表示されます。
		* 受信を停止する場合はウィンドウを閉じます。
		* 受信したログファイルは即座に表示されます。
	* ログファイルを送信する場合は「**機能(T)**」メニュー内の「**外部へ動作情報ファイルを送信する(S)...**」をクリックします。
		* 送信先のアドレスとポート番号と送信するログファイルを指定します。

## 更新履歴

| # |バージョン|開発コード名|更新日    |リリースノート                                                    |
|--:|:--------:|:-----------|:--------:|:-----------------------------------------------------------------|
|  5|v0.1.0.1  |c19r.lv01a1 |0000/00/00|まだ                                                              |
|  4|v0.1.0.0  |c19r.lv01a0 |2021/07/03|<https://github.com/YigtyORG/CocoaLogViewer/releases/tag/v0.1.0.0>|
|  3|v0.0.0.3  |c19r.lv00a3 |2021/06/06|<https://github.com/YigtyORG/CocoaLogViewer/releases/tag/v0.0.0.3>|
|  2|v0.0.0.2  |c19r.lv00a2 |2021/05/22|<https://github.com/YigtyORG/CocoaLogViewer/releases/tag/v0.0.0.2>|
|  1|v0.0.0.1  |c19r.lv00a1 |2021/05/10|<https://github.com/YigtyORG/CocoaLogViewer/releases/tag/v0.0.0.1>|
|  0|v0.0.0.0  |c19r.lv00a0 |2021/05/10|<https://github.com/YigtyORG/CocoaLogViewer/releases/tag/v0.0.0.0>|

## 貢献方法
* Issue や Pull Request (PR) は何時でも歓迎しています。気軽に投稿してください！
* このソフトウェアは下記のフレームワークを用いて開発しております。
	* 本体は [WPF](https://docs.microsoft.com/ja-jp/visualstudio/designers/getting-started-with-wpf) を使っております。
	* ランチャーは [Windows Forms](https://docs.microsoft.com/ja-jp/dotnet/desktop/winforms/overview/?view=netdesktop-5.0) を使っております。
* このソフトウェアの UI は**全て日本語**で記述します。
	* 英語版は拡張機能として用意しています。
* できる限り外部のライブラリに依存しないで開発しております。
	* [COCOA](https://github.com/cocoa-mhlw/cocoa) 本体にも依存しておりません。
	* ただし、拡張機能やテストプロジェクトでは外部ライブラリを利用している場合があります。
* 著作権を [@Takym](https://github.com/Takym) へ**譲渡・転移する事に同意**してくださった方の PR のみ Merge します。
	* あなたの PR に他者が作成したコードを**含めないで**ください。
* PR は GitHub Actions を用いて検証しています。
* 現在はアプリアイコンを募集しております。

### ビルド
* **Microsoft Visual Studio 2019**以降を推奨します。併せて下記のワークロードをインストールしてください。
	* .NET デスクトップ開発
	* .NET クロスプラットフォーム開発
	* (この他に必要なものがあればご指摘ください。)
* ビルド構成は二つあります。
	* `Debug` - デバッグに適した形でビルドします。
		* 最適化は行いません。
		* デバッグ情報を生成します。
	* `Release` - リリースに適した形でビルドします。
		* 最適化を行います。
		* デバッグ情報は生成しません。
* ビルドを実行すると下記のディレクトリを生成します。
	* `bin` - 実行可能ファイルを格納しています。
	* `obj` - 一時ディレクトリです。中間ファイルを格納しています。

## 利用ライブラリ
* Microsoft.NET.Sdk
	* [.NET プロジェクト SDK](https://docs.microsoft.com/ja-jp/dotnet/core/project-sdk/overview)
	* 著作権表記：Copyright (c) .NET Foundation and Contributors
	* リポジトリ：<https://github.com/dotnet/sdk>
	* 利用規約：[MITライセンス](https://github.com/dotnet/sdk/blob/main/LICENSE.TXT)
* [Microsoft.Extensions.FileSystemGlobbing](https://www.nuget.org/packages/Microsoft.Extensions.FileSystemGlobbing/)
	* 著作権表記：Copyright (c) .NET Foundation and Contributors
	* リポジトリ：<https://github.com/dotnet/runtime>
	* 利用規約：[MITライセンス](https://github.com/dotnet/runtime/blob/main/LICENSE.TXT)
* [Microsoft.NET.Test.Sdk](https://www.nuget.org/packages/Microsoft.NET.Test.Sdk/)
	* 著作権表記：Copyright (c) 2020 Microsoft Corporation
	* リポジトリ：<https://github.com/microsoft/vstest>
	* 利用規約：[MITライセンス](https://github.com/microsoft/vstest/blob/master/LICENSE)
	* 利用規約：<https://www.microsoft.com/web/webpi/eula/net_library_eula_enu.htm>
* [MSTest.TestAdapter](https://www.nuget.org/packages/MSTest.TestAdapter/)
	* 著作権表記：Copyright (c) 2020 Microsoft Corporation
	* リポジトリ：<https://github.com/microsoft/testfx>
	* 利用規約：[MITライセンス](https://github.com/microsoft/testfx/blob/master/LICENSE.txt)
* [MSTest.TestFramework](https://www.nuget.org/packages/MSTest.TestFramework/)
	* 著作権表記：Copyright (c) 2020 Microsoft Corporation
	* リポジトリ：<https://github.com/microsoft/testfx>
	* 利用規約：[MITライセンス](https://github.com/microsoft/testfx/blob/master/LICENSE.txt)
* [coverlet.collector](https://www.nuget.org/packages/coverlet.collector/)
	* 著作権表記：Copyright (c) 2018 Toni Solarin-Sodara
	* リポジトリ：<https://github.com/coverlet-coverage/coverlet>
	* 利用規約：[MITライセンス](https://github.com/coverlet-coverage/coverlet/blob/master/LICENSE)
* [BenchmarkDotNet](https://www.nuget.org/packages/BenchmarkDotNet/)
	* 著作権表記：Copyright (c) 2013–2021 .NET Foundation and contributors
	* リポジトリ：<https://github.com/dotnet/BenchmarkDotNet>
	* 利用規約：[MITライセンス](https://github.com/dotnet/BenchmarkDotNet/blob/master/LICENSE.md)

## 謝辞
この場を借りてお礼を申し上げます。COCOA と接触確認アプリ開発関係者各位に感謝致します。

## 利用規約
このソフトウェアは[MITライセンス](LICENSE.md)に基づいて配布されています。
個人情報については自身の責任で管理してください。
