# CocoaLogViewer
Copyright (C) 2020-2021 Yigty.ORG; all rights reserved.
Copyright (C) 2020-2021 Takym.

[![Version](https://img.shields.io/badge/version-none-inactive)](https://github.com/YigtyORG/CocoaLogViewer/releases)
[![License](https://img.shields.io/github/license/YigtyORG/CocoaLogViewer)](LICENSE.md)
[![Build](https://github.com/YigtyORG/CocoaLogViewer/workflows/Build/badge.svg)](https://github.com/YigtyORG/CocoaLogViewer/actions/workflows/Build.yml)
[![GitHub Watchers](https://img.shields.io/github/watchers/YigtyORG/CocoaLogViewer?style=social)](https://github.com/YigtyORG/CocoaLogViewer/watchers)
[![GitHub Stars](https://img.shields.io/github/stars/YigtyORG/CocoaLogViewer?style=social)](https://github.com/YigtyORG/CocoaLogViewer/stargazers)
[![GitHub Forks](https://img.shields.io/github/forks/YigtyORG/CocoaLogViewer?style=social)](https://github.com/YigtyORG/CocoaLogViewer/network/members)

## 概要
このソフトウェアは、接触確認アプリ [COCOA](https://github.com/cocoa-mhlw/cocoa) の動作情報ファイル(ログファイル)を人間が読み易い形で表示します。
今後はログ情報を解析する機能を開発する予定です。
現在実装されている主な機能は下記になります。
* ログレベル毎で色分け
* 一部メッセージを日本語化
* 詳細情報のコピー

## 推奨環境
* OS: **Windows 10 20H2**以降
* ランタイム: **.NET 5.0**以降
* 言語: **C# 9.0**以降

## 使い方
起動方法は二つあります。

### Covid19Radar.LogViewer.exe を使う場合
0. ソリューションを開いて、プロジェクトをビルドします。
1. `Covid19Radar.LogViewer.exe` を起動します。
2. 「**開く**」ボタンから COCOA のログファイルを開きます。
	* ログファイルは「**お問い合わせ**」→「**動作情報を送信**」→「**動作情報を確認する**」から抽出できます。
3. ログ情報が全て読み込まれるまで待機します。
	* 通常のログファイルはかなり大きいので時間が掛かります。
	* ログファイルの読み込みが完了するまで、ウィンドウを閉じないでください。
		* 完了するとメッセージボックスが表示されます。

### Covid19Radar.LogViewer.Launcher.exe を使う場合
0. ソリューションを開いて、プロジェクトをビルドします。
1. `Covid19Radar.LogViewer.Launcher.exe` を起動します。
2. 「**動作情報ファイルを開く(O)**」ボタンから COCOA のログファイルを開きます。
	* ログファイルは「**お問い合わせ**」→「**動作情報を送信**」→「**動作情報を確認する**」から抽出できます。
3. ログ情報が全て読み込まれるまで待機します。
	* 通常のログファイルはかなり大きいので時間が掛かります。
	* ログファイルの読み込みが完了するまで、ウィンドウを閉じないでください。
		* 完了するとメッセージボックスが表示されます。

## 更新履歴
まだリリースされていません。気長にお待ちください。

| # |バージョン|開発コード名|更新日    |更新内容            |リリースノート|
|--:|:--------:|:-----------|:--------:|:-------------------|:-------------|
|  0|v0.0.0.0  |c19r.lv00a0 |0000/00/00|最初のリリースです。|              |

## 貢献方法
* Issue や Pull Request (PR) は何時でも歓迎しています。気軽に投稿してください！
* このソフトウェアは [WPF](https://docs.microsoft.com/ja-jp/visualstudio/designers/getting-started-with-wpf) を用いて開発しております。
	* ランチャーのみ [Windows Forms](https://docs.microsoft.com/ja-jp/dotnet/desktop/winforms/overview/?view=netdesktop-5.0) を使っております。
* このソフトウェアの UI は**全て日本語**で記述します。
* できる限り外部のライブラリに依存しないで開発しております。
	* [COCOA](https://github.com/cocoa-mhlw/cocoa) 本体にも依存しておりません。
* 著作権を [@Takym](https://github.com/Takym) へ**譲渡・転移する事に同意**してくださった方の PR のみ Merge します。
	* あなたの PR に他者が作成したコードを**含めないで**ください。
* 現在はアプリアイコンを募集しております。

## 利用ライブラリ
* Microsoft.NET.Sdk
	* [.NET プロジェクト SDK](https://docs.microsoft.com/ja-jp/dotnet/core/project-sdk/overview)
	* 著作権表記: Copyright (c) .NET Foundation and Contributors
	* リポジトリ: <https://github.com/dotnet/sdk>
	* 利用規約: [MITライセンス](https://github.com/dotnet/sdk/blob/main/LICENSE.TXT)

## 謝辞
この場を借りてお礼を申し上げます。COCOA と接触確認アプリ開発関係者各位に感謝致します。

## 利用規約
このライブラリ群は[MITライセンス](LICENSE.md)に基づいて配布されています。
