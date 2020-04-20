using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Windows.Forms;

namespace PowerSearch
{
    class baserout
    {
        public static bool FileExists(string FileName)
        {
            return System.IO.File.Exists(FileName);
        }
    }

    public enum Error_Number_AJB_Enum
    {
        // FYI. Any message starting with '1' will log as 'I' in AJB logs
        Console_Messages = 100000,                                  // 100000
        Trace_Message,                                              // 100001
        Trace_Message_Iso,                                          // 100002
        Trace_Message_Entry,                                        // 100003

        // FYI. Any message starting with '2' will log as 'A' in AJB logs
        Informational_Messages = 200000,                            // 200000
        Ini_Variable_Not_found,                                     // 200001
        Application_Starting,                                       // 200002
        Comm_Handler_Calling_Remote,                                // 200003
        Comm_Handler_Waiting_For_Remotes,                           // 200004
        Connected_To_Remote,                                        // 200005
        Remote_Connected_Waiting_For_NodeId,                        // 200006
        Socket_Closed,                                              // 200007
        Starting_Process,                                           // 200008
        Stopping_Process,                                           // 200009
        UserAuthenticated,                                          // 200010
        New_User_LoggedIn,                                          // 200011
        Command_Starting,                                           // 200012
        Command_Finished,                                           // 200013
        Operator_Requested_Connect,                                 // 200014
        Operator_Requested_Disconnect,                              // 200015
        Operator_Set_Site_Service_Ok,                               // 200016
        Operator_Set_Site_Ok,                                       // 200017
        ExecutingCommand,                                           // 200018
        EventlogAlarmCleared,                                       // 200019
        EventlogMailNotification,                                   // 200020
        ConfigChange,                                               // 200021
        RequestSentToAuthorizer,                                    // 200022
        ResponseBackFromAuthorizer,                                 // 200023
        RequestReadFromStore,                                       // 200024
        ResponseSentBackToStore,                                    // 200025
        StoreAndForwardTransaction,                                 // 200026
        AuditEntry,                                                 // 200027
        Reflserv_Requested_Connect,                                 // 200028
        StandAlone_Application,                                     // 200029
        Iso_SignOn_Requested_From_Host,                             // 200030
        Iso_Echo_Requested_From_Host,                               // 200031
        Iso_SignOn_Response_Received,                               // 200032
        ConfigInformation,                                          // 200033
        SecureServer_ProcessingMessage,                             // 200034
        IBMMQ_Info,                                                 // 200035
        ActivationComplete,                                         // 200036
        ActivationFailed,                                           // 200037
        SSLSession,                                                 // 200038
        Informal_Message,                                           // 200039
        MonitoringStarted,                                          // 200040
        MonitoringStopped,                                          // 200041

        CSM_Alert_Message = 200042,                                 // 200042 - CSM ALERT (A->Information) range - Start

        Fipay_Pinpad_Message = 200043,                              // 306000 - For FIPAY PINPAD request and response

        CSM_Alert_Last = 201041,                                    // 201041 - CSM ALERT (A->Information) range - End

        RPM_Alert_Message = 201042,                                 // 201042 - RPM ALERT (A->Information) range - Start
        Polling_File_Transfer_Complete,                             // 201043
        Polling_File_Transfer_Start,                                // 201044
        Polling_ZipFile_Transfer_Complete,                          // 201045
        RPM_Alert_Last = 202041,                                    // 202041 - RPM ALERT (A->Information) range - End
        Iso_NewKey_Requested_From_Host,                             // 202042
        Iso_NewKeyRequest_Response_Received,                        // 202043

        DFSCHED_Alert_Message = 202044,                             // 202044 - DFSched ALERT (A->Information) range - Start
        DFSCHED_Alert_Message_Last = 203043,                        // 203043 - DFSched ALERT (A->Information) range - End

        IFC_Alert_Message = 203044,                                 // 203044 - IFC ALERT (A->Information) range - Start
        IFC_Alert_Debug,                                            // 203045   For alerts when debugging is enabled
        IFC_Alert_Velocity,                                         // 203046
        IFC_Alert_Restrict,                                         // 203047
        IFC_Alert_OverrideAmt,                                      // 203048
        IFC_Alert_Limit,                                            // 203049
        IFC_Alert_Message_Last = 204043,                            // 204043 - IFC ALERT (A->Information) range - End

        // FYI. Above this point (errcodes < 300K) will log as "Information" in the system event log (assuming you configure them in ntevents.cfg)
        // FYI. Below this point but before Fatal_Messages (errcodes in [300k, 400k)) will log as "Warning" in the system event log (assuming you configure them in ntevents.cfg)

