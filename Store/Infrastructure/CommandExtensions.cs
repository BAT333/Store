
using System.Data;


namespace Store.Infrastructure
{
    public static class CommandExtensions
    {
        public static void AddParam(this IDbCommand cmd, string name, object value)
        {
            var param = cmd.CreateParameter();
            param.ParameterName = name;
            param.Value = value;
            cmd.Parameters.Add(param);
        }
    }
}
