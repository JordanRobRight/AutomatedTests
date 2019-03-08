
using QA.Automation.APITests.Models;

namespace QA.Automation.APITests.LG20.Services
{
    public class LGMServiceAppointmentsService : APIActionsBase, IApiPage
    {
        private static readonly string _serviceName = "LG.LGM.ServiceAppointmentsService";

        public LGMServiceAppointmentsService(APRIConfigSettings config) : base (config)
        {
            config.ServiceName = _serviceName;
        }
    }
}
