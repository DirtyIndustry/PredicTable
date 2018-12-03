using System.Configuration;
using System.Web.Services.Description;

namespace PredicTable.WebServiceClass
{
    public class SoapAddressReflector: SoapExtensionReflector
    {
        public override void ReflectMethod()
        {
            ServiceDescription sd = ReflectionContext.ServiceDescription;
            foreach(Service service in sd.Services)
            {
                foreach(Port port in service.Ports)
                {
                    foreach(ServiceDescriptionFormatExtension extension in port.Extensions)
                    {
                        if(extension is SoapAddressBinding)
                        {
                            SoapAddressBinding address = (SoapAddressBinding)extension;
                            address.Location = ConfigurationManager.AppSettings["SoapAddress"];
                        }
                        else if (extension is Soap12AddressBinding)
                        {
                            Soap12AddressBinding address = (Soap12AddressBinding)extension;
                            address.Location = ConfigurationManager.AppSettings["SoapAddress"];
                        }
                        else
                        {
                            HttpAddressBinding address = (HttpAddressBinding)extension;
                            address.Location = ConfigurationManager.AppSettings["SoapAddress"];
                        }
                    }
                }
            }
        }
    }
}