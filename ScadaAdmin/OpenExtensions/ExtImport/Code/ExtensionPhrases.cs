﻿using Scada.Lang;

namespace Scada.Admin.Extensions.ExtImport.Code
{
	/// <summary>
	/// The phrases used by the extension.
	/// <para>Фразы, используемые расширением.</para>
	/// </summary>
	internal class ExtensionPhrases
	{
		// Scada.Admin.Extensions.ExtImport.ExtImportLogic
		public static string GeneralOptionsNode { get; private set; }
		public static string DriversNode { get; private set; }
		public static string DataSourcesNode { get; private set; }
		public static string LinesNode { get; private set; }
		public static string LineOptionsNode { get; private set; }
		public static string LineStatsNode { get; private set; }
		public static string LogsNode { get; private set; }

		// Scada.Admin.Extensions.ExtImport.Code.ExtensionUtils
		public static string DeviceNotSupported { get; private set; }
		public static string UnableCreateDeviceView { get; private set; }
		public static string NoDeviceProperties { get; private set; }

		// Scada.Admin.Extensions.ExtImport.Controls.CtrlCnlImport1
		public static string AllCommLines { get; private set; }
		public static string DeviceInfo { get; private set; }
		public static string DeviceNotFound { get; private set; }
		public static string NoDeviceSelected { get; private set; }

		// Scada.Admin.Extensions.ExtImport.Controls.CtrlExtensionMenu
		public static string ConfirmDeleteLine { get; private set; }
		public static string CnlNodeNotFound { get; private set; }

		// Scada.Admin.Extensions.ExtImport.Controls.CtrlLineMain
		public static string UndefinedChannelType { get; private set; }
		public static string ChannelNotSupported { get; private set; }
		public static string UnableCreateChannelView { get; private set; }
		public static string NoChannelProperties { get; private set; }

		// Scada.Admin.Extensions.ExtImport.Forms.FrmCnlCreate
		public static string ImportCnlsStep1 { get; private set; }
		public static string ImportCnlsStep2 { get; private set; }
		public static string ImportCnlsStep3 { get; private set; }
		public static string ImportCnlsCompleted { get; private set; }

		public static string CreateCnlsStep1 { get; private set; }
		public static string CreateCnlsStep2 { get; private set; }
		public static string CreateCnlsStep3 { get; private set; }
		public static string CreateCnlsCompleted { get; private set; }

		// Scada.Admin.Extensions.ExtImport.Forms.FrmCommLogs
		public static string AppFilter { get; private set; }
		public static string LinesFilter { get; private set; }
		public static string DevicesFilter { get; private set; }

		// Scada.Admin.Extensions.ExtImport.Forms.FrmDataSources
		public static string DriverNotSpecified { get; private set; }
		public static string DataSourceNotSupported { get; private set; }
		public static string UnableCreateDataSourceView { get; private set; }
		public static string NoDataSourceProperties { get; private set; }

		// Scada.Admin.Extensions.ExtProjectTools.Forms.FrmDeviceAdd
		public static string DeviceExistsInConfigDatabase { get; private set; }
		public static string DeviceExistsInLineConfig { get; private set; }
		public static string LineNotFoundInCommConfig { get; private set; }
		public static string ChooseLine { get; private set; }

		// Scada.Admin.Extensions.ExtProjectTools.Forms.FrmLineAdd
		public static string LineExistsInConfigDatabase { get; private set; }
		public static string LineExistsInCommConfig { get; private set; }

		// Scada.Admin.Extensions.ExtImport.Forms.FrmLineConfig
		public static string LineConfigTitle { get; private set; }

		// Scada.Admin.Extensions.ExtImport.Forms.FrmLineStats
		public static string LineStatsTitle { get; private set; }

		// Scada.Admin.Extensions.ExtImport.Forms.FrmSync
		public static string SyncCompleted { get; private set; }
		public static string NoDataToSync { get; private set; }
		public static string SyncCompletedWithError { get; private set; }


