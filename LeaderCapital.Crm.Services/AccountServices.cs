using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaderCapital.Crm.Services
{
    public class AccountServices
    {
        public AccountRegistrationResponse Execute(
         Guid ownerUserId,
         string organizationName,
         string businessUnitName,
         RealAccountRegistrationRequest request)
        {
            try
            {
                ////m_utils = new General();
                ////if ("true".Equals(m_utils.GetConfigKey("debugMode", false), StringComparison.InvariantCultureIgnoreCase))
                ////    m_debugMode = true;
                ////m_logPath = m_utils.GetConfigKey("RegisterRealAccountServiceLogPath");
                ////m_log = new Logs(m_logPath, m_debugMode);
                ////m_log.FileName = "RegisterRealAccountService";
                ////////m_log.WriteInfoLog("============= RegisterRealAccountService Start =================== Start", "Execute", null);
                ////log.Append("\n\n\n============= RegisterRealAccountService Start ===================");

                BllHelpers.Initializer(className, ref m_utils, ref m_log, ref m_debugMode, ref log, null);

                #region Create XRM object

                if (request == null)
                {
                    throw new NullReferenceException("request cannot be null");
                }
                AuthenticationManager = new engine.AuthenticationManager();
                AuthenticationManager.CrmServiceFactory = new utilities.CrmServiceFactory();
                AuthenticationManager.BusinessUnitSettingsManager = new engine.BusinessUnitSettingsManager();
                AuthenticationManager.ClientsManager = new engine.ClientsManager();
                AuthenticationManager.m_log = m_log;

                AccountsRegistrationManager = new AccountsRegistrationManager();
                AccountsRegistrationManager.m_log = m_log;
                AccountsRegistrationManager.CountriesManager = new CountriesManager();

                AccountsRegistrationManager.BusinessUnitSettingsManager = new BusinessUnitSettingsManager();
                AccountsRegistrationManager.BusinessUnitSettingsManager.m_log = m_log;
                AccountsRegistrationManager.BusinessUnitSettingsManager.SystemUsersManager = new utilities.SystemUsersManager();

                AccountsRegistrationManager.EmailsManager = new EmailsManager();
                AccountsRegistrationManager.CasesManager = new CasesManager();
                AccountsRegistrationManager.TradingPlatformAccountsManager = new TradingPlatformAccountsManager();
                AccountsRegistrationManager.TradingPlatformAccountsManager.m_log = m_log;
                AccountsRegistrationManager.TradingPlatformManagementServiceFactory = new TradingPlatformManagementServiceFactory();
                AccountsRegistrationManager.TradingPlatformsManager = new TradingPlatformsManager();
                AccountsRegistrationManager.CurrenciesManager = new CurrenciesManager();
                AccountsRegistrationManager.EntityOperations = new EntityOperations();

                if (EntityOperations == null)
                {
                    EntityOperations = new EntityOperations();
                }

                LogsManager = new LogsManager();
                LogsManager.EntOperations = new EntityOperations();
                attributes = new AttributeCollection();

                xrm = AuthenticationManager.Authenticate(organizationName, businessUnitName, ownerUserId, ref log);
                //// m_log.WriteInfoLog("After Create XRM object", "AccountRegistrationResponse.Execute", null);
                BllHelpers.AfterCreateXRMObject(ref log);
                #endregion Create XRM object

                unique_value = className + "_" + request.FullName.ToString() + "_" + DateTime.Now.ToString("ddMMyyyyHHmmss");
                BllHelpers.UniqueValue(unique_value, ref log);

                string input_params = EntityOperations.PropertiesToString(request);
                BllHelpers.InputParams(input_params, ref log);

                #region Create Log Record
                attributes.Add(new KeyValuePair<string, object>("lv_input_params", input_params));
                attributes.Add(new KeyValuePair<string, object>("lv_unique_value", unique_value));

                logsEntity = LogsManager.CreateLogRecord(xrm, className + "_" + unique_value,
                    LogPicklists.Type.CreateRealAccount.Value, LogPicklists.Type.CreateRealAccount.name, attributes, ref log);
                if (logsEntity == null)
                {
                    BllHelpers.ProblemToCreateLogRecord(ref log);
                }
                #endregion Create Log Record

                //Entity ac = null;
                #region Check Request Params
                if (request.LoggedInAccountId != null && request.LoggedInAccountId != Guid.Empty)
                {

                }
                else
                {

                }

                #endregion Check Request Params

                if (ownerUserId != Guid.Empty)
                    xrm.CallerId = ownerUserId;

                AccountRegistrationResponse realAccountRegistrationResponse = AccountsRegistrationManager.RegisterRealAccount(xrm, businessUnitName, request, ownerUserId, ref log, logsEntity);

                if (EntityOperations == null)
                {
                    EntityOperations = new EntityOperations();
                }
                string properties = EntityOperations.PropertiesToString(realAccountRegistrationResponse);
                //// m_log.WriteInfoLog(string.Format("Register REAL Account response :  requestId={0}, {1}", realAccountRegistrationResponse.RequestId, properties), "AccountRegistrationResponse.Execute", null);
                //// m_log.WriteInfoLog("============= RegisterRealAccountService END ===================", "Execute", null);

                if (!String.IsNullOrEmpty(properties) && logsEntity != null)
                {
                    #region Update Log Record With Output Params

                    attributes = new Microsoft.Xrm.Sdk.AttributeCollection();
                    attributes.Add(new KeyValuePair<string, object>("lv_output_params", properties));
                    LogsManager.UpdateLogRecord(xrm, logsEntity, attributes, ref log);
                    #endregion Update Log Record With Output Params
                }
                BllHelpers.PrintResponse(string.Format("Register REAL Account response:  requestId={0}, {1}", realAccountRegistrationResponse.RequestId, properties), ref log);

                return realAccountRegistrationResponse;
            }
            catch (Exception ex)
            {
                BllHelpers.PrintException(className, "Exception", m_log.CatchExceptionMessage(ex), ref log);
                //// log.Append("\n " + className + " Exception : " + ex.ToString());
                CreateExceptionLogRecord(ex);
                m_log.WriteErrorLog(ex);
                throw;
            }
            finally
            {
                BllHelpers.GetEndOfLogForMainClass(className, ref log);
                if (xrm != null && logsEntity != null && logsEntity.Id != Guid.Empty && LogsManager != null)
                {
                    LogsManager.UpdateLog(xrm, logsEntity, log.ToString(), ref log);
                }
                if (m_log != null)
                {
                    m_log.WriteInfoLog(log.ToString(), "Execute", null);
                }
            }

        }

        private static void CreateExceptionLogRecord(Exception ex)
        {
            if (logsEntity != null && xrm != null)
            {
                if (LogsManager == null)
                {
                    LogsManager = new LogsManager();
                    LogsManager.EntOperations = new EntityOperations();
                }
                LogsManager.CreateLogExceptionRecord(logsEntity, ex.ToString(), "Exception RegisterRealAccountService Execute ", xrm, ref log);
            }
           
        }
    }
}