        // FYI. Any message starting with '3' will log as 'S' in AJB logs
        Warning_Messages = 300000,                                  // 300000
        Error_During_Processing,                                    // 300001
        Socket_Failure_OS_Reported,                                 // 300002
        TimeOut_Waiting_For_Nodeid,                                 // 300003
        Bank_Response_Validation_Error,                             // 300004
        Comm_Handler_Disconnecting_Remote,                          // 300005
        Defined_Variables,                                          // 300006
        Disconnecting_Previous,                                     // 300007
        Disconnecting_Unknown_Caller,                               // 300008
        Error_Delivering_Request,                                   // 300009
        Error_Processing_Message,                                   // 300010
        Failed_During_Processing_Communication_Data,                // 300011
        Failed_To_Connect_To_Remote_Process,                        // 300012
        Killing_Process,                                            // 300013
        Merchant_Variable_Not_Defined,                              // 300014
        Process_On_Hold,                                            // 300015
        Unable_To_Find_Application_Process,                         // 300016
        Debit_Missing_PinBlock,                                     // 300017
        Failed_To_Deliver_Request,                                  // 300018
        Message_Received_From_Site,                                 // 300019
        TimeOut_Waiting_For_Response,                               // 300020
        Response_Already_Received,                                  // 300021
        Response_Received_After_Timeout,                            // 300022
        Authentication_Denied,                                      // 300023
        Failed_During_Processing_Authentication,                    // 300024
        Commanding_Pending_Completion,                              // 300025
        Operator_Asked_To_Exit_Process,                             // 300026
        ExecutingCommandTimeOut,                                    // 300027
        EventLogCheckRegistryError,                                 // 300028
        EventLogPingError,                                          // 300029
        Failed_DuringProcessingEventlog,                            // 300030
        SMTP_EmailFailure,                                          // 300031
        EventLogNodeSetupFailed,                                    // 300032
        OfflineDecline,                                             // 300033
        NodeId_Not_Defined,                                         // 300034
        NodeId_Not_Connected,                                       // 300035
        NodeId_CheckConfig,                                         // 300036
        Iso_SignOff_Requested_From_Host,                            // 300037
        Iso_SignOff_Response_Received,                              // 300038
        Unable_to_Access_or_Update_Database_Table,                  // 300039
        Error_Processing_Configuration_File,                        // 300040
        IBMMQ_Error,                                                // 300041
        ValueLink_EAN,                                              // 300042
        Incorrect_Message_Format,                                   // 300043
        WebserviceNotAvailable,                                     // 300044
        Error_Message,                                              // 300045

        CSM_Warning_Message = 300046,                               // 300046 - CSM WARNING (S->Warning) range - Start
        CSM_Warning_Message_Last = 301045,                          // 301045 - CSM WARNING (S->Warning) range - End

        RPM_Warning_Message = 301046,                               // 301046 - RPM WARNING (S->Warning) range - Start
        Polling_File_Not_Found,                                     // 301047
        Polling_File_Transfer_Offset_Reset,                         // 301048
        Polling_File_Not_Found_At_Store,                            // 301049
        Polling_Missing_Directory,                                  // 301050
        RPM_Warning_Message_Last = 302045,                          // 302045 - RPM WARNING (S->Warning) range - End

        COMM_OS_Slow_Send = 302046,                                 // 302046
        XiSecure_Error = 302047,                                    // 302047

        DFSCHED_Warning_Message = 302048,                           // 302048 - DFSched WARNING (S->Warning) range - Start
        DFSCHED_Warning_Message_Last = 303047,                      // 303047 - DFSched WARNING (S->Warning) range - End

        IFC_Warning_Message = 303048,                               // 303048 - IFC WARNING (S->Warning) range - Start
        IFC_Warning_Message_Format,                                 // 303049 
        IFC_Warning_Message_Config,                                 // 303050 
        IFC_Warning_Message_Last = 304047,                          // 304047 - IFC WARNING (S->Warning) range - End

        // FYI. Below this point (errcodes >= 400k) will log as "Error" in the system event log (assuming you configure them in ntevents.cfg)

