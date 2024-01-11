using System.Reflection;

namespace Konefeld.Kopiec.VodkaApp.Core
{
    public enum ProducerExportStatus
    {
        [DisplayValue("Domestic only")]
        DomesticOnly,

        [DisplayValue("Regional export")]
        RegionalExport,

        [DisplayValue("Global export")]
        GlobalExport,
    }

    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    sealed class DisplayValueAttribute : Attribute
    {
        public string DisplayValue { get; }

        public DisplayValueAttribute(string displayValue)
        {
            DisplayValue = displayValue;
        }
    }

    public static class ProducerExportStatusExtensions
    {
        /// <summary>
        /// Podmienia wartość enuma na stringa ze spacjami
        /// </summary>
        public static string ToPrettyString(this ProducerExportStatus status)
        {
            var type = status.GetType();
            var fieldInfo = type.GetField(status.ToString());

            var attribute = fieldInfo.GetCustomAttribute<DisplayValueAttribute>();

            return attribute?.DisplayValue ?? status.ToString();
        }
    }
}