		public static void Init()
		{
			LocaleDict dict = Locale.GetDictionary("Scada.Admin.Extensions.ExtImport.ExtImportLogic");
			GeneralOptionsNode = dict["GeneralOptionsNode"];
			DriversNode = dict["DriversNode"];
			DataSourcesNode = dict["DataSourcesNode"];
			LinesNode = dict["LinesNode"];
			LineOptionsNode = dict["LineOptionsNode"];
			LineStatsNode = dict["LineStatsNode"];
			LogsNode = dict["LogsNode"];

			dict = Locale.GetDictionary("Scada.Admin.Extensions.ExtImport.Code.ExtensionUtils");
			DeviceNotSupported = dict["DeviceNotSupported"];
			UnableCreateDeviceView = dict["UnableCreateDeviceView"];
			NoDeviceProperties = dict["NoDeviceProperties"];

			dict = Locale.GetDictionary("Scada.Admin.Extensions.ExtImport.Controls.CtrlCnlImport1");
			AllCommLines = dict["AllCommLines"];
			DeviceInfo = dict["DeviceInfo"];
			DeviceNotFound = dict["DeviceNotFound"];
			NoDeviceSelected = dict["NoDeviceSelected"];

			dict = Locale.GetDictionary("Scada.Admin.Extensions.ExtImport.Controls.CtrlExtensionMenu");
			ConfirmDeleteLine = dict["ConfirmDeleteLine"];
			CnlNodeNotFound = dict["CnlNodeNotFound"];

			dict = Locale.GetDictionary("Scada.Admin.Extensions.ExtImport.Controls.CtrlLineMain");
			UndefinedChannelType = dict["UndefinedChannelType"];
			ChannelNotSupported = dict["ChannelNotSupported"];
			UnableCreateChannelView = dict["UnableCreateChannelView"];
			NoChannelProperties = dict["NoChannelProperties"];

			dict = Locale.GetDictionary("Scada.Admin.Extensions.ExtImport.Forms.FrmCnlImport");
			ImportCnlsStep1 = dict["ImportCnlsStep1"];
			ImportCnlsStep2 = dict["ImportCnlsStep2"];
			ImportCnlsStep3 = dict["ImportCnlsStep3"];
			ImportCnlsCompleted = dict["ImportCnlsCompleted"];

			//TODO  : ADD Specific in dictionnary
			dict = Locale.GetDictionary("Scada.Admin.Extensions.ExtCommConfig.Forms.FrmCnlCreate");
			CreateCnlsStep1 = dict["CreateCnlsStep1"];
			CreateCnlsStep2 = dict["CreateCnlsStep2"];
			CreateCnlsStep3 = dict["CreateCnlsStep3"];
			CreateCnlsCompleted = dict["CreateCnlsCompleted"];


			dict = Locale.GetDictionary("Scada.Admin.Extensions.ExtImport.Forms.FrmCommLogs");
			AppFilter = dict["AppFilter"];
			LinesFilter = dict["LinesFilter"];
			DevicesFilter = dict["DevicesFilter"];

			dict = Locale.GetDictionary("Scada.Admin.Extensions.ExtImport.Forms.FrmDataSources");
			DriverNotSpecified = dict["DriverNotSpecified"];
			DataSourceNotSupported = dict["DataSourceNotSupported"];
			UnableCreateDataSourceView = dict["UnableCreateDataSourceView"];
			NoDataSourceProperties = dict["NoDataSourceProperties"];

			dict = Locale.GetDictionary("Scada.Admin.Extensions.ExtImport.Forms.FrmDeviceAdd");
			DeviceExistsInConfigDatabase = dict["DeviceExistsInConfigDatabase"];
			DeviceExistsInLineConfig = dict["DeviceExistsInLineConfig"];
			LineNotFoundInCommConfig = dict["LineNotFoundInImport"];
			ChooseLine = dict["ChooseLine"];

			dict = Locale.GetDictionary("Scada.Admin.Extensions.ExtImport.Forms.FrmLineAdd");
			LineExistsInConfigDatabase = dict["LineExistsInConfigDatabase"];
			LineExistsInCommConfig = dict["LineExistsInExtImport"];

			dict = Locale.GetDictionary("Scada.Admin.Extensions.ExtImport.Forms.FrmLineConfig");
			LineConfigTitle = dict["LineConfigTitle"];

			dict = Locale.GetDictionary("Scada.Admin.Extensions.ExtImport.Forms.FrmLineStats");
			LineStatsTitle = dict["LineStatsTitle"];

			dict = Locale.GetDictionary("Scada.Admin.Extensions.ExtImport.Forms.FrmSync");
			SyncCompleted = dict["SyncCompleted"];
			SyncCompletedWithError = dict["SyncCompletedWithError"];
			NoDataToSync = dict["NoDataToSync"];
		}
	}
}