        // FYI. Any message starting with '4' will log as 'C' in AJB logs
        Fatal_Messages = 400000,                                    // 400000
        Sql_Update_Failed,                                          // 400001
        Sql_Insert_Failed,                                          // 400002
        File_IO_Writing_Failed,                                     // 400003
        Program_Leaking_Memory_Handles_Failed,                      // 400004
        Program_Aborting,                                           // 400005
        Bank_Configuration_Issue,                                   // 400006
        Error_Checking_On_Secure_DLL,                               // 400007
        Error_During_Call_Setup,                                    // 400008
        Error_Logging_To_Disk,                                      // 400009
        File_Not_Found,                                             // 400010
        Incorrect_Version_Secure_DLL,                               // 400011
        Internal_Queue_Not_Created,                                 // 400012
        Missing_Remoting_Dll,                                       // 400013
        Missing_Rts_Interface_DLL,                                  // 400014
        Process_Exited,                                             // 400015
        System_Trace_Stan_Missing_in_response,                      // 400016
        Trying_To_Run_From_Wrong_Directory,                         // 400017
        ValidationError_Fipay_Message,                              // 400018
        Missing_Pager_Cfg,                                          // 400019
        NODATA_Failure,                                             // 400020
        CHKIO_NoResponse,                                           // 400021
        EventlogCheckIniError,                                      // 400022
        EventlogFileText,                                           // 400023
        EventlogCheckSysConfig,                                     // 400024
        EventlogCheckMemFree,                                       // 400025
        EventlogCheckMapDrive,                                      // 400026
        EventlogCheckFileExists,                                    // 400027
        EventlogCheckDirectoryExists,                               // 400028
        EventlogCheckFileOlderThan,                                 // 400029
        EventlogCheckFileSizeSmaller,                               // 400030
        EventlogCheckFileSizeLarger,                                // 400031
        EventlogCheckDiskSpace,                                     // 400032
        EventlogHighCPU,                                            // 400033
        EventlogHighHandleCount,                                    // 400034
        EventlogHighThreadCount,                                    // 400035
        EventlogExcessiveConnections,                               // 400036
        EventlogNodeCheckDown,                                      // 400037
        EventlogNetStatConnections,                                 // 400038
        EventlogNetStatListens,                                     // 400039
        EventlogNetStatOther,                                       // 400040
        EventlogNetStatFailed,                                      // 400041
        EventlogCheckLinks,                                         // 400042
        EventlogCheckPolling,                                       // 400043
        EventlogCheckCounters,                                      // 400044
        TCPPortListenerError,                                       // 400045
        NodePortConfigurationError,                                 // 400046
        ErrorApplyingNewCode,                                       // 400047
        DLLNotFound,                                                // 400048
        UserDisconnected,                                           // 400049
        Unable_To_Send901_Reversal,                                 // 400050
        N901_Arrived_After_Timeout,                                 // 400051
        Reversing_Timed_Out_Transaction,                            // 400052
        Reversing_Timed_Out_901Transaction,                         // 400053
        UnableToFindConnectedBankNode,                              // 400054
        // Polling codes used to be here, from 400055..400069
        // Feel free to reuse those codes until you fill the gap
        SSL_Config_Error = 400070,                                  // 400070
        SSL_Failed,                                                 // 400071
        Encryption_Installed_Temporary_System_Key,                  // 400072
        Encryption_Installed_Temporary_Shared_Key,                  // 400073
        NTEventLogFailure,                                          // 400074
        SecureServer_ErrorProcessingMessage,                        // 400075
        EventlogCheckRTSCode,                                       // 400076
        EventlogCheckProcess,                                       // 400077
        Https_Connection_Failed,                                    // 400078
        Https_Send_Request_Failed,                                  // 400079
        Https_Get_Response_Failed,                                  // 400080
        EventlogCheckTranRate,                                      // 400081
        InvalidCommandLine,                                         // 400082
        Severe_Message,                                             // 400083

        CSM_Severe_Message = 400084,                                // 400084 - CSM CRITICAL (C->Error) range - Start
        CSM_Severe_Message_Last = 401083,                           // 401083 - CSM CRITICAL (C->Error) range - End

        RPM_Severe_Message = 401084,                                // 401084 - RPM CRITICAL (C->Error) range - Start
        Polling_File_Transfer_Failed,                               // 401085
        Polling_File_Transfer_Fail_Size_Match,                      // 401086
        Polling_File_Transfer_Fail_CRC_Match,                       // 401087
        Polling_ZipFile_Transfer_Failed,                            // 401088
        Polling_File_Transfer_RenameFailed,                         // 401089
        Polling_File_Transfer_DatabaseUpdateFailed,                 // 401090
        Polling_Version_Request_Failed,                             // 401091
        Polling_Auto_DownloadRequest_Failed,                        // 401092
        HTTPPortListenerError,                                      // 401093       
        RPM_Severe_Message_Last = 402083,                           // 402083 - RPM CRITICAL (C->Error) range - End

        DFSCHED_Severe_Message = 402084,                            // 402084 - DFSched CRITICAL (C->Error) range - Start
        DFSCHED_Task_Alert,                                         // 402085
        DFSCHED_Job_Alert,                                          // 402086
        DFSCHED_Step_Alert,                                         // 402087
        DFSCHED_Severe_Message_Last = 403083,                       // 403083 - DFSched CRITICAL (C->Error) range - End

        IFC_Severe_Message = 403084,                                // 403084 - IFC CRITICAL (C->Error) range - Start
        IFC_Severe_Message_DBError,                                 // 403085
        IFC_Severe_Message_Last = 404083,                           // 404083 - IFC CRITICAL (C->Error) range - End

        EventlogCheckHealth,                                        // 404084
        EventlogMoveFileOlderThan,                                  // 404085

        // FYI. Any message starting with '5' will log as 'u' in AJB logs
        UnCategorizedMessages = 500000,                             // 500000

        // Anything over this (>= 550000 until 600000) will NOT be logged at the console, but will be saved on disk ('normal' node log + system.dayYYYYMMDD.log)
        DiskOnly = 550000,

        // Anything over this (>= 600000) will NOT be logged in system.dayYYYYMMDD.log
        VerboseInfo = 600000,

        // Anything over this (>= 650000) will be logged ONLY in the 'normal' node log.
        // It will NOT be logged in system.dayYYYYMMDD.log NOR at the console (with Console.WriteXXX)
        // This is usefull for console applications, allowing them to log in the normal logs, but have DIFFERENT output on the CONSOLE
        LogFileOnlyMessages = 650000,
    }

}